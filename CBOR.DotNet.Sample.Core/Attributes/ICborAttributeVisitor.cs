namespace CBOR.DotNet.Sample.Core.Attributes
{
	public interface ICborAttributeVisitor
	{
		void Visit(IntegerCborAttribute integerAttr);
		void Visit(Integer64CborAttribute integer64Attr);
		void Visit(StringCborAttribute stringAttr);
		void Visit(ArrayCborAttribute arrayAttr);
		void Visit(DecimalCborAttribute decimalAttr);
		void Visit(DoubleCborAttribute doubleAttr);
	}
}