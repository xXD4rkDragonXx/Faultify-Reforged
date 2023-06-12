namespace Faultify_Reforged.TestRunner
{
    internal interface ITestRunner
    {
        public Task<string> RunTests(); // Change string To result object
    }
}
