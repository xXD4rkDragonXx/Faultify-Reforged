using Newtonsoft.Json.Linq;

namespace Faultify_Reforged.Core.Mutator
{
    /// <summary>
    /// Loads all mutations
    /// </summary>
    internal class MutationLoader
    {
        /// <summary>
        /// Load all mutations who are located at specified location
        /// </summary>
        /// <param name="mutationsPath">Path to mutation folder</param>
        /// <returns></returns>
        public static List<IMutation> LoadMutations(string mutationsPath)
        {
            var mutations = new List<IMutation>();
            foreach (var mutationFile in Directory.GetFiles(mutationsPath))
            {
                var mutation = JObject.Parse(File.ReadAllText(mutationFile));
                if (mutation.ContainsKey("Name") && mutation.ContainsKey("Description") && mutation.ContainsKey("Identifier") && mutation.ContainsKey("Mutations"))
                {
                    mutations.Add(mutation.ToObject<Mutation>());
                }
                else
                {
                    Console.WriteLine($"[WARNING] The mutation at {mutationFile} is missing a required \"Name\", \"Description\", \"Identifier\" or \"Mutations\" property.");
                }
            }

            return mutations;
        }
    }
}
