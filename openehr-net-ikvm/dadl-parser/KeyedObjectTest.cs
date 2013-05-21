using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.parser;
namespace openehr_net_ikvm.dadl_parser
{
    [TestClass]
    public class KeyedObjectTest
    {
        private void assertKeyedObject(KeyedObject ko, int key,
            String attribute, String value) {
		SimpleValue keyValue = ko.getKey();
		Assert.IsInstanceOfType( keyValue, typeof(IntegerValue));
		IntegerValue iv = (IntegerValue) keyValue;
		 //Assert.AreEqual(iv.getValue(), key);
		
		ObjectBlock ob = ko.getObject();
		 Assert.IsInstanceOfType(ob, typeof(SingleAttributeObjectBlock));
		SingleAttributeObjectBlock saob = (SingleAttributeObjectBlock) ob;
		java.util.List attributes = saob.getAttributeValues();
		 Assert.AreEqual(attributes.size(), 1);
		AttributeValue av = (AttributeValue)attributes.get(0);
		 Assert.AreEqual( av.getId(), attribute);
		
		ob = av.getValue();
		Assert.IsInstanceOfType(ob, typeof(PrimitiveObjectBlock));		
		SimpleValue sv = ((PrimitiveObjectBlock) ob).getSimpleValue();
		Assert.IsInstanceOfType(sv, typeof(StringValue));
		StringValue str = (StringValue) sv;
		 Assert.AreEqual( str.getValue(), value);
	}
        [TestMethod]
        public void testParseAndVerifySimpleValues()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\dadl-parser\src\test\resources\keyed_objects.dadl");
            DADLParser parser = new DADLParser(adl);
            ContentObject content = parser.parse();
            Assert.IsNotNull(content);
            Assert.IsNull(content.getComplexObjectBlock());
            Assert.IsNotNull(content.getAttributeValues());
            Assert.AreEqual(1, content.getAttributeValues().size());
            AttributeValue av = (AttributeValue)content.getAttributeValues().get(0);
            ObjectBlock ob = av.getValue();
            Assert.IsInstanceOfType(ob, typeof(MultipleAttributeObjectBlock));
            MultipleAttributeObjectBlock maob = (MultipleAttributeObjectBlock)ob;
             java.util.List keyedObjects = maob.getKeyObjects();
            assertKeyedObject((KeyedObject)keyedObjects.get(0), 1, "name", "systolic");
            assertKeyedObject((KeyedObject)keyedObjects.get(1), 2, "name", "diastolic");
        }
    }
}
