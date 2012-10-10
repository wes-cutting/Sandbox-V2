using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using System.IO;

namespace Rentler
{
    public static class SerializerExtensions
    {
        public static byte[] ToBinaryArray<T>(this T o)
        {
            if (o == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<T>(ms, o);
                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] item)
        {
            if (item == null)
                return default(T);

            using (MemoryStream ms = new MemoryStream(item))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }
    }
}
