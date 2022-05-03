using CBOR.DotNet.Sample.Core.Attributes;

namespace CBOR.DotNet.Sample.Core.Tests.Entities
{
	public class Integer64Entity
	{
		[Integer64Cbor]
		public long Id { get; set; }
	}
}