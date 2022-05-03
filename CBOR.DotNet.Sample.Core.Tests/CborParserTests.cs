using CBOR.DotNet.Sample.Core.Tests.Entities;
using FluentAssertions;
using Xunit;

namespace CBOR.DotNet.Sample.Core.Tests;

public class CborParserTests
{
	[Fact]
	public void CborParser_ParseEntityWithOneIntegerProperty_ShouldReturnValidByteArray()
	{
		// Arrange
		var entity = new IntegerEntity { Id = 1 };

		// Act
		var bytes = new CborParser().Encrypt(entity);

		// Assert
		bytes.Should()
			.HaveCount(1)
			.And
			.Contain(1);
	}

	[Fact]
	public void CborParser_ReadEntityWithOneIntegerProperty_ShouldReturnValidIntegerEntity()
	{
		// Arrange
		var byteEntity = new byte[] { 1 };

		// Act
		var entity = new CborParser().Decrypt<IntegerEntity>(byteEntity);

		// Assert
		entity.Id.Should().Be(1);
	}

	[Fact]
	public void CborParser_ParseEntityWithOneInteger64Property_ShouldReturnValidByteArray()
	{
		// Arrange
		var entity = new Integer64Entity { Id = 1651614656252 };

		// Act
		var bytes = new CborParser().Encrypt(entity);

		// Assert
		bytes.Should()
			.HaveCount(9)
			.And
			.ContainInOrder(new byte[] { 27, 0, 0, 1, 128, 139, 231, 166, 252 });
	}

	[Fact]
	public void CborParser_ReadEntityWithOneInteger64Property_ShouldReturnValidInteger64Entity()
	{
		// Arrange
		var byteEntity = new byte[] { 27, 0, 0, 1, 128, 139, 231, 166, 252 };

		// Act
		var entity = new CborParser().Decrypt<Integer64Entity>(byteEntity);

		// Assert
		entity.Id.Should().Be(1651614656252);
	}

	[Fact]
	public void CborParser_ParseEntityWithOneStringProperty_ShouldReturnValidByteArray()
	{
		// Arrange
		var entity = new StringEntity() { Name = "Sample string" };

		// Act
		var bytes = new CborParser().Encrypt(entity);

		// Assert
		bytes.Should()
			.HaveCount(14)
			.And
			.ContainInOrder(new byte[] { 109, 83, 97, 109, 112, 108, 101, 32, 115, 116, 114, 105, 110, 103 });
	}

	[Fact]
	public void CborParser_ParseEntityWithOneNullStringProperty_ShouldReturnValidByteArray()
	{
		// Arrange
		var entity = new StringEntity();

		// Act
		var bytes = new CborParser().Encrypt(entity);

		// Assert
		bytes.Should()
			.HaveCount(1)
			.And
			.OnlyContain(x => x == 246);
	}

	[Fact]
	public void CborParser_ReadEntityWithOneStringProperty_ShouldReturnValidStringEntity()
	{
		// Arrange
		var byteEntity = new byte[] { 109, 83, 97, 109, 112, 108, 101, 32, 115, 116, 114, 105, 110, 103 };

		// Act
		var entity = new CborParser().Decrypt<StringEntity>(byteEntity);

		// Assert
		entity.Name.Should().Be("Sample string");
	}

	[Fact]
	public void CborParser_ParseEntityWithOneListPropertyOfIntegerEntity_ShouldReturnValidByteArray()
	{
		// Arrange
		var entity = new ListEntity();
		entity.Ids.Add(new IntegerEntity{ Id = 2 });

		// Act
		var bytes = new CborParser().Encrypt(entity);

		// Assert
		bytes.Should()
			.HaveCount(2)
			.And
			.ContainInOrder(new byte[] { 129, 2 });
	}

	[Fact]
	public void CborParser_ReadEntityWithOneListPropertyOfIntegerEntity_ShouldReturnValidListEntity()
	{
		// Arrange
		var byteEntity = new byte[] { 129 , 2};

		// Act
		var entity = new CborParser().Decrypt<ListEntity>(byteEntity);

		// Assert
		entity.Ids.Should()
			.HaveCount(1)
			.And
			.OnlyContain(x => x.Id == 2);
	}

	[Fact]
	public void CborParser_ParseComplexEntity_ShouldReturnValidByteArray()
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

		// Assert
		bytes.Should()
			.HaveCount(38)
			.And
			.ContainInOrder(new byte[] { 10, 100, 74, 117, 97, 110, 101, 80, 101, 114, 101, 122, 131, 1, 2, 3, 131, 251, 63, 243, 51, 51, 51, 51, 51, 51, 251, 63, 246, 102, 102, 102, 102, 102, 102, 249, 62, 0});
	}

	[Fact]
	public void CborParser_ReadComplexEntity_ShouldReturnValidComplexEntity()
	{
		// Arrange
		var bytes = new byte[] { 10, 100, 74, 117, 97, 110, 101, 80, 101, 114, 101, 122, 131, 1, 2, 3, 131, 251, 63, 243, 51, 51, 51, 51, 51, 51, 251, 63, 246, 102, 102, 102, 102, 102, 102, 249, 62, 0};

		// Act
		var result = new CborParser().Decrypt<ComplexEntity>(bytes);

		// Assert
		result.Id.Should().Be(10);
		result.FirstName.Should().Be("Juan");
		result.LastName.Should().Be("Perez");

		result.Friends.Should().HaveCount(3);
		result.Friends.Should().OnlyContain(x => x.Id == 1 || x.Id == 2 || x.Id == 3);
		result.HeightHistoryPerYear.Should().HaveCount(3);
		result.HeightHistoryPerYear.Should().OnlyContain(x => x == 1.2 || x == 1.4 || x == 1.5);
	}
}