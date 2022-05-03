using System.Formats.Cbor;

namespace CBOR.DotNet.Sample.Core.Parsers
{
	public class StringWriter : BaseCborWriter
	{
		public StringWriter(CborWriter writer)
			: base(writer)
		{
		}

		public override void Write(object? obj)
		{
			if(obj is null || obj.ToString() is null)
				Writer.WriteNull();
			else
			{
				string? str = obj.ToString();
				if(str is not null)
					Writer.WriteTextString(str);
				else 
					Writer.WriteNull();
			}
		}
	}
}