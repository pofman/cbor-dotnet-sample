namespace CBOR.DotNet.Sample.Core.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public abstract class CborAttribute : Attribute
{
	public CborAttribute()
	{
	}

	public abstract void Accept(ICborAttributeVisitor visitor);
}
