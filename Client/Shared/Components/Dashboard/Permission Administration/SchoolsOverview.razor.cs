using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;
using System.Security.Cryptography;
using System.Text;

namespace Horrografia.Client.Shared.Components.Dashboard.Permission_Administration
{
    public partial class SchoolsOverview
    {
        [Parameter]
        public List<EscuelaModel> Escuelas { get; set; }

        [Parameter]
        public EventCallback<EscuelaModel> OnSchoolCreation { get; set; }

        private bool _showSchoolCreationDialog { get; set; }

        private ClientEscuelaModel _model { get; set; }

        public SchoolsOverview()
        {
            _showSchoolCreationDialog = false;
            _model = new();
        }

        private void OpenSchoolCreationDialog()
        {
            _showSchoolCreationDialog = true;
        }

        private void CloseSchoolCreationDialog()
        {
            _showSchoolCreationDialog = false;
        }

        private async Task CreateSchool()
        {
            EscuelaModel escuela = new();
            escuela.Nombre = _model.Nombre;
            //Genera código de 8 números aleatorios. 
            var code = GenerarCodigo();
            //Verifica que el código no sea repetido.
            while (Escuelas.Where(e => e.Codigo == code).Any())
            {
                code = GenerarCodigo();
            }
            escuela.Codigo = code;
            _showSchoolCreationDialog = false;
            await OnSchoolCreation.InvokeAsync(escuela);
        }

        private string GenerarCodigo()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 8; i++)
            { 
                var randomNumer = RandomNumberGenerator.GetInt32(0, 9);
                sb.Append(randomNumer);
            }
            return sb.ToString();
        }
    }
}
