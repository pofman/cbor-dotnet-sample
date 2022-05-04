using System.Formats.Cbor;
using CBOR.DotNet.Sample.Support;

namespace CBOR.DotNet.Sample.Core
{
	public class CborParser
	{
		public virtual Task EncryptAsync<T>(T obj, Stream stream, CancellationToken token = default)
		{
			using ByteBufferWriter bufferWriter = new ByteBufferWriter();
			return bufferWriter.CopyToAsync(stream, token);
		}

		public virtual byte[] Encrypt<T> (T obj) where T : class
		{
			var type = typeof(T);
			return Encrypt(type, obj);
		}

		public virtual byte[] Encrypt(Type type, object obj)
		{
			var writer = new CborWriter(allowMultipleRootLevelValues: true);
			Write(writer, type, obj);
			return writer.Encode();
		}

		public virtual T Decrypt<T> (byte[] obj) where T : class, new()
		{
			return (T)Decrypt(typeof(T), new CborReader(obj, allowMultipleRootLevelValues: true));
		}

		internal virtual object Decrypt(Type type, CborReader reader)
		{
			if(type.IsValueType)
				return new ValueTypeCborParser().Decrypt(type, reader);
			else
				return new ObjectCborParser().Decrypt(type, reader);
		}

		internal virtual void Write(CborWriter writer, Type type, object obj)
		{
			if(type.IsValueType)
				new ValueTypeCborParser().Write(writer, obj);
			else
				new ObjectCborParser().Encrypt(writer, type, obj);
		}
	}
}