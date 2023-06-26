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
        private string? originalCodeMutated;
        private string? mutatedCodeMutated;

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
            this.originalCodeMutated = originalCode;
            this.mutatedCodeMutated = mutatedCode;
        }

        public string? GetOriginalCode()
        {
            return originalCodeMutated;
        }

        public string? GetMutatedCode()
        {
            return mutatedCodeMutated;
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
