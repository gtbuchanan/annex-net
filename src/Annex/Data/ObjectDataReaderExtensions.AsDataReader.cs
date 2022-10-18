#if NET
namespace Annex.Data;

using System.Data.Common;

/// <content />
public static partial class ObjectDataReaderExtensions
{
    /// <summary>
    /// Converts the specified sequence into a data reader.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    /// <param name="this">The source sequence.</param>
    /// <returns>The data reader.</returns>
    /// <example>
    /// For explicit control over the fields projected by the DataReader, just wrap your collection
    /// of entities in a anonymous type projection before wrapping it in an ObjectDataReader.
    ///
    /// Instead of:
    /// <code language="csharp">
    /// IEnumerable&lt;Order&gt; orders;
    /// ...
    /// IDataReader dr = orders.AsDataReader();
    /// </code>
    /// do:
    /// <code language="csharp">
    /// IEnumerable&gt;Order&lt; orders;
    /// ...
    /// IDataReader dr = orders
    ///     .Select(o => new
    ///     {
    ///        o.ID,
    ///        o.ShipDate,
    ///        ProductName = o.Product.Name,
    ///        ...
    ///     })
    ///     .AsDataReader();
    /// </code>
    /// </example>
    [Pure]
    public static DbDataReader AsDataReader<T>([NoEnumeration] this IEnumerable<T> @this) =>
        new ObjectDataReader<T>(@this);
}
#endif
