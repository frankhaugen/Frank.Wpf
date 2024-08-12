namespace Frank.Wpf.Controls.SimpleInputs;

public static class ConditionHelper
{
    public static bool IsOneOf<T>(params Type[] types) => types.Any(type => typeof(T) == type);

    public static bool IsNumericType(Type type)
    {
        return Type.GetTypeCode(type) switch
        {
            TypeCode.Byte => true,
            TypeCode.SByte => true,
            TypeCode.UInt16 => true,
            TypeCode.UInt32 => true,
            TypeCode.UInt64 => true,
            TypeCode.Int16 => true,
            TypeCode.Int32 => true,
            TypeCode.Int64 => true,
            TypeCode.Decimal => true,
            TypeCode.Double => true,
            TypeCode.Single => true,
            _ => false
        };
    }
}