using MantisBase2ApiRestSharpNetCore.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Projects
{
    public class GetProjectGetRequest : RequestBase
    {
        public GetProjectGetRequest(string id)
        {
            requestService = "/api/rest/projects/{project_id}";
            method = Method.GET;

            parameters.Add("project_id", id);
        }
    }
}
