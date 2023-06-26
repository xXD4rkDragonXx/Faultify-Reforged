using System.Text;

namespace Faultify_Reforged.Reporter
{
    public class ReportBuilder
    {
        private StringBuilder ReportData = new StringBuilder();
        public ReportBuilder()
        {
            AddBoilerPlateTop();
        }

        public void AddTestResult(string MutationName, string TestResult, string OriginalCode, string MutatedCode, string TestName, string FileName)
        {
            string htmlID = MutationName + TestResult + OriginalCode + MutatedCode;
            string testResult =
                $"<div class=\"accordion-item\">" +
                $"<h2 class=\"accordion-header {TestResult}\" id=heading{htmlID}>" +
                $"<button class=\"accordion-button\" type=\"button\" data-toggle=\"collapse\" data-target=\"#collapse{htmlID}\" aria-expanded=\"true\" aria-controls=\"collapse{htmlID}\">" +
                    $"{MutationName} in {FileName}:{TestResult}" +
                $"</button>" +
                $"</h2>" +
                $"<div id=collapse{htmlID} class=\"accordion-collapse collapse show\" aria-labelledby=\"heading{htmlID}\" data-bs-parent=\"#faultifyaccordion\">" +
                    $"<div class=\"accordion-body\"><div>" +
                     $"Original: {OriginalCode}" +
                    $"</div>" +
                    $"<div>" +
                     $"Mutated: {MutatedCode}" +
                    $"</div>" +
                    $"<div>Test: {TestName} </div>" +
                $"</div></div></div";
            ReportData.Append(testResult);
        }

        private void AddBoilerPlateTop()
        {
            ReportData.Append(
              @"<head>
                    <title>Faultify Raport</title>

                    <meta name=""viewport"" content=""width=device-width, initial-scale=1, shrink-to-fit=no"">
                    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css"" integrity=""sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"" crossorigin=""anonymous"">

                    <style>
                        body{
                            width: 80%;
                            margin: auto;
                            margin-top: 5px;
                        }
                        
                        .Killed {
                            background-color: green;
                        }

                        .Survived {
                            background-color: red;
                        }
                    </style>
                </head>
                <body>
                    <div class=""accordion"" id=""faultifyaccordion"">
            ");
        }

        private void AddBoilerPlateBottom()
        {
            ReportData.Append(@"
                    </div>
                    <script src=""https://code.jquery.com/jquery-3.2.1.slim.min.js"" integrity=""sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"" crossorigin=""anonymous""></script>
                    <script src=""https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js"" integrity=""sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q"" crossorigin=""anonymous""></script>
                    <script src=""https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js"" integrity=""sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"" crossorigin=""anonymous""></script>
                </body>
            ");
        }

        public void BuildReport(string outputLocation)
        {
            AddBoilerPlateBottom();
            File.WriteAllText(Path.Combine(outputLocation, "FaultifyReport.html"), ReportData.ToString());
            Console.WriteLine($"Report has been published at: {Path.Combine(outputLocation, "FaultifyReport.html")}");
        }

    }
}
