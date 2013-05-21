using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class MissingLanguageTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            bool missingLanguageCompatible = true;
            bool emptyPurposeCompatible = false;
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.missing_language.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl, missingLanguageCompatible, emptyPurposeCompatible);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            Assert.AreEqual("zh",archetype.getOriginalLanguage().getCodeString(),"originalLanguage wrong");
        }
    }
}
