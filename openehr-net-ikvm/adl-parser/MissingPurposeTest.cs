using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.rm.common.resource;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class MissingPurposeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            bool missingLanguageCompatible = false;
            bool emptyPurposeCompatible = true;
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_desc_missing_purpose.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl, missingLanguageCompatible, emptyPurposeCompatible);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            ResourceDescriptionItem c = (ResourceDescriptionItem)archetype.getDescription().getDetails().get(0);
         Assert.IsNotNull(c.getPurpose(),"purpose null");

        }
    }
}
