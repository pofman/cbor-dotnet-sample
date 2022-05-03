using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class IntegerReader : BaseCborReader<int>
	{
		public IntegerReader(CborReader reader)
			: base(reader)
		{
		}

		public override int Read()
		{
			return Reader.ReadInt32();
		}
	}
}