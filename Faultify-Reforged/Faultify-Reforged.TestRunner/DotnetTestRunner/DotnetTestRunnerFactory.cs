namespace Faultify_Reforged.TestRunner.DotnetTestRunner
{
    internal class DotnetTestRunnerFactory : ITestRunnerFactory
    {
        public ITestRunner CreateTestRunner(string testProjectAssemblyPath)
        {
            return new DotnetTestRunner(testProjectAssemblyPath);
        }
    }
}
