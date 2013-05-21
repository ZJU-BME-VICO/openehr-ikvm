using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.rm.common.resource;
using java.util;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class ArchetypeLanguageTest
    {
        [TestMethod]
        public void testParseLanguageSection()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_language.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            
           java.util.Map translations =  archetype.getTranslations();
            Assert.IsNotNull(translations);
            TranslationDetails td = (TranslationDetails)translations.get("de");
            Assert.IsNotNull( td,"translation de missing");
            java.util.Map map = td.getAuthor();
             Assert.IsNotNull( map);
             Assert.AreEqual( "Harry Potter", map.get("name"));
             Assert.AreEqual("harry@something.somewhere.co.uk",  map.get("email"));

             Assert.AreEqual( "British Medical Translator id 00400595", td.getAccreditation());

            map = td.getOtherDetails();
            Assert.AreEqual( "Ron Weasley", map.get("review 1"));
            Assert.AreEqual( "Rubeus Hagrid", map.get("review 2"));

        }
        public void testParseLanguageWithoutAccreditation()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_language_no_accreditation.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            java.util. Map translations = archetype .getTranslations();
            Assert.IsNotNull(translations);

            TranslationDetails td = (TranslationDetails)translations.get("de");
            Assert.IsNotNull( td);
            java.util.Map map = td.getAuthor();
            Assert.IsNotNull( map);
            Assert.AreEqual( "Harry Potter", map.get("name"));
            Assert.AreEqual("harry@something.somewhere.co.uk",
                    map.get("email"));

            Assert.AreEqual( null, td.getAccreditation());

            map = td.getOtherDetails();
            Assert.AreEqual( "Ron Weasley", map.get("review 1"));
            Assert.AreEqual( "Rubeus Hagrid", map.get("review 2"));
        }

        public void testParseLanguageWithAccreditationBeforeLanguage()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_language_order_of_translation_details.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);

            Map translations = archetype.getTranslations();

            TranslationDetails td = (TranslationDetails)translations.get("de");
            Assert.IsNotNull(td);
            Map map = td.getAuthor();
            Assert.IsNotNull(map);
            Assert.AreEqual("Harry Potter", map.get("name"));
            Assert.AreEqual("harry@something.somewhere.co.uk",
                    map.get("email"));

            Assert.AreEqual(null, td.getAccreditation());

            map = td.getOtherDetails();
            Assert.AreEqual("Ron Weasley", map.get("review 1"));
            Assert.AreEqual("Rubeus Hagrid", map.get("review 2"));
        }
        public void testParseTranslationsLanguageAuthor()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.translations_language_author.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
             Map translations = archetype .getTranslations();
            Assert.IsNotNull(translations);

            TranslationDetails td = (TranslationDetails)translations.get("de");
            Assert.IsNotNull(td);
            Map map = td.getAuthor();
            Assert.IsNotNull(map);

        }
        public void testParseTranslationsAuthorLanguage()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.translations_author_language.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            Map translations = archetype .getTranslations();
            Assert.IsNotNull(translations);

            TranslationDetails td = (TranslationDetails)translations.get("de");
            Assert.IsNotNull(td);
            Map map = td.getAuthor();
            Assert.IsNotNull(map);
        }

    }
}