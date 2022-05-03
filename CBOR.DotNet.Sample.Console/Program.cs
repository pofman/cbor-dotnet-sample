using CBOR.DotNet.Sample.Core;
using CBOR.DotNet.Sample.Core.Attributes;
using Newtonsoft.Json;

namespace CBOR.DotNet.Sample.Console
{
	public class Program
	{
		private class MockObject
		{
			[DoubleCbor]
			public double TimeStamp { get; set; }
			[StringCbor]
			public string? Name { get; set; }
			[IntegerCbor]
			public int Id { get; set; }
		}
		static void Main(string[] args)
		{
			var obj = new MockObject
			{
				TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
				Name = "John Doe",
				Id = 42
			};

			var parser = new CborParser();
			var cbored = parser.Encrypt(obj);
			var unCbored = parser.Decrypt<MockObject>(cbored);
			
			System.Console.WriteLine(JsonConvert.SerializeObject(unCbored));
		}
	}
}
