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
        const string projectName = "PowerBIClients";
        const string repoName = "PowerBIClients";
        const string pat = "64gsO8qa8PrxgdhTlLKSFHRdSD8YCKy1LCfSvSIzFzAPcSjzWPkkJQQJ99AJACAAAAAAArohAAASAZDODB5c";

        static void Main(string[] args)
        {
            var creds = new Microsoft.VisualStudio.Services.Common.VssBasicCredential(string.Empty, pat);

            // Connect to Azure DevOps Services
            var connection = new VssConnection(new Uri(collectionUri), creds);

            // Get a GitHttpClient to talk to the Git endpoints
            var gitClient = connection.GetClient<GitHttpClient>();

            // Get data about a specific repository
            var repo = gitClient.GetRepositoryAsync(projectName, repoName).Result;

            var branch = repo.DefaultBranch;

            Console.WriteLine(branch);
        }
    }
}
