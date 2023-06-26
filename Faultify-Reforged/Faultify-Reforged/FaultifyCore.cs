using Faultify_Reforged.Core.Mutator;
using Faultify_Reforged.Core.ProjectBuilder;
using Faultify_Reforged.Reporter;
using Faultify_Reforged.TestRunner;
using Faultify_Reforged.TestRunner.DotnetTestRunner;
using Microsoft.CodeAnalysis;
using System.Text.RegularExpressions;

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
        public FaultifyCore(string inputProject, string testProjectLocation, string mutationLocation) : this(inputProject, testProjectLocation, mutationLocation, $"{Path.GetDirectoryName(inputProject)}\\") //Constructor overloading reference Calls the constructor with a default value
        {

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
            var analyserResult = GenerateProject(testProjectLocation);
            var testProjects = DuplicateProjects(analyserResult);
            var projectLoader = new ProjectLoader(inputProject);
            var outputs = new List<string>();
            var reporter = new ReportBuilder();
            Console.WriteLine("Running original tests");
            var originalTestRunner = RunTestRunner(testProjectLocation);
            var originalTestResults = TestResultParser.ParseResults(originalTestRunner.getOutput());
            Console.WriteLine("Test Run compelete");

            if (mutationLocation == null)
            {
                mutationLocation = $"{Directory.GetCurrentDirectory()}\\Mutator\\Mutations";
            }
            Console.WriteLine($"Loading mutations located at {mutationLocation}");
            List<IMutation> mutations = GetMutations(mutationLocation);
            Console.WriteLine($"{mutations.Count} Mutations loaded");
            foreach (var compilation in projectLoader.getSolutionCompilations())
            {
                if (!TestProjectDetector.IsTestProject(compilation.Value))
                {
                    foreach (Mutation mutation in mutations)
                    {
                        var mutationReporter = new MutationReporter(mutation);
                        var mutatedCompilation = ASTMutator.Mutate(compilation.Value, mutation, mutationReporter);
                        if (mutationReporter.HasMutated())
                        {
                            Console.WriteLine($"Compiling {mutatedCompilation.AssemblyName}.dll");
                            string testFolder = testProjects.First();
                            string mutationOutputLocation = $"{testFolder}\\{mutatedCompilation.AssemblyName}.dll";
                            ASTMutator.compileCodeToLocation(mutatedCompilation, mutationOutputLocation);
                            Console.WriteLine("Running tests");
                            var testRunner = RunTestRunner($"{testFolder}\\{Path.GetFileNameWithoutExtension(testProjectLocation)}.dll");
                            outputs.Add(testRunner.getOutput());
                            ReportResult(reporter, testRunner.getOutput(), mutationReporter, originalTestResults);
                        }
                    }
                }
            }
            reporter.BuildReport(outputLocation);
        }

        /// <summary>
        /// Loads the mutations from the given mutationLocation
        /// </summary>
        /// <param name="mutationLocation"></param>
        /// <returns></returns>
        public static List<IMutation> GetMutations(string mutationLocation)
        {
            return MutationLoader.LoadMutations(mutationLocation);
        }

        /// <summary>
        /// Build the projectPath project
        /// </summary>
        /// <param name="projectPath">Path to the project</param>
        /// <returns></returns>
        private static IProjectInfo GenerateProject(string projectPath)
        {
            Console.WriteLine(@"Started building the test project");
            var analyserResult = TestProjectGenerator.GenerateTestProject(projectPath);
            Console.WriteLine(@"Finished building the test project");
            return analyserResult;
        }

        /// <summary>
        /// Duplicates a build project over multiple folders
        /// </summary>
        /// <param name="analyserResult">project to duplicate</param>
        /// <returns></returns>
        private static List<string> DuplicateProjects(IProjectInfo analyserResult)
        {
            Console.WriteLine("Started project duplication");
            ProjectDuplicator projectDuplicator = new ProjectDuplicator(Directory.GetParent(analyserResult.AssemblyPath).FullName);
            projectDuplicator.MakeInitialCopies(2);
            Console.WriteLine("Finished project duplication");
            return projectDuplicator.GetProjectFolders();
        }

        /// <summary>
        /// Runs the test runner
        /// </summary>
        /// <param name="testAssemblyPath">Path to the test assembly</param>
        /// <returns></returns>
        private static ITestRunner RunTestRunner(string testAssemblyPath)
        {
            ITestRunnerFactory testRunnerFactory = new DotnetTestRunnerFactory();
            ITestRunner testRunner = testRunnerFactory.CreateTestRunner(testAssemblyPath);
            testRunner.RunTests();
            return testRunner;
        }

        /// <summary>
        /// Report test result
        /// </summary>
        /// <param name="reportBuilder">Report builder to add report to</param>
        /// <param name="testResult">Output from testrunner</param>
        /// <param name="mutationReporter">Reporter to pass on report data</param>
        private static void ReportResult(ReportBuilder reportBuilder, string testResult, MutationReporter mutationReporter, MatchCollection originalTestResult)
        {
            Console.WriteLine("Reporting result");

            var parsedResults = TestResultParser.ParseResults(testResult);

            var (testName, killed) = IsMutationKilled(originalTestResult, parsedResults);

            var result = killed ? "Killed" : "Survived";

            reportBuilder.AddTestResult(mutationReporter.GetMutation().Name, result, mutationReporter.GetOriginalCode(), mutationReporter.GetMutatedCode(), testName, mutationReporter.GetFileName());
        }

        private static (string, bool) IsMutationKilled(MatchCollection originalTestResult, MatchCollection mutationTestResults)
        {
            for (int i = 0; i < originalTestResult.Count(); i++)
            {
                var match = originalTestResult[i];
                var mutationMatch = mutationTestResults[i];

                if (match.Groups[1].Value != mutationMatch.Groups[1].Value)
                {
                    return (match.Groups[2].Value, true);
                }
            }
            return (null, false);
        }
    }
}
