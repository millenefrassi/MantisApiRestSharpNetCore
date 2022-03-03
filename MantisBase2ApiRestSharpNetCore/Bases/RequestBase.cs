using RestSharp;
using MantisBase2ApiRestSharpNetCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantisBase2ApiRestSharpNetCore.Bases
{
    public class RequestBase
    {
        #region Parameters
        protected string jsonBody = null;

        protected string url = JsonBuilder.ReturnParameterAppSettings("URL");

        protected string requestService = null;

        protected Method method;

        protected bool httpBasicAuthenticator = false;

        protected bool ntlmAuthenticator = false;

        protected bool isFileRequest = false;

        protected FileTypes fileType;
        
        protected IDictionary<string, string> headers = new Dictionary<string, string>()
        {
            //Dicionário de headeres deve ser iniciado com os headers comuns a todos os métodos da API
            {"Content-Type", "application/json"},
            {"Authorization", JsonBuilder.ReturnParameterAppSettings("TOKEN")}, //adicionei pois a API requer autenticação
        };
               
        protected IDictionary<string, string> cookies = new Dictionary<string, string>()
        {
            //Dicionário de cookies deve ser iniciado com os headers comuns à todas os métodos da API
        };

        protected IDictionary<string, string> parameters = new Dictionary<string, string>();

        protected bool parameterTypeIsUrlSegment = true;
        #endregion

        #region Actions
        public IRestResponse<dynamic> ExecuteRequest()
        {
            IRestResponse<dynamic> response = RestSharpHelpers.ExecuteRequest(url, requestService, method, headers, cookies, parameters, parameterTypeIsUrlSegment, jsonBody, httpBasicAuthenticator, ntlmAuthenticator);
            ExtentReportHelpers.AddTestInfo(url, requestService, method.ToString(), headers, cookies, parameters, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, response, false);

            return response;
        }

         public IRestResponse<dynamic> ExecuteFileRequest()
        {
            IRestResponse<dynamic> response = RestSharpHelpers.ExecuteFileRequest(url, requestService, method, headers, cookies, parameters, parameterTypeIsUrlSegment, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, fileType);
            ExtentReportHelpers.AddTestInfo(url, requestService, method.ToString(), headers, cookies, parameters, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, response, true);

            return response;
        }

        public void RemoveHeader(string header)
        {
            headers.Remove(header);           
        }

        public void RemoveCookie(string cookie)
        {
            cookies.Remove(cookie);
        }

        public void RemoveParameter(string parameter)
        {
            parameters.Remove(parameter);
        }

        public void SetMethod(Method method)
        {
            this.method = method;
        }
        #endregion
    }
}
