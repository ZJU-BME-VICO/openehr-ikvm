using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.parser;
namespace openehr_net_ikvm.dadl_parser
{
    [TestClass]
    public class EmptyAttributeListBlockTest
    {
        [TestMethod]
        public void testParseBlockWithEmptyAttrList()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\dadl-parser\src\test\resources\empty_attr_list.dadl");
            DADLParser parser = new DADLParser(adl);
            ContentObject obj = parser.parse();
            Assert.IsNotNull(obj);
            Assert.AreEqual("DESTINATION_PROFILE", obj.getComplexObjectBlock().getTypeIdentifier(), "type identifier missing");
            Assert.IsInstanceOfType(obj.getComplexObjectBlock(), typeof(SingleAttributeObjectBlock));
        }

        [TestMethod]
        public void testParseEmptyAttrListWithoutTypeIdentifier()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\dadl-parser\src\test\resources\empty_attr_list_without_type.dadl");
            DADLParser parser = new DADLParser(adl);
            ContentObject obj = parser.parse();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.getComplexObjectBlock().getTypeIdentifier());
            Assert.IsInstanceOfType(obj.getComplexObjectBlock(), typeof(SingleAttributeObjectBlock));
        }
    }
}
