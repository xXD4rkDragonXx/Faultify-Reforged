using Faultify_Reforged.Core.Mutator;

namespace Faultify_Reforged.Core
{
    /// <summary>
    /// The core of the application, this calls and implements all the components necesary to run the application.
    /// </summary>
    public class FaultifyCore
    {
        readonly string inputProject;
        readonly string? outputProject;
        readonly string? mutationLocation;

        /// <summary>
        /// Creates an instance of the core.
        /// </summary>
        /// <param name="inputProject">The sln file location</param>
        /// <param name="mutationLocation">The Folder containting the mutation files</param>
        public FaultifyCore(string inputProject, string mutationLocation)
        {
            this.inputProject = inputProject;
            ProjectLoader projectLoader = new ProjectLoader(inputProject);
            if(mutationLocation == null)
            {
                mutationLocation = $"{Directory.GetCurrentDirectory()}\\Mutator\\Mutations";
            }
            List<IMutation> mutations = GetMutations(mutationLocation);
            foreach (var compilation in projectLoader.getSolutionCompilations())
            {
                if (TestProjectDetector.IsTestProject(compilation.Value))
                {
                    foreach (Mutation mutation in mutations)
                    {
                        var mutatedCompilation = ASTMutator.Mutate(compilation.Value, mutation);
                        var x = mutatedCompilation.AssemblyName;
                        string outputLocation = $"C:\\FaultifyReforgedOutput\\{mutatedCompilation.AssemblyName}";
                        ASTMutator.compileCodeToLocation(mutatedCompilation, outputLocation);
                    }
                }
            }

            // run testrunner
        }

        /// <summary>
        /// Creates an instance of the core with an output location.
        /// </summary>
        /// <param name="inputProject">The sln file location</param>
        /// <param name="mutationLocation">The Folder containting the mutation files</param>
        /// <param name="outputLocation">Location to put the output</param>
        public FaultifyCore(string inputProject, string mutationLocation, string outputLocation)
        {
            this.inputProject = inputProject;
            this.outputProject = outputLocation;
            this.mutationLocation = mutationLocation;
        }

        public static List<IMutation> GetMutations(string mutationLocation)
        {
            return MutationLoader.LoadMutations(mutationLocation);
        }
    }
}
