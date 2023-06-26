namespace Faultify_Reforged.Core.Mutator
{
    internal class MutationReporter
    {
        private readonly Mutation mutation;
        private string? originalCode;
        private string? mutatedCode;
        private string? fileName;
        private bool hasMutated;
        private string? mutatedFileName;

        public MutationReporter(Mutation mutation)
        {
            this.mutation = mutation;
            this.hasMutated = false;
        }

        public void AddOriginalCode(string originalCode)
        {
            this.originalCode = originalCode;
        }

        public void AddMutatedCode(string mutatedCode)
        {
            this.mutatedCode = mutatedCode;
        }

        public void AddFileName(string fileName)
        {
            this.fileName = fileName;
        }

        public void setMutated()
        {
            this.hasMutated = true;
            this.mutatedFileName = fileName; //Quick and dirty to set correct filename
        }

        public string? GetOriginalCode()
        {
            return originalCode;
        }

        public string? GetMutatedCode()
        {
            return mutatedCode;
        }

        public Mutation GetMutation()
        {
            return mutation;
        }

        public string? GetFileName()
        {
            return mutatedFileName;
        }

        public bool HasMutated()
        {
            return hasMutated;
        }

    }
}
