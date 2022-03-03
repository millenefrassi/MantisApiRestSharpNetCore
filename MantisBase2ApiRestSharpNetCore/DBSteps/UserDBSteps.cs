using MantisBase2ApiRestSharpNetCore.Helpers;
using MantisBase2ApiRestSharpNetCore.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.DBSteps
{
    class UserDBSteps
    {
        public static List<string> RetornaUsuario(string idUser)
        {
            string query = UserQueries.ReturnUserCreate.Replace("$id", idUser);

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaNomeEmail()
        {
            string query = UserQueries.ReturnNameEmail;

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaIdUsuarioDelete()
        {
            string query = UserQueries.ReturnIdUserDelete;

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaCountUsuarioDelete(string id)
        {
            string query = UserQueries.ReturnCountUserDelete.Replace("$userId", id);

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaIdUsuarioDeleteProtegido()
        {
            string query = UserQueries.ReturnIdUserDeleteProtected;

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaUsuarioAdministrador()
        {
            string query = UserQueries.ReturnUserAdministrator;

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static void DeletarUsuarioCadastrado(string username)
        {
            string query = UserQueries.DeleteUserCreate.Replace("$username", username);
            DataBaseHelpers.ExecuteQuery(query);
        }

        public static void CargaUsuarioMassa()
        {
            string query1 = UserQueries.ReturnUser.Replace("$username", "NameUserAPI1");
            List<string> result1 = DataBaseHelpers.RetornaDadosQuery(query1);

            if(result1[0] == "0")
            {
                string query2 = UserQueries.InsertUserOne;
                DataBaseHelpers.ExecuteQuery(query2);
            }

            string query3 = UserQueries.ReturnUser.Replace("$username", "NameUserAPI2");
            List<string> result3 = DataBaseHelpers.RetornaDadosQuery(query3);

            if (result3[0] == "0")
            {
                string query2 = UserQueries.InsertUserTwo;
                DataBaseHelpers.ExecuteQuery(query2);
            }

            string query4 = UserQueries.ReturnUser.Replace("$username", "NameUserAPI3");
            List<string> result4 = DataBaseHelpers.RetornaDadosQuery(query4);

            if (result4[0] == "0")
            {
                string query2 = UserQueries.InsertUserThree;
                DataBaseHelpers.ExecuteQuery(query2);
            }
        }
    }
}
