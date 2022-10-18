#if NET
namespace Annex.Data;

using System.Collections;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

/// <summary>
/// <para>
/// The ObjectDataReader wraps a collection of CLR objects in a DbDataReader.
/// Only "scalar" properties are projected.
/// </para>
/// <para>
/// This is useful for doing high-speed data loads with SqlBulkCopy, and copying collections
/// of entities to a DataTable for use with SQL Server Table-Valued parameters, or for interop
/// with older ADO.NET applciations.
/// </para>
/// </summary>
/// <typeparam name="T">The type of element.</typeparam>
/// <remarks>
/// Ideally, <see href="https://github.com/mgravell/fast-member">FastMember's</see>
/// <c>ObjectReader</c> would be used instead. However, using it in some cases causes cryptic
/// exceptions from ADO.NET.
/// </remarks>
/// <seealso href="https://github.com/microsoftarchive/msdn-code-gallery-community-m-r/blob/master/ObjectDataReader/%5BC%23%5D-ObjectDataReader/C%23/ObjectDataReader/ObjectDataReader.cs">
/// Adapted from Microsoft sample
/// </seealso>
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
internal sealed class ObjectDataReader<T> : DbDataReader
{
    private const string SchemaTableSchema =
        @"<?xml version=""1.0"" standalone=""yes""?> 
<xs:schema id=""NewDataSet"" xmlns=""""
           xmlns:xs=""http://www.w3.org/2001/XMLSchema""
           xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata""> 
  <xs:element name=""NewDataSet"" msdata:IsDataSet=""true"" msdata:MainDataTable=""SchemaTable""
              msdata:Locale=""""> 
    <xs:complexType> 
      <xs:choice minOccurs=""0"" maxOccurs=""unbounded""> 
        <xs:element name=""SchemaTable"" msdata:Locale="""" msdata:MinimumCapacity=""1""> 
          <xs:complexType> 
            <xs:sequence> 
              <xs:element name=""ColumnName"" msdata:ReadOnly=""true"" type=""xs:string""
                          minOccurs=""0"" /> 
              <xs:element name=""ColumnOrdinal"" msdata:ReadOnly=""true"" type=""xs:int""
                          default=""0"" minOccurs=""0"" /> 
              <xs:element name=""ColumnSize"" msdata:ReadOnly=""true"" type=""xs:int""
                          minOccurs=""0"" /> 
              <xs:element name=""NumericPrecision"" msdata:ReadOnly=""true"" type=""xs:short""
                          minOccurs=""0"" /> 
              <xs:element name=""NumericScale"" msdata:ReadOnly=""true"" type=""xs:short""
                          minOccurs=""0"" /> 
              <xs:element name=""IsUnique"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          minOccurs=""0"" /> 
              <xs:element name=""IsKey"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          minOccurs=""0"" /> 
              <xs:element name=""BaseServerName"" msdata:ReadOnly=""true"" type=""xs:string""
                          minOccurs=""0"" /> 
              <xs:element name=""BaseCatalogName"" msdata:ReadOnly=""true"" type=""xs:string""
                          minOccurs=""0"" /> 
              <xs:element name=""BaseColumnName"" msdata:ReadOnly=""true"" type=""xs:string""
                          minOccurs=""0"" /> 
              <xs:element name=""BaseSchemaName"" msdata:ReadOnly=""true"" type=""xs:string""
                          minOccurs=""0"" /> 
              <xs:element name=""BaseTableName"" msdata:ReadOnly=""true"" type=""xs:string""
                          minOccurs=""0"" /> 
              <xs:element name=""DataType"" msdata:DataType=""System.Type, mscorlib, " +
        @"Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" " +
        @"msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" /> 
              <xs:element name=""AllowDBNull"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          minOccurs=""0"" /> 
              <xs:element name=""ProviderType"" msdata:ReadOnly=""true"" type=""xs:int""
                          minOccurs=""0"" /> 
              <xs:element name=""IsAliased"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          minOccurs=""0"" /> 
              <xs:element name=""IsExpression"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          minOccurs=""0"" /> 
              <xs:element name=""IsIdentity"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          minOccurs=""0"" /> 
              <xs:element name=""IsAutoIncrement"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          minOccurs=""0"" /> 
              <xs:element name=""IsRowVersion"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          minOccurs=""0"" /> 
              <xs:element name=""IsHidden"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          minOccurs=""0"" /> 
              <xs:element name=""IsLong"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          default=""false"" minOccurs=""0"" /> 
              <xs:element name=""IsReadOnly"" msdata:ReadOnly=""true"" type=""xs:boolean""
                          minOccurs=""0"" /> 
              <xs:element name=""ProviderSpecificDataType"" msdata:DataType=""System.Type, " +
        @"mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089""
                          msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" /> 
              <xs:element name=""DataTypeName"" msdata:ReadOnly=""true"" type=""xs:string""
                          minOccurs=""0"" /> 
              <xs:element name=""XmlSchemaCollectionDatabase"" msdata:ReadOnly=""true""
                          type=""xs:string"" minOccurs=""0"" /> 
              <xs:element name=""XmlSchemaCollectionOwningSchema"" msdata:ReadOnly=""true""
                          type=""xs:string"" minOccurs=""0"" /> 
              <xs:element name=""XmlSchemaCollectionName"" msdata:ReadOnly=""true""
                          type=""xs:string"" minOccurs=""0"" /> 
              <xs:element name=""UdtAssemblyQualifiedName"" msdata:ReadOnly=""true""
                          type=""xs:string"" minOccurs=""0"" /> 
              <xs:element name=""NonVersionedProviderType"" msdata:ReadOnly=""true""
                          type=""xs:int"" minOccurs=""0"" /> 
            </xs:sequence> 
          </xs:complexType> 
        </xs:element> 
      </xs:choice> 
    </xs:complexType> 
  </xs:element> 
</xs:schema>";

    private bool closed;

    private T? current;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectDataReader{T}"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="data"/> is <c>null</c>.
    /// </exception>
    public ObjectDataReader([NoEnumeration] IEnumerable<T> data) =>
        this.Enumerator = data.GetEnumerator();

    /// <inheritdoc/>
    public override int Depth => 1;

    /// <inheritdoc/>
    public override bool IsClosed => this.closed;

    /// <inheritdoc/>
    public override int RecordsAffected => -1;

    /// <inheritdoc/>
    public override int FieldCount => Columns.Length;

    /// <inheritdoc/>
    public override bool HasRows => throw new NotSupportedException();

    private static HashSet<Type> ScalarTypes { get; } = new()
    {
        // Reference types
        typeof(string),
        typeof(byte[]),

        // Value types
        typeof(byte),
        typeof(short),
        typeof(int),
        typeof(long),
        typeof(float),
        typeof(double),
        typeof(decimal),
        typeof(DateTime),
        typeof(Guid),
        typeof(bool),
        typeof(TimeSpan),

        // Nullable value types
        typeof(byte?),
        typeof(short?),
        typeof(int?),
        typeof(long?),
        typeof(float?),
        typeof(double?),
        typeof(decimal?),
        typeof(DateTime?),
        typeof(Guid?),
        typeof(bool?),
        typeof(TimeSpan?),
    };

    private static Column[] Columns { get; } = DiscoverColumns(typeof(T));

    private IEnumerator<T> Enumerator { get; }

    /// <inheritdoc/>
    public override object this[string name] => this.GetValue(this.GetOrdinal(name));

    /// <inheritdoc/>
    public override object this[int ordinal] => this.GetValue(ordinal);

    /// <inheritdoc/>
    [SuppressMessage(
        "Security",
        "CA5366:Use XmlReader for 'DataSet.ReadXml()'",
        Justification = "Schema is intentionally defined in code.")]
    public override DataTable GetSchemaTable()
    {
        var s = new DataSet { Locale = CultureInfo.CurrentCulture };
        s.ReadXmlSchema(new StringReader(SchemaTableSchema));
        var t = s.Tables[0];
        for (var i = 0; i < this.FieldCount; i++)
        {
            var row = t.NewRow();
            row["ColumnName"] = this.GetName(i);
            row["ColumnOrdinal"] = i;
            row["DataType"] = this.GetFieldType(i);
            row["DataTypeName"] = this.GetDataTypeName(i);
            row["ColumnSize"] = -1;
            t.Rows.Add(row);
        }

        return t;
    }

    /// <inheritdoc/>
    public override void Close() => this.closed = true;

    /// <inheritdoc/>
    public override bool NextResult() => false;

    /// <inheritdoc/>
    public override bool Read()
    {
        var rv = this.Enumerator.MoveNext();
        if (rv)
            this.current = this.Enumerator.Current;
        return rv;
    }

    /// <inheritdoc/>
    public override bool GetBoolean(int ordinal) => this.GetValue<bool>(ordinal);

    /// <inheritdoc/>
    public override byte GetByte(int ordinal) => this.GetValue<byte>(ordinal);

    /// <inheritdoc/>
    public override long GetBytes(
        int ordinal,
        long dataOffset,
        byte[]? buffer,
        int bufferOffset,
        int length)
    {
        if (buffer == null)
            throw new ArgumentNullException(nameof(buffer));
        var buf = this.GetValue<byte[]>(ordinal);

        if (buf == null)
            return 0;

        var bytes = Math.Min(length, buf.Length - (int)dataOffset);
        Buffer.BlockCopy(buf, (int)dataOffset, buffer, bufferOffset, bytes);
        return bytes;
    }

    /// <inheritdoc/>
    public override char GetChar(int ordinal) => this.GetValue<char>(ordinal);

    /// <inheritdoc/>
    public override long GetChars(
        int ordinal,
        long dataOffset,
        char[]? buffer,
        int bufferOffset,
        int length)
    {
        if (buffer == null)
            throw new ArgumentNullException(nameof(buffer));
        var s = this.GetValue<string>(ordinal);

        if (s == null)
            return 0;

        var chars = Math.Min(length, s.Length - (int)dataOffset);
        s.CopyTo((int)dataOffset, buffer, bufferOffset, chars);
        return chars;
    }

    /// <inheritdoc/>
    public override string GetDataTypeName(int ordinal) => this.GetFieldType(ordinal).Name;

    /// <inheritdoc/>
    public override DateTime GetDateTime(int ordinal) => this.GetValue<DateTime>(ordinal);

    /// <inheritdoc/>
    public override decimal GetDecimal(int ordinal) => this.GetValue<decimal>(ordinal);

    /// <inheritdoc/>
    public override double GetDouble(int ordinal) => this.GetValue<double>(ordinal);

    /// <inheritdoc/>
    public override Type GetFieldType(int ordinal)
    {
        var t = Columns[ordinal].Type;
        return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>)
            ? t.GetGenericArguments()[0] : t;
    }

    /// <inheritdoc/>
    public override float GetFloat(int ordinal) => this.GetValue<float>(ordinal);

    /// <inheritdoc/>
    public override Guid GetGuid(int ordinal) => this.GetValue<Guid>(ordinal);

    /// <inheritdoc/>
    public override short GetInt16(int ordinal) => this.GetValue<short>(ordinal);

    /// <inheritdoc/>
    public override int GetInt32(int ordinal) => this.GetValue<int>(ordinal);

    /// <inheritdoc/>
    public override long GetInt64(int ordinal) => this.GetValue<long>(ordinal);

    /// <inheritdoc/>
    public override string GetName(int ordinal) => Columns[ordinal].Name;

    /// <inheritdoc/>
    public override int GetOrdinal(string name)
    {
        for (var i = 0; i < Columns.Length; i++)
        {
            if (this.GetName(i) == name)
                return i;
        }

        return -1;
    }

    /// <inheritdoc/>
    public override string GetString(int ordinal) => this.GetValue<string>(ordinal)!;

    /// <inheritdoc/>
    public override int GetValues(object?[] values)
    {
        for (var i = 0; i < Columns.Length; i++)
            values[i] = this.GetValue(i);
        return Columns.Length;
    }

    /// <inheritdoc/>
    public override object GetValue(int ordinal) => this.GetValue<object>(ordinal)!;

    /// <inheritdoc/>
    public override bool IsDBNull(int ordinal) => this.GetValue<object>(ordinal) == null;

    /// <inheritdoc/>
    public override IEnumerator GetEnumerator() => this.Enumerator;

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        this.Close();
        base.Dispose(disposing);
    }

    private static bool IsScalarType(Type t) => ScalarTypes.Contains(t);

    private static Column[] DiscoverColumns(Type type)
    {
        if (IsScalarType(type)) return new[] { new Column("Value", type, t => t) };
        var properties = type.GetProperties().Where(p => IsScalarType(p.PropertyType)).ToArray();

        // Try to determine order from constructor accepting all property values (e.g. anonymous
        // types)
        return type.GetConstructors()
            .Select(constructor => constructor.GetParameters())
            .Where(parameters => parameters.Length == properties.Length)
            .Select(parameters => parameters
                .Join(
                    properties,
                    param => new
                    {
                        Name = param.Name?.ToLower(CultureInfo.InvariantCulture),
                        Type = param.ParameterType,
                    },
                    prop => new
                    {
                        Name = prop.Name?.ToLower(CultureInfo.InvariantCulture),
                        Type = prop.PropertyType,
                    },
                    (param, prop) => new { param.Position, Property = prop })
                .ToArray())
            .Where(matches => matches.Length == properties.Length)
            .Select(matches => matches.OrderBy(m => m.Position).Select(m => m.Property))
            .DefaultIfEmpty(properties)
            .Single()
            .Select(p => new Column(p))
            .ToArray();
    }

    private TField? GetValue<TField>(int i) =>
        this.current == null
            ? throw new InvalidOperationException(
                $"Expected {nameof(this.current)} to be not null.")
            : (TField?)Columns[i].GetValue(this.current);

    private sealed class Column
    {
        public Column(PropertyInfo pi)
        {
            this.Name = pi.Name;
            this.Type = pi.PropertyType;
            this.ValueAccessor = MakePropertyAccessor<T, object?>(pi);
        }

        public Column(string name, Type type, Func<T, object?> valueAccessor)
        {
            this.Name = name;
            this.Type = type;
            this.ValueAccessor = valueAccessor;
        }

        public string Name { get; }

        public Type Type { get; }

        private Func<T, object?> ValueAccessor { get; }

        public object? GetValue(T target) => this.ValueAccessor(target);

        private static Func<TObject, TValue> MakePropertyAccessor<TObject, TValue>(MemberInfo pi)
        {
            var objParam = Expression.Parameter(typeof(TObject), "obj");
            var typedAccessor = Expression.PropertyOrField(objParam, pi.Name);
            var castToObject = Expression.Convert(typedAccessor, typeof(object));
            LambdaExpression lambdaExpr = Expression.Lambda<Func<TObject, TValue>>(
                castToObject,
                objParam);
            return (Func<TObject, TValue>)lambdaExpr.Compile();
        }
    }
}
#endif
