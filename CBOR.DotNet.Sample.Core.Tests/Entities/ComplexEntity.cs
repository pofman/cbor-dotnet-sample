using System.Collections.Generic;
using CBOR.DotNet.Sample.Core.Attributes;

namespace CBOR.DotNet.Sample.Core.Tests.Entities
{
	public class ComplexEntity
	{
		[IntegerCbor]
		public int Id { get; set; }
		[StringCbor]
		public string? FirstName { get; set; }
		[StringCbor]
		public string? LastName { get; set; }
		public string? FullName => string.Format("{0} {1}", FirstName, LastName);
		[ArrayCbor]
		public IList<IntegerEntity> Friends { get; set; }
		[ArrayCbor]
		public IList<double> HeightHistoryPerYear { get; set; }

		public ComplexEntity()
		{
			Friends = new List<IntegerEntity>();
			HeightHistoryPerYear = new List<double>();
		}
	}
}