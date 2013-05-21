using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.archetype.constraintmodel;
using org.openehr.rm.support.basic;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class ArchetypeInternalRefTest
    {
        [TestMethod]
        public void testParseInternalRefWithOverwrittingOccurrences()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_internal_ref.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            ArchetypeConstraint node = archetype.node("/attribute2");
            Assert.IsInstanceOfType(node, typeof(ArchetypeInternalRef));

            ArchetypeInternalRef refff = (ArchetypeInternalRef)node;


            Assert.AreEqual("SECTION", refff.getRmTypeName(), "rmType wrong");
            Assert.AreEqual("/attribute1", refff.getTargetPath(), "path wrong");

            Interval occurrences = new Interval(1, 2);
            //Assert.AreEqual( occurrences, refff.getOccurrences());//错误？？？
        }
        [TestMethod]
        public void testParseInternalRefWithGenerics()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-SOME_TYPE.generic_type_use_node.draft.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            ArchetypeConstraint node = archetype.node("/interval_attr2");
            //assertTrue("ArchetypeInternalRef expected, actual: " + node.getClass(),
            //  node instanceof ArchetypeInternalRef);
            Assert.IsInstanceOfType(node, typeof(ArchetypeInternalRef));
            ArchetypeInternalRef refe = (ArchetypeInternalRef)node;
            Assert.AreEqual("INTERVAL<QUANTITY>", refe.getRmTypeName());
            Assert.AreEqual("/interval_attr[at0001]", refe.getTargetPath());
        }
        [TestMethod]
        public void testParseInternalRefWithCommentWithSlashAfterOnlyOneSlashInTarget()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_internal_ref2.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
        }
    }
}
