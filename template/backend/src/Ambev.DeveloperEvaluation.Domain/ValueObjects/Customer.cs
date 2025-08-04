namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Customer(Guid id, string name)
        {
            if (id == Guid.Empty)
                throw new DomainException("Customer ID cannot be empty.");

            Id = id;
            Name = name ?? throw new DomainException("Customer name is required.");
        }
    }
}
