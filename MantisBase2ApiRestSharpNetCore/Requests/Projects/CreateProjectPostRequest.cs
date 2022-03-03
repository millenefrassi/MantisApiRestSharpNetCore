using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Projects
{
    public class CreateProjectPostRequest : RequestBase
    {
		public CreateProjectPostRequest()
		{
			requestService = "/api/rest/projects/";
			method = RestSharp.Method.POST;
		}

		public void SetJsonBody(string idProject, string nameProject, string idStatus, string nameStatus, string labelStatus, string description, string enabled, string filePath, string viewStateId, string viewStateName, string viewStateLabel)
		{
			jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons\\Project\\CreateProject.json", Encoding.UTF8);
			jsonBody = jsonBody.Replace("$idProject", idProject);
			jsonBody = jsonBody.Replace("$nameProject", nameProject);
			jsonBody = jsonBody.Replace("$idStatus", idStatus);
			jsonBody = jsonBody.Replace("$nameStatus", nameStatus);
			jsonBody = jsonBody.Replace("$labelStatus", labelStatus);
			jsonBody = jsonBody.Replace("description", description);
			jsonBody = jsonBody.Replace("$enabled", enabled);
			jsonBody = jsonBody.Replace("$path", filePath);
			jsonBody = jsonBody.Replace("$idView", viewStateId);
			jsonBody = jsonBody.Replace("$nameView", viewStateName);
			jsonBody = jsonBody.Replace("$labelView", viewStateLabel);
		}
	}
}
