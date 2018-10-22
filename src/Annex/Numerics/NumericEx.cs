using System;
using System.Collections.Generic;
using System.Numerics;

namespace Annex.Numerics
{
    internal static class NumericEx
    {
        internal static Dictionary<Type, NumericClass> TypeMap { get; } = new Dictionary<Type, NumericClass>
        {
            { typeof(byte), NumericClass.Integral },
            { typeof(sbyte), NumericClass.Integral },
            { typeof(ushort), NumericClass.Integral },
            { typeof(short), NumericClass.Integral },
            { typeof(uint), NumericClass.Integral },
            { typeof(int), NumericClass.Integral },
            { typeof(ulong), NumericClass.Integral },
            { typeof(long), NumericClass.Integral },
            { typeof(decimal), NumericClass.FloatingPoint },
            { typeof(double), NumericClass.FloatingPoint },
            { typeof(float), NumericClass.FloatingPoint },
#if NET461 || NETCOREAPP || NETSTANDARD || TIZEN || WINDOWS_UWP
            { typeof(BigInteger), NumericClass.Integral },
            { typeof(Complex), NumericClass.Complex },
#endif
#if NET461 || NETCOREAPP || TIZEN || WINDOWS_UWP
            { typeof(Matrix3x2), NumericClass.Vector },
            { typeof(Matrix4x4), NumericClass.Vector },
            { typeof(Plane), NumericClass.Vector },
            { typeof(Quaternion), NumericClass.Vector },
            { typeof(Vector2), NumericClass.Vector },
            { typeof(Vector3), NumericClass.Vector },
            { typeof(Vector4), NumericClass.Vector },

#endif
#if NETCOREAPP || TIZEN || WINDOWS_UWP
            { typeof(Vector<>), NumericClass.Vector }
#endif
        };
    }
}
