using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Customer : IEquatable<Customer>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;

        private Customer() { }

        public Customer(Guid id, string name)
        {
            if (id == Guid.Empty)
                throw new DomainException("Customer ID is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Customer name is required.");

            Id = id;
            Name = name.Trim();
        }

        public bool Equals(Customer? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Customer);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        public static bool operator ==(Customer? left, Customer? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Customer? left, Customer? right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"Customer: {Name} ({Id})";
        }
    }
}
