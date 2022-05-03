using System.Collections;
using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class ListReader : BaseCborReader<ICollection>
	{
		private Type CollectionType { get; }
		private Type ResultType { get; }
		public ListReader(CborReader reader, Type collectionType)
			: base(reader)
		{
			CollectionType = collectionType;
			ResultType = typeof(List<>).MakeGenericType(CollectionType);
		}

		public override ICollection Read()
		{
			var result = Activator.CreateInstance(ResultType) as ICollection;
			if(result is null)
				throw new InvalidOperationException("Cannot create instance of type " + ResultType.FullName);

			var length = Reader.ReadStartArray();
			for(var i = 0; i < length; i++)
			{
				var item = new CborParser().Decrypt(CollectionType, Reader);
				ResultType.GetMethod("Add")?.Invoke(result, new object[] { item });
			}

			Reader.ReadEndArray();
			return result;
		}
	}
}