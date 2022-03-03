using NUnit.Framework;
using MantisBase2ApiRestSharpNetCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantisBase2ApiRestSharpNetCore.DBSteps;

namespace MantisBase2ApiRestSharpNetCore.Bases
{
    public class TestBase
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            UserDBSteps.CargaUsuarioMassa(); //Adicionei para criação de massa
            ProjectDBSteps.CargaProjetoMassa(); //Adicionei para criação de massa
            ExtentReportHelpers.CreateReport();
        }

        [SetUp]
        public void SetUp()
        {
            ExtentReportHelpers.AddTest();
        }

        [TearDown]
        public void TearDown()
        {
            ExtentReportHelpers.AddTestResult();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportHelpers.GenerateReport();
        }
    }
}
