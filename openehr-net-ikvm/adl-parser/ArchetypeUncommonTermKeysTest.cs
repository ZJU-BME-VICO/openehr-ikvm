using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class ArchetypeUncommonTermKeysTest
    {
        [TestMethod]
        public void testArchetypeUncommonTerm()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_uncommonkeys.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            org.openehr.am.archetype.ontology.ArchetypeTerm aterm = archetype.getOntology().termDefinition("at0000");

            Assert.AreEqual("another key value", aterm.getItem("anotherkey").ToString(), "key value wrong");
            Assert.AreEqual("test text", aterm.getItem("text").ToString(), "key value wrong");
            Assert.AreEqual("test description", aterm.getItem("description").ToString(), "key value wrong");
        }
    }
}
