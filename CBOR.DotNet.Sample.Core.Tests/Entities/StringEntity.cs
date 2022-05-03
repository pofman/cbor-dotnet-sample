using CBOR.DotNet.Sample.Core.Attributes;

namespace CBOR.DotNet.Sample.Core.Tests.Entities
{
	public class StringEntity
	{
		[StringCbor]
		public string? Name { get; set; }

		public StringEntity()
		{
		}
	}
}