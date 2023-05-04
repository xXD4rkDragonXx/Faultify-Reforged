using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System.Collections.Immutable;

namespace Faultify_Reforged.Core
{
    internal class ProjectLoader
    {
        string projectPath;
        private Dictionary<string, Compilation> solutionCompilations;

        public ProjectLoader() {        
            solutionCompilations = LoadProjectSolution().GetAwaiter().GetResult();
        }
        
        public ProjectLoader(string projectPath) { 
            this.projectPath = projectPath;
            solutionCompilations = LoadProjectSolution().GetAwaiter().GetResult();
        }

        private async Task<Dictionary<string, Compilation>> LoadProjectSolution()
        {
            MSBuildLocator.RegisterDefaults();
            try
            {
                using (var workspace = MSBuildWorkspace.Create())
                {
                    workspace.LoadMetadataForReferencedProjects = true;
                    var solution = await workspace.OpenSolutionAsync(projectPath);

                    ImmutableList<WorkspaceDiagnostic> diagnostics = workspace.Diagnostics;

                    // Error logging
                    foreach (var diagnostic in diagnostics)
                    {
                        Console.WriteLine(diagnostic.Message);
                    }

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

        public Dictionary<string, Compilation> getSolutionCompilations() { 
            return solutionCompilations; 
        }
    }
}
