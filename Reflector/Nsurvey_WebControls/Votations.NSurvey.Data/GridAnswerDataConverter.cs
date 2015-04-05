namespace Votations.NSurvey.Data
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    public class GridAnswerDataConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
        {
            return ((destType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destType));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object val, Type destType)
        {
            if (destType == typeof(InstanceDescriptor))
            {
                return new InstanceDescriptor(val.GetType().GetConstructor(Type.EmptyTypes), null, false);
            }
            return base.ConvertTo(context, culture, val, destType);
        }
    }
}

