﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared;
using Horrografia.Shared.Models;
using Horrografia.Server.Data.Repos.Interfaces;
using Horrografia.Server.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Extensions;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public UsuariosRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IDataAccess dbContext,
            IConfiguration configuration)
        {

            _UserManager = userManager;
            _SignInManager = signInManager;
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<UsuarioDTO>> GetAllAsync()
        {
            string sql = "SELECT * FROM aspnetusers";
            var aspUsers = await _dbContext.LoadData<ApplicationUser, dynamic>(sql, new { }, ConectionString);
            List<UsuarioDTO> listaDeUsuarios = aspUsers.Select(u => u.getDTOFromApplicationUser()).ToList();
            return listaDeUsuarios;
        }

        //Revisa si el correo está disponible en la base de datos. 
        public async Task<bool> CorreoDisponible(string correo)
        {
            var userCheck = await _UserManager.FindByEmailAsync(correo);
            bool correoDisponible = false;
            if (userCheck == null)
            {
                correoDisponible = true;
            }
            return correoDisponible;
        }

        public async Task<bool> CrearUsuario(ClientUserRegisterDTO usuarioACrear)
        {
            bool exito = false;
            var usuario = new ApplicationUser
            {
                UserName = usuarioACrear.Email,
                NombreDeUsuario = usuarioACrear.Name,
                NormalizedUserName = usuarioACrear.Email,
                Email = usuarioACrear.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var result = await _UserManager.CreateAsync(usuario, usuarioACrear.Password);
            if (result.Succeeded)
            {
                exito = true;
                if (SharedConstants.Administradores.Contains(usuario.Email))
                {
                    await _UserManager.AddToRoleAsync(usuario, "Admin");
                }
                await _SignInManager.SignInAsync(usuario, true);
            }
            return exito;
        }

        public async Task<SharedConstants.LoginState> IniciarSesion(ClientUserLoginDTO modeloEnviado)
        {
            var userCheck = await _UserManager.FindByEmailAsync(modeloEnviado.Email);

            if (userCheck == null)
            { 
                return SharedConstants.LoginState.LoginFailure;
            }

            // Si se quiere implementar checkeo de email, este metodo es el encargado de revisar.
            // Como el constructor en el método de CrearUsuario siempre lo pone como true, nunca va a retornar esto. 
            // Si se desea habilitar, cambiar el constructor e implementar la funcionalidad de confirmar.
            /*
            if (!userCheck.EmailConfirmed)
            {
                return SharedConstants.LoginState.EmailNotConfirmed;
            }
            */

            //Salta si la contraseña enviada no es la misma
            if (await _UserManager.CheckPasswordAsync(userCheck, modeloEnviado.Password) == false)
            {
                return SharedConstants.LoginState.NoPasswordMatch;
            }

            var result = await _SignInManager.PasswordSignInAsync(modeloEnviado.Email, modeloEnviado.Password, modeloEnviado.RememberMe, false);
            if (result.Succeeded)
            {
                return SharedConstants.LoginState.LoginSucess;
            }
            else
            {
                return SharedConstants.LoginState.LoginFailure;
            }
        }

        public async Task CerrarSesion()
        {
            await _SignInManager.SignOutAsync();
        }


    }
}
