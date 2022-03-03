using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Users
{
    public class CreateUserPostRequest : RequestBase
    {
        public CreateUserPostRequest()
        {
            requestService = "/api/rest/users/";
            method = Method.POST;
        }

        public void SetJsonBody(string userName, string password, string realName, string email, string accesLevel, string enabled, string protectedd)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons\\User\\CreateUser.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$username", userName);
            jsonBody = jsonBody.Replace("$password", password);
            jsonBody = jsonBody.Replace("$realName", realName);
            jsonBody = jsonBody.Replace("$email", email);
            jsonBody = jsonBody.Replace("$acessLevel", accesLevel);
            jsonBody = jsonBody.Replace("$enabled", enabled);
            jsonBody = jsonBody.Replace("$protected", protectedd);
        }
    }
}
