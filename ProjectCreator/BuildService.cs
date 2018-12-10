using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;

namespace ProjectBuilder
{
    public class BuildService : IBuildService
    {
        public BuildService()
        {

        }

        public bool Build(string solutionDirectory, string outputPath)
        {

            string projectFilePath = Path.Combine(solutionDirectory);

            ProjectCollection pc = new ProjectCollection();

            // THERE ARE A LOT OF PROPERTIES HERE, THESE MAP TO THE MSBUILD CLI PROPERTIES
            Dictionary<string, string> globalProperty = new Dictionary<string, string>();
            globalProperty.Add("OutputPath", outputPath);
            globalProperty.Add("Configuration", "Debug");
            globalProperty.Add("Platform", "Any CPU");

            BuildParameters bp = new BuildParameters(pc);
            bp.Loggers = new[] {
                new FileLogger
                {
                    Verbosity = LoggerVerbosity.Detailed,
                    ShowSummary = true,
                    SkipProjectStartedText = true
                }
            };
            BuildManager.DefaultBuildManager.BeginBuild(bp);

            BuildRequestData BuildRequest = new BuildRequestData(projectFilePath, globalProperty, "4.7", new string[] { "Build" }, null);

            BuildSubmission BuildSubmission = BuildManager.DefaultBuildManager.PendBuildRequest(BuildRequest);
            BuildSubmission.Execute();
            BuildManager.DefaultBuildManager.EndBuild();

            return BuildSubmission.BuildResult.OverallResult== BuildResultCode.Success ? true : false;

        }
    }
}
