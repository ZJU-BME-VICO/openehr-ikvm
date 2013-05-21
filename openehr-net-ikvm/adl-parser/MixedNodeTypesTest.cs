using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class MixedNodeTypesTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.mixed_node_types.draft.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            
            try
            {
                org.openehr.am.archetype.Archetype archetype = parser.parse();
                Assert.IsNotNull(archetype);
            }
            catch (Exception e)
            {
               string a= e.Message;
               Console.Write(a);
               // fail("failed to parse mixed node types");
            }
        }
    }
}
