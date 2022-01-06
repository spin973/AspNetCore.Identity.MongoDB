namespace AspNetCore.Identity.MongoDB.Utility;

public class ObjectIdConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }

        return base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        if (value is string id)
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                return objectId;
            }

            return default;
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        if (destinationType == typeof(string))
        {
            return true;
        }

        return base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is ObjectId objectId)
        {
            return objectId.ToString();
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
