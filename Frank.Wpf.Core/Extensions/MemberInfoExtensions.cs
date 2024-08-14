using System.Reflection;

namespace Frank.Wpf.Core;

public static class MemberInfoExtensions
{
    public static bool IsPublic(this MemberInfo memberInfo) =>
        memberInfo.MemberType switch
        {
            MemberTypes.Field => ((FieldInfo)memberInfo).IsPublic,
            MemberTypes.Property => ((PropertyInfo)memberInfo).GetAccessors().Any(MethodInfo => MethodInfo.IsPublic),
            _ => false
        };

    public static bool IsString(this MemberInfo memberInfo) =>
        memberInfo.MemberType switch
        {
            MemberTypes.Field => ((FieldInfo)memberInfo).FieldType == typeof(string),
            MemberTypes.Property => ((PropertyInfo)memberInfo).PropertyType == typeof(string),
            _ => false
        };

    public static bool IsGuid(this MemberInfo memberInfo) =>
        memberInfo.MemberType switch
        {
            MemberTypes.Field => ((FieldInfo)memberInfo).FieldType == typeof(Guid),
            MemberTypes.Property => ((PropertyInfo)memberInfo).PropertyType == typeof(Guid),
            _ => false
        };

    public static bool IsPrimitive(this MemberInfo memberInfo) =>
        memberInfo.MemberType switch
        {
            MemberTypes.Field => ((FieldInfo)memberInfo).FieldType.IsPrimitive,
            MemberTypes.Property => ((PropertyInfo)memberInfo).PropertyType.IsPrimitive,
            _ => false
        };
}