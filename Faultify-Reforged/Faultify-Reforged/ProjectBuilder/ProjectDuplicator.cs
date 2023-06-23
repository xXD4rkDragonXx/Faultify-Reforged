using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Faultify_Reforged.Core.ProjectBuilder
{
    internal class ProjectDuplicator
    {
        private readonly string _testDirectory;
        private List<string> projectFolders = new List<string>();

        public ProjectDuplicator(string testDirectory) 
        {
            _testDirectory = testDirectory;
        }

        public void MakeInitialCopies(int count)
        {
            var directoryInfo = new DirectoryInfo(_testDirectory);

            foreach (var directory in directoryInfo.GetDirectories("*"))
            {
                var match = Regex.Match(directory.Name,
                    "(^cs$|^pl$|^rt$|^de$|^en$|^es$|^fr$|^it$|^ja$|^ko$|^ru$|^zh-Hans$|^zh-Hant$|^tr$|^pt-BR$|^test-duplication-\\d$)");

                if (match.Captures.Count != 0) Directory.Delete(directory.FullName, true);
            }

            // Start the initial copy
            var allFiles = Directory.GetFiles(_testDirectory, "*.*", SearchOption.AllDirectories).ToList();
            var newDirInfo = Directory.CreateDirectory(Path.Combine(_testDirectory, "test-duplication-0"));


            foreach (var file in allFiles)
            {
                try
                {
                    var mFile = new FileInfo(file);

                    if (mFile.Directory.FullName == newDirInfo.Parent.FullName)
                    {
                        var newPath = Path.Combine(newDirInfo.FullName, mFile.Name);
                        mFile.MoveTo(newPath);
                    }
                    else
                    {
                        var path = mFile.FullName.Replace(newDirInfo.Parent.FullName, "");
                        var newPath = new FileInfo(Path.Combine(newDirInfo.FullName, path.Trim('\\')));

                        if (!Directory.Exists(newPath.DirectoryName)) Directory.CreateDirectory(newPath.DirectoryName);

                        mFile.MoveTo(newPath.FullName, true);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                };
            }

            Parallel.ForEach(Enumerable.Range(1, count), i =>
            {
                var newDirectoryPath = Path.Combine(_testDirectory, $"test-duplication-{i}");
                CopyDirectoryRecursively(newDirInfo, Directory.CreateDirectory(newDirectoryPath));
                projectFolders.Add(newDirectoryPath);
            });

        }

        public static void CopyDirectoryRecursively(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory)
        {
            foreach (var directory in sourceDirectory.GetDirectories())
            {
                CopyDirectoryRecursively(directory, targetDirectory.CreateSubdirectory(directory.Name));
            }
            foreach (var file in sourceDirectory.GetFiles())
            {
                file.CopyTo(Path.Combine(targetDirectory.FullName, file.Name));
            }
        }

        public List<string> GetProjectFolders()
        {
            return projectFolders;
        }
    }
}
