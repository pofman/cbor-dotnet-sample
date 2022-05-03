using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class Integer64Writer : BaseCborWriter
	{
		public Integer64Writer(CborWriter writer)
			: base(writer)
		{
		}

		public override void Write(object? obj)
		{
			Writer.WriteInt64(Convert.ToInt64(obj));
		}
	}
}