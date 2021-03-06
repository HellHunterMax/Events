namespace Events.Core.Shared.ValueObjects
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }
        public bool Equals(ValueObject? other)
        {
            if (other is null || other.GetType() != GetType())
            {
                return false;
            }
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject one, ValueObject two)
        {
            return one?.Equals(two) ?? (one is null && two is null);
        }

        public static bool operator !=(ValueObject one, ValueObject two)
        {
            return !(one?.Equals(two) ?? (one is null && two is null));
        }
    }
}
