using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class CCodePhraseTest
    {
        //[TestMethod]
        public CCodePhraseTest()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.c_code_phrase.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            archetype = parser.parse();
        }

        [TestMethod]
	    public void testParseExternalCodes()
        {
            node = archetype.node("/types[at0001]/items[at10002]/value");
		    String[] codes = { "F43.00", "F43.01", "F32.02" };
		    assertCCodePhrase(node, "icd10", codes, null);
	    }
	
        [TestMethod]
	    public void testParseExternalCodesWithAssumedValue()
        {
            node = archetype.node("/types[at0001]/items[at10005]/value");
		    String[] codes = { "F43.00", "F43.01", "F32.02" };
		    assertCCodePhrase(node, "icd10", codes, "F43.00");
	    }
	
	    [TestMethod]
        public void testParseLocalCodes()
        {
            node = archetype.node("/types[at0001]/items[at10003]/value");
		    String[] codeList = { "at1311","at1312", "at1313", "at1314","at1315" }; 
		    assertCCodePhrase(node, "local", codeList, null);
	    }
	    
        [TestMethod]
	    public void testParseEmptyCodeList()
        {
            node = archetype.node("/types[at0001]/items[at10004]/value");
		    String[] codeList = null; 
		    assertCCodePhrase(node, "icd10", codeList, null);
	    }

        private void assertCCodePhrase(org.openehr.am.archetype.constraintmodel.ArchetypeConstraint actual, 
			    String terminologyId, String[] codes, String assumedValue) 
        {	
		    // check type
            Assert.IsTrue(actual is org.openehr.am.openehrprofile.datatypes.text.CCodePhrase, "CCodePhrase expected, got " + actual.getClass());
            org.openehr.am.openehrprofile.datatypes.text.CCodePhrase cCodePhrase = (org.openehr.am.openehrprofile.datatypes.text.CCodePhrase)actual;
		
		    // check terminology
            Assert.AreEqual(terminologyId,
                    cCodePhrase.getTerminologyId().getValue(), "terminology");
		
		    // check code list
		    if(codes == null) 
            {
                Assert.AreEqual(null, cCodePhrase.getCodeList(), "codeList expected null");
		    } 
            else 
            {
                java.util.List codeList = cCodePhrase.getCodeList();
                Assert.AreEqual(codes.Length, codeList.size(), "codes.size wrong");
                for (int i = 0; i < codes.Length; i++)
                {
				    Object c = codeList.get(i);
                    Assert.AreEqual(codes[i], c, "code wrong, got: " + c);
			    }
		    }
		
		    // check assumed value
		    if(assumedValue == null) 
            {
			    Assert.IsFalse(cCodePhrase.hasAssumedValue());
		    } 
            else 
            {
                Assert.IsTrue(cCodePhrase.hasAssumedValue(), "expected assumedValue");
                org.openehr.rm.datatypes.text.CodePhrase temp = (org.openehr.rm.datatypes.text.CodePhrase)(cCodePhrase.getAssumedValue());
                Assert.AreEqual(assumedValue, temp.getCodeString(), "assumed value wrong");
		    }
	    }

        private org.openehr.am.archetype.Archetype archetype;
        private org.openehr.am.archetype.constraintmodel.ArchetypeConstraint node;
    }
}
