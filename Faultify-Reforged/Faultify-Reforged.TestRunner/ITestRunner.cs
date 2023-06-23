namespace Faultify_Reforged.TestRunner
{
    public interface ITestRunner
    {
        public Task<string> RunTests(); // Change string To result object
    }
}
