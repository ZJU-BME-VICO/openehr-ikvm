using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.parser;
using org.openehr.rm.datatypes.quantity.datetime;
using java.lang;
namespace openehr_net_ikvm.dadl_parser
{
    [TestClass]
    public class SimpleValuesTest :ParserTestBase
    {
        [TestMethod]
        public void testParseAndVerifySimpleValues()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\dadl-parser\src\test\resources\simple_values.dadl");
            DADLParser parser = new DADLParser(adl);
            ContentObject content = parser.parse();
            Assert.IsNotNull(content);
            Assert.IsNull(content.getComplexObjectBlock());
            Assert.IsNotNull(content.getAttributeValues());
            Assert.AreEqual(1, content.getAttributeValues().size());
            AttributeValue av = (AttributeValue)content.getAttributeValues().get(0);
            Assert.AreEqual("simple_values", av.getId());
            ObjectBlock ob = av.getValue();
            Assert.IsInstanceOfType(ob, typeof(SingleAttributeObjectBlock));
            SingleAttributeObjectBlock single = (SingleAttributeObjectBlock)ob;
            java.util.List values = single.getAttributeValues();
            Assert.AreEqual(9, values.size());
            assertDateTimeValue((AttributeValue)values.get(0), "2007-10-30T09:22:00");
            assertDateValue((AttributeValue)values.get(1), "2008-04-02");

            assertTimeValue((AttributeValue)values.get(2), "11:09:40");

            assertDurationValue((AttributeValue)values.get(3), "PT10M");

            assertStringValue((AttributeValue)values.get(4), "a string value");

            assertCharacterValue((AttributeValue)values.get(5), 'a');

            assertIntegerValue((AttributeValue)values.get(6), 100);

            assertRealValue((AttributeValue)values.get(7), 9.5);

            assertBooleanValue((AttributeValue)values.get(8), true);
        }
        private void assertDateTimeValue(AttributeValue attr, string value) {
		Assert.IsInstanceOfType(attr.getValue(),typeof(PrimitiveObjectBlock));
		PrimitiveObjectBlock pob = (PrimitiveObjectBlock) attr.getValue();
		Assert.IsInstanceOfType(pob.getSimpleValue(),typeof(DateTimeValue));
		DateTimeValue actual = (DateTimeValue) pob.getSimpleValue();
		DvDateTime expected = new DvDateTime(value.ToString());
		Assert.AreEqual(actual.getValue(), expected);
	}

        private void assertDateValue(AttributeValue attr, string value) {
            Assert.IsInstanceOfType(attr.getValue(), typeof(PrimitiveObjectBlock));
            PrimitiveObjectBlock pob = (PrimitiveObjectBlock)attr.getValue();
            Assert.IsInstanceOfType(pob.getSimpleValue(), typeof(DateValue));
		DateValue actual = (DateValue) pob.getSimpleValue();
		DvDate expected = new DvDate(value);
        Assert.AreEqual(actual.getValue(), expected);
	}

        private void assertTimeValue(AttributeValue attr, string value) {
            Assert.IsInstanceOfType(attr.getValue(), typeof(PrimitiveObjectBlock));
            PrimitiveObjectBlock pob = (PrimitiveObjectBlock)attr.getValue();
            Assert.IsInstanceOfType(pob.getSimpleValue(), typeof(TimeValue));
		TimeValue actual = (TimeValue) pob.getSimpleValue();
		DvTime expected = new DvTime(value);
        Assert.AreEqual(actual.getValue(), expected);
	}

        private void assertDurationValue(AttributeValue attr, string value) {
            Assert.IsInstanceOfType(attr.getValue(), typeof(PrimitiveObjectBlock));
            PrimitiveObjectBlock pob = (PrimitiveObjectBlock)attr.getValue();
            Assert.IsInstanceOfType(pob.getSimpleValue(), typeof(DurationValue));
		DurationValue actual = (DurationValue) pob.getSimpleValue();
		DvDuration expected = new DvDuration(value);
        Assert.AreEqual(actual.getValue(), expected);
	}

        private void assertStringValue(AttributeValue attr, string value) {
            Assert.IsInstanceOfType(attr.getValue(), typeof(PrimitiveObjectBlock));
            PrimitiveObjectBlock pob = (PrimitiveObjectBlock)attr.getValue();
            Assert.IsInstanceOfType(pob.getSimpleValue(), typeof(StringValue));
		StringValue str = (StringValue) pob.getSimpleValue();
        Assert.AreEqual(str.getValue(), value);
	}

        private void assertCharacterValue(AttributeValue attr, char value) {
            Assert.IsInstanceOfType(attr.getValue(), typeof(PrimitiveObjectBlock));
            PrimitiveObjectBlock pob = (PrimitiveObjectBlock)attr.getValue();
            Assert.IsInstanceOfType(pob.getSimpleValue(), typeof(CharacterValue));
		CharacterValue actual = (CharacterValue) pob.getSimpleValue();	
		Character expected = new Character(value);
		Assert.AreEqual(actual.getValue(), expected);
	}

        private void assertIntegerValue(AttributeValue attr, int value) {
            Assert.IsInstanceOfType(attr.getValue(), typeof(PrimitiveObjectBlock));
            PrimitiveObjectBlock pob = (PrimitiveObjectBlock)attr.getValue();
            Assert.IsInstanceOfType(pob.getSimpleValue(), typeof(IntegerValue));
		IntegerValue actual = (IntegerValue) pob.getSimpleValue();	
		Integer expected = new Integer(value);
		Assert.AreEqual(actual.getValue(), expected);
	}

        private void assertRealValue(AttributeValue attr, double value) {
            Assert.IsInstanceOfType(attr.getValue(), typeof(PrimitiveObjectBlock));
            PrimitiveObjectBlock pob = (PrimitiveObjectBlock)attr.getValue();
            Assert.IsInstanceOfType(pob.getSimpleValue(), typeof(RealValue));
            RealValue actual = (RealValue)pob.getSimpleValue();
            java.lang.Double expected = new java.lang.Double(value);
		Assert.AreEqual( actual.getValue(), expected);
	}

        private void assertBooleanValue(AttributeValue attr, bool value) {
            Assert.IsInstanceOfType(attr.getValue(), typeof(PrimitiveObjectBlock));
            PrimitiveObjectBlock pob = (PrimitiveObjectBlock)attr.getValue();
            Assert.IsInstanceOfType(pob.getSimpleValue(), typeof(BooleanValue));
            BooleanValue actual = (BooleanValue)pob.getSimpleValue();
            java.lang.Boolean expected = new java.lang.Boolean(value);
           Assert.AreEqual(actual.getValue(), expected);
	}
    }
}
