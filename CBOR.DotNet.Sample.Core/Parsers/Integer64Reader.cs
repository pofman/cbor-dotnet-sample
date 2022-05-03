using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class Integer64Reader : BaseCborReader<long>
	{
		public Integer64Reader(CborReader reader)
			: base(reader)
		{
		}

		public override long Read()
		{
			return Reader.ReadInt64();
		}
	}
}