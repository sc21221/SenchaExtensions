using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Globalization;

namespace SenchaExtensions
{
    public class SortConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    value = value.ToString().Replace("\"", "'");

                    return new Sort()
                    {
                        Operations = JsonConvert.DeserializeObject<SortOperation[]>((string)value)
                    };
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
