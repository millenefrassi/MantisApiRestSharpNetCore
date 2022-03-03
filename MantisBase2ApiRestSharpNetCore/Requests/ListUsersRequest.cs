using RestSharp;
using MantisBase2ApiRestSharpNetCore.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.PetStore
{
    public class ListUsersRequest : RequestBase
    {

        public ListUsersRequest()
        {
            url = "https://reqres.in";
            requestService = "/api/users?page=2";
            method = Method.GET;
        }
    }
}
