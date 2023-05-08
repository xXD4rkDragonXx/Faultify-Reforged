namespace Faultify_Reforged.Core.Mutator
{
    internal class Mutation : IMutation
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Identifier { get; set; }

        public string[] Mutations { get; set; }
    }
}
