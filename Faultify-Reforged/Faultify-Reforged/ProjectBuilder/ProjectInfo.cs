﻿using Buildalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faultify_Reforged.Core.ProjectBuilder
{
    internal class ProjectInfo : IProjectInfo
    {
        private readonly IAnalyzerResult _analyzerResult;
        private string _assemblyPath;

        public ProjectInfo(IAnalyzerResult analyzerResult)
        {
            _analyzerResult = analyzerResult;
        }

        public string ProjectFilePath => _analyzerResult.ProjectFilePath;

        public IEnumerable<string> ProjectReferences => _analyzerResult.ProjectReferences;
        public string AssemblyPath => _assemblyPath ??= Path.Combine(TargetDirectory, TargetFileName);
        public string ProjectName => GetProperty("ProjectName");

        public string TargetDirectory => GetProperty("TargetDir");

        public string TargetFileName => GetProperty("TargetFileName");

        private string GetProperty(string name)
        {
            return _analyzerResult.GetProperty(name);
        }
    }
}
