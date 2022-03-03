using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.DBSteps;
using MantisBase2ApiRestSharpNetCore.Helpers;
using MantisBase2ApiRestSharpNetCore.Requests.Projects;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Tests.Projects
{
   // [Parallelizable(ParallelScope.All)] //fazer paralelismo - descomentar quando rodar
    public class GetProjectGetTests : TestBase
    {
        [Test]
        public void RetornaProjectSucesso204()
        {
            List<string> projeto = ProjectDBSteps.RetornaProjeto();

            GetProjectGetRequest getProjectGetRequest = new GetProjectGetRequest(projeto[0]);

            IRestResponse<dynamic> response = getProjectGetRequest.ExecuteRequest();

            string idProject = response.Data["projects"][0]["id"];
            string nameProjectRetorno = response.Data["projects"][0]["name"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(projeto[0], idProject, "Valida se o id do projeto está igual");
                Assert.AreEqual(projeto[1], nameProjectRetorno, "Valida se o nome do projeto está igual");
            });
        }

        [Test]
        public void RetornaProjectInexistente404()
        {
            List<string> idSalvoProject = ProjectDBSteps.RetornaProjeto();
            int id = Int16.Parse(idSalvoProject[0]);
            int idProjectMaisUm = id + 111;
            string idInexistente = Convert.ToString(idProjectMaisUm);

            string mensagemEsperada = "Project #" + idInexistente + " not found";

            GetProjectGetRequest getProjectGetRequest = new GetProjectGetRequest(idInexistente);

            IRestResponse<dynamic> response = getProjectGetRequest.ExecuteRequest();

            string message = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
                Assert.AreEqual(message, mensagemEsperada, "Valida se a mensagem é a esperada.");
            });
        }
    }
}
