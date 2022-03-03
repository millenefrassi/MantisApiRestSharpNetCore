using MantisBase2ApiRestSharpNetCore.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Projects
{
    public class DeleteProjectDelRequest : RequestBase
    {
        public DeleteProjectDelRequest(string id)
        {
            requestService = "/api/rest/projects/{project_id}";
            method = Method.DELETE;

            parameters.Add("project_id", id);
        }
    }
}
