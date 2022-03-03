using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Issues
{
    public class CreateIssuePostRequest : RequestBase
    {
		public CreateIssuePostRequest()
		{
			requestService = "/api/rest/issues";
			method = RestSharp.Method.POST;
		}

		public void SetJsonBody( string summary, string description, string additionalInformation, string projectId, string projectName, string categoryId, string categoryName,  string handlerName, string viewStateId, string viewStateName, string priorityName, string severityName, string reproducibilityName, string sticky, string idField, string nameField, string value, string tagName)
		{
			jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons\\Issue\\CreateIssue.json", Encoding.UTF8);
			jsonBody = jsonBody.Replace("$summary", summary);
			jsonBody = jsonBody.Replace("$description", description);
			jsonBody = jsonBody.Replace("$additionalInformation", additionalInformation);
			jsonBody = jsonBody.Replace("$projectId", projectId);
			jsonBody = jsonBody.Replace("$projectName", projectName);
			jsonBody = jsonBody.Replace("$categoryId", categoryId);
			jsonBody = jsonBody.Replace("$categoryName", categoryName);
			jsonBody = jsonBody.Replace("$handlerName", handlerName);
			jsonBody = jsonBody.Replace("$viewStateId", viewStateId);
			jsonBody = jsonBody.Replace("$viewStateName", viewStateName);
			jsonBody = jsonBody.Replace("$priorityName", priorityName);
			jsonBody = jsonBody.Replace("$severityName", severityName);
			jsonBody = jsonBody.Replace("$reproducibilityName", reproducibilityName);
			jsonBody = jsonBody.Replace("$sticky", sticky);
			jsonBody = jsonBody.Replace("$idField", idField);
			jsonBody = jsonBody.Replace("$nameField", nameField);
			jsonBody = jsonBody.Replace("$value", value);
			jsonBody = jsonBody.Replace("$tagName", tagName);
		}
	}
}
