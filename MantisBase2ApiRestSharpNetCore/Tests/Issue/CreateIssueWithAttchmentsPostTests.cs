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
    public class CreateIssueWithAttchmentsPostTests : TestBase
    {
        [Test]
        public void CadastrarIssueWithAttchmentsSucesso201()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string idProject = projeto[0];
            string nameProject = projeto[1];
            string idCategory = "1";
            string nameCategory = "General";
            string idField = "1";
            string nameField = "teste";
            string value = "Seattle";
            string nameFile = "file.txt";
            string contentFile = "aaaa";

            CreateIssueWithAttchmentsPostRequest createIssueWithAttchmentsPostRequest = new CreateIssueWithAttchmentsPostRequest();
            createIssueWithAttchmentsPostRequest.SetJsonBody(summary, description, idProject, nameProject, idCategory, nameCategory, idField, nameField, value, nameFile, contentFile);

            IRestResponse<dynamic> response = createIssueWithAttchmentsPostRequest.ExecuteRequest();

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
        public void CadastrarIssueWithAttchmentsSemProjeto404()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string idProject = null;
            string nameProject = "aaaa";
            string idCategory = "1";
            string nameCategory = "General";
            string idField = "1";
            string nameField = "teste";
            string value = "Seattle";
            string nameFile = "file.txt";
            string contentFile = "bbbb";
            string mensagemEsperada = "Project not specified";

            CreateIssueWithAttchmentsPostRequest createIssueWithAttchmentsPostRequest = new CreateIssueWithAttchmentsPostRequest();
            createIssueWithAttchmentsPostRequest.SetJsonBody(summary, description, idProject, nameProject, idCategory, nameCategory, idField, nameField, value, nameFile, contentFile);

            IRestResponse<dynamic> response = createIssueWithAttchmentsPostRequest.ExecuteRequest();

            string mensagem = response.Data["message"];
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                Assert.True(response.StatusDescription.Contains("Project not specified"));
                Assert.AreEqual(mensagem, mensagemEsperada, "Valida se a mensagem esperada é válida");
            });
        }

        [Test]
        public void CadastrarIssueWithAttchmentsProjetoInexistente404()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            int id = Int16.Parse(projeto[0]);
            int idProjectMaisUm = id + 111;
            string idInexistente = Convert.ToString(idProjectMaisUm);
            string nameProject = "aaaa";
            string idCategory = "1";
            string nameCategory = "General";
            string idField = "1";
            string nameField = "teste";
            string value = "Seattle";
            string nameFile = "file.txt";
            string contentFile = "bbbb";
            string mensagemEsperada = "Project '"+ idInexistente + "' not found";

            CreateIssueWithAttchmentsPostRequest createIssueWithAttchmentsPostRequest = new CreateIssueWithAttchmentsPostRequest();
            createIssueWithAttchmentsPostRequest.SetJsonBody(summary, description, idInexistente, nameProject, idCategory, nameCategory, idField, nameField, value, nameFile, contentFile);

            IRestResponse<dynamic> response = createIssueWithAttchmentsPostRequest.ExecuteRequest();

            string mensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
                Assert.AreEqual(mensagem, mensagemEsperada, "Valida se a mensagem esperada é válida");
            });
        }

        [Test]
        public void CadastrarIssueWithAttchmentsSemSumario400()
        {
            string summary = null;
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string idProject = projeto[0];
            string nameProject = projeto[1];
            string idCategory = "1";
            string nameCategory = "General";
            string idField = "1";
            string nameField = "teste";
            string value = "Seattle";
            string nameFile = "file.txt";
            string contentFile = "aaaa";
            string mensagemEsperada = "Summary not specified";

            CreateIssueWithAttchmentsPostRequest createIssueWithAttchmentsPostRequest = new CreateIssueWithAttchmentsPostRequest();
            
            createIssueWithAttchmentsPostRequest.SetJsonBody(summary, description, idProject, nameProject, idCategory, nameCategory, idField, nameField, value, nameFile, contentFile);

            IRestResponse<dynamic> response = createIssueWithAttchmentsPostRequest.ExecuteRequest();

            string mensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                Assert.True(response.StatusDescription.Contains("Summary not specified"));
                Assert.AreEqual(mensagem, mensagemEsperada, "Valida se a mensagem é a esperada.");
            });
        }

        [Test]
        public void CadastrarIssueWithAttchmentsSemDescricao400()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = null;
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string idProject = projeto[0];
            string nameProject = projeto[1];
            string idCategory = "1";
            string nameCategory = "General";
            string idField = "1";
            string nameField = "teste";
            string value = "Seattle";
            string nameFile = "file.txt";
            string contentFile = "aaaa";
            string mensagemEsperada = "Description not specified";

            CreateIssueWithAttchmentsPostRequest createIssueWithAttchmentsPostRequest = new CreateIssueWithAttchmentsPostRequest();

            createIssueWithAttchmentsPostRequest.SetJsonBody(summary, description, idProject, nameProject, idCategory, nameCategory, idField, nameField, value, nameFile, contentFile);

            IRestResponse<dynamic> response = createIssueWithAttchmentsPostRequest.ExecuteRequest();

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
