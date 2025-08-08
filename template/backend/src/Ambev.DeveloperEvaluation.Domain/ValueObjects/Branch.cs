using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Branch : IEquatable<Branch>
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; } = string.Empty;

        private Branch() { }

        public Branch(Guid id, string description)
        {
            if (id == Guid.Empty)
                throw new DomainException("Branch ID is required.");

            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Branch description is required.");

            Id = id;
            Description = description.Trim();
        }

        public bool Equals(Branch? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Description == other.Description;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Branch);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Description);
        }

        public static bool operator ==(Branch? left, Branch? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Branch? left, Branch? right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"Branch: {Description} ({Id})";
        }
    }
}
