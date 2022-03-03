using RestSharp;
using MantisBase2ApiRestSharpNetCore.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Requests.PetStore
{
    public class FileRequest : RequestBase
    {

        public FileRequest()
        {
            url = "http://files.customersaas.com/files";
            requestService = "/Samsung_A510F_Galaxy_A5_(2016)_Manual_do_usuário.pdf";
            method = Method.GET;
            fileType = FileTypes.pdf;
        }
    }
}
