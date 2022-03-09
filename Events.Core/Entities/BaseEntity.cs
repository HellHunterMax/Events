namespace Events.Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; init; }

        protected BaseEntity()
        {

        }
        public override bool Equals(object? obj)
        {
            if (obj is not BaseEntity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id.Equals(other);
        }

        public static bool operator ==(BaseEntity a, BaseEntity b)
        {
            if ((a is null) && (b is null))
                return true;

            if ((a is null) || (b is null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity a, BaseEntity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
