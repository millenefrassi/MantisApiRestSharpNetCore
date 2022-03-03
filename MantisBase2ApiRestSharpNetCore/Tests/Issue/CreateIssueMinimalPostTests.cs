using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.DBSteps;
using MantisBase2ApiRestSharpNetCore.Helpers;
using MantisBase2ApiRestSharpNetCore.Requests.Issues;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Tests.Issue
{
   // [Parallelizable(ParallelScope.All)] //fazer paralelismo - descomentar quando rodar
    public class CreateIssueMinimalPostTests : TestBase
    {
        [Test]
        public void CadastrarIssueMinimalSucesso201()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string nameCategory = "General";
            string nameProject = projeto[1];
            

            CreateIssueMinimalPostRequest createIssueMinimalPostRequest = new CreateIssueMinimalPostRequest();
            createIssueMinimalPostRequest.SetJsonBody(summary, description, nameCategory, nameProject);

            IRestResponse<dynamic> response = createIssueMinimalPostRequest.ExecuteRequest();

            string id = response.Data["issue"]["id"];
            string sumario = response.Data["issue"]["summary"];
            string descricao = response.Data["issue"]["description"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
                Assert.True(response.StatusDescription.Contains("Issue Created"));
                Assert.True(response.StatusDescription.Contains(id));
                Assert.AreEqual(sumario, summary, "Valida se o sumário está igual");
                Assert.AreEqual(descricao, description, "Valida se a descrição está igual");
            });
        }

        [Test]
        public void CadastrarIssueMinimalSemProjeto400()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string nameCategory = "General";
            string nameProject = "";
            string mensagemEsperada = "Project not specified";


            CreateIssueMinimalPostRequest createIssueMinimalPostRequest = new CreateIssueMinimalPostRequest();
            createIssueMinimalPostRequest.SetJsonBody(summary, description, nameCategory, nameProject);

            IRestResponse<dynamic> response = createIssueMinimalPostRequest.ExecuteRequest();

            string mensagem = response.Data["message"];
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                Assert.True(response.StatusDescription.Contains("Project not specified"));
                Assert.AreEqual(mensagem, mensagemEsperada, "Valida se a mensagem é a esperada.");
            });
        }

        [Test]
        public void CadastrarIssueMinimalSemSummary400()
        {
            string summary = "";
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string nameCategory = "General";
            string nameProject = projeto[1];
            string mensagemEsperada = "Summary not specified";


            CreateIssueMinimalPostRequest createIssueMinimalPostRequest = new CreateIssueMinimalPostRequest();
            createIssueMinimalPostRequest.SetJsonBody(summary, description, nameCategory, nameProject);

            IRestResponse<dynamic> response = createIssueMinimalPostRequest.ExecuteRequest();

            string mensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                Assert.True(response.StatusDescription.Contains("Summary not specified"));
                Assert.AreEqual(mensagem, mensagemEsperada, "Valida se a mensagem é a esperada.");
            });
        }

        [Test]
        public void CadastrarIssueMinimalSemDescription400()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "";
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string nameCategory = "General";
            string nameProject = projeto[1];
            string mensagemEsperada = "Description not specified";


            CreateIssueMinimalPostRequest createIssueMinimalPostRequest = new CreateIssueMinimalPostRequest();
            createIssueMinimalPostRequest.SetJsonBody(summary, description, nameCategory, nameProject);

            IRestResponse<dynamic> response = createIssueMinimalPostRequest.ExecuteRequest();

            string mensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                Assert.True(response.StatusDescription.Contains("Description not specified"));
                Assert.AreEqual(mensagem, mensagemEsperada, "Valida se a mensagem é a esperada.");
            });
        }
    }
}
