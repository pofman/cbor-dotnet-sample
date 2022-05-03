using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class DoubleWriter : BaseCborWriter
	{
		public DoubleWriter(CborWriter writer)
			: base(writer)
		{
		}

		public override void Write(object? obj)
		{
			Writer.WriteDouble(Convert.ToDouble(obj));
		}
	}
}