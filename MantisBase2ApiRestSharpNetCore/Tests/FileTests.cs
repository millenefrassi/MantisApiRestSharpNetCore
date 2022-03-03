using NUnit.Framework;
using RestSharp;
using MantisBase2ApiRestSharpNetCore.Bases;
using MantisBase2ApiRestSharpNetCore.Helpers;
using MantisBase2ApiRestSharpNetCore.Requests.PetStore;

namespace MantisBase2ApiRestSharpNetCore.Tests
{    
    [TestFixture]
    public class FileTests : TestBase
    {

        FileRequest fileRequest;

        [Test]
        public void ShouldDownloadTheFile()
        {
            string statusCodeEsperado = "OK";

            fileRequest = new FileRequest();

            IRestResponse<dynamic> response = fileRequest.ExecuteFileRequest();

            Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
        }

        
        
    }
}
