namespace CBOR.DotNet.Sample.Core.Attributes;

public class DecimalCborAttribute : CborAttribute
{
	public override void Accept(ICborAttributeVisitor visitor)
	{
		visitor.Visit(this);
	}
}
