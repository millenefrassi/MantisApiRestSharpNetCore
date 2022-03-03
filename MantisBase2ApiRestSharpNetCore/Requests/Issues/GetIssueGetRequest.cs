using MantisBase2ApiRestSharpNetCore.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Issues
{
    public class GetIssueGetRequest : RequestBase
    {
        public GetIssueGetRequest(string id)
        {
            requestService = "/api/rest/issues/{userId}";
            method = Method.GET;

            parameters.Add("userId", id);
        }
    }
}
