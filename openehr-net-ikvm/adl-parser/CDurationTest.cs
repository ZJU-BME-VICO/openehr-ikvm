using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class CDurationTest : ParserTestBase
    {
        //[TestMethod]
        public CDurationTest()
        {
		    string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.durations.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            archetype = parser.parse();
	    }

	    /**
	     * Tests duration constraints parsing
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testParseCDuration()
        {
		    assertCDuration(archetype.node("/types[at0001]/items[at1001]/value"), 
				    "PT0s", null);

		    assertCDuration(archetype.node("/types[at0001]/items[at1002]/value"), 
				    "P1d", null);

		    assertCDuration(archetype.node("/types[at0001]/items[at1003]/value"), 
				    "PT2h5m0s",	null);

		    assertCDuration(archetype.node("/types[at0001]/items[at1004]/value"), 
				    null,
				    new org.openehr.rm.support.basic.Interval(org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance("PT1h55m0s"), 
						    org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance("PT2h5m0s")));

		    assertCDuration(archetype.node("/types[at0001]/items[at1005]/value"),
				    null,
				    new org.openehr.rm.support.basic.Interval(null, org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance("PT1h"), 
						    false, true));
		
		    assertCDuration(archetype.node("/types[at0001]/items[at1006]/value"), 
				    "P1DT1H2M3S", null);
		
		    // bug fix for ISO durationg with weeks
		    assertCDuration(archetype.node("/types[at0001]/items[at1007]/value"), 
				    "P1W2DT1H2M3S", null);
		
		    // bug fix for ISO duration with months
		    assertCDuration(archetype.node("/types[at0001]/items[at1008]/value"), 
				    "P3M1W2DT1H2M3S", null);

		    // to supported newly added duration pattern
		    assertCDuration(archetype.node("/types[at0001]/items[at1009]/value"), 
				    null, null, null, "PDTH");		
	    }
	
	    /**
	     * Verifies the support for "|PT10M|", single duration interval
	     */
        [TestMethod]
	    public void testParseSingleDurationInverval()
        {
		    org.openehr.rm.support.basic.Interval interval = new org.openehr.rm.support.basic.Interval(
				    org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance("PT10M"), 
				    org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance("PT10M"));
		    assertCDuration(archetype.node("/types[at0001]/items[at1010]/value"), 
				    null, interval, null, null);
		
		    // test with assumed value
		    assertCDuration(archetype.node("/types[at0002]/items[at1010]/value"), 
				    null, interval, "PT12M", null);
	    }
	
	    /**
	     * Tests parsing CDurations with assumed values
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testParseCDurationWithAssumedValue()
        {
		    assertCDuration(archetype.node("/types[at0002]/items[at1001]/value"), 
				    "PT0s", null, "P1d");

		    assertCDuration(archetype.node("/types[at0002]/items[at1002]/value"), 
				    "P1d", null, "P1d");

		    assertCDuration(archetype.node("/types[at0002]/items[at1003]/value"), 
				    "PT2h5m0s",	null, "P1d");

		    assertCDuration(archetype.node("/types[at0002]/items[at1004]/value"), 
				    null,
				    new org.openehr.rm.support.basic.Interval(org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance("PT1h55m0s"), 
						    org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance("PT2h5m0s")), "P1d");

		    assertCDuration(archetype.node("/types[at0002]/items[at1005]/value"), 
				    null,
				    new org.openehr.rm.support.basic.Interval(null, org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance("PT1h"), 
						    false, true), "P1d");
		    // to supported newly added duration pattern
		    assertCDuration(archetype.node("/types[at0002]/items[at1006]/value"), 
				    null, null, "P1d", "PDTH");
	    }
	
	    /**
	     * Tests parsing CDurations with assumed values
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testParseCDurationWithMixedPatternAndInterval()
        {
            org.openehr.rm.support.basic.Interval interval = new org.openehr.rm.support.basic.Interval(
                    org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance("PT0S"),
                    org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance("PT120S"));
		
		
		    assertCDuration(archetype.node("/types[at0001]/items[at1014]/value"), 
				    null, interval, null, "PTS");
		
	    }

	    private org.openehr.am.archetype.Archetype archetype;
    }
}
