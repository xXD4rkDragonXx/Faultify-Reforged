using Faultify_Reforged.Core.Mutator;
using Faultify_Reforged.Core.ProjectBuilder;
using Microsoft.CodeAnalysis;
using Faultify_Reforged.TestRunner;
using Faultify_Reforged.TestRunner.DotnetTestRunner;

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
        /// <param name="testProjectLocation">The test project file location</param>
        /// <param name="mutationLocation">The Folder containting the mutation files</param>
        public FaultifyCore(string inputProject, string testProjectLocation, string mutationLocation)
        {
            this.inputProject = inputProject;
            var analyserResult = GenerateProject(testProjectLocation, inputProject);
            var testProjects = DuplicateProjects(analyserResult);
            var projectLoader = new ProjectLoader(inputProject);

            if(mutationLocation == null)
            {
                mutationLocation = $"{Directory.GetCurrentDirectory()}\\Mutator\\Mutations";
            }
            List<IMutation> mutations = GetMutations(mutationLocation);
            foreach (var compilation in projectLoader.getSolutionCompilations())
            {
                foreach (Mutation mutation in mutations)
                {
                    var mutatedCompilation = ASTMutator.Mutate(compilation.Value, mutation);
                    var x = mutatedCompilation.AssemblyName;
                    string testFolder = testProjects.First();
                    string outputLocation = $"{testFolder}\\{mutatedCompilation.AssemblyName}.dll";
                    ASTMutator.compileCodeToLocation(mutatedCompilation, outputLocation);
                    var testRunner = runTestRunner($"{testFolder}\\{Path.GetFileNameWithoutExtension(testProjectLocation)}.dll");
                }

            }


        }

        /// <summary>
        /// Creates an instance of the core with an output location.
        /// </summary>
        /// <param name="inputProject">The sln file location</param>
        /// <param name="testProjectLocation">The test project file location</param>
        /// <param name="mutationLocation">The Folder containting the mutation files</param>
        /// <param name="outputLocation">Location to put the output</param>
        public FaultifyCore(string inputProject, string testProjectLocation, string mutationLocation, string outputLocation)
        {
            this.inputProject = inputProject;
            this.outputProject = outputLocation;
            this.mutationLocation = mutationLocation;
        }

        public static List<IMutation> GetMutations(string mutationLocation)
        {
            return MutationLoader.LoadMutations(mutationLocation);
        }

        private static IProjectInfo GenerateProject(string projectPath, string solutionPath)
        {
            var analyserResult = TestProjectGenerator.GenerateTestProject(solutionPath, projectPath);
            return analyserResult;
        }

        private static List<string> DuplicateProjects(IProjectInfo analyserResult)
        {
            ProjectDuplicator projectDuplicator = new ProjectDuplicator(Directory.GetParent(analyserResult.AssemblyPath).FullName);
            projectDuplicator.MakeInitialCopies(2);
            return projectDuplicator.GetProjectFolders();
        }

        private static ITestRunner runTestRunner(string testAssemblyPath)
        {
            ITestRunnerFactory testRunnerFactory = new DotnetTestRunnerFactory();
            ITestRunner testRunner = testRunnerFactory.CreateTestRunner(testAssemblyPath);
            testRunner.RunTests().GetAwaiter().GetResult();
            return testRunner;
        }
    }
}
