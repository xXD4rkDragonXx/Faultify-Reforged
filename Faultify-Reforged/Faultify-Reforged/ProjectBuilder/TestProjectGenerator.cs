using Buildalyzer;
using Buildalyzer.Environment;

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
