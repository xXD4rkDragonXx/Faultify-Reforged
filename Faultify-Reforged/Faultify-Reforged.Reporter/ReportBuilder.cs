using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faultify_Reforged.Reporter
{
    public class ReportBuilder
    {
        private StringBuilder ReportData = new StringBuilder(); 
        public ReportBuilder() 
        {
            AddBoilerPlateTop();
        }

        public void AddTestResult(string MutationName, string TestResult, string OriginalCode, string MutatedCode)
        {

            string testResult = $"<div>{MutationName}:{TestResult}, Original: {OriginalCode}, Mutated: {MutatedCode}";
            ReportData.Append(testResult);
        }

        private void AddBoilerPlateTop()
        {
            ReportData.Append(
              @"<head>
                    <title>Faultify Raport</title>
                </head>");
        }

        private void AddBoilerPlateBottom()
        {

        }

        public void BuildReport(string outputLocation)
        {
            AddBoilerPlateBottom();
            File.WriteAllText(Path.Combine(outputLocation, "FaultifyReport.html"), ReportData.ToString());
        }

    }
}
