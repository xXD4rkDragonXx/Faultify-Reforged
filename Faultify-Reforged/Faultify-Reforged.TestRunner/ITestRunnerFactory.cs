namespace Faultify_Reforged.TestRunner
{
    public interface ITestRunnerFactory
    {
        public ITestRunner CreateTestRunner(string testProjectAssemblyPath);
    }
}
