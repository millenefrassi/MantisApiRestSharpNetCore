using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.DBSteps;
using MantisBase2ApiRestSharpNetCore.Helpers;
using MantisBase2ApiRestSharpNetCore.Requests.Users;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Tests.Users
{
   // [Parallelizable(ParallelScope.All)] //fazer paralelismo - descomentar quando rodar
    public class CreateUserPostTests : TestBase
    {
        #region Data Driven Providers
        public static IEnumerable CreateUserTestDataIProvider()
        {
            return GeneralHelpers.ReturnCSVData(GeneralHelpers.ReturnProjectPath() + "DataDriven/UsersData.csv");
        }
        #endregion

        [Test]
        public void CadastrarUsuarioSucesso201()
        {
            #region parametros
            string userName = "testeAPI"+ GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI"+ GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "updater";
            string enabled = "true";
            string protectedd = "false";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            string name = response.Data["user"]["name"];
            string real_name = response.Data["user"]["real_name"];
            string emailUsu = response.Data["user"]["email"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(id, resultBanco[0], "Valida se o id do usuário é o mesmo que foi salvo no banco.");
                Assert.AreEqual(name, resultBanco[1], "Valida se o nome usuário é o mesmo que foi salvo no banco.");
                Assert.AreEqual(real_name, resultBanco[2], "Valida se o nome real do usuário é o mesmo que foi salvo no banco.");
                Assert.AreEqual(emailUsu, resultBanco[3], "Valida se o o email do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test]
        public void CadastrarUsuarioNomeNULL400()
        {
            #region parametros
            string userName = null;
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "updater";
            string enabled = "true";
            string protectedd = "false";
            string mensagemEsperada = "Invalid username";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string message = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("Invalid username"));
                Assert.True(message.Contains(mensagemEsperada), "Valida se a mensagem esperada é do nome vazio");
            });
        }

        [Test]
        public void CadastrarUsuarioNomeJaCadastrado400()
        {
            #region parametros
            List<string> userName = UserDBSteps.RetornaNomeEmail() ;
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "updater";
            string enabled = "true";
            string protectedd = "false";
            string mensagemEsperada = "Username '"+userName[0]+"' already used.";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName[0], password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string message = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains(mensagemEsperada));
                Assert.True(message.Contains(mensagemEsperada), "Valida se a mensagem esperada é do nome já existente.");
            });
        }

        [Test]
        public void CadastrarUsuarioEmailJaCadastrado400()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            List<string> email = UserDBSteps.RetornaNomeEmail();
            string accessLevel = "updater";
            string enabled = "true";
            string protectedd = "false";
            string mensagemEsperada = "Email '" + email[1] + "' already used.";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email[1], accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string message = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains(mensagemEsperada));
                Assert.True(message.Contains(mensagemEsperada), "Valida se a mensagem esperada é do email já existente.");
            });
        }

        [Test]
        public void CadastrarUsuarioPerfilAdministrador201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "administrator";
            string enabled = "true";
            string protectedd = "false";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            string idAccessLevel = response.Data["user"]["access_level"]["id"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(idAccessLevel, resultBanco[4], "Valida se o perfil do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test]
        public void CadastrarUsuarioPerfilAtualizador201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "updater";
            string enabled = "true";
            string protectedd = "false";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            string idAccessLevel = response.Data["user"]["access_level"]["id"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(idAccessLevel, resultBanco[4], "Valida se o perfil do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test]
        public void CadastrarUsuarioPerfilDesenvolvedor201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "developer";
            string enabled = "true";
            string protectedd = "false";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            string idAccessLevel = response.Data["user"]["access_level"]["id"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(idAccessLevel, resultBanco[4], "Valida se o perfil do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test]
        public void CadastrarUsuarioPerfilGerente201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "manager";
            string enabled = "true";
            string protectedd = "false";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            string idAccessLevel = response.Data["user"]["access_level"]["id"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(idAccessLevel, resultBanco[4], "Valida se o perfil do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test]
        public void CadastrarUsuarioPerfilRelator201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "reporter";
            string enabled = "true";
            string protectedd = "false";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            string idAccessLevel = response.Data["user"]["access_level"]["id"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(idAccessLevel, resultBanco[4], "Valida se o perfil do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test]
        public void CadastrarUsuarioPerfilVisualizador201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "viewer";
            string enabled = "true";
            string protectedd = "false";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            string idAccessLevel = response.Data["user"]["access_level"]["id"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(idAccessLevel, resultBanco[4], "Valida se o perfil do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test]
        public void CadastrarUsuarioPerfilNaoExiste201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "qualquer";
            string enabled = "true";
            string protectedd = "false";
            string mensagemEsperada = "Invalid access level";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string message = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains(mensagemEsperada));
                Assert.AreEqual(mensagemEsperada, message, "Valida a mensagem de perfil que não existe.");
            });
        }

        [Test]
        public void CadastrarUsuarioEnableTrue201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "updater";
            string enabled = "true";
            string protectedd = "false";
            string mensagemEsperada = "1";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(mensagemEsperada, resultBanco[5], "Valida se o enable do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test]
        public void CadastrarUsuarioEnableFalse201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "updater";
            string enabled = "false";
            string protectedd = "false";
            string mensagemEsperada = "0";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(mensagemEsperada, resultBanco[5], "Valida se o enable do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test]
        public void CadastrarUsuarioProtectedTrue201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "updater";
            string enabled = "true";
            string protectedd = "true";
            string mensagemEsperada = "1";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(mensagemEsperada, resultBanco[6], "Valida se o protected do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test]
        public void CadastrarUsuarioProtectedFalse201()
        {
            #region parametros
            string userName = "testeAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string password = "senhaA@";
            string realName = "testeRealNameAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = "emaiTesteAPI" + GeneralHelpers.ReturnStringWithRandomCharacters(5) + "@gmail.com";
            string accessLevel = "updater";
            string enabled = "true";
            string protectedd = "false";
            string mensagemEsperada = "0";
            #endregion

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, accessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(mensagemEsperada, resultBanco[6], "Valida se o protected do usuário é o mesmo que foi salvo no banco.");
            });
        }

        [Test, TestCaseSource("CreateUserTestDataIProvider")]
        [Description("Realizar Cadastro de Usuários extraídos no TestData - DataDriven")]
        public void DataDriven_CadastrarUsuariosTest201(ArrayList testData)
        {
            #region Parameters
            string userName = testData[0].ToString();
            string password = testData[1].ToString();
            string realName = testData[2].ToString();
            string email = testData[3].ToString();
            string nameAccessLevel = testData[4].ToString();
            string enabled = testData[5].ToString();
            string protectedd = testData[6].ToString();
            #endregion

            UserDBSteps.DeletarUsuarioCadastrado(userName);

            CreateUserPostRequest createUserPostRequest = new CreateUserPostRequest();
            createUserPostRequest.SetJsonBody(userName, password, realName, email, nameAccessLevel, enabled, protectedd);

            IRestResponse<dynamic> response = createUserPostRequest.ExecuteRequest();

            string id = response.Data["user"]["id"];
            string name = response.Data["user"]["name"];
            string real_name = response.Data["user"]["real_name"];
            string emailUsu = response.Data["user"]["email"];
            List<string> resultBanco = UserDBSteps.RetornaUsuario(id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode, "Valida o status code");
                Assert.True(response.StatusDescription.Contains("User created"));
                Assert.AreEqual(id, resultBanco[0], "Valida se o id do usuário é o mesmo que foi salvo no banco.");
                Assert.AreEqual(name, resultBanco[1], "Valida se o nome usuário é o mesmo que foi salvo no banco.");
                Assert.AreEqual(real_name, resultBanco[2], "Valida se o nome real do usuário é o mesmo que foi salvo no banco.");
                Assert.AreEqual(emailUsu, resultBanco[3], "Valida se o o email do usuário é o mesmo que foi salvo no banco.");
            });
        }
    }
}
