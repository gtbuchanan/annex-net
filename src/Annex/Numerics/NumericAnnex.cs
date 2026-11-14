namespace Annex.Numerics;

using System.Numerics;

internal static class NumericAnnex
{
    internal static Dictionary<Type, NumericTypeOption> TypeMap { get; } = new()
    {
        { typeof(byte), NumericTypeOption.Integral },
        { typeof(sbyte), NumericTypeOption.Integral },
        { typeof(ushort), NumericTypeOption.Integral },
        { typeof(short), NumericTypeOption.Integral },
        { typeof(uint), NumericTypeOption.Integral },
        { typeof(int), NumericTypeOption.Integral },
        { typeof(ulong), NumericTypeOption.Integral },
        { typeof(long), NumericTypeOption.Integral },
        { typeof(decimal), NumericTypeOption.FloatingPoint },
        { typeof(double), NumericTypeOption.FloatingPoint },
        { typeof(float), NumericTypeOption.FloatingPoint },
        { typeof(BigInteger), NumericTypeOption.Integral },
        { typeof(Complex), NumericTypeOption.Complex },
        { typeof(Matrix3x2), NumericTypeOption.Vector },
        { typeof(Matrix4x4), NumericTypeOption.Vector },
        { typeof(Plane), NumericTypeOption.Vector },
        { typeof(Quaternion), NumericTypeOption.Vector },
        { typeof(Vector2), NumericTypeOption.Vector },
        { typeof(Vector3), NumericTypeOption.Vector },
        { typeof(Vector4), NumericTypeOption.Vector },
#if NET6_0_OR_GREATER || NETFRAMEWORK || NETSTANDARD || TIZEN || WINDOWS_UWP
        { typeof(Vector<>), NumericTypeOption.Vector },
#endif
    };
}
