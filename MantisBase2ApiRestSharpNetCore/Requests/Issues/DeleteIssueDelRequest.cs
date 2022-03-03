using MantisBase2ApiRestSharpNetCore.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Issues
{
    public class DeleteIssueDelRequest : RequestBase
    {
        public DeleteIssueDelRequest(string id)
        {
            requestService = "/api/rest/issues/{userId}";
            method = Method.DELETE;

            parameters.Add("userId", id);
        }
    }
}
