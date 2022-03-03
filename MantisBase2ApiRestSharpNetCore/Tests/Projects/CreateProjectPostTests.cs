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
    public class CreateProjectPostTests : TestBase
    {
        [Test]
        public void CadastrarProjectSucesso201()
        {
            string id = "100";
            string nameProject = "Name Project" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string idStatus = "10";
            string nameStatus = "nameStatus" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string labelStatus = "development";
            string description = "description";
            string enabled = "true";
            string file_path = "arquivo";
            string idView = "10";
            string nameView = "nameView";
            string labelView = "public";

            CreateProjectPostRequest createProjectPostRequest = new CreateProjectPostRequest();
           
            createProjectPostRequest.SetJsonBody(id, nameProject, idStatus, nameStatus,
                                               labelStatus, description, enabled, file_path,
                                               idView, nameView, labelView);

            IRestResponse<dynamic> response = createProjectPostRequest.ExecuteRequest();

            List<string> idSalvoProject = ProjectDBSteps.RetornaProjetoSalvo(nameProject);

            string idProject = response.Data["project"]["id"];
            string nameProjectRetorno = response.Data["project"]["name"];
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
                Assert.AreEqual(idSalvoProject[0], idProject, "Valida se o id do projeto está igual");
                Assert.AreEqual(nameProject, nameProjectRetorno, "Valida se o nome do projeto está igual");
            });
        }
    }
}
