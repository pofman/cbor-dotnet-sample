using System.Formats.Cbor;
using System.Reflection;
using CBOR.DotNet.Sample.Core.Attributes;

namespace CBOR.DotNet.Sample.Core
{
	public class ObjectCborParser
	{
		public virtual void Encrypt(CborWriter writer, Type type, object obj)
		{
			GetCborProperties(type)
				.ForEach(x => new CborWriterVisitor(writer, GetCborAttribute(x)).Write(x.GetValue(obj)));
		}

		public object Decrypt(Type type, CborReader reader)
		{
			var result = Activator.CreateInstance(type);
			if(result is null)
			 	throw new InvalidOperationException("Cannot create instance of type " + type.FullName);

			GetCborProperties(type)
				.ForEach(x => new CborReaderVisitor(reader, GetCborAttribute(x), x).ReadTo(result));

			return result;
		}

		private List<PropertyInfo> GetCborProperties(Type type)
		{
			return type.GetProperties().Where(x => x.GetCustomAttributes(typeof(CborAttribute), true).Length > 0).ToList();
		}

		private CborAttribute GetCborAttribute(PropertyInfo property)
		{
			return property.GetCustomAttributes(typeof(CborAttribute), true).Cast<CborAttribute>().First();
		}
	}
}