using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class StringReader : BaseCborReader<string>
	{
		public StringReader(CborReader reader)
			: base(reader)
		{
		}

		public override string Read()
		{
			return Reader.ReadTextString();
		}
	}
}