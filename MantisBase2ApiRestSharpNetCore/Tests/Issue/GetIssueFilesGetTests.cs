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
    public class GetIssueFilesGetTests : TestBase
    {
        [Test]
        public void RetornaIssuesFileSucesso204()
        {
            List<string> file = IssuesDBSteps.RetornaFileIssues();

            GetIssueFilesGetRequest getIssueGetRequest = new GetIssueFilesGetRequest(file[3]);
            IRestResponse<dynamic> response = getIssueGetRequest.ExecuteRequest();

            string idFile = response.Data["files"][0]["id"];
            string idName = response.Data["files"][0]["filename"];
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("OK"));
                Assert.AreEqual(idFile, file[0], "Valida se trouxe o Id do File correto");
                Assert.AreEqual(idName, file[1], "Valida se trouxe o Nome do File correto");
            });
        }

        [Test]
        public void RetornaIssuesFileInexistente404()
        {
            List<string> file = IssuesDBSteps.RetornaIssues();
            int fileId = Int16.Parse(file[0]);
            int fileNaoExiste = fileId+11;
            string id = Convert.ToString(fileNaoExiste);
            string mensagemEsperada = "Issue #" + fileNaoExiste + " not found";

            GetIssueFilesGetRequest getIssueGetRequest = new GetIssueFilesGetRequest(id);
            IRestResponse<dynamic> response = getIssueGetRequest.ExecuteRequest();
          
            string mensagem = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode, "Valida o status code");
                Assert.AreEqual(mensagem, mensagemEsperada, "Valida se a mensagem é a esperada.");
            });
        }

        [Test]
        public void RetornaIssuesFilePassandoNull400()
        {
            string id = "null";
            string mensagemEsperada = "'issue_id' must be numeric";
            string mensagemEsperadaLocalized = "Invalid value for 'issue_id'";

            GetIssueFilesGetRequest getIssueGetRequest = new GetIssueFilesGetRequest(id);
            IRestResponse<dynamic> response = getIssueGetRequest.ExecuteRequest();

            string mensagem = response.Data["message"];
            string mensagemLocalizacao = response.Data["localized"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode, "Valida o status code");
                Assert.AreEqual(mensagem, mensagemEsperada, "Valida se a mensagem é a esperada.");
                Assert.AreEqual(mensagemLocalizacao, mensagemEsperadaLocalized, "Valida se a mensagem localização é a esperada.");
            });
        }
    }
}
