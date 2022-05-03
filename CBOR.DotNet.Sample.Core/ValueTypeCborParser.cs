using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core
{
	public class ValueTypeCborParser
	{
		public virtual void Write(CborWriter writer, object obj)
		{
			switch (obj)
			{
				case int i:
					writer.WriteInt32(i);
					break;
				case long l:
					writer.WriteInt64(l);
					break;
				case string s:
					writer.WriteTextString(s);
					break;
				case decimal d:
					writer.WriteDecimal(d);
					break;
				case double d:
					writer.WriteDouble(d);
					break;
				case null:
					writer.WriteNull();
					break;
				default:
					throw new System.ArgumentException("Unsupported type: " + obj.GetType().Name);
			}
		}

		public virtual object Decrypt(Type type, CborReader reader)
		{
			switch (type)
			{
				case var t when t == typeof(int):
					return reader.ReadInt32();
				case var t when t == typeof(long):
					return reader.ReadInt64();
				case var t when t == typeof(string):
					return reader.ReadTextString();
				case var t when t == typeof(decimal):
					return reader.ReadDecimal();
				case var t when t == typeof(double):
					return reader.ReadDouble();
				default:
					throw new System.ArgumentException("Unsupported type: " + type.Name);
			}
		}
	}
}