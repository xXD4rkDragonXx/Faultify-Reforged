namespace Faultify_Reforged.Core.Mutator
{
    public interface IMutation
    {
        string Name { get; }

        string Description { get; }

        string Identifier { get; }

        public string Mutation();

        void Run() { }
    }
}
