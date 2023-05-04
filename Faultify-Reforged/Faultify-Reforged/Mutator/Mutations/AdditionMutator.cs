using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faultify_Reforged.Core.Mutator.Mutations
{
    internal class AdditionMutator : IMutation
    {
        public string Name => "Addition Mutator";

        public string Description => "Mutates addition operations";

        public string Identifier => "+";

        public string Mutation()
        {
            throw new NotImplementedException();
        }
    }
}
