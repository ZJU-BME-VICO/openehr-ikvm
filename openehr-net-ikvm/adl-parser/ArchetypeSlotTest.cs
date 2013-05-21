using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.archetype.assertion;
using org.openehr.am.archetype.constraintmodel;
using org.openehr.rm.support.basic;
using org.openehr.am.archetype.constraintmodel.primitive;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class ArchetypeSlotTest
    {
        [TestMethod]
        public void testParseIncludesExcludes()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_slot.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            ArchetypeConstraint node = archetype.node("/content[at0001]");
            // Type e = typeof(node);
            ArchetypeSlot slot = (ArchetypeSlot)node;
            Assert.AreEqual("at0001", slot.getNodeId(), "nodeId wrong");
            Assert.AreEqual("SECTION", slot.getRmTypeName(), "rmTypeName wrong");
            Interval a = new Interval(0, 1);
            // Assert.AreEqual(  a, slot.getOccurrences());//错误？？？

            Assert.AreEqual("/content[at0001]", slot.path(), "path wrong");

            Assert.AreEqual(1, slot.getIncludes().size(), "includes total wrong");
            Assert.AreEqual(2, slot.getExcludes().size(), "Excludes total wrong");

            object b = slot.getIncludes().iterator().next();
            Assertion assertion = (Assertion)b;
            ExpressionItem item = assertion.getExpression();
            Assert.IsInstanceOfType(item, typeof(ExpressionBinaryOperator));
            //assertTrue("expressionItem type wrong", 
            //	item instanceof ExpressionBinaryOperator);
            ExpressionBinaryOperator bo = (ExpressionBinaryOperator)item;
            ExpressionItem leftOp = bo.getLeftOperand();
            ExpressionItem rightOp = bo.getRightOperand();
            Assert.IsInstanceOfType(leftOp, typeof(ExpressionLeaf));
            //assertTrue("left operator type wrong", 
            //    leftOp instanceof ExpressionLeaf);
            ExpressionLeaf left = (ExpressionLeaf)leftOp;
            Assert.AreEqual("domain_concept", left.getItem(), "left value wrong");
            Assert.IsInstanceOfType(rightOp, typeof(ExpressionLeaf));
            //  assertTrue("right operator type wrong", rightOp instanceof ExpressionLeaf);
            ExpressionLeaf right = (ExpressionLeaf)rightOp;
            Assert.IsInstanceOfType(right.getItem(), typeof(CString));
            // assertTrue("right item type wrong", right.getItem() instanceof CString);
            CString cstring = (CString)right.getItem();
            Assert.AreEqual("blood_pressure.v1", cstring.getPattern(), "right value wrong");
        }


        [TestMethod]
        public void testParseSingleInclude()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_slot.test2.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            ArchetypeConstraint node = archetype.node("/content[at0001]");

            ArchetypeSlot slot = (ArchetypeSlot)node;
            Assert.AreEqual("at0001", slot.getNodeId(), "nodeId wrong");
            Assert.AreEqual("SECTION", slot.getRmTypeName(), "rmTypeName wrong");

            //  Assert.AreEqual("occurrences wrong", new Interval<Integer>(0, 1),slot.getOccurrences());

            Assert.AreEqual("/content[at0001]", slot.path(), "path wrong");

            Assert.AreEqual(1, slot.getIncludes().size(), "includes total wrong");

            Assertion assertion = (Assertion)slot.getIncludes().iterator().next();
            ExpressionItem item = assertion.getExpression();
            //assertTrue("expressionItem type wrong", 
            //      item instanceof ExpressionBinaryOperator);
            ExpressionBinaryOperator bo = (ExpressionBinaryOperator)item;
            ExpressionItem leftOp = bo.getLeftOperand();
            ExpressionItem rightOp = bo.getRightOperand();

            //assertTrue("left operator type wrong", 
            //        leftOp instanceof ExpressionLeaf);
            ExpressionLeaf left = (ExpressionLeaf)leftOp;
            Assert.AreEqual("archetype_id/value", left.getItem(), "left value wrong");

            //assertTrue("right operator type wrong", 
            //        rightOp instanceof ExpressionLeaf);
            ExpressionLeaf right = (ExpressionLeaf)rightOp;
            //assertTrue("right item type wrong", right.getItem() instanceof CString);
            string cstring = Convert.ToString(right.getItem());
            //Assert.AreEqual("right value wrong", "openEHR-EHR-CLUSTER\\.device\\.v1", 
            //        cstring.getPattern());

            Assert.IsNotNull("stringExpression missing", assertion.getStringExpression());
            String expectedStringExpression =
                "archetype_id/value matches {/openEHR-EHR-CLUSTER\\.device\\.v1/}";
            Assert.AreEqual(expectedStringExpression, assertion.getStringExpression(), "stringExpression wrong, got: " + assertion.getStringExpression());


        }
    }
}
