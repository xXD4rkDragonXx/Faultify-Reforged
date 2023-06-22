using Buildalyzer;
using Buildalyzer.Environment;
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
        public static IProjectInfo GenerateTestProject(string projectPath)
        {

            var analyzerManager = new AnalyzerManager();
            analyzerManager.SetGlobalProperty("Configuration", "Debug");

            var projectAnalyzer = analyzerManager.GetProject(projectPath);
            var analyzerResult = projectAnalyzer.Build(new EnvironmentOptions
            {
                DesignTime = false,
                Restore = true
            }).First();

            IProjectInfo projectInfo = new ProjectInfo(analyzerResult);
            return projectInfo;
        }
    }
}
