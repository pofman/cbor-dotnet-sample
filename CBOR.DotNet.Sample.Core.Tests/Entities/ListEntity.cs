using System.Collections.Generic;
using CBOR.DotNet.Sample.Core.Attributes;

namespace CBOR.DotNet.Sample.Core.Tests.Entities
{
	public class ListEntity
	{
		[ArrayCbor]
		public IList<IntegerEntity> Ids { get; set; }

		public ListEntity()
		{
			Ids = new List<IntegerEntity>();
		}
	}
}