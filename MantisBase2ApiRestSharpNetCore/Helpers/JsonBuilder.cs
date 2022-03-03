using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.Helpers
{
    public class JsonBuilder
    {
        public static IConfigurationRoot configuration { get; set; } = null;

        public static string ReturnParameterAppSettings(string param)
        {
            //adicionei as linhas abaixo para fazer a configuração em dois ambientes
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            #if DEV
               .AddJsonFile($"appsettings.DEV.json", optional: false, reloadOnChange: true)
            #endif
            #if HOM
               .AddJsonFile($"appsettings.HOM.json", optional: false, reloadOnChange: true)
            #endif
            .AddEnvironmentVariables()
            .Build();

            return config[param].ToString();
        }


        public static void UpdateParameterAppSettings(string parameter, string newValue)
        {
            string json = File.ReadAllText(Directory.GetCurrentDirectory() + "/appsettings.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj[parameter] = newValue;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Directory.GetCurrentDirectory() + "/appsettings.json", output);
        }

    }
}
