namespace CheckflixApp.Domain.Common.Primitives;

// Learn more: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects
public abstract class ValueObject : IEquatable<ValueObject>
{
    //protected static bool EqualOperator(ValueObject left, ValueObject right)
    //{
    //    if (left is null ^ right is null)
    //    {
    //        return false;
    //    }

    //    return left?.Equals(right!) != false;
    //}

    //protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    //{
    //    return !EqualOperator(left, right);
    //}

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }
    public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);

    public bool Equals(ValueObject? other) => !(other is null) && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// Gets the atomic values of the value object.
    /// </summary>
    /// <returns>The collection of objects representing the value object values.</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();
}
