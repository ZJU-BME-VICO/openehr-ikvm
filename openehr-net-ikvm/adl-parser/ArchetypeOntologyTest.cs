using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.archetype.ontology;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class ArchetypeOntologyTest
    {



        [TestMethod]
        public void testParseTermDefinition()
        {

            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_ontology.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            org.openehr.am.archetype.ontology.ArchetypeOntology ontology = archetype.getOntology();
            org.openehr.am.archetype.ontology.ArchetypeTerm term = ontology.termDefinition("en", "at0000");
            Assert.AreEqual("some text", term.getItem("text"), "text wrong");
            Assert.AreEqual("some comment", term.getItem("comment"), "comment wrong");
            Assert.AreEqual("some description", term.getItem("description"), "description wrong");
            

        }
        [TestMethod]
        public void testBindings()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_bindings.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            org.openehr.am.archetype.ontology.ArchetypeOntology ontology = archetype.getOntology();
        
            Assert.IsNotNull(ontology);
            object a = ontology.getTermBindingList().get(0);
            OntologyBinding termBinding = a as OntologyBinding;
            Assert.AreEqual("SNOMED-CT", termBinding.getTerminology(), "term bindings wrong");
         
            TermBindingItem tbi = (TermBindingItem)termBinding.getBindingList().get(0);
            Assert.AreEqual("[SNOMED-CT::123456]", tbi.getTerms().get(0), "term binding item wrong");
            OntologyBinding constrBinding = (OntologyBinding)ontology.getConstraintBindingList().get(0);

            Assert.AreEqual("SNOMED-CT", constrBinding.getTerminology(), "binding ontology wrong");
  
            QueryBindingItem qbi = (QueryBindingItem)constrBinding.getBindingList().get(0);
            Assert.AreEqual("http://openEHR.org/testconstraintbinding", qbi.getQuery().getUrl(), "query binding item wrong");
        }

    }
}
