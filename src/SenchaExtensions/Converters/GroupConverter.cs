using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions
{
    public class GroupConverter : TypeConverter
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

                    return new Group()
                    {
                        Operations = new List<SortOperation>()
                        {
                            JsonConvert.DeserializeObject<SortOperation>((string)value)
                        }   
                    };
                }
                catch (Exception ex)
                {
                    //log ex

                    return null;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
