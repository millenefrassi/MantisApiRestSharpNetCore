using MantisBase2ApiRestSharpNetCore.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Issues
{
    public class GetIssueFilesGetRequest : RequestBase
    {
        public GetIssueFilesGetRequest(string id)
        {
            requestService = "/api/rest/issues/{userId}/files";
            method = Method.GET;

            parameters.Add("userId", id);
        }
    }
}
