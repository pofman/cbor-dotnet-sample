namespace CBOR.DotNet.Sample.Core.Attributes;

public class StringCborAttribute : CborAttribute
{
		public override void Accept(ICborAttributeVisitor visitor)
	{
		visitor.Visit(this);
	}
}
