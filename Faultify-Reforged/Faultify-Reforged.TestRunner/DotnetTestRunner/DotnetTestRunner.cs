using System.Diagnostics;

namespace Faultify_Reforged.TestRunner.DotnetTestRunner
{
    internal class DotnetTestRunner : ITestRunner
    {
        private string testProjectAssemblyPath;
        public string Output;
        public string Error;

        public DotnetTestRunner(string testProjectAssemblyPath)
        {
            this.testProjectAssemblyPath = testProjectAssemblyPath;
            Output = string.Empty;
            Error = string.Empty;
        }

        public Task<string> RunTests()
        {
            var dotnetTestAruguments = new DotnetTestArgumentBuilder(testProjectAssemblyPath)
                                            .WithoutLogo()
                                            .Build();
            var processStartInfo = GetProcessStartInfo(dotnetTestAruguments);
            var process = RunProcess(processStartInfo);

            throw new NotImplementedException("Still need to add tests"); // TODO: Implement
        }

        private ProcessStartInfo GetProcessStartInfo(string dotnetTestAruguments)
        {
            var processStartInfo = new ProcessStartInfo("dotnet", dotnetTestAruguments)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = true,
                CreateNoWindow = true,
            };
            return processStartInfo;
        }

        private Process RunProcess(ProcessStartInfo processStartInfo)
        {
            Process process = new Process();
            process.StartInfo = processStartInfo;

            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Output += e.Data + Environment.NewLine;
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Error += e.Data + Environment.NewLine;
            };

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            return process;
        }
    }
}
