using MantisBase2ApiRestSharpNetCore.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Users
{
    public class UserMyInfoGetRequest : RequestBase
    {
        public UserMyInfoGetRequest()
        {
            requestService = "/api/rest/users/me";
            method = Method.GET;
        }
    }
}
