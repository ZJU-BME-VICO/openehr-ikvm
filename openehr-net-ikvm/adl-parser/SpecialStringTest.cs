using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.archetype.constraintmodel;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class SpecialStringTest : ParserTestBase
    {
        //[TestMethod]
        public SpecialStringTest()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.special_string.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);

		    attributeList = parser.parse().getDefinition().getAttributes();
	    }

        [TestMethod]
	    public void testParseEscapedDoubleQuote()
        {
		    list = getConstraints(0);
		    assertCString(list.get(0), null, new String[] { "some\"thing" }, null);
	    }

        [TestMethod]
	    public void testParseEscapedBackslash()
        {
		    list = getConstraints(0);
		    assertCString(list.get(1), null, new String[] { "any\\thing" }, null);
	    }

        private java.util.List getConstraints(int index) 
        {
		    CAttribute ca = (CAttribute) attributeList.get(index);
		    return ((CComplexObject) ca.getChildren().get(0)).getAttributes();
	    }
	
	    private java.util.List attributeList;
	    private java.util.List list;
    }
}
