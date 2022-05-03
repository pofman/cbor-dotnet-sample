namespace CBOR.DotNet.Sample.Core.Attributes;

public class Integer64CborAttribute : CborAttribute
{
	public override void Accept(ICborAttributeVisitor visitor)
	{
		visitor.Visit(this);
	}
}
