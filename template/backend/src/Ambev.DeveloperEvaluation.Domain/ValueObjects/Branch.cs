using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Branch
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }

        public Branch(Guid id, string description)
        {
            if (id == Guid.Empty)
                throw new DomainException("Branch ID cannot be empty.");

            Id = id;
            Description = description ?? throw new DomainException("Branch description is required.");
        }
    }
}
