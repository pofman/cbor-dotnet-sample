namespace CBOR.DotNet.Sample.Core.Attributes;

public class DoubleCborAttribute : CborAttribute
{
		public override void Accept(ICborAttributeVisitor visitor)
	{
		visitor.Visit(this);
	}
}
