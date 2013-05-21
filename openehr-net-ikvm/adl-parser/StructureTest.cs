using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.archetype;
using org.openehr.rm.support.basic;
using org.openehr.am.archetype.constraintmodel;
using java.util;
using org.openehr.am.archetype.constraintmodel.primitive;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class StructureTest : ParserTestBase
    {
        public StructureTest()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.structure_test1.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            //org.openehr.am.archetype.Archetype archetype = parser.parse();
            definition = parser.parse().getDefinition();
        }
        
        [TestMethod]
        public void testStructure()
        {
            // root object
            CComplexObject obj = definition;
            java.lang.Integer temp1 = new java.lang.Integer(1);
            Interval occurrences = new Interval(temp1, temp1);
            assertCComplexObject(obj, "ENTRY", "at0000", occurrences, 2);

            // first attribute of root object
            CAttribute attr = (CAttribute) obj.getAttributes().get(0);
            assertCAttribute(attr, "subject_relationship", 1);

            // 2nd level object
            obj = (CComplexObject) attr.getChildren().get(0);
            assertCComplexObject(obj, "RELATED_PARTY", null, occurrences, 1);

            // attribute of 2nd level object
            attr = (CAttribute) obj.getAttributes().get(0);
            assertCAttribute(attr, "relationship", 1);

            // leaf object
            obj = (CComplexObject) attr.getChildren().get(0);
            assertCComplexObject(obj, "TEXT", null, occurrences, 1);

            // attribute of leaf object
            attr = (CAttribute) obj.getAttributes().get(0);
            assertCAttribute(attr, "value", 1);

            // primitive constraint of leaf object
            CString str = (CString) ((CPrimitiveObject) attr.getChildren().get(0)).getItem();
            Assert.AreEqual(null, str.getPattern(), "pattern");
            Assert.AreEqual(1, str.getList().size(), "set.size");
            Assert.IsTrue(str.getList().contains("self"), "set has");
        }

        [TestMethod]
        public void testExistenceCardinalityAndOccurrences()
        {
            // second attribute of root object
            CAttribute attr = (CAttribute) definition.getAttributes().get(1);
            Cardinality card = new Cardinality(true, false, interval(0, 8));
            assertCAttribute(attr, "members", CAttribute.Existence.OPTIONAL, card, 2);

            // 1st PERSON
            CComplexObject obj = (CComplexObject) attr.getChildren().get(0);
            assertCComplexObject(obj, "PERSON", null, interval(1, 1), 1);

            // 2nd PERSON
            obj = (CComplexObject) attr.getChildren().get(1);
            assertCComplexObject(obj, "PERSON", null,
                    new Interval(new java.lang.Integer(0), null, true, false), 1);
        }
    
        [TestMethod]
        public void testParseCommentWithSlashChar()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.structure_test2.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            
            Assert.IsNotNull(archetype);        
        }

        [TestMethod]
        public void testParseCommentWithSlashCharAfterSlot()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\openEHR-EHR-CLUSTER.auscultation.v1.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();

            Assert.IsNotNull(archetype);    
        }

        private CComplexObject definition;
    }
}
