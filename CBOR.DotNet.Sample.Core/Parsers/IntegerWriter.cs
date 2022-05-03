using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class IntegerWriter : BaseCborWriter
	{
		public IntegerWriter(CborWriter writer)
			: base(writer)
		{
		}

		public override void Write(object? obj)
		{
			Writer.WriteInt32(Convert.ToInt32(obj));
		}
	}
}