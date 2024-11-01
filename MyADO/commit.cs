using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.WebApi;

namespace MyADO
{
    class commit
    {
        const string projectName = "PowerBIClients";
        const string repoName = "PowerBIClients";

        private GitRepository repo;
        private GitHttpClient client;

        public commit(VssConnection connect)
        {
            this.client = connect.GetClient<GitHttpClient>();
            this.repo = client.GetRepositoryAsync(projectName, repoName).Result;

        }

            public List<GitCommitRef> GetAllCommits()
            {

                return this.client
                    .GetCommitsAsync(repo.Id, new GitQueryCommitsCriteria()).Result;
            }

            public List<GitCommitRef> GetCommitsByAuthor()
            {
                return this.client
                    .GetCommitsAsync(repo.Id, new GitQueryCommitsCriteria()
                    {
                        Author = "John Qiao"
                    }).Result;
            }

            public List<GitCommitRef> GetCommitsByCommitter()
            {
                return this.client
                    .GetCommitsAsync(repo.Id, new GitQueryCommitsCriteria()
                    {
                        Committer = "xiaoqiao@microsoft.com"
                    }).Result;
            }

            public List<GitCommitRef> GetCommitsOnABranch()
            {
                string branchName = repo.DefaultBranch;
                string branchNameWithoutRefsHeads = branchName.Remove(0, "refs/heads/".Length);

                return this.client
                    .GetCommitsAsync(repo.Id, new GitQueryCommitsCriteria()
                    {
                        ItemVersion = new GitVersionDescriptor()
                        {
                            VersionType = GitVersionType.Branch,
                            VersionOptions = GitVersionOptions.None,
                            Version = branchNameWithoutRefsHeads
                        }
                    }).Result;
            }

            public List<GitCommitRef> GetCommitsOnABranchAndInAPath()
            {
                string branchName = repo.DefaultBranch;
                string branchNameWithoutRefsHeads = branchName.Remove(0, "refs/heads/".Length);

                return this.client
                    .GetCommitsAsync(repo.Id, new GitQueryCommitsCriteria()
                    {
                        ItemVersion = new GitVersionDescriptor()
                        {
                            VersionType = GitVersionType.Branch,
                            VersionOptions = GitVersionOptions.None,
                            Version = branchNameWithoutRefsHeads
                        },
                        ItemPath = "/debug.log"
                    }).Result;
            }

            public List<GitCommitRef> GetCommitsInDateRange()
            {
                return this.client
                    .GetCommitsAsync(repo.Id, new GitQueryCommitsCriteria()
                    {
                        FromDate = new DateTime(2024, 6, 14).ToString(),
                        ToDate = new DateTime(2024, 6, 16).ToString()
                    }).Result;
            }

            public List<GitCommitRef> GetCommitsReachableFromACommit()
            {
                return this.client
                    .GetCommitsAsync(repo.Id, new GitQueryCommitsCriteria()
                    {
                    // Earliest commit in the graph to search.
                    CompareVersion = m_oldestDescriptor
                    }).Result;
            }

            public List<GitCommitRef> GetCommitsReachableFromACommitAndInPath()
            {
                string branchName = repo.DefaultBranch;
                string branchNameWithoutRefsHeads = branchName.Remove(0, "refs/heads/".Length);
                GitVersionDescriptor tipCommitDescriptor = new GitVersionDescriptor()
                {
                    VersionType = GitVersionType.Branch,
                    VersionOptions = GitVersionOptions.None,
                    Version = branchNameWithoutRefsHeads
                };


                return this.client
                    .GetCommitsAsync(repo.Id, new GitQueryCommitsCriteria()
                    {
                        CompareVersion = tipCommitDescriptor,
                        ItemVersion = m_oldestDescriptor,
                        ItemPath = "/README.md",
                    }).Result;
            }

            public List<GitCommitRef> GetCommitsPaging()
            {
                return this.client
                    .GetCommitsAsync(repo.Id, new GitQueryCommitsCriteria() { }, skip: 1, top: 2).Result;
            }

            private GitVersionDescriptor m_oldestDescriptor = new GitVersionDescriptor()
            {
                VersionType = GitVersionType.Commit,
                VersionOptions = GitVersionOptions.None,
                Version = "4fa42e1a7b0215cc70cd4e927cb70c422123af84"
            };

    }
}
