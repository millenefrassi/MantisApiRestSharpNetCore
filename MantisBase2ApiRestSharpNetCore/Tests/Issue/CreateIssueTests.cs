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
    public class CreateIssueTests : TestBase
    {
        [Test]
        public void CadastrarIssueSucesso201()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3); 
            string additionalInformation = "Informacao Adicional Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3); ;
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string projectId = projeto[0];
            string projectName = projeto[1];
            string categoryId = "1";
            string categoryName = "General";
            string handlerName = "teste";
            string viewStateId = "10";
            string viewStateName = "public";
            string priorityName = "normal";
            string severityName = "trivial";
            string reproducibilityName = "always";
            string sticky = "false";
            List<string> field = ProjectDBSteps.RetornaField();
            string idField = field[0];
            string nameField = field[1];
            string value = "Seattle";
            string tagName = "mantishub";

            CreateIssuePostRequest createIssuePostRequest = new CreateIssuePostRequest();
            createIssuePostRequest.SetJsonBody(summary, description, additionalInformation, projectId,
                                               projectName, categoryId, categoryName, handlerName,
                                               viewStateId, viewStateName, priorityName, severityName,
                                               reproducibilityName, sticky, idField, nameField, value, tagName);

            IRestResponse<dynamic> response = createIssuePostRequest.ExecuteRequest();

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
        public void CadastrarIssueSemSumario400()
        {
            string summary = "";
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3); 
            string additionalInformation = "Informacao Adicional Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3); ;
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string projectId = projeto[0];
            string projectName = projeto[1];
            string categoryId = "1";
            string categoryName = "General";
            string handlerName = "teste";
            string viewStateId = "10";
            string viewStateName = "public";
            string priorityName = "normal";
            string severityName = "trivial";
            string reproducibilityName = "always";
            string sticky = "false";
            List<string> field = ProjectDBSteps.RetornaField();
            string idField = field[0];
            string nameField = field[1];
            string value = "Seattle";
            string tagName = "mantishub";
            string mensagemEsperada = "Summary not specified";

            CreateIssuePostRequest createIssuePostRequest = new CreateIssuePostRequest();
            createIssuePostRequest.SetJsonBody(summary, description, additionalInformation, projectId,
                                               projectName, categoryId, categoryName, handlerName,
                                               viewStateId, viewStateName, priorityName, severityName,
                                               reproducibilityName, sticky, idField, nameField, value, tagName);

            IRestResponse<dynamic> response = createIssuePostRequest.ExecuteRequest();

            string retornoMensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                Assert.AreEqual(mensagemEsperada, retornoMensagem, "Valida o retorno da mensagem do sumário vazio");
            });
        }

        [Test]
        public void CadastrarIssueSemDescricao400()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "";
            string additionalInformation = "Informacao Adicional Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3); ;
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string projectId = projeto[0];
            string projectName = projeto[1];
            string categoryId = "1";
            string categoryName = "General";
            string handlerName = "teste";
            string viewStateId = "10";
            string viewStateName = "public";
            string priorityName = "normal";
            string severityName = "trivial";
            string reproducibilityName = "always";
            string sticky = "false";
            List<string> field = ProjectDBSteps.RetornaField();
            string idField = field[0];
            string nameField = field[1];
            string value = "Seattle";
            string tagName = "mantishub";
            string mensagemEsperada = "Description not specified";

            CreateIssuePostRequest createIssuePostRequest = new CreateIssuePostRequest();
            createIssuePostRequest.SetJsonBody(summary, description, additionalInformation, projectId,
                                               projectName, categoryId, categoryName, handlerName,
                                               viewStateId, viewStateName, priorityName, severityName,
                                               reproducibilityName, sticky, idField, nameField, value, tagName);

            IRestResponse<dynamic> response = createIssuePostRequest.ExecuteRequest();

            string retornoMensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                Assert.AreEqual(mensagemEsperada, retornoMensagem, "Valida o retorno da mensagem da descrição vazio");
            });
        }

        [Test]
        public void CadastrarIssueSemProjeto400()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string additionalInformation = "Informacao Adicional Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3); ;
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string projectId = "0";
            string projectName = "";
            string categoryId = "1";
            string categoryName = "General";
            string handlerName = "teste";
            string viewStateId = "10";
            string viewStateName = "public";
            string priorityName = "normal";
            string severityName = "trivial";
            string reproducibilityName = "always";
            string sticky = "false";
            List<string> field = ProjectDBSteps.RetornaField();
            string idField = field[0];
            string nameField = field[1];
            string value = "Seattle";
            string tagName = "mantishub";
            string mensagemEsperada = "Project not specified";

            CreateIssuePostRequest createIssuePostRequest = new CreateIssuePostRequest();
            createIssuePostRequest.SetJsonBody(summary, description, additionalInformation, projectId,
                                               projectName, categoryId, categoryName, handlerName,
                                               viewStateId, viewStateName, priorityName, severityName,
                                               reproducibilityName, sticky, idField, nameField, value, tagName);

            IRestResponse<dynamic> response = createIssuePostRequest.ExecuteRequest();

            string retornoMensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                Assert.AreEqual(mensagemEsperada, retornoMensagem, "Valida o retorno da mensagem do projeto vazio");
            });
        }

        [Test]
        public void CadastrarIssueProjetoInexistente400()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string additionalInformation = "Informacao Adicional Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3); ;
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string projectId = "111111";
            string projectName = "";
            string categoryId = "1";
            string categoryName = "General";
            string handlerName = "teste";
            string viewStateId = "10";
            string viewStateName = "public";
            string priorityName = "normal";
            string severityName = "trivial";
            string reproducibilityName = "always";
            string sticky = "false";
            List<string> field = ProjectDBSteps.RetornaField();
            string idField = field[0];
            string nameField = field[1];
            string value = "Seattle";
            string tagName = "mantishub";
            string mensagemEsperada = "Project '" + projectId + "' not found";

            CreateIssuePostRequest createIssuePostRequest = new CreateIssuePostRequest();
            createIssuePostRequest.SetJsonBody(summary, description, additionalInformation, projectId,
                                               projectName, categoryId, categoryName, handlerName,
                                               viewStateId, viewStateName, priorityName, severityName,
                                               reproducibilityName, sticky, idField, nameField, value, tagName);

            IRestResponse<dynamic> response = createIssuePostRequest.ExecuteRequest();

            string retornoMensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
                Assert.AreEqual(mensagemEsperada, retornoMensagem, "Valida o retorno da mensagem do projeto que não existe");
            });
        }

        [Test]
        public void CadastrarIssueTagVazia400()
        {
            string summary = "Sumario Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string description = "Descricao Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string additionalInformation = "Informacao Adicional Mantis" + GeneralHelpers.ReturnStringWithRandomCharacters(3); ;
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            string projectId = projeto[0];
            string projectName = projeto[1];
            string categoryId = "1";
            string categoryName = "General";
            string handlerName = "teste";
            string viewStateId = "10";
            string viewStateName = "public";
            string priorityName = "normal";
            string severityName = "trivial";
            string reproducibilityName = "always";
            string sticky = "false";
            List<string> field = ProjectDBSteps.RetornaField();
            string idField = field[0];
            string nameField = field[1];
            string value = "Seattle";
            string tagName = "";
            string mensagemEsperada = "Tag name '"+tagName+"' is not valid.";

            CreateIssuePostRequest createIssuePostRequest = new CreateIssuePostRequest();
            createIssuePostRequest.SetJsonBody(summary, description, additionalInformation, projectId,
                                               projectName, categoryId, categoryName, handlerName,
                                               viewStateId, viewStateName, priorityName, severityName,
                                               reproducibilityName, sticky, idField, nameField, value, tagName);

            IRestResponse<dynamic> response = createIssuePostRequest.ExecuteRequest();

            string retornoMensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                Assert.AreEqual(mensagemEsperada, retornoMensagem, "Valida o retorno da mensagem da tag vazia");
            });
        }       
    }
 }
