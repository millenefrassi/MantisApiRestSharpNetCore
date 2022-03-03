using RestSharp;
using MantisBase2ApiRestSharpNetCore.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.PetStore
{
    public class GetUserRequest : RequestBase
    {

        public GetUserRequest(string idUsuario)
        {
            url = "https://reqres.in";
            requestService = "/api/users/"+ idUsuario;
            method = Method.GET;
        }
    }
}
