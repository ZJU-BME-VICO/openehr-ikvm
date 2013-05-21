using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.archetype;
using org.openehr.am.openehrprofile.datatypes.text;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class DvCodedTextTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-composition.dv_coded_text.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            org.openehr.am.archetype.constraintmodel.ArchetypeConstraint node = archetype.node("/category/defining_code");
            //assertTrue("CCodePhrase expected, but got " + node.getClass(),
            //        node instanceof CCodePhrase);
            Assert.IsInstanceOfType(node, typeof(CCodePhrase));
           CCodePhrase ccp = (CCodePhrase)node;
            Assert.AreEqual( ccp.getTerminologyId().toString(), "openehr","terminologyId wrong");
            Assert.AreEqual(ccp.getCodeList().get(0), "431", "codeString wrong");
        }
    }
}
