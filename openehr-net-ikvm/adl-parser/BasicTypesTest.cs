using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class BasicTypesTest : ParserTestBase
    {
        public BasicTypesTest()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.basic_types.test.adl");
            
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            attributeList = parser.parse().getDefinition().getAttributes();
        }
        
        [TestMethod]
	    public void testStringConstraints()
        {
            //string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.basic_types.test.adl");

            //se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            //attributeList = parser.parse().getDefinition().getAttributes();

            java.util.List list = getConstraints(0);
            assertCString(list.get(0), null, new String[] { "something" }, null);

		    assertCString(list.get(1), "this|that|something else", null, null);

		    assertCString(list.get(2), "cardio.*", null, null);

		    assertCString(list.get(3), "mg|mg/ml|mg/g", null, null);

		    assertCString(list.get(4), null, new String[] { "apple", "pear" }, null);

		    // with assumed values
		    assertCString(list.get(5), null, new String[] { "something" },
				    "nothing");

		    assertCString(list.get(6), "this|that|something else", null, "those");

		    assertCString(list.get(7), "cardio.*", null, "cardio.txt");

		    assertCString(list.get(8), "mg|mg/ml|mg/g", null, "mg");

		    assertCString(list.get(9), null, new String[] { "apple", "pear" },
				    "orange");

	    }

        private java.util.List getConstraints(int index) 
        {
		    org.openehr.am.archetype.constraintmodel.CAttribute ca = (org.openehr.am.archetype.constraintmodel.CAttribute) attributeList.get(index);
		    return ((org.openehr.am.archetype.constraintmodel.CComplexObject) ca.getChildren().get(0)).getAttributes();
	    }

	    [TestMethod]
	    public void testBooleanConstraints()
        {
            java.util.List list = getConstraints(1);

		    assertCBoolean(list.get(0), true, false, false, false);

		    assertCBoolean(list.get(1), false, true, false, false);

		    assertCBoolean(list.get(2), true, true, false, false);

		    // with assumed values
		    assertCBoolean(list.get(3), true, false, false, true);

		    assertCBoolean(list.get(4), false, true, true, true);

		    assertCBoolean(list.get(5), true, true, true, true);
	    }

	    /**
	     * Tests integer constraints parsing
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testIntegerConstraints()
        {
            java.util.List list = getConstraints(2);

		    assertCInteger(list.get(0), null, new int[] { 55 }, null);

		    assertCInteger(list.get(1), null, new int[] { 10, 20, 30 }, null);

            assertCInteger(list.get(2), new org.openehr.rm.support.basic.Interval(new java.lang.Integer(0), new java.lang.Integer(100)), null, null);

            assertCInteger(list.get(3), greaterThan(new java.lang.Integer(10)), null, null);

            assertCInteger(list.get(4), lessThan(new java.lang.Integer(10)), null, null);

            assertCInteger(list.get(5), greaterEqual(new java.lang.Integer(10)), null, null);

            assertCInteger(list.get(6), lessEqual(new java.lang.Integer(10)), null, null);

            assertCInteger(list.get(7), new org.openehr.rm.support.basic.Interval(new java.lang.Integer(-10), new java.lang.Integer(-5)), null, null);

		    // with assumed values
            assertCInteger(list.get(8), null, new int[] { 55 }, new java.lang.Integer(50));

            assertCInteger(list.get(9), null, new int[] { 10, 20, 30 }, new java.lang.Integer(20));

            assertCInteger(list.get(10), new org.openehr.rm.support.basic.Interval(new java.lang.Integer(0), new java.lang.Integer(100)), null, new java.lang.Integer(50));

            assertCInteger(list.get(11), greaterThan(new java.lang.Integer(10)), null, new java.lang.Integer(20));

            assertCInteger(list.get(12), lessThan(new java.lang.Integer(10)), null, new java.lang.Integer(5));

            assertCInteger(list.get(13), greaterEqual(new java.lang.Integer(10)), null, new java.lang.Integer(12));

            assertCInteger(list.get(14), lessEqual(new java.lang.Integer(10)), null, new java.lang.Integer(8));

            assertCInteger(list.get(15), new org.openehr.rm.support.basic.Interval(new java.lang.Integer(-10), new java.lang.Integer(-5)), null, new java.lang.Integer(-7));

            assertCInteger(list.get(16), new org.openehr.rm.support.basic.Interval(new java.lang.Integer(100), new java.lang.Integer(100)), null, null);
		
		    // non-inclusive intervals
            assertCInteger(list.get(17), new org.openehr.rm.support.basic.Interval(new java.lang.Integer(0), new java.lang.Integer(100), false, true), null, null);

            assertCInteger(list.get(18), new org.openehr.rm.support.basic.Interval(new java.lang.Integer(0), new java.lang.Integer(100), true, false), null, null);

            assertCInteger(list.get(19), new org.openehr.rm.support.basic.Interval(new java.lang.Integer(0), new java.lang.Integer(100), false, false), null, null);
	    }

	    /**
	     * Tests double constraints parsing
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testDoubleConstraints()
        {
            java.util.List list = getConstraints(3);

            assertCReal(list.get(0), null, new double[] { 100.0 }, null);

		    assertCReal(list.get(1), null, new double[] { 10.0, 20.0, 30.0 }, null);

            assertCReal(list.get(2), new org.openehr.rm.support.basic.Interval(new java.lang.Double(0.0),
				    new java.lang.Double(100.0)), null, null);

		    assertCReal(list.get(3), greaterThan(new java.lang.Double(10.0)), null, null);

		    assertCReal(list.get(4), lessThan(new java.lang.Double(10.0)), null, null);

		    assertCReal(list.get(5), greaterEqual(new java.lang.Double(10.0)), null, null);

		    assertCReal(list.get(6), lessEqual(new java.lang.Double(10.0)), null, null);

            assertCReal(list.get(7), new org.openehr.rm.support.basic.Interval(new java.lang.Double(-10.0), new java.lang.Double(-5.0)), null, null);

		    // with assumed values
		    assertCReal(list.get(8), null, new double[] { 100.0 }, new java.lang.Double(80.0));

            assertCReal(list.get(9), null, new double[] { 10.0, 20.0, 30.0 }, new java.lang.Double(20.0));

            assertCReal(list.get(10), new org.openehr.rm.support.basic.Interval(new java.lang.Double(0.0), new java.lang.Double(100.0)), null, new java.lang.Double(60.0));

            assertCReal(list.get(11), greaterThan(new java.lang.Double(10.0)), null, new java.lang.Double(30.0));

		    assertCReal(list.get(12), lessThan(new java.lang.Double(10.0)), null, new java.lang.Double(2.0));

		    assertCReal(list.get(13), greaterEqual(new java.lang.Double(10.0)), null, new java.lang.Double(10.0));

		    assertCReal(list.get(14), lessEqual(new java.lang.Double(10.0)), null, new java.lang.Double(9.0));

            assertCReal(list.get(15), new org.openehr.rm.support.basic.Interval(new java.lang.Double(-10.0),
				    new java.lang.Double(-5.0)), null, new java.lang.Double(-8.0));
		
		    // single value as interval
            assertCReal(list.get(16), new org.openehr.rm.support.basic.Interval(new java.lang.Double(100.0),
				    new java.lang.Double(100.0)), null, null);
		
		    // non-inclusive interval
            assertCReal(list.get(17), new org.openehr.rm.support.basic.Interval(new java.lang.Double(0.0),
				    new java.lang.Double(100.0), false, true), null, null);

            assertCReal(list.get(18), new org.openehr.rm.support.basic.Interval(new java.lang.Double(0.0),
				    new java.lang.Double(100.0), true, false), null, null);

            assertCReal(list.get(19), new org.openehr.rm.support.basic.Interval(new java.lang.Double(0.0),
				    new java.lang.Double(100.0), false, false), null, null);		
	    }

	    /**
	     * Tests date and partial date constraints parsing
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testDateConstraints()
        {
            java.util.List list = getConstraints(4);

		    assertCDate(list.get(0), "yyyy-mm-dd", null, null, null);

		    assertCDate(list.get(1), "yyyy-??-??", null, null, null);

		    assertCDate(list.get(2), "yyyy-mm-??", null, null, null);

		    assertCDate(list.get(3), "yyyy-??-XX", null, null, null);

		    assertCDate(list.get(4), null, null, new String[] { "1983-12-25" },
				    null);

		    assertCDate(list.get(5), null, null, new String[] { "2000-01-01" },
				    null);

            assertCDate(list.get(6), null, new org.openehr.rm.support.basic.Interval(date("2004-09-20"),
				    date("2004-10-20")), null, null);

		    assertCDate(list.get(7), null, lessThan(date("2004-09-20")), null,
				    null);

		    assertCDate(list.get(8), null, lessEqual(date("2004-09-20")), null,
				    null);

		    assertCDate(list.get(9), null, greaterThan(date("2004-09-20")), null,
				    null);

		    assertCDate(list.get(10), null, greaterEqual(date("2004-09-20")), null,
				    null);

		    // test assumed values
		    assertCDate(list.get(11), "yyyy-mm-dd", null, null, "2000-01-01");

		    assertCDate(list.get(12), "yyyy-??-??", null, null, "2001-01-01");

		    assertCDate(list.get(13), "yyyy-mm-??", null, null, "2002-01-01");

		    assertCDate(list.get(14), "yyyy-??-XX", null, null, "2003-01-01");

		    assertCDate(list.get(15), null, null, new String[] { "1983-12-25" },
				    "2004-01-01");

		    assertCDate(list.get(16), null, null, new String[] { "2000-01-01" },
				    "2005-01-01");

            assertCDate(list.get(17), null, new org.openehr.rm.support.basic.Interval(
				    date("2004-09-20"), date("2004-10-20")), null, 
				    "2004-09-30");

		    assertCDate(list.get(18), null, lessThan(date("2004-09-20")), null,
				    "2004-09-01");

		    assertCDate(list.get(19), null, lessEqual(date("2004-09-20")), null,
				    "2003-09-20");

		    assertCDate(list.get(20), null, greaterThan(date("2004-09-20")), null,
				    "2005-01-02");

		    assertCDate(list.get(21), null, greaterEqual(date("2004-09-20")), null,
				    "2005-10-30");
	    }

	    /**
	     * Tests time and partial time constraints parsing
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testTimeConstraints()
        {
            java.util.List list = getConstraints(5);

		    assertCTime(list.get(0), "hh:mm:ss", null, null, null);

		    assertCTime(list.get(1), "hh:mm:XX", null, null, null);

		    assertCTime(list.get(2), "hh:??:XX", null, null, null);

		    assertCTime(list.get(3), "hh:??:??", null, null, null);

		    assertCTime(list.get(4), null, null, new String[] { "22:00:05" },
				    null);

		    assertCTime(list.get(5), null, null, new String[] { "00:00:59" },
				    null);

		    assertCTime(list.get(6), null, null, new String[] { "12:35" }, 
				    null);

		    assertCTime(list.get(7), null, null, new String[] { "12:35:45.666" },
				    null);

		    assertCTime(list.get(8), null, null, new String[] { "12:35:45-0700" },
				    null);

		    assertCTime(list.get(9), null, null, new String[] { "12:35:45+0800" },
				    null);

		    assertCTime(list.get(10), null, null,
				    new String[] { "12:35:45.999-0700" }, null);

		    assertCTime(list.get(11), null, null,
				    new String[] { "12:35:45.000+0800" }, null);

		    assertCTime(list.get(12), null, null,
				    new String[] { "12:35:45.000+0000" }, null);

		    assertCTime(list.get(13), null, null,
				    new String[] { "12:35:45.995-0700" }, null);

		    assertCTime(list.get(14), null, null,
				    new String[] { "12:35:45.001+0800" }, null);

            assertCTime(list.get(15), null, new org.openehr.rm.support.basic.Interval(time("12:35"),
				    time("16:35")), null, null);

		    assertCTime(list.get(16), null, lessThan(time("12:35")), null, null);

		    assertCTime(list.get(17), null, lessEqual(time("12:35")), null, null);

		    assertCTime(list.get(18), null, greaterThan(time("12:35")), null, null);

		    assertCTime(list.get(19), null, greaterEqual(time("12:35")), null, null);

	    }

	    /**
	     * Tests time and partial time constraints parsing
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testTimeConstraintsWithAssumedValues()
        {
            java.util.List list = getConstraints(5);

		    assertCTime(list.get(20), "hh:mm:ss", null, null, "10:00:00");

		    assertCTime(list.get(21), "hh:mm:XX", null, null, "10:00:00");

		    assertCTime(list.get(22), "hh:??:XX", null, null, "10:00:00");

		    assertCTime(list.get(23), "hh:??:??", null, null, "10:00:00");

		    assertCTime(list.get(24), null, null, new String[] { "22:00:05" },
				    "10:00:00");

		    assertCTime(list.get(25), null, null, new String[] { "00:00:59" },
				    "10:00:00");

		    assertCTime(list.get(26), null, null, new String[] { "12:35" },
				    "10:00:00");

		    assertCTime(list.get(27), null, null, new String[] { "12:35:45.666" },
				    "10:00:00");

		    assertCTime(list.get(28), null, null, new String[] { "12:35:45-0700" },
				    "10:00:00");

		    assertCTime(list.get(29), null, null, new String[] { "12:35:45+0800" },
				    "10:00:00");

		    assertCTime(list.get(30), null, null,
				    new String[] { "12:35:45.999-0700" }, "10:00:00");

		    assertCTime(list.get(31), null, null,
				    new String[] { "12:35:45.000+0800" }, "10:00:00");

		    assertCTime(list.get(32), null, null,
				    new String[] { "12:35:45.000+0000" }, "10:00:00");

		    assertCTime(list.get(33), null, null,
				    new String[] { "12:35:45.995-0700" }, "10:00:00");

		    assertCTime(list.get(34), null, null,
				    new String[] { "12:35:45.001+0800" }, "10:00:00");

            assertCTime(list.get(35), null, new org.openehr.rm.support.basic.Interval(time("12:35"),
				    time("16:35")), null, "10:00:00");

		    assertCTime(list.get(36), null, lessThan(time("12:35")), null,
				    "10:00:00");

		    assertCTime(list.get(37), null, lessEqual(time("12:35")), null,
				    "10:00:00");

		    assertCTime(list.get(38), null, greaterThan(time("12:35")), null,
				    "10:00:00");

		    assertCTime(list.get(39), null, greaterEqual(time("12:35")), null,
				    "10:00:00");
	    }

	    /**
	     * Tests datetime and partial datetime constraints parsing
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testDateTimeConstraints()
        {
            java.util.List list = getConstraints(6);

		    assertCDateTime(list.get(0), "yyyy-mm-dd hh:mm:ss", null, null, null);

		    assertCDateTime(list.get(1), "yyyy-mm-dd hh:mm:??", null, null, null);

		    assertCDateTime(list.get(2), "yyyy-mm-dd hh:mm:XX", null, null, null);

		    assertCDateTime(list.get(3), "yyyy-mm-dd hh:??:XX", null, null, null);

		    assertCDateTime(list.get(4), "yyyy-??-?? ??:??:??", null, null, null);		

		    assertCDateTime(list.get(5), null, null,
				    new String[] { "1983-12-25T22:00:05" }, null);

		    assertCDateTime(list.get(6), null, null,
				    new String[] { "2000-01-01T00:00:59" }, null);

		    assertCDateTime(list.get(7), null, null,
				    new String[] { "2000-01-01T00:00:59" },	null);

		    assertCDateTime(list.get(8), null, null,
				    new String[] { "2000-01-01T00:00:59.105" }, null);

		    assertCDateTime(list.get(9), null, null,
				    new String[] { "2000-01-01T00:00:59+0000" }, null);

		    assertCDateTime(list.get(10), null, null,
				    new String[] { "2000-01-01T00:00:59+1200" }, null);

		    assertCDateTime(list.get(11), null, null,
				    new String[] { "2000-01-01T00:00:59.500+0000" }, null);

		    assertCDateTime(list.get(12), null, null,
				    new String[] { "2000-01-01T00:00:59.500+1200" }, null);

		    assertCDateTime(list.get(13), null, null,
				    new String[] { "2000-01-01T00:00:59.000+0000" }, null);

		    assertCDateTime(list.get(14), null, null,
				    new String[] { "2000-01-01T00:00:59.000+1200" }, null);

            assertCDateTime(list.get(15), null, new org.openehr.rm.support.basic.Interval(
				    dateTime("2000-01-01T00:00:00"),
				    dateTime("2000-01-02T00:00:00")), null, null);

		    assertCDateTime(list.get(16), null,
				    lessThan(dateTime("2000-01-01T00:00:00")), null, null);

		    assertCDateTime(list.get(17), null,
				    lessEqual(dateTime("2000-01-01T00:00:00")), null, null);

		    assertCDateTime(list.get(18), null,
				    greaterThan(dateTime("2000-01-01T00:00:00")), null, null);

		    assertCDateTime(list.get(19), null,
				    greaterEqual(dateTime("2000-01-01T00:00:00")), null, null);
		
		    assertCDateTime(list.get(40), "yyyy-??-??T??:??:??", null, null, null);
	    }

	    /**
	     * Tests datetime and partial datetime constraints parsing
	     * 
	     * @throws Exception
	     */
        [TestMethod]
	    public void testDateTimeConstraintsWithAssumedValues()
        {
            java.util.List list = getConstraints(6);

            assertCDateTime(list.get(20), "yyyy-mm-dd hh:mm:ss", null, null,
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(21), "yyyy-mm-dd hh:mm:??", null, null,
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(22), "yyyy-mm-dd hh:mm:XX", null, null,
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(23), "yyyy-mm-dd hh:??:XX", null, null,
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(24), "yyyy-??-?? ??:??:??", null, null,
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(25), null, null,
				    new String[] { "1983-12-25T22:00:05" },
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(26), null, null,
				    new String[] { "2000-01-01T00:00:59" },
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(27), null, null,
				    new String[] { "2000-01-01T00:00:59.000" },
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(28), null, null,
				    new String[] { "2000-01-01T00:00:59.105" },
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(29), null, null,
				    new String[] { "2000-01-01T00:00:59+0000" },
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(30), null, null,
				    new String[] { "2000-01-01T00:00:59+1200" },
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(31), null, null,
				    new String[] { "2000-01-01T00:00:59.500+0000" },
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(32), null, null,
				    new String[] { "2000-01-01T00:00:59.500+1200" },
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(33), null, null,
				    new String[] { "2000-01-01T00:00:59.000+0000" },
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(34), null, null,
				    new String[] { "2000-01-01T00:00:59.000+1200" },
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(35), null, new org.openehr.rm.support.basic.Interval(
				    dateTime("2000-01-01T00:00:00"),
				    dateTime("2000-01-02T00:00:00")), null,
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(36), null,
				    lessThan(dateTime("2000-01-01T00:00:00")), null,
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(37), null,
				    lessEqual(dateTime("2000-01-01T00:00:00")), null,
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(38), null,
				    greaterThan(dateTime("2000-01-01T00:00:00")), null,
				    "2006-03-31T01:12:00");

            assertCDateTime(list.get(39), null,
				    greaterEqual(dateTime("2000-01-01T00:00:00")), null,
				    "2006-03-31T01:12:00");
	    }

        private org.openehr.rm.support.basic.Interval greaterThan(java.lang.Comparable value)
        {
            return new org.openehr.rm.support.basic.Interval(value, null, false, false);
	    }

        private org.openehr.rm.support.basic.Interval greaterEqual(java.lang.Comparable value)
        {
            return new org.openehr.rm.support.basic.Interval(value, null, true, false);
	    }

        private org.openehr.rm.support.basic.Interval lessThan(java.lang.Comparable value)
        {
            return new org.openehr.rm.support.basic.Interval(null, value, false, false);
	    }

        private org.openehr.rm.support.basic.Interval lessEqual(java.lang.Comparable value)
        {
            return new org.openehr.rm.support.basic.Interval(null, value, false, true);
	    }

        private java.util.List attributeList;
        }
    }
