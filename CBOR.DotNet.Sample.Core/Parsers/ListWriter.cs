using System.Collections;
using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class ListWriter : BaseCborWriter
	{
		public ListWriter(CborWriter Writer)
			: base(Writer)
		{
		}

		public override void Write(object? obj)
		{
			if(obj == null || !(typeof(ICollection).IsAssignableFrom(obj.GetType())))
				return;

			var collection = (ICollection)obj;
			Writer.WriteStartArray(collection.Count);
			var parser = new CborParser();

			foreach (var item in collection)
			{
				parser.Write(Writer, item.GetType(), item);
			}

			Writer.WriteEndArray();
		}
	}
}