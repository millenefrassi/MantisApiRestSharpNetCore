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
    public class GetIssueGetTests : TestBase
    {
        [Test]
        public void RetornaIssuesSucesso204()
        {
            List<string> idIssue = IssuesDBSteps.RetornaIssues();

            GetIssueGetRequest getIssueGetRequest = new GetIssueGetRequest(idIssue[0]);
            IRestResponse<dynamic> response = getIssueGetRequest.ExecuteRequest();

            string idRetorno = response.Data["issues"][0]["id"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("OK"));
                Assert.AreEqual(idRetorno, idIssue[0], "Valida se trouxe a Issue correta");
            });
        }

        [Test]
        public void RetornaIssuesInexistente404()
        {
            List<string> idIssue = IssuesDBSteps.RetornaIssues();
            string id = idIssue[0] + 1;

            string mensagemEsperada = "Issue #" + id + " not found";


            GetIssueGetRequest getIssueGetRequest = new GetIssueGetRequest(id);
            IRestResponse<dynamic> response = getIssueGetRequest.ExecuteRequest();

            string mensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode, "Valida o status code");
                Assert.AreEqual(mensagem, mensagemEsperada, "Valida se a mensagem é a esperada.");
            });
        }
    }
}
