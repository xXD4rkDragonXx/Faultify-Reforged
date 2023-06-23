namespace Faultify_Reforged.TestRunner.DotnetTestRunner
{
    public class DotnetTestRunnerFactory : ITestRunnerFactory
    {
        public ITestRunner CreateTestRunner(string testProjectAssemblyPath)
        {
            return new DotnetTestRunner(testProjectAssemblyPath);
        }
    }
}
