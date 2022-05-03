using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class DoubleReader : BaseCborReader<double>
	{
		public DoubleReader(CborReader reader)
			: base(reader)
		{
		}

		public override double Read()
		{
			return Reader.ReadDouble();
		}
	}
}