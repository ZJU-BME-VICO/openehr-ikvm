using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class ConstraintBindingTest
    {
        [TestMethod]
        public void testConstraintBindingWithMultiTerminologies()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.constraint_binding.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            java.util.List list = archetype.getOntology().getConstraintBindingList();

            Assert.AreEqual(2, list.size(), "unexpected number of onotology binding");

            // verify the first constraint binding
            org.openehr.am.archetype.ontology.OntologyBinding binding = (org.openehr.am.archetype.ontology.OntologyBinding)list.get(0);
            Assert.AreEqual("SNOMED_CT", binding.getTerminology(), "unexpected binding terminology");

            org.openehr.am.archetype.ontology.QueryBindingItem item = (org.openehr.am.archetype.ontology.QueryBindingItem)binding.getBindingList().get(0);

            Assert.AreEqual("ac0001", item.getCode(), "unexpected local code");
            Assert.AreEqual("http://terminology.org?terminology_id=snomed_ct&&has_relation=102002;with_target=128004", item.getQuery().getUrl(), "exexpected query");

            // verify the second constraint binding
            binding = (org.openehr.am.archetype.ontology.OntologyBinding)list.get(1);
            Assert.AreEqual("ICD10", binding.getTerminology(), "unexpected binding terminology");

            item = (org.openehr.am.archetype.ontology.QueryBindingItem)binding.getBindingList().get(0);

            Assert.AreEqual("ac0001", item.getCode(), "unexpected local code");
            Assert.AreEqual("http://terminology.org?terminology_id=icd10&&has_relation=a2;with_target=b19",
                    item.getQuery().getUrl(), "exexpected query");
        }
    }
}
