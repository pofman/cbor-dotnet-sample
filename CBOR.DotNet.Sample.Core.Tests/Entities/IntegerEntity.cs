using CBOR.DotNet.Sample.Core.Attributes;

namespace CBOR.DotNet.Sample.Core.Tests.Entities
{
	public class IntegerEntity
	{
		[IntegerCbor]
		public int Id { get; set; }
	}
}