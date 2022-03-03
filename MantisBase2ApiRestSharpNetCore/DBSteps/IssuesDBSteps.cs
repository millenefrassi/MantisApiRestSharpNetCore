using MantisBase2ApiRestSharpNetCore.Helpers;
using MantisBase2ApiRestSharpNetCore.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantisBase2ApiRestSharpNetCore.DBSteps
{
    class IssuesDBSteps
    {
        public static List<string> RetornaIssues()
        {
            string query = IssuesQueries.ReturnIdIssue;

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaCountIssues(string id)
        {
            string query = IssuesQueries.ReturnCountIssue.Replace("$idIssue", id);

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaFileIssues()
        {
            string query = IssuesQueries.ReturnIssueFile;

            return DataBaseHelpers.RetornaDadosQuery(query);
        }

        public static List<string> RetornaPrimeiroIssues()
        {
            string query = IssuesQueries.ReturnPrimeiroIssue;

            return DataBaseHelpers.RetornaDadosQuery(query);
        }
    }
}
