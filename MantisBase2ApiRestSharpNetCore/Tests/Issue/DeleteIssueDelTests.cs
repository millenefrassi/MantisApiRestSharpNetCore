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
    public class DeleteIssueDelTests : TestBase
    {
        [Test]
        public void DeletaIssuesSucesso204()
        {
            List<string> idIssue = IssuesDBSteps.RetornaIssues();

            DeleteIssueDelRequest deleteIssueDelRequest = new DeleteIssueDelRequest(idIssue[0]);
            IRestResponse<dynamic> response = deleteIssueDelRequest.ExecuteRequest();

            List<string> result = IssuesDBSteps.RetornaCountIssues(idIssue[0]);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("No Content"));
                Assert.AreEqual(result[0], "0", "Valida se a issue foi realmente deletado");
            });
        }

        [Test]
        public void DeletaIssuesInexistente404()
        {
            List<string> idIssue = IssuesDBSteps.RetornaIssues();
            string id = idIssue[0] + 1;

            string mensagemEsperada = "Issue #"+id+" not found";

            DeleteIssueDelRequest deleteIssueDelRequest = new DeleteIssueDelRequest(id);
            IRestResponse<dynamic> response = deleteIssueDelRequest.ExecuteRequest();

            string mensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode, "Valida o status code");
                Assert.AreEqual(mensagem, mensagemEsperada, "Valida se a mensagem é a esperada.");
            });
        }
    }
}
