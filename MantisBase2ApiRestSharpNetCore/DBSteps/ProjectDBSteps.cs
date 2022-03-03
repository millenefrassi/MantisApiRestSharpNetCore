using MantisBase2ApiRestSharpNetCore.Helpers;
using MantisBase2ApiRestSharpNetCore.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.DBSteps
{
    class ProjectDBSteps
    {
        public static List<string> RetornaProjeto()
        {
            string query = ProjectQueries.ReturnProject;

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaField()
        {
            string query = ProjectQueries.ReturnField;

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaProjetoSalvo(string nameProject)
        {
            string query = ProjectQueries.ReturnProjectCreate.Replace("$nameProject", nameProject);

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaProjetoDeletado(string id)
        {
            string query = ProjectQueries.ReturnProjectDelete.Replace("$id", id);

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static void CargaProjetoMassa()
        {
            string projeto1 = "ProjectName1" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string projeto2 = "ProjectName2" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string projeto3 = "ProjectName3" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            string query1 = ProjectQueries.ReturnProjectExists.Replace("$nameProject", projeto1);
            List<string> result1 = DataBaseHelpers.RetornaDadosQuery(query1);

            if (result1[0] == "0")
            {
                string query2 = ProjectQueries.InsertProjectOne.Replace("$project1", projeto1);
                DataBaseHelpers.ExecuteQuery(query2);
            }

            string query3 = ProjectQueries.ReturnProjectExists.Replace("$nameProject", projeto2);
            List<string> result3 = DataBaseHelpers.RetornaDadosQuery(query3);

            if (result3[0] == "0")
            {
                string query2 = ProjectQueries.InsertProjectTwo.Replace("$project2", projeto2);
                DataBaseHelpers.ExecuteQuery(query2);
            }

            string query4 = ProjectQueries.ReturnProjectExists.Replace("$nameProject", projeto3);
            List<string> result4 = DataBaseHelpers.RetornaDadosQuery(query4);

            if (result4[0] == "0")
            {
                string query2 = ProjectQueries.InsertProjectThree.Replace("$project3", projeto3);
                DataBaseHelpers.ExecuteQuery(query2);
            }
        }

        public static void DeleteProjetoMassa()
        {
            string query1 = ProjectQueries.DeleteProjectMassa;
             DataBaseHelpers.ExecuteQuery(query1);
        }   
    }
}
