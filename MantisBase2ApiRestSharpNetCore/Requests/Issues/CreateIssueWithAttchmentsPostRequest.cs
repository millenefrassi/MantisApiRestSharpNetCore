using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.Issues
{
    public class CreateIssueWithAttchmentsPostRequest : RequestBase
    {
        public CreateIssueWithAttchmentsPostRequest()
        {
            requestService = "/api/rest/issues";
            method = Method.POST;
        }
        public void SetJsonBody(string summary, string description, string IdProject, string nameProject, string idCategory, string nameCategory, string idField, string nameField, string value, string nameFile, string contentFile)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons\\Issue\\CreateIssueWithAttchments.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$summary", summary);
            jsonBody = jsonBody.Replace("$description", description);
            jsonBody = jsonBody.Replace("$idProject", IdProject);
            jsonBody = jsonBody.Replace("$nameProject", nameProject);
            jsonBody = jsonBody.Replace("$idCategory", idCategory);
            jsonBody = jsonBody.Replace("$nameCategory", nameCategory);
            jsonBody = jsonBody.Replace("$idField", idField);
            jsonBody = jsonBody.Replace("$nameField", nameField);
            jsonBody = jsonBody.Replace("$value", value);
            jsonBody = jsonBody.Replace("$nameFile", nameFile);
            jsonBody = jsonBody.Replace("$contentFile", contentFile);
        }
    }
}
