using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.archetype;
using org.openehr.am.parser;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class ArchetypeValidatorTest
    {

        [TestMethod]
        public void testCheckInternalReferences()
        {

            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-car.use_node.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            se.acode.openehr.parser.ArchetypeValidator validator = new se.acode.openehr.parser.ArchetypeValidator(archetype);
            java.util.Map expected = new java.util.HashMap();

            // wrong target path
            expected.put("/wheels[at0005]/parts",
                    "/engine[at0001]/parts[at0002]");

            // wrong type
            expected.put("/wheels[at0006]/parts",
                    "/wheels[at0001]/parts[at0002]");

            //Assert.AreEqual(expected, validator.checkInternalReferences());
        }
    }
}
