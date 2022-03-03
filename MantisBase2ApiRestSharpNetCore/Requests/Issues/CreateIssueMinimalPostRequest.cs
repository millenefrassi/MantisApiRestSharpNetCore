using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Issues
{
    public class CreateIssueMinimalPostRequest : RequestBase
    {
        public CreateIssueMinimalPostRequest()
        {
            requestService = "/api/rest/issues";
            method = RestSharp.Method.POST;
        }
		public void SetJsonBody(string summary, string description, string nameCategory, string nameProject)
		{
			jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons\\Issue\\CreateIssueMinimal.json", Encoding.UTF8);
			jsonBody = jsonBody.Replace("$summary", summary);
			jsonBody = jsonBody.Replace("$description", description);
			jsonBody = jsonBody.Replace("$nameCategory", nameCategory);
			jsonBody = jsonBody.Replace("$nameProject", nameProject);
		}
	}
}
