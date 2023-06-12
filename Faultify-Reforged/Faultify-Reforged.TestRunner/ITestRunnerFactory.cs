namespace Faultify_Reforged.TestRunner
{
    internal interface ITestRunnerFactory
    {
        public ITestRunner CreateTestRunner(string testProjectAssemblyPath);
    }
}
