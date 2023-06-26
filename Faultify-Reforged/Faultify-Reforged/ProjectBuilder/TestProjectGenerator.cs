using Buildalyzer;
using Buildalyzer.Environment;
using Buildalyzer.Workspaces;
using Microsoft.Build.Construction;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faultify_Reforged.Core.ProjectBuilder
{
    internal class TestProjectGenerator
    {
        public static IProjectInfo GenerateTestProject(string testProjectPath)
        {

            var analyzerManager = new AnalyzerManager();
            analyzerManager.SetGlobalProperty("Configuration", "Debug");

            var projectAnalyzer = analyzerManager.GetProject(testProjectPath);
            var projectBuild = projectAnalyzer.Build(new EnvironmentOptions
            {
                DesignTime = false,
                Restore = true
            });
            var analyzerResult = projectBuild.First();

            IProjectInfo projectInfo = new ProjectInfo(analyzerResult);
            return projectInfo;
        }
    }
}
