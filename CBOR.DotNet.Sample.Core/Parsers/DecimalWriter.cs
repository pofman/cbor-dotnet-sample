using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class DecimalWriter : BaseCborWriter
	{
		public DecimalWriter(CborWriter writer)
			: base(writer)
		{
		}

		public override void Write(object? obj)
		{
			Writer.WriteDecimal(Convert.ToDecimal(obj));
		}
	}
}