using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class TermBindingTest
    {
        [TestMethod]
        public void testTermBindingWithMultiTerminologies()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.term_binding.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();

            // verify the first term binding
            org.openehr.am.archetype.ontology.OntologyBinding binding = (org.openehr.am.archetype.ontology.OntologyBinding)archetype.getOntology().getTermBindingList().get(0);
            Assert.AreEqual("SNOMED_CT", binding.getTerminology(), "wrong binding terminology");

            org.openehr.am.archetype.ontology.TermBindingItem item = (org.openehr.am.archetype.ontology.TermBindingItem) binding.getBindingList().get(0);

            Assert.AreEqual("at0000", item.getCode(), "wrong local code");
            Assert.AreEqual(1, item.getTerms().size(), "wrong terms size");
            Assert.AreEqual("[snomed_ct::1000339]", item.getTerms().get(0), "wrong term");

            // verify the second term binding
            binding = (org.openehr.am.archetype.ontology.OntologyBinding)archetype.getOntology().getTermBindingList().get(1);
            Assert.AreEqual("ICD10", binding.getTerminology(), "wrong binding terminology");

            item = (org.openehr.am.archetype.ontology.TermBindingItem) binding.getBindingList().get(0);

            Assert.AreEqual("at0000", item.getCode(), "wrong local code");
            Assert.AreEqual(2, item.getTerms().size(), "wrong terms size");
            Assert.AreEqual("[icd10::1000]", item.getTerms().get(0), "wrong 1st term");
            Assert.AreEqual("[icd10::1001]", item.getTerms().get(1), "wrong 2nd term");
        }

        [TestMethod]
        public void testPathBasedBinding()
        {
    	    string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.term_binding2.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            
    	    org.openehr.am.archetype.ontology.OntologyBinding binding = (org.openehr.am.archetype.ontology.OntologyBinding)archetype.getOntology().getTermBindingList().get(0);
            Assert.AreEqual("LNC205", binding.getTerminology(), "wrong binding terminology");

            org.openehr.am.archetype.ontology.TermBindingItem item = (org.openehr.am.archetype.ontology.TermBindingItem) binding.getBindingList().get(0);

            Assert.AreEqual("/data[at0002]/events[at0003]/data[at0001]/item[at0004]", 
        		    item.getCode(), "wrong local code path");
            Assert.AreEqual(1, item.getTerms().size(), "wrong terms size");
            Assert.AreEqual("[LNC205::8310-5]", item.getTerms().get(0), "wrong term");

        }

        [TestMethod]
	    public void testPathBasedBindingWithinInternalReference()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\openEHR-EHR-OBSERVATION.test_internal_ref_binding.v1.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();

            org.openehr.am.archetype.ontology.OntologyBinding binding = (org.openehr.am.archetype.ontology.OntologyBinding)archetype.getOntology().getTermBindingList().get(0);
            Assert.AreEqual("DDB00", binding.getTerminology(), "wrong binding terminology");


            org.openehr.am.archetype.ontology.TermBindingItem item1 = (org.openehr.am.archetype.ontology.TermBindingItem)binding.getBindingList().get(0);
            Assert.AreEqual(1, item1.getTerms().size(), "wrong terms size");

            Assert.AreEqual("/data[at0001]/events[at0002]/data[at0003]/items[at0004]",
                    item1.getCode(), "wrong local code path");
            Assert.AreEqual("[DDB00::12345]", item1.getTerms().get(0), "wrong term");

            org.openehr.am.archetype.ontology.TermBindingItem item2 = (org.openehr.am.archetype.ontology.TermBindingItem)binding.getBindingList().get(1);
            Assert.AreEqual(1, item2.getTerms().size(), "wrong terms size");

            Assert.AreEqual("/data[at0001]/events[at0005]/data[at0003]/items[at0004]",
                    item2.getCode(), "wrong local code path");
            Assert.AreEqual("[DDB00::98765]", item2.getTerms().get(0), "wrong term");

            Assert.IsTrue(archetype.physicalPaths().contains("/data[at0001]/events[at0002]/data[at0003]/items[at0004]"));
            Assert.IsTrue(archetype.physicalPaths().contains("/data[at0001]/events[at0005]/data[at0003]/items[at0004]")); // path within an archetype internal ref. Must be included in the physical paths!
            Assert.IsFalse(archetype.physicalPaths().contains("/data[at0001]/events[at9999]/data[at0003]/items[at0004]"));

        }
    }
}
