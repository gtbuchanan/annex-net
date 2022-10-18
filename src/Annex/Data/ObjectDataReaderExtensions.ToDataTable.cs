#if NET
namespace Annex.Data;

using System.Data;
using System.Globalization;

/// <content />
public static partial class ObjectDataReaderExtensions
{
    /// <summary>
    /// Converts the specified sequence into a data table.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    /// <param name="this">The source sequence.</param>
    /// <returns>The data table.</returns>
    /// <example>
    /// For explicit control over the fields projected by the DataTable, just wrap your collection
    /// of entities in a anonymous type projection before wrapping it in an ObjectDataReader.
    ///
    /// Instead of:
    /// <code language="csharp">
    /// IEnumerable&lt;Order&gt; orders;
    /// ...
    /// DataTable dt = orders.ToDataTable();
    /// </code>
    /// do:
    /// <code language="csharp">
    /// IEnumerable&gt;Order&lt; orders;
    /// ...
    /// DataTable dt = orders
    ///     .Select(o => new
    ///     {
    ///        o.ID,
    ///        o.ShipDate,
    ///        ProductName = o.Product.Name,
    ///        ...
    ///     })
    ///     .ToDataTable();
    /// </code>
    /// </example>
    public static DataTable ToDataTable<T>(this IEnumerable<T> @this)
    {
        var dt = new DataTable
        {
            Locale = CultureInfo.CurrentCulture,
            TableName = typeof(T).Name,
        };
        using var reader = @this.AsDataReader();
        dt.Load(reader);
        return dt;
    }
}
#endif
