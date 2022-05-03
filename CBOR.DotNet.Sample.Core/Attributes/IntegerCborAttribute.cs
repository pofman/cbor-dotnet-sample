namespace CBOR.DotNet.Sample.Core.Attributes;

public class IntegerCborAttribute : CborAttribute
{
	public override void Accept(ICborAttributeVisitor visitor)
	{
		visitor.Visit(this);
	}
}
