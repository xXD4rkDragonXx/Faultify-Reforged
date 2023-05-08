namespace Faultify_Reforged.Core.Mutator
{
    public interface IMutation
    {
        string Name { get; set; }
        string Description { get; set; }
        string Identifier { get; set; }
        string[] Mutations { get; set; }
    }
}
