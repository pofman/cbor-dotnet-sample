using System.Formats.Cbor;
using CBOR.DotNet.Sample.Core.Attributes;
using CBOR.DotNet.Sample.Core.Parsers;

namespace CBOR.DotNet.Sample.Core
{
	public class CborWriterVisitor : ICborAttributeVisitor
	{
		private CborWriter Writer { get; }
		private CborAttribute CborAttribute { get; }
		private object? Value { get; set;}

		public CborWriterVisitor(CborWriter writer, CborAttribute attr)
		{
			Writer = writer;
			CborAttribute = attr;
		}		

		public void Write(object? value)
		{
			Value = value;
			CborAttribute.Accept(this);
		}

		void ICborAttributeVisitor.Visit(IntegerCborAttribute attr)
		{
			new IntegerWriter(Writer).Write(Value);
		}

		void ICborAttributeVisitor.Visit(StringCborAttribute attr)
		{
			new Parsers.StringWriter(Writer).Write(Value);
		}

		void ICborAttributeVisitor.Visit(ArrayCborAttribute attr)
		{
			new ListWriter(Writer).Write(Value);
		}

		void ICborAttributeVisitor.Visit(DecimalCborAttribute decimalAttr)
		{
			new DecimalWriter(Writer).Write(Value);
		}

		void ICborAttributeVisitor.Visit(DoubleCborAttribute doubleAttr)
		{
			new DoubleWriter(Writer).Write(Value);
		}

		void ICborAttributeVisitor.Visit(Integer64CborAttribute integer64Attr)
		{
			new Integer64Writer(Writer).Write(Value);
		}
	}
}