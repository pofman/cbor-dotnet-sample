using System.Formats.Cbor;
using System.Reflection;
using CBOR.DotNet.Sample.Core.Attributes;
using CBOR.DotNet.Sample.Core.Parsers;

namespace CBOR.DotNet.Sample.Core
{
	public class CborReaderVisitor : ICborAttributeVisitor
	{
		private CborReader Reader { get; }
		private CborAttribute CborAttribute { get; }
		private PropertyInfo PropertyInfo { get; }
		private object? Object { get; set; }

		public CborReaderVisitor(CborReader reader, CborAttribute cborAttribute, PropertyInfo propertyInfo)
		{
			Reader = reader;
			CborAttribute = cborAttribute;
			PropertyInfo = propertyInfo;
		}

		public void ReadTo(object obj)
		{
			Object = obj;
			CborAttribute.Accept(this);
		}

		void ICborAttributeVisitor.Visit(IntegerCborAttribute attr)
		{
			Save(new IntegerReader(Reader).Read());
		}

		void ICborAttributeVisitor.Visit(StringCborAttribute attr)
		{
			Save(new Parsers.StringReader(Reader).Read());
		}

		void ICborAttributeVisitor.Visit(ArrayCborAttribute attr)
		{
			Save(new ListReader(Reader, PropertyInfo.PropertyType.GetGenericArguments()[0]).Read());
		}

		void ICborAttributeVisitor.Visit(DecimalCborAttribute decimalAttr)
		{
			Save(new DecimalReader(Reader).Read());
		}

		void ICborAttributeVisitor.Visit(DoubleCborAttribute doubleAttr)
		{
			Save(new DoubleReader(Reader).Read());
		}

		void ICborAttributeVisitor.Visit(Integer64CborAttribute integer64Attr)
		{
			Save(new Integer64Reader(Reader).Read());
		}

		private void Save(object value)
		{
			PropertyInfo.SetValue(Object, value);
		}
	}
}