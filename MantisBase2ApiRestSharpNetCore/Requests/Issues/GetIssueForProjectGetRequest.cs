using MantisBase2ApiRestSharpNetCore.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Issues
{
    public class GetIssueForProjectGetRequest : RequestBase
    {
        public GetIssueForProjectGetRequest(string idProject)
        {
            requestService = "/api/rest/issues?project_id="+ idProject;
            method = Method.GET;
         }
    }
}
