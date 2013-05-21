using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.parser;
namespace openehr_net_ikvm.dadl_parser
{
    [TestClass]
    public class ItemListTest
    {
        [TestMethod]
        public void testParseBlockWithEmptyAttrList()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\dadl-parser\src\test\resources\state_item_list.dadl");
            DADLParser parser = new DADLParser(adl);
            ContentObject obj = parser.parse();
            AttributeValue av =(AttributeValue)obj.getAttributeValues().get(0);
            Assert.AreEqual("state", av.getId());
            Assert.IsInstanceOfType(av.getValue(),typeof(SingleAttributeObjectBlock));
            SingleAttributeObjectBlock saob = (SingleAttributeObjectBlock)av.getValue();
            Assert.AreEqual(3, saob.getAttributeValues().size());
            av = (AttributeValue)saob.getAttributeValues().get(2);
            Assert.AreEqual("items", av.getId());
            ObjectBlock ob = av.getValue();
            Assert.IsInstanceOfType(ob, typeof(SingleAttributeObjectBlock));
            saob = (SingleAttributeObjectBlock)ob;
            Assert.IsTrue(saob.getAttributeValues().isEmpty());
        }
    }
}
