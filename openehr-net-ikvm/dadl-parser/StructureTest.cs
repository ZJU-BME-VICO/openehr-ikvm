using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.parser;
namespace openehr_net_ikvm.dadl_parser
{
    [TestClass]
    public class StructureTest
    {
        [TestMethod]
        public void testParseSimpleDADL()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\dadl-parser\src\test\resources\blood_pressure_001.dadl");
            DADLParser parser = new DADLParser(adl);
            ContentObject content = parser.parse();
            Assert.IsNotNull(content);
        }
        [TestMethod]
        public void testTypedObjectWithKeyedAttributes()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\dadl-parser\src\test\resources\person_001.dadl");
            DADLParser parser = new DADLParser(adl);
            ContentObject content = parser.parse();
            Assert.IsNotNull(content);
        }
    }
}
