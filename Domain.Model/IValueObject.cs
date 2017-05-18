using System;

namespace Domain.Model
{
    /// <summary>
    /// Overriden in a class, identifies this class as 'value object'.
    /// </summary>
    /// <remarks>
    /// DDD tip: value objects should be immutable.
    /// </remarks>
    public interface IValueObject<T> : IEquatable<T> where T : IValueObject<T>
    {
    }
}