﻿using Microsoft.TeamFoundation.SourceControl.WebApi;
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
        const string pat = "";
        static List<string> teamMembers =
            new List<string> {
                "xiaoqiao@microsoft.com",
                "yanzhang4@microsoft.com",
                "ktan@microsoft.com",
                "wenyzou@microsoft.com",
                "yifso@microsoft.com",
                "yuwwang@microsoft.com",
                "zesluo@microsoft.com",
                "yingjiaozhao@microsoft.com",
                "haipengliu@microsoft.com",
                "heli2@microsoft.com",
                "fengqi@microsoft.com",
                "zhwe@microsoft.com",
                "yadu@microsoft.com",
                "lirsun@microsoft.com",
                "kuoranli@microsoft.com",
                "shengchenxu@microsoft.com",
                "mandyw@microsoft.com",
                "dianzhang@microsoft.com",
                "jiangtaoliu@microsoft.com",
                "tianhongwu@microsoft.com",
                "yiqiliu@microsoft.com",
                "zixiaohao@microsoft.com",
                "xiaofanzhang@microsoft.com",
                "xchenyang@microsoft.com",
                "xinyuesu@microsoft.com",
                "yuyanglin@microsoft.com",
                "yuyang@microsoft.com"
            };

        public static void visit(commit commit, string scope)
        {
            
            int target = commit.GetCommitsOnABranchAndInAPath(scope, teamMembers);
         //   if (target > 5)
                Console.WriteLine(scope + ":" + target);

            // visit children only if the contribution in parent is not big enough.
            if (target < 50)
            {

                var l = commit.ListOneLayerItems(scope);
                foreach (var result in l)
                {
                    if (!result.IsFolder || result.Path.Equals(scope))
                        continue;
                    visit(commit, result.Path);
                }
            }
        }

        static void Main(string[] args)
        {
            var creds = new Microsoft.VisualStudio.Services.Common.VssBasicCredential(string.Empty, pat);

            // Connect to Azure DevOps Services
            var connection = new VssConnection(new Uri(collectionUri), creds);
            var commit = new commit(connection);
            visit(commit, "/trident");

            Console.ReadLine();
        }
    }
}
