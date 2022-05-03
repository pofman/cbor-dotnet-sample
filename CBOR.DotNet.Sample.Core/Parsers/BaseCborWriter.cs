using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public abstract class BaseCborWriter
	{
		protected CborWriter Writer { get; }
		public BaseCborWriter(CborWriter writer)
		{
			Writer = writer;
		}

		public abstract void Write(object? obj);
	}
}