using System;
using System.ComponentModel;

namespace GestaoClientes.Application.Utils
{
    public static class Utils
    {
        public static string GetDescricaoEnum(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes?.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }
    }
}
