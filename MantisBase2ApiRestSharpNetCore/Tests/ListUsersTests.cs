

using NUnit.Framework;
using RestSharp;
using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.Requests.PetStore;

namespace MantisBase2ApiRestSharpNetCore.Tests
{    
    [TestFixture]
    public class ListUsersTests : TestBase
    {

        ListUsersRequest listUsersPetRequest;
        GetUserRequest getUserRequest;

        [Test]
        public void ShouldSearchUsers()
        {
            string expectedStatusCode = "OK";

            listUsersPetRequest = new ListUsersRequest();

            IRestResponse<dynamic> response = listUsersPetRequest.ExecuteRequest();

            Assert.AreEqual(expectedStatusCode, response.StatusCode.ToString());
        }
        
    }
}
