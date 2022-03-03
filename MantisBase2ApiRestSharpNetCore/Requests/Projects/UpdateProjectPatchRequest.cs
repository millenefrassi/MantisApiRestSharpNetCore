using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Projects
{
    public class UpdateProjectPatchRequest : RequestBase
    {
        public UpdateProjectPatchRequest(string id)
        {
            requestService = "/api/rest/projects/{project_id}";
            method = Method.PATCH;

            parameters.Add("project_id", id);
        }

        public void SetJsonBody(string idProject, string nameProject, string enabled)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons\\Project\\UpdateProject.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$id", idProject);
            jsonBody = jsonBody.Replace("$nameProject", nameProject);
            jsonBody = jsonBody.Replace("$enabled", enabled);
        }
    }
}
