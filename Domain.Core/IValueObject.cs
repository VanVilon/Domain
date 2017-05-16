using System;

namespace Domain.Core
{
    /// <summary>
    /// Identifies this class as 'value object' container.
    /// </summary>
    /// <remarks>
    /// DDD tip: value objects should be immutable.
    /// </remarks>
    public interface IValueObject<T> : IEquatable<T> where T : IValueObject<T>
    {
    }
}