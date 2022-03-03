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
    public class UpdateProjectPatchTests : TestBase
    {
        [Test]
        public void UpdateProjectSucesso201()
        {
            List<string> projeto = ProjectDBSteps.RetornaProjeto(); 
            string nameProjectAlterado = projeto[1] + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string enabled = "true";

            UpdateProjectPatchRequest updateProjectPatchRequest = new UpdateProjectPatchRequest(projeto[0]);

            updateProjectPatchRequest.SetJsonBody(projeto[0], nameProjectAlterado, enabled);

            IRestResponse<dynamic> response = updateProjectPatchRequest.ExecuteRequest();

            List<string> projectAlterado = ProjectDBSteps.RetornaProjetoSalvo(nameProjectAlterado);

            string idProject = response.Data["project"]["id"];
            string nameProjectRetorno = response.Data["project"]["name"];
            string enabledProject = response.Data["project"]["enabled"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(projectAlterado[0], projeto[0], "Valida se o id do projeto está igual");
                Assert.AreEqual(projectAlterado[1], nameProjectAlterado, "Valida se o nome do projeto está igual");
                Assert.AreEqual(projectAlterado[2], "1", "Valida se a habilitação do projeto está igual");
            });
        }

        [Test]
        public void UpdateProjectInexistente404()
        {
            List<string> projeto = ProjectDBSteps.RetornaProjeto();
            int id = Int16.Parse(projeto[0]);
            int idProjectMaisUm = id + 1;
            string idInexistente = Convert.ToString(idProjectMaisUm);
            string enabled = "false";

            string mensagemEsperada = "Project #" + idInexistente + " not found";

            UpdateProjectPatchRequest updateProjectPatchRequest = new UpdateProjectPatchRequest(idInexistente);

            updateProjectPatchRequest.SetJsonBody(idInexistente, "", enabled);

            IRestResponse<dynamic> response = updateProjectPatchRequest.ExecuteRequest();

            string message = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
                Assert.AreEqual(message, mensagemEsperada, "Valida se a mensagem é a esperada.");
            });
        }
    }
}
