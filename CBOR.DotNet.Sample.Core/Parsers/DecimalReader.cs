using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class DecimalReader : BaseCborReader<decimal>
	{
		public DecimalReader(CborReader reader)
			: base(reader)
		{
		}

		public override decimal Read()
		{
			return Reader.ReadDecimal();
		}
	}
}