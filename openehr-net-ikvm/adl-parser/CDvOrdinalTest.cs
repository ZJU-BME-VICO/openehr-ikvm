using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class CDvOrdinalTest
    {
        //[TestMethod]
        public CDvOrdinalTest()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.c_dv_ordinal.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            archetype = parser.parse();
        }

        [TestMethod]
        public void testCDvOrdinalWithoutAssumedValue()
        {
            node = archetype.node("/types[at0001]/items[at10001]/value");
            String[] codes = {
                "at0003.0", "at0003.1", "at0003.2", "at0003.3", "at0003.4"
            };
            String terminology = "local";
        
            Assert.IsFalse(((org.openehr.am.openehrprofile.datatypes.quantity.CDvOrdinal) node).hasAssumedValue(), "unexpected assumed value");       
        
            assertCDvOrdinal(node, terminology, codes, null);
        }

        [TestMethod]
        public void testCDvOrdinalWithAssumedValue()
        {
            node = archetype.node("/types[at0001]/items[at10002]/value");
            String[] codes = {
                "at0003.0", "at0003.1", "at0003.2", "at0003.3", "at0003.4"
            };
            String terminology = "local";
            org.openehr.am.openehrprofile.datatypes.quantity.Ordinal assumed = new org.openehr.am.openehrprofile.datatypes.quantity.Ordinal(0, new org.openehr.rm.datatypes.text.CodePhrase(terminology, codes[0]));     
        
            Assert.IsTrue(((org.openehr.am.openehrprofile.datatypes.quantity.CDvOrdinal) node).hasAssumedValue(), "expected to have assumed value");
        
            assertCDvOrdinal(node, terminology, codes, assumed);
        }

        [TestMethod]
        public void testEmptyCDvOrdinal()
        {
    	    node = archetype.node("/types[at0001]/items[at10003]/value");
            Assert.IsTrue(node is org.openehr.am.openehrprofile.datatypes.quantity.CDvOrdinal, "CDvOrdinal expected");
            org.openehr.am.openehrprofile.datatypes.quantity.CDvOrdinal cordinal = (org.openehr.am.openehrprofile.datatypes.quantity.CDvOrdinal)node;
            Assert.IsTrue(cordinal.isAnyAllowed());
        }
    
        private void assertCDvOrdinal(org.openehr.am.archetype.constraintmodel.ArchetypeConstraint node, String terminoloy,
    		    String[] codes,	org.openehr.am.openehrprofile.datatypes.quantity.Ordinal assumedValue) {
    	
    	    Assert.IsTrue(node is org.openehr.am.openehrprofile.datatypes.quantity.CDvOrdinal, "CDvOrdinal expected");
            org.openehr.am.openehrprofile.datatypes.quantity.CDvOrdinal cordinal = (org.openehr.am.openehrprofile.datatypes.quantity.CDvOrdinal) node;
        
            java.util.List codeList = java.util.Arrays.asList(codes);
            java.util.Set list = cordinal.getList();
            Assert.AreEqual(codes.Length, list.size(), "codes.size");
            for(java.util.Iterator it = list.iterator(); it.hasNext();)
            {
                org.openehr.am.openehrprofile.datatypes.quantity.Ordinal ordinal = (org.openehr.am.openehrprofile.datatypes.quantity.Ordinal)it.next();
     
                Assert.AreEqual("local", ordinal.getSymbol().getTerminologyId().getValue(), "terminology");
                Assert.IsTrue(codeList.contains(ordinal.getSymbol().getCodeString()), "code missing");
            }
            Assert.AreEqual(assumedValue, cordinal.getAssumedValue(), "assumedValue wrong");        
        }

        private org.openehr.am.archetype.Archetype archetype;
        private org.openehr.am.archetype.constraintmodel.ArchetypeConstraint node;
    }
}
