using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.am.archetype.constraintmodel;
using org.openehr.rm.support.basic;
using org.openehr.rm.datatypes.quantity.datetime;
using java.util;
namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class DateTimeTest : ParserTestBase
    {
        [TestMethod]
        public void testDateConstraints()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.datetime.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            int n = archetype.getDefinition().getAttributes().size();
            List<object> AttributeList = new List<object>();
            for (int i = 0; i < n; i++)
            {
                object Attribute = archetype.getDefinition().getAttributes().get(i);
                AttributeList.Add(Attribute);
            }
            CAttribute ca = (CAttribute)AttributeList[0];
            CComplexObject a = (CComplexObject)ca.getChildren().get(0);
            CSingleAttribute b = (CSingleAttribute)a.getAttributes().get(0);
            Assert.IsNotNull(ca);
            Assert.IsNotNull(b);

            assertCDate(b, "yyyy-mm-dd", null, null, null);

            assertCDate((CSingleAttribute)a.getAttributes().get(1), "yyyy-??-??", null, null, null);

            assertCDate((CSingleAttribute)a.getAttributes().get(2), "yyyy-mm-??", null, null, null);

            assertCDate((CSingleAttribute)a.getAttributes().get(3), "yyyy-??-XX", null, null, null);

            assertCDate((CSingleAttribute)a.getAttributes().get(4), null, null, new String[] { "1983-12-25" },
                    null);

            assertCDate((CSingleAttribute)a.getAttributes().get(5), null, null, new String[] { "2000-01-01" },
                    null);

            assertCDate((CSingleAttribute)a.getAttributes().get(6), null, new Interval(date("2004-09-20"),
                    date("2004-10-20")), null, null);

            assertCDate((CSingleAttribute)a.getAttributes().get(7), null, lessThan(date("2004-09-20")), null,
                    null);

            assertCDate((CSingleAttribute)a.getAttributes().get(8), null, lessEqual(date("2004-09-20")), null,
                    null);

            assertCDate((CSingleAttribute)a.getAttributes().get(9), null, greaterThan(date("2004-09-20")), null,
                    null);

            assertCDate((CSingleAttribute)a.getAttributes().get(10), null, greaterEqual(date("2004-09-20")), null,
                    null);

            // test assumed values
            assertCDate((CSingleAttribute)a.getAttributes().get(11), "yyyy-mm-dd", null, null, "2000-01-01");

            assertCDate((CSingleAttribute)a.getAttributes().get(12), "yyyy-??-??", null, null, "2001-01-01");

            assertCDate((CSingleAttribute)a.getAttributes().get(13), "yyyy-mm-??", null, null, "2002-01-01");

            assertCDate((CSingleAttribute)a.getAttributes().get(14), "yyyy-??-XX", null, null, "2003-01-01");

            assertCDate((CSingleAttribute)a.getAttributes().get(15), null, null, new String[] { "1983-12-25" },
                    "2004-01-01");

            assertCDate((CSingleAttribute)a.getAttributes().get(16), null, null, new String[] { "2000-01-01" },
                    "2005-01-01");

            assertCDate((CSingleAttribute)a.getAttributes().get(17), null, new Interval(
                    date("2004-09-20"), date("2004-10-20")), null,
                    "2004-09-30");

            assertCDate((CSingleAttribute)a.getAttributes().get(18), null, lessThan(date("2004-09-20")), null,
                    "2004-09-01");

            assertCDate((CSingleAttribute)a.getAttributes().get(19), null, lessEqual(date("2004-09-20")), null,
                    "2003-09-20");

            assertCDate((CSingleAttribute)a.getAttributes().get(20), null, greaterThan(date("2004-09-20")), null,
                    "2005-01-02");

            assertCDate((CSingleAttribute)a.getAttributes().get(21), null, greaterEqual(date("2004-09-20")), null,
                    "2005-10-30");
        }

        [TestMethod]
        public void testTimeConstraints()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.datetime.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            int n = archetype.getDefinition().getAttributes().size();
            List<object> AttributeList = new List<object>();
            for (int i = 0; i < n; i++)
            {
                object Attribute = archetype.getDefinition().getAttributes().get(i);
                AttributeList.Add(Attribute);
            }
            CAttribute ca = (CAttribute)AttributeList[1];
            CComplexObject a = (CComplexObject)ca.getChildren().get(0);
            CSingleAttribute b = (CSingleAttribute)a.getAttributes().get(0);
            Assert.IsNotNull(ca);
            Assert.IsNotNull(b);
            assertCTime((CSingleAttribute)a.getAttributes().get(0), "hh:mm:ss", null, null, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(1), "hh:mm:XX", null, null, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(2), "hh:??:XX", null, null, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(3), "hh:??:??", null, null, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(4), null, null, new String[] { "22:00:05" },
                    null);

            assertCTime((CSingleAttribute)a.getAttributes().get(5), null, null, new String[] { "00:00:59" },
                    null);

            assertCTime((CSingleAttribute)a.getAttributes().get(6), null, null, new String[] { "12:35" },
                    null);

            assertCTime((CSingleAttribute)a.getAttributes().get(7), null, null, new String[] { "12:35:45.666" },
                    null);

            assertCTime((CSingleAttribute)a.getAttributes().get(8), null, null, new String[] { "12:35:45-0700" },
                    null);

            assertCTime((CSingleAttribute)a.getAttributes().get(9), null, null, new String[] { "12:35:45+0800" },
                    null);

            assertCTime((CSingleAttribute)a.getAttributes().get(10), null, null, new String[] { "12:35:45.999-0700" }, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(11), null, null,
                    new String[] { "12:35:45.000+0800" }, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(12), null, null,
                    new String[] { "12:35:45.000+0000" }, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(13), null, null,
                    new String[] { "12:35:45.995-0700" }, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(14), null, null,
                    new String[] { "12:35:45.001+0800" }, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(15), null, new Interval(time("12:35"),
                    time("16:35")), null, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(16), null, lessThan(time("12:35")), null, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(17), null, lessEqual(time("12:35")), null, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(18), null, greaterThan(time("12:35")), null, null);

            assertCTime((CSingleAttribute)a.getAttributes().get(19), null, greaterEqual(time("12:35")), null, null);
        }
        [TestMethod]
        public void testTimeConstraintsWithAssumedValues()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.datetime.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            int n = archetype.getDefinition().getAttributes().size();
            List<object> AttributeList = new List<object>();
            for (int i = 0; i < n; i++)
            {
                object Attribute = archetype.getDefinition().getAttributes().get(i);
                AttributeList.Add(Attribute);
            }
            CAttribute ca = (CAttribute)AttributeList[1];
            CComplexObject a = (CComplexObject)ca.getChildren().get(0);
            CSingleAttribute b = (CSingleAttribute)a.getAttributes().get(0);
            Assert.IsNotNull(ca);
            assertCTime((CSingleAttribute)a.getAttributes().get(20), "hh:mm:ss", null, null, "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(21), "hh:mm:XX", null, null, "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(22), "hh:??:XX", null, null, "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(23), "hh:??:??", null, null, "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(24), null, null, new String[] { "22:00:05" },
                    "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(25), null, null, new String[] { "00:00:59" },
                    "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(26), null, null, new String[] { "12:35" },
                    "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(27), null, null, new String[] { "12:35:45.666" },
                    "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(28), null, null, new String[] { "12:35:45-0700" },
                    "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(29), null, null, new String[] { "12:35:45+0800" },
                    "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(30), null, null,
                    new String[] { "12:35:45.999-0700" }, "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(31), null, null,
                    new String[] { "12:35:45.000+0800" }, "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(32), null, null,
                    new String[] { "12:35:45.000+0000" }, "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(33), null, null,
                    new String[] { "12:35:45.995-0700" }, "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(34), null, null,
                    new String[] { "12:35:45.001+0800" }, "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(35), null, new Interval(time("12:35"),
                    time("16:35")), null, "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(36), null, lessThan(time("12:35")), null,
                    "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(37), null, lessEqual(time("12:35")), null,
                    "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(38), null, greaterThan(time("12:35")), null,
                    "10:00:00");

            assertCTime((CSingleAttribute)a.getAttributes().get(39), null, greaterEqual(time("12:35")), null,
                    "10:00:00");
        }
        [TestMethod]
        public void testDateTimeConstraints()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.datetime.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            int n = archetype.getDefinition().getAttributes().size();
            List<object> AttributeList = new List<object>();
            for (int i = 0; i < n; i++)
            {
                object Attribute = archetype.getDefinition().getAttributes().get(i);
                AttributeList.Add(Attribute);
            }
            CAttribute ca = (CAttribute)AttributeList[2];
            CComplexObject a = (CComplexObject)ca.getChildren().get(0);
            CSingleAttribute b = (CSingleAttribute)a.getAttributes().get(0);
            assertCDateTime((CSingleAttribute)a.getAttributes().get(0), "yyyy-mm-dd hh:mm:ss", null, null, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(1), "yyyy-mm-dd hh:mm:??", null, null, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(2), "yyyy-mm-dd hh:mm:XX", null, null, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(3), "yyyy-mm-dd hh:??:XX", null, null, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(4), "yyyy-??-?? ??:??:??", null, null, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(5), null, null,
                    new String[] { "1983-12-25T22:00:05" }, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(6), null, null,
                    new String[] { "2000-01-01T00:00:59" }, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(7), null, null,
                    new String[] { "2000-01-01T00:00:59" }, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(8), null, null,
                    new String[] { "2000-01-01T00:00:59.105" }, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(9), null, null,
                    new String[] { "2000-01-01T00:00:59+0000" }, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(10), null, null,
                    new String[] { "2000-01-01T00:00:59+1200" }, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(11), null, null,
                    new String[] { "2000-01-01T00:00:59.500+0000" }, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(12), null, null,
                    new String[] { "2000-01-01T00:00:59.500+1200" }, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(13), null, null,
                    new String[] { "2000-01-01T00:00:59.000+0000" }, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(14), null, null,
                    new String[] { "2000-01-01T00:00:59.000+1200" }, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(15), null, new Interval(
                    dateTime("2000-01-01T00:00:00"),
                    dateTime("2000-01-02T00:00:00")), null, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(16), null,
                    lessThan(dateTime("2000-01-01T00:00:00")), null, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(17), null,
                    lessEqual(dateTime("2000-01-01T00:00:00")), null, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(18), null,
                    greaterThan(dateTime("2000-01-01T00:00:00")), null, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(19), null,
                    greaterEqual(dateTime("2000-01-01T00:00:00")), null, null);

            assertCDateTime((CSingleAttribute)a.getAttributes().get(40), "yyyy-??-??T??:??:??", null, null, null);
        }

        [TestMethod]
        public void testDateTimeConstraintsWithAssumedValues()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.datetime.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            int n = archetype.getDefinition().getAttributes().size();
            List<object> AttributeList = new List<object>();
            for (int i = 0; i < n; i++)
            {
                object Attribute = archetype.getDefinition().getAttributes().get(i);
                AttributeList.Add(Attribute);
            }
            CAttribute ca = (CAttribute)AttributeList[2];
            CComplexObject a = (CComplexObject)ca.getChildren().get(0);
            CSingleAttribute b = (CSingleAttribute)a.getAttributes().get(0);
            assertCDateTime((CSingleAttribute)a.getAttributes().get(20), "yyyy-mm-dd hh:mm:ss", null, null,
                "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(21), "yyyy-mm-dd hh:mm:??", null, null,
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(22), "yyyy-mm-dd hh:mm:XX", null, null,
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(23), "yyyy-mm-dd hh:??:XX", null, null,
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(24), "yyyy-??-?? ??:??:??", null, null,
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(25), null, null,
                    new String[] { "1983-12-25T22:00:05" },
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(26), null, null,
                    new String[] { "2000-01-01T00:00:59" },
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(27), null, null,
                    new String[] { "2000-01-01T00:00:59.000" },
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(28), null, null,
                    new String[] { "2000-01-01T00:00:59.105" },
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(29), null, null,
                    new String[] { "2000-01-01T00:00:59+0000" },
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(30), null, null,
                    new String[] { "2000-01-01T00:00:59+1200" },
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(31), null, null,
                    new String[] { "2000-01-01T00:00:59.500+0000" },
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(32), null, null,
                    new String[] { "2000-01-01T00:00:59.500+1200" },
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(33), null, null,
                    new String[] { "2000-01-01T00:00:59.000+0000" },
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(34), null, null,
                    new String[] { "2000-01-01T00:00:59.000+1200" },
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(35), null, new Interval(
                    dateTime("2000-01-01T00:00:00"),
                    dateTime("2000-01-02T00:00:00")), null,
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(36), null,
                    lessThan(dateTime("2000-01-01T00:00:00")), null,
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(37), null,
                    lessEqual(dateTime("2000-01-01T00:00:00")), null,
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(38), null,
                    greaterThan(dateTime("2000-01-01T00:00:00")), null,
                    "2006-03-31T01:12:00");

            assertCDateTime((CSingleAttribute)a.getAttributes().get(39), null,
                    greaterEqual(dateTime("2000-01-01T00:00:00")), null,
                    "2006-03-31T01:12:00");
        }
        private Interval greaterThan(java.lang.Comparable value)
        {
            return new Interval(value, null, false, false);
        }

        private Interval greaterEqual(java.lang.Comparable value)
        {
            return new Interval(value, null, true, false);
        }

        private Interval lessThan(java.lang.Comparable value)
        {
            return new Interval(null, value, false, false);
        }

        private Interval lessEqual(java.lang.Comparable value)
        {
            return new Interval(null, value, false, true);
        }

        // private java.util.List attributeList;

    }
}
