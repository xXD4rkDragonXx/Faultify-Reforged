using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace Faultify_Reforged.Core
{
    /// <summary>
    /// Loads all projects in a given solution.
    /// </summary>
    internal class ProjectLoader
    {
        readonly string projectPath;
        private readonly Dictionary<string, Compilation> solutionCompilations;

        public ProjectLoader() {        
            solutionCompilations = LoadProjectSolution().GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Create an object to hold all the project data
        /// </summary>
        /// <param name="projectPath">Location of the .sln file</param>
        public ProjectLoader(string projectPath) { 
            this.projectPath = projectPath;
            solutionCompilations = LoadProjectSolution().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Loads all project in the given solution.
        /// </summary>
        /// <returns>The Compiled projects of the solution</returns>
        private async Task<Dictionary<string, Compilation>> LoadProjectSolution()
        {
            //Register MSBuild location so it can be used. (Otherwise all compilations will be null)
            MSBuildLocator.RegisterDefaults();
            try
            {
                using (var workspace = MSBuildWorkspace.Create())
                {
                    workspace.LoadMetadataForReferencedProjects = true;
                    var solution = await workspace.OpenSolutionAsync(projectPath);
                    
                    Dictionary<string, Compilation> solutionCompilations = new Dictionary<string, Compilation>();

                    foreach (var project in solution.Projects)
                    {
                        var compilation = await project.GetCompilationAsync();
                        if (compilation != null)
                        {
                            solutionCompilations.Add(project.Name, compilation);
                        }
                    }

                    return solutionCompilations;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        /// <summary>
        /// Provides the compiled projects
        /// </summary>
        /// <returns>Compiled projects</returns>
        public Dictionary<string, Compilation> getSolutionCompilations() { 
            return solutionCompilations; 
        }
    }
}
