using System;
using System.Diagnostics;
using System.Text;
using CBOR.DotNet.Sample.Core.Tests.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace CBOR.DotNet.Sample.Core.Tests
{
	public class CborParserPerformanceTests
	{
		private readonly ITestOutputHelper output;

		public CborParserPerformanceTests(ITestOutputHelper output)
		{
			this.output = output;
		}

		[Fact]
		public void CborParser_EncryptSimpleEntity_ShouldTakeLessThan10MS()
		{
			// Arrange
			var entity = new ComplexEntity 
			{
				Id = 10,
				FirstName = "Juan",
				LastName = "Perez"
			};
			
			entity.Friends.Add(new IntegerEntity { Id = 1 });
			entity.Friends.Add(new IntegerEntity { Id = 2 });
			entity.Friends.Add(new IntegerEntity { Id = 3 });

			entity.HeightHistoryPerYear.Add(1.2);
			entity.HeightHistoryPerYear.Add(1.4);
			entity.HeightHistoryPerYear.Add(1.5);

			// Act
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			var bytes = new CborParser().Encrypt(entity);
			stopWatch.Stop();

			// Assert
			output.WriteLine($"Encrypt: {stopWatch.ElapsedMilliseconds} ms");
			stopWatch.ElapsedMilliseconds.Should().BeLessThan(10);
		}

		[Fact]
		public void CborParser_EncryptSimpleEntity_WhosFasterCborOrJSONSerialization()
		{
			// Arrange
			var entity = new ComplexEntity 
			{
				Id = 10,
				FirstName = "Juan",
				LastName = "Perez"
			};
			
			entity.Friends.Add(new IntegerEntity { Id = 1 });
			entity.Friends.Add(new IntegerEntity { Id = 2 });
			entity.Friends.Add(new IntegerEntity { Id = 3 });

			entity.HeightHistoryPerYear.Add(1.2);
			entity.HeightHistoryPerYear.Add(1.4);
			entity.HeightHistoryPerYear.Add(1.5);

			// Act
			var stopWatchCbor = new Stopwatch();
			stopWatchCbor.Start();
			var bytes = new CborParser().Encrypt(entity);
			stopWatchCbor.Stop();

			var stopWatchJson = new Stopwatch();
			stopWatchJson.Start();
			var json = JsonConvert.SerializeObject(entity);
			stopWatchJson.Stop();

			// Assert
			output.WriteLine($"Encrypt Cbor: {stopWatchCbor.ElapsedMilliseconds} ms");
			output.WriteLine($"Serialize JSON: {stopWatchJson.ElapsedMilliseconds} ms");
		}

		[Fact]
		public void CborParser_EncryptSimpleEntity_WhosHeavierCborOrJSONSerialization()
		{
			// Arrange
			var entity = new ComplexEntity 
			{
				Id = 10,
				FirstName = "Juan",
				LastName = "Perez"
			};
			
			entity.Friends.Add(new IntegerEntity { Id = 1 });
			entity.Friends.Add(new IntegerEntity { Id = 2 });
			entity.Friends.Add(new IntegerEntity { Id = 3 });

			entity.HeightHistoryPerYear.Add(1.2);
			entity.HeightHistoryPerYear.Add(1.4);
			entity.HeightHistoryPerYear.Add(1.5);

			// Act
			var bytes = new CborParser().Encrypt(entity);
			var json = JsonConvert.SerializeObject(entity);
			var jsonInBytes =  Encoding.UTF8.GetBytes(json);

			// Assert
			output.WriteLine($"Encrypt Cbor Size: {bytes.Length}");
			output.WriteLine($"Serialize JSON Size: {jsonInBytes.Length}");
			output.WriteLine($"JSON is {Math.Round((double)jsonInBytes.Length / (double)bytes.Length, 2)} times heavier than CBOR");
		}

		[Fact]
		public void CborParser_EncryptBiggerEntity_WhosFasterCborOrJson()
		{
			// Arrange
			var entity = new ComplexEntity 
			{
				Id = 10,
				FirstName = "Juan",
				LastName = "Perez"
			};	

			for (int i = 0; i < 5000; i++)
			{
				entity.Friends.Add(new IntegerEntity { Id = i });
				entity.HeightHistoryPerYear.Add(new Random(i).NextDouble());
			}

			// Act
			var stopWatchCbor = new Stopwatch();
			stopWatchCbor.Start();
			var bytes = new CborParser().Encrypt(entity);
			stopWatchCbor.Stop();

			var stopWatchJson = new Stopwatch();
			stopWatchJson.Start();
			var json = JsonConvert.SerializeObject(entity);
			stopWatchJson.Stop();

			output.WriteLine($"Encrypt Cbor: {stopWatchCbor.ElapsedMilliseconds} ms");
			output.WriteLine($"Serialize JSON: {stopWatchJson.ElapsedMilliseconds} ms");

			stopWatchCbor.Restart();
			var decryptedEntity = new CborParser().Decrypt<ComplexEntity>(bytes);
			stopWatchCbor.Stop();

			stopWatchJson.Restart();
			var decryptedEntityFromJson = JsonConvert.DeserializeObject<ComplexEntity>(json);
			stopWatchJson.Stop();

			output.WriteLine($"Decrypt Cbor: {stopWatchCbor.ElapsedMilliseconds} ms");
			output.WriteLine($"Deserialize JSON: {stopWatchJson.ElapsedMilliseconds} ms");
		}
	}
}