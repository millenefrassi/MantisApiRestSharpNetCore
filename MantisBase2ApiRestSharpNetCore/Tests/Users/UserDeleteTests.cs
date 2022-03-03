using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.DBSteps;
using MantisBase2ApiRestSharpNetCore.Requests.Users;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Tests.Users
{
   // [Parallelizable(ParallelScope.All)] //fazer paralelismo - descomentar quando rodar
    public class UserDeleteTests : TestBase
    {
        [Test]
        public void DeletaUsuarioNaoProtegido204()
        {
           List< string> idUsuario = UserDBSteps.RetornaIdUsuarioDelete();

            UserDeleteRequest userDeleteRequest = new UserDeleteRequest(idUsuario[0]);
            IRestResponse<dynamic> response = userDeleteRequest.ExecuteRequest();

            List<string> resultBanco = UserDBSteps.RetornaCountUsuarioDelete(idUsuario[0]);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("No Content"));
                Assert.AreEqual("0", resultBanco[0], "Valida se o usuário foi realmente deletado.");
            });
        }

        [Test]
        public void DeletaUsuarioProtegido403()
        {
            List<string> idUsuario = UserDBSteps.RetornaIdUsuarioDeleteProtegido();
            string mensagemEsperada = "User protected.";

            UserDeleteRequest userDeleteRequest = new UserDeleteRequest(idUsuario[0]);
            IRestResponse<dynamic> response = userDeleteRequest.ExecuteRequest();

            string message = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Forbidden, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User protected"));
                Assert.AreEqual(message, mensagemEsperada, "Valida se a mensagem é a esperada");
            });
        }

        [Test]
        public void DeletaUsuarioInexistente204()
        {
            string idUsuario = "123456";

            UserDeleteRequest userDeleteRequest = new UserDeleteRequest(idUsuario);
            IRestResponse<dynamic> response = userDeleteRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("No Content"));
            });
        }
    }
}
