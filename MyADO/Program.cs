using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyADO
{
    class Program
    {
        const string collectionUri = "https://dev.azure.com/powerbi";
        const string pat = "64gsO8qa8PrxgdhTlLKSFHRdSD8YCKy1LCfSvSIzFzAPcSjzWPkkJQQJ99AJACAAAAAAArohAAASAZDODB5c";

        static void Main(string[] args)
        {
            var creds = new Microsoft.VisualStudio.Services.Common.VssBasicCredential(string.Empty, pat);

            // Connect to Azure DevOps Services
            var connection = new VssConnection(new Uri(collectionUri), creds);

            var commit = new commit(connection);
            var r = commit.GetCommitsInDateRange();
            foreach (var result in r)
            {
                Console.WriteLine(result.Author.Email, result.CommitId);
            }

            Console.ReadLine();
        }
    }
}
