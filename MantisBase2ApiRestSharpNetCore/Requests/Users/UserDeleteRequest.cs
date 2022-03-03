using MantisBase2ApiRestSharpNetCore.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Users
{
    public class UserDeleteRequest : RequestBase
    {
        public UserDeleteRequest(string id)
        {
            requestService = "/api/rest/users/{userId}";
            method = Method.DELETE;

            parameters.Add("userId", id);
        }
    }
}
