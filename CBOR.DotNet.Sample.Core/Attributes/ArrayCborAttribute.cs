namespace CBOR.DotNet.Sample.Core.Attributes;

public class ArrayCborAttribute : CborAttribute
{
	public override void Accept(ICborAttributeVisitor visitor)
	{
		visitor.Visit(this);
	}
}
