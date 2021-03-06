using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Constants;
namespace Horrografia.Client.Shared.Objects
{
    public class ControllerResponse <T>
    {
        private List<T> _response = new List<T>();
        private int _responseStatus;

        public List<T> Response
        {
            get => _response;
            set => _response = value;
        }

        public int Status
        {
            get => _responseStatus;
            set
            {
                if (value == Constantes.OKSTATUS|| 
                    value == Constantes.NOTFOUNDSTATUS || 
                    value == Constantes.INTERNALERRORSTATUS)
                {
                    _responseStatus = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} must be a valid status.");
                }
            }
        }

        public bool isResponseSuccesfull()
        {
            bool apiControllerResponseStatus = false;
            if (_responseStatus == Constantes.OKSTATUS)
            {
                apiControllerResponseStatus = true;
            }
            return apiControllerResponseStatus;
        }

    }
}
