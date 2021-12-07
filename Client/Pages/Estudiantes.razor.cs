using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using Horrografia.Client.Data.Services.Interfaces;
using Horrografia.Client.Shared.Objects.ClientModels;
using MudBlazor;


namespace Horrografia.Client.Pages
{
    public partial class Estudiantes
    {
        [Inject]
        protected AuthenticationStateProvider authProvider { get; set; }
        
        [Inject]
        protected IReporteService _reporteService { get; set; }
        
        [Inject]
        protected ITagService _tagService { get; set; }
        
        [Inject]
        protected INivelService _nivelService { get; set; }
        
        [Inject]
        protected  IUserService _userService { get; set; }
        
        [Inject]
        protected ISnackbar _snackbar { get; set; }
        
        private bool IsLoading { get; set; }
        private List<NivelModel> Niveles { get; set; }
        private List<UsuarioDTO> Usuarios { get; set; }
        private UsuarioDTO UsuarioSeleccionado { get; set; }
        private List<ReporteModel> Partidas{ get; set; }
        private List<ReporteModel> PartidasLuegoDeSerFiltradas { get; set; }
        private Dictionary<string,int> TagCounterDictionary { get; set; }

        private string _UsuarioID;
        private string _IdEnSeleccionDePagina
        { 
            get => _UsuarioID;
            set
            {
                _UsuarioID = value;
                UsuarioSeleccionado = Usuarios.FirstOrDefault(n => n.id == _UsuarioID);
            }
        }

        private enum Opciones
        {
            TodasLasPartidas,
            Estudiante
        }
        
        private Opciones OpcionEscogida { get; set; }

        public Estudiantes()
        {
            IsLoading = true;
        }

        private string GetUserNameFromId(string id)
        {
            return Usuarios.FirstOrDefault(u => u.id == id)?.nombreDeUsuario;
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await CargarUsuarios();
                await CargarNiveles();
                await CargarPartidas();
                IsLoading = false;
                StateHasChanged();
            }
            catch (Exception e)
            {
                ShowNotification(e.Message, Severity.Error);
            }
        }
        
        public void ShowNotification(string mensaje, Severity s)
        {
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            _snackbar.Add(mensaje, s);
        }
        
        private async Task CargarPartidas()
        {
            var response = await _reporteService.GetAsync();
            if (response.isResponseSuccesfull())
            {
                Partidas = response.Response;
                var usersId = Usuarios.Select(u => u.id);
                Partidas = Partidas.Where(reporte => usersId.Contains(reporte.IdUsuario)).ToList();
            }
            else
            {
                throw new InvalidOperationException($"Error al cargar partidas.");
            }
        }

        private async Task CargarNiveles()
        {
            var response = await _nivelService.GetAsync();
            if (response.isResponseSuccesfull())
            {
                Niveles = response.Response;
            }
            else
            {
                throw new InvalidOperationException($"Error al cargar niveles.");
            }
        }

        private async Task CargarUsuarios()
        {
            var schoolCode = await GetCódigoDeEscuela();
            var response = await _userService.GetSchoolUsers(schoolCode);
            if (response.isResponseSuccesfull())
            {
                Usuarios = response.Response;
            }
            else
            {
                throw new InvalidOperationException($"Error al cargar usuarios.");
            }
        }

        private async Task<string> GetCódigoDeEscuela()
        {
            //Consigo el id del usuario.
            AuthenticationState authState = await authProvider.GetAuthenticationStateAsync();
            var userId = authState.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            string codigo = "";
            //Con el id consigo el resto de datos del usuario
            var response = await _userService.GetUserById(userId);
            if (response.isResponseSuccesfull())
            {
                var usuarioDeProfesor = response.Response.FirstOrDefault();
                codigo = usuarioDeProfesor.codigoEscuela;
            }
            else
            {
                throw new InvalidOperationException($"Error al datos de usuario.");
            }
            return codigo;
        }

    }
}