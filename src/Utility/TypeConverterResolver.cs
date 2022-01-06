namespace AspNetCore.Identity.MongoDB.Utility;

internal static class TypeConverterResolver
{
    internal static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
    {
        TypeDescriptor.AddAttributes(typeof(T), new Attribute[1] { new TypeConverterAttribute(typeof(TC)) });
    }
}
