using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.DBSteps;
using MantisBase2ApiRestSharpNetCore.Requests.Projects;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Tests.Projects
{
   // [Parallelizable(ParallelScope.All)] //fazer paralelismo - descomentar quando rodar
    public class DeleteProjectDelTests : TestBase
    {
        [Test]
        public void DeletaProjectSucesso200()
        {
            List<string> idSalvoProject = ProjectDBSteps.RetornaProjeto();

            string mensagemEsperada = "Project with id "+ idSalvoProject[0] +" deleted";

            DeleteProjectDelRequest deleteProjectDelRequest = new DeleteProjectDelRequest(idSalvoProject[0]);

            IRestResponse<dynamic> response = deleteProjectDelRequest.ExecuteRequest();

            List<string> resultBanco = ProjectDBSteps.RetornaProjetoDeletado(idSalvoProject[0]);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
                Assert.True(response.StatusDescription.Contains(mensagemEsperada));
                Assert.AreEqual(resultBanco[0], "0", "Valida se o projeto foi deletado no banco");
            });
        }

        [Test]
        public void DeletaProjectIdNull400()
        {
            string id = "null";

            string mensagemEsperada = "Invalid project id";

            DeleteProjectDelRequest deleteProjectDelRequest = new DeleteProjectDelRequest(id);

            IRestResponse<dynamic> response = deleteProjectDelRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                Assert.True(response.StatusDescription.Contains(mensagemEsperada));
            });
        }

    }
}
