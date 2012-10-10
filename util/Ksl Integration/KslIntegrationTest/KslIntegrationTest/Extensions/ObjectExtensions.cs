using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace KslIntegrationTest.Extensions
{
	public static class ObjectExtensions
	{
		// Convert an object to a byte array
		public static byte[] ToByteArray(this Object obj)
		{
			if(obj == null)
				return null;
			BinaryFormatter bf = new BinaryFormatter();
			MemoryStream ms = new MemoryStream();
			bf.Serialize(ms, obj);
			return ms.ToArray();
		}

		// Convert a byte array to an Object
		public static T ToObject<T>(this byte[] arrBytes)
		{
			MemoryStream memStream = new MemoryStream();
			BinaryFormatter binForm = new BinaryFormatter();
			memStream.Write(arrBytes, 0, arrBytes.Length);
			memStream.Seek(0, SeekOrigin.Begin);
			var obj = (T)binForm.Deserialize(memStream);
			return obj;
		}
	}
}