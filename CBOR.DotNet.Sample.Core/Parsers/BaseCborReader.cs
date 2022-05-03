using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public abstract class BaseCborReader<T>
	{
		protected CborReader Reader { get; }
		public BaseCborReader(CborReader reader)
		{
			Reader = reader;
		}

		public abstract T Read();
	}
}