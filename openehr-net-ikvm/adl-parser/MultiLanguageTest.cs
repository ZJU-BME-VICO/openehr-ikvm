using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.archetype;
using org.openehr.rm.common.resource;
using org.openehr.am.archetype.ontology;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class MultiLanguageTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string test = System.IO.Directory.GetCurrentDirectory();
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.multi_language.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
         //   List<OntologyDefinitions> list =  archetype.getOntology().getTermDefinitionsList();

            Assert.AreEqual( 2, archetype.getOntology().getTermDefinitionsList().size());

            OntologyDefinitions defs = (OntologyDefinitions)archetype.getOntology().getTermDefinitionsList().get(0);
            Assert.AreEqual( "en", defs.getLanguage());
            OntologyDefinitions defs2 = (OntologyDefinitions)archetype.getOntology().getTermDefinitionsList().get(1);
            Assert.AreEqual("sv", defs2.getLanguage());
        }
        [TestMethod]
        public void testMultiLanguageConstraintDefinitions()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.multi_language.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            //List<OntologyDefinitions> list =
            //archetype.getOntology().getConstraintDefinitionsList();
            Assert.AreEqual( 2, archetype.getOntology().getConstraintDefinitionsList().size());

            OntologyDefinitions defs = (OntologyDefinitions)archetype.getOntology().getConstraintDefinitionsList().get(0);
            Assert.AreEqual("en", defs.getLanguage());

            defs = (OntologyDefinitions)archetype.getOntology().getConstraintDefinitionsList().get(1);
            Assert.AreEqual( "sv", defs.getLanguage());
        }
        [TestMethod]
        public void testTranslationDetails()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.testtranslations.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            
            java.util.Map translations = archetype.getTranslations();
            TranslationDetails transDet = (TranslationDetails)translations.get("de");
            Assert.AreEqual("test Accreditation!", transDet.getAccreditation());
            Assert.AreEqual( "test organisation", transDet.getAuthor().get("organisation"));
            TranslationDetails transDet2 = (TranslationDetails)translations.get("es");
            Assert.AreEqual(null, transDet2.getAccreditation());
            Assert.AreEqual(null, transDet2.getAuthor().get("organisation"));
      
        	

        }

    }
}
