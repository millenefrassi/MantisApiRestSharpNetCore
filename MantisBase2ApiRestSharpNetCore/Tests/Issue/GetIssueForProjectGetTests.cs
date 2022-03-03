using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.DBSteps;
using MantisBase2ApiRestSharpNetCore.Requests.Issues;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Tests.Issue
{
   // [Parallelizable(ParallelScope.All)] //fazer paralelismo - descomentar quando rodar
    public class GetIssueForProjectGetTests : TestBase
    {
        [Test]
        public void RetornaIssuesForProjectSucesso200()
        {
            List<string> idIssue = IssuesDBSteps.RetornaPrimeiroIssues();

            GetIssueForProjectGetRequest getIssueForProjectGetRequest = new GetIssueForProjectGetRequest(idIssue[1]);
            IRestResponse<dynamic> response = getIssueForProjectGetRequest.ExecuteRequest();

            string idRetornoProjeto = response.Data["issues"][0]["project"]["id"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("OK"));
                Assert.AreEqual(idRetornoProjeto, idIssue[1], "Valida se trouxe o id do projeto correta");
            });
        }

        [Test]
        public void RetornaIssuesForProjectInexistente404()
        {
            string idProject = "111111";

            string mensagemEsperada = "Project '" + idProject + "' doesn't exist";

            GetIssueForProjectGetRequest getIssueForProjectGetRequest = new GetIssueForProjectGetRequest(idProject);
            IRestResponse<dynamic> response = getIssueForProjectGetRequest.ExecuteRequest();

            string resultResponse = response.StatusDescription.ToString();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode, "Valida o status code");
                Assert.AreEqual(mensagemEsperada, resultResponse, "Valida se a mensagem é a esperada");
            });
        }
    }
}
