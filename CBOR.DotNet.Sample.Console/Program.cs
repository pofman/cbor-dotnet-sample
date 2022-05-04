using CBOR.DotNet.Sample.Core;
using CBOR.DotNet.Sample.Core.Attributes;
using Newtonsoft.Json;
using static CBOR.DotNet.Sample.Console.Person.Types;

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

			// Testing Google Protobuf
			var john = new Person
			{
				Id = 1234,
				Name = "John Doe",
				Email = "jdoe@example.com",
				Phones = { new PhoneNumber { Number = "555-4321", Type = PhoneType.Home } }
			};
			using var streamWrite = new MemoryStream();
			using var codedOutputStream = new Google.Protobuf.CodedOutputStream(streamWrite);
			john.WriteTo(codedOutputStream);
			codedOutputStream.Flush();

			var john2 = Person.Parser.ParseFrom(streamWrite.ToArray());
			System.Console.WriteLine(JsonConvert.SerializeObject(john2));
		}
	}
}
