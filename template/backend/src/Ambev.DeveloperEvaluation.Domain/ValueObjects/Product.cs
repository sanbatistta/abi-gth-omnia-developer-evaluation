using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Product : IEquatable<Product>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;

        private Product() { }

        public Product(Guid id, string name)
        {
            if (id == Guid.Empty)
                throw new DomainException("Product ID is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name is required.");

            Id = id;
            Name = name.Trim();
        }

        public bool Equals(Product? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Product);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        public static bool operator ==(Product? left, Product? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Product? left, Product? right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"Product: {Name} ({Id})";
        }
    }
}
