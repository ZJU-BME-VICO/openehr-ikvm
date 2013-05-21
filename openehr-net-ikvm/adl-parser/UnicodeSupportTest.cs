using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class UnicodeSupportTest
    {
        //[TestMethod]
        public UnicodeSupportTest()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.unicode_support.test.adl", System.Text.Encoding.GetEncoding("UTF-8"));
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            archetype = parser.parse();
        }

        [TestMethod]
	    public void testParse() 
        {
		    Assert.IsNotNull(archetype, "parsing failed");
	    }

	    /**
	     * Tests parsing of Chinese text in the ADL file
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testParsingWithChineseText()
        {
		    org.openehr.am.archetype.ontology.ArchetypeTerm term = archetype.getOntology().termDefinition("zh", "at0000");
		    Assert.IsNotNull(term, "definition in zh not found");

		    // "\u6982\u5ff5" is "concept" in Chinese in escaped unicode format 
		    Assert.AreEqual("\u6982\u5ff5", term.getItem("text"), "concept text wrong");

		    // "\u63cf\u8ff0" is "description" in Chinese in escaped unicode format 
		    Assert.AreEqual("\u63cf\u8ff0", term.getItem("description"), "concept description wrong");
	    }

	    /**
	     * Tests parsing of Swedish text in the ADL file
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testParsingWithSwedishText()
        {
            org.openehr.am.archetype.ontology.ArchetypeTerm term = archetype.getOntology().termDefinition("sv", "at0000");
            Assert.IsNotNull(term, "definition in sv not found");

		    // "spr\u00e5k" is "language" in Swedish in escaped unicode format 
            Assert.AreEqual("spr\u00e5k", term.getItem("text"), "concept text wrong");

		    // "Hj\u00e4lp" is "help" in Swedish in escaped unicode format 
		    Assert.AreEqual("Hj\u00e4lp",
                    term.getItem("description"), "concept description wrong");
	    }

        private org.openehr.am.archetype.Archetype archetype;
    }
}
