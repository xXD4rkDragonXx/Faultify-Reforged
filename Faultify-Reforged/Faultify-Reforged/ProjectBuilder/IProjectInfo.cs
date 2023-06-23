using Buildalyzer;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faultify_Reforged.Core.ProjectBuilder
{
    internal interface IProjectInfo
    {
        string ProjectFilePath { get; }
        IEnumerable<string> ProjectReferences { get; }
        string AssemblyPath { get; }

        string ProjectName { get; }
        string TargetDirectory { get; }
        string TargetFileName { get; }
        IAnalyzerResult analyzerResult { get; }
    }
}
