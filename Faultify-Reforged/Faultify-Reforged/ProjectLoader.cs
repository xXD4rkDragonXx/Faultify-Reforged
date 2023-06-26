using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis;

namespace Faultify_Reforged.Core
{
    /// <summary>
    /// Loads all projects in a given solution.
    /// </summary>
    internal class ProjectLoader
    {
        readonly string projectPath;
        readonly string testProjectPath = string.Empty;
        private readonly Dictionary<string, Compilation> solutionCompilations;

        public ProjectLoader()
        {
            solutionCompilations = LoadProjectSolution().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Create an object to hold all the project data
        /// </summary>
        /// <param name="projectPath">Location of the .sln file</param>
        public ProjectLoader(string projectPath)
        {
            this.projectPath = projectPath;
            solutionCompilations = LoadProjectSolution().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Create an object to hold all the project data
        /// </summary>
        /// <param name="projectPath">Location of the .sln file</param>
        public ProjectLoader(string projectPath, string testProjectPath)
        {
            this.projectPath = projectPath;
            this.testProjectPath = testProjectPath;
            solutionCompilations = LoadProjectSolution().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Loads the SyntaxTrees of the projects in the solution
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<string, Compilation>> LoadProjectSolution()
        {
            var solutionAnalyzerManager = new AnalyzerManager(projectPath);
            var solutionWorkspace = solutionAnalyzerManager.GetWorkspace();

            Dictionary<string, Compilation> solutionCompilations = new Dictionary<string, Compilation>();

            foreach (var project in solutionWorkspace.CurrentSolution.Projects)
            {
                if (project.Name != Path.GetFileNameWithoutExtension(testProjectPath))
                {
                    var compilation = project.GetCompilationAsync().Result;
                    solutionCompilations.Add(project.Name, compilation);
                }
            }

            return solutionCompilations;
        }


        /// <summary>
        /// Provides the compiled projects
        /// </summary>
        /// <returns>Compiled projects</returns>
        public Dictionary<string, Compilation> getSolutionCompilations()
        {
            return solutionCompilations;
        }
    }
}
