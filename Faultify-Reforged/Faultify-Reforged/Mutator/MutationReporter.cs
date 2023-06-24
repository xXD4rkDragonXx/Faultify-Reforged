using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faultify_Reforged.Core.Mutator
{
    internal class MutationReporter
    {
        private readonly Mutation mutation;
        private string? originalCode;
        private string? mutatedCode;

        public MutationReporter(Mutation mutation) 
        { 
            this.mutation = mutation;
        }

        public void AddOriginalCode(string originalCode)
        {
            this.originalCode = originalCode;
        }

        public void AddMutatedCode(string mutatedCode)
        {
            this.mutatedCode = mutatedCode;
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

    }
}
