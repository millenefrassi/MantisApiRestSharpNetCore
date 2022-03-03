using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.DBSteps;
using MantisBase2ApiRestSharpNetCore.Requests.Users;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MantisBase2ApiRestSharpNetCore.Tests.Users
{
   // [Parallelizable(ParallelScope.All)] //fazer paralelismo - descomentar quando rodar
    public class UserMyInfoGetTests : TestBase
    {
        [Test]
        public void RetornaInformacaoUsuarioLogado200()
        {
            List<string> usuario = UserDBSteps.RetornaUsuarioAdministrador();

            UserMyInfoGetRequest userMyInfoGetRequest = new UserMyInfoGetRequest();
            IRestResponse<dynamic> response = userMyInfoGetRequest.ExecuteRequest();

            string id = response.Data["id"];
            string name = response.Data["name"];
            string email = response.Data["email"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("OK"));
                Assert.AreEqual(id, usuario[0], "Valida se o o id do usuário é do administrador");
                Assert.AreEqual(name, usuario[1], "Valida se o o id do usuário é do administrador");
                Assert.AreEqual(email, usuario[3], "Valida se o o id do usuário é do administrador");
            });
        }
        [Test]
        public void Regex_RetornaInformacaoUsuarioLogado200()
        {
            List<string> usuario = UserDBSteps.RetornaUsuarioAdministrador();

            UserMyInfoGetRequest userMyInfoGetRequest = new UserMyInfoGetRequest();

            IRestResponse<dynamic> response = userMyInfoGetRequest.ExecuteRequest();

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Valida o status code");

            string[] arrayRegex = new string[]
            {
                "\"id\":(.*?)"+usuario[0],
                "\"name\":(.*?)\""+usuario[1],
                "\"email\":(.*?)\""+usuario[3],
            };
            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }
        }
    }
}
