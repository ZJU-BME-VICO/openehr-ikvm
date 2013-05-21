using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class ParserTestBase
    {
        //public ParserTestBase(String test)
        //{
        //    super(test);
        //}
        public ParserTestBase()
        {
        }
        //protected java.io.InputStream loadFromClasspath(String adl)
        //{
        //    return (java.lang.Object)this.getClass().getClassLoader().getResourceAsStream(adl);
        //}
        // assert CComplexObject object has expected values
        public void assertCComplexObject(org.openehr.am.archetype.constraintmodel.CComplexObject obj, String rmTypeName,
                              String nodeID, org.openehr.rm.support.basic.Interval occurrences,
                              int attributes)
        {
            assertCObject(obj, rmTypeName, nodeID, occurrences);
            Assert.AreEqual(attributes, obj.getAttributes().size(), "attributes.size");
        }

        // assert CObject has expected valuess
        public void assertCObject(org.openehr.am.archetype.constraintmodel.CObject obj, String rmTypeName, String nodeID,
                       org.openehr.rm.support.basic.Interval occurrences)
        { 
            Assert.AreEqual(rmTypeName, obj.getRmTypeName(), "rmTypeName");
            Assert.AreEqual(nodeID, obj.getNodeId(), "nodeID");
            Assert.AreEqual(occurrences, obj.getOccurrences(), "occurrences");
        }

        // assert CAttribute has expected values
        public void assertCAttribute(org.openehr.am.archetype.constraintmodel.CAttribute attr, String rmAttributeName,
                          org.openehr.am.archetype.constraintmodel.CAttribute.Existence existence,
                          org.openehr.am.archetype.constraintmodel.Cardinality cardinality, int children)
        {
            Assert.AreEqual(rmAttributeName, attr.getRmAttributeName(), "rmAttributeName");
            Assert.AreEqual(existence, attr.getExistence(), "existence");
            if(attr is org.openehr.am.archetype.constraintmodel.CMultipleAttribute) 
            {
                org.openehr.am.archetype.constraintmodel.CMultipleAttribute mattr = (org.openehr.am.archetype.constraintmodel.CMultipleAttribute) attr;
                Assert.AreEqual(cardinality, mattr.getCardinality(), "cardinality");
            }
            Assert.AreEqual(children, attr.getChildren().size(), "children.size");
        }

        // assert CAttribute has expected values
        public void assertCAttribute(org.openehr.am.archetype.constraintmodel.CAttribute attr, String rmAttributeName,
                          int children)
        {
            assertCAttribute(attr, rmAttributeName, org.openehr.am.archetype.constraintmodel.CAttribute.Existence.REQUIRED,
                    null, children);
        }

        // assertion on primitive types
        public void assertCBoolean(Object obj, bool trueValid, bool falseValid,
    		bool assumed, bool hasAssumed)
        {
            org.openehr.am.archetype.constraintmodel.primitive.CBoolean b = (org.openehr.am.archetype.constraintmodel.primitive.CBoolean) fetchFirst(obj);
            Assert.AreEqual(trueValid, b.isTrueValid(), "trueValid");
            Assert.AreEqual(falseValid, b.isFalseValid(), "falseValid");
            Assert.AreEqual(assumed, b.assumedValue().booleanValue(), "assumed value");
            Assert.AreEqual(hasAssumed, b.hasAssumedValue(), "has assumed value");      
        }

        public void assertCInteger(Object obj, org.openehr.rm.support.basic.Interval interval, int[] values,
            java.lang.Integer assumed)
        {
            org.openehr.am.archetype.constraintmodel.primitive.CInteger c = (org.openehr.am.archetype.constraintmodel.primitive.CInteger) fetchFirst(obj);
            Assert.AreEqual(interval, c.getInterval(), "interval");
            Assert.AreEqual(intSet(values), c.getList(), "list");
            Assert.AreEqual(assumed, c.assumedValue(), "unexpected assumed value");
        }

        public void assertCReal(Object obj, org.openehr.rm.support.basic.Interval interval, double[] values,
            java.lang.Double assumed)
        {
            org.openehr.am.archetype.constraintmodel.primitive.CReal c = (org.openehr.am.archetype.constraintmodel.primitive.CReal) fetchFirst(obj);
            Assert.AreEqual(interval, c.getInterval(), "interval");
            Assert.AreEqual(doubleSet(values), c.getList(), "list");
            Assert.AreEqual(assumed, c.assumedValue(), "unexpected assumed value");
        }

        public void assertCDate(Object obj, String pattern, org.openehr.rm.support.basic.Interval interval,
                     String[] values, String assumed)
        {
            org.openehr.am.archetype.constraintmodel.primitive.CDate c = (org.openehr.am.archetype.constraintmodel.primitive.CDate) fetchFirst(obj);
            Assert.AreEqual(pattern, c.getPattern(), "pattern");
            Assert.AreEqual(interval, c.getInterval(), "interval");
            Assert.AreEqual(dateSet(values), c.getList(), "list");
            Assert.AreEqual(assumed == null? null :new org.openehr.rm.datatypes.quantity.datetime.DvDate(assumed), c.assumedValue(), "assumed value");
        }

        public void assertCDateTime(Object obj, String pattern, org.openehr.rm.support.basic.Interval interval,
                         String[] values, String assumed)
    	{
            org.openehr.am.archetype.constraintmodel.primitive.CDateTime c = (org.openehr.am.archetype.constraintmodel.primitive.CDateTime) fetchFirst(obj);
            
            Assert.AreEqual(pattern, c.getPattern(), "pattern");
            Assert.AreEqual(interval, c.getInterval(), "interval");
            Assert.AreEqual(dateTimeSet(values), c.getList(), "list");
            Assert.AreEqual(assumed == null? null :new org.openehr.rm.datatypes.quantity.datetime.DvDateTime(assumed), c.assumedValue(), "assumed value");
        }
    
        // without assumed value
        public void assertCDateTime(Object obj, String pattern, org.openehr.rm.support.basic.Interval interval,
            String[] values)
        {
            assertCDateTime(obj, pattern, interval, values, null);
        }


        public void assertCTime(Object obj, String pattern, org.openehr.rm.support.basic.Interval interval,
                     String[] values, String assumed) 
    	{
            org.openehr.am.archetype.constraintmodel.primitive.CTime c = (org.openehr.am.archetype.constraintmodel.primitive.CTime) fetchFirst(obj);
            Assert.AreEqual(pattern, c.getPattern(), "pattern");
            Assert.AreEqual(interval, c.getInterval(), "interval");
            Assert.AreEqual(timeSet(values), c.getList(), "list");
            Assert.AreEqual(assumed == null? null :new org.openehr.rm.datatypes.quantity.datetime.DvTime(assumed), c.assumedValue(), "assumed value");
        }
    
        // without assumed value
        public void assertCTime(Object obj, String pattern, org.openehr.rm.support.basic.Interval interval,
            String[] values)
        {
    	    assertCTime(obj, pattern, interval, values, null);
        }   
    
        // full assertion with CDuration
        public void assertCDuration(java.lang.Object obj, String value, org.openehr.rm.support.basic.Interval interval, 
    		String assumed, String pattern)
        {    
            org.openehr.am.archetype.constraintmodel.primitive.CDuration c = (org.openehr.am.archetype.constraintmodel.primitive.CDuration) ((org.openehr.am.archetype.constraintmodel.CPrimitiveObject) obj).getItem();
            Assert.AreEqual(value == null ? null : org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance(value),
                    c.getValue(), "list");
            Assert.AreEqual(interval, c.getInterval(), "interval");
            Assert.AreEqual(assumed == null? null 
        				    : org.openehr.rm.datatypes.quantity.datetime.DvDuration.getInstance(assumed), c.assumedValue(), "assumed value");
            Assert.AreEqual(pattern, c.getPattern(), "pattern wrong");
        }
    
        // without pattern
        public void assertCDuration(java.lang.Object obj, String value, org.openehr.rm.support.basic.Interval interval, 
    		String assumed)
        {
    	    assertCDuration(obj, value, interval, assumed, null);
        }
    
        // without assumed value, pattern
        public void assertCDuration(java.lang.Object obj, String value, org.openehr.rm.support.basic.Interval interval)
        {
    	    assertCDuration(obj, value, interval, null);
        }
    
        // fetch the first CPrimitive from the CAttribute
        public org.openehr.am.archetype.constraintmodel.primitive.CPrimitive fetchFirst(Object obj) 
        {
             return ( (org.openehr.am.archetype.constraintmodel.CPrimitiveObject) ( (org.openehr.am.archetype.constraintmodel.CAttribute) obj ).getChildren().get(0) ).getItem();
        }

        public void assertCString(Object obj, String pattern, String[] values, 
    		String assumedValue) 
        {
            org.openehr.am.archetype.constraintmodel.primitive.CString c = (org.openehr.am.archetype.constraintmodel.primitive.CString) ( (org.openehr.am.archetype.constraintmodel.CPrimitiveObject) ( (org.openehr.am.archetype.constraintmodel.CAttribute) obj ).getChildren().get(0) ).getItem();
            if (pattern == null) 
            {
                Assert.IsTrue(c.getPattern() == null, "pattern null");
            } 
            else 
            {
                Assert.AreEqual(pattern, c.getPattern(), "pattern");
            }
            Assert.AreEqual(values == null
                    ? null : java.util.Arrays.asList(values), c.getList(), "list");
            Assert.AreEqual(assumedValue,
        		    c.assumedValue(), "unexpected CString assumed value");
        }

        //void assertDateEquals(java.util.List set, String[] dates, String pattern)
        //{
        //    if (dates == null)
        //    {
        //        Assert.AreEqual(0, set.size(), "set not empty");
        //        return;
        //    }
        //    java.util.List setFromArray = new java.util.ArrayList();
        //    for (int i = 0; i < dates.Length; i++)
        //    {
        //        setFromArray.add(new java.text.SimpleDateFormat(pattern).parse(dates[i]));
        //    }
        //    Assert.IsTrue("set not equals array, expected: " + setFromArray
        //            + ", actual: " + set, set.equals(setFromArray));
        //}

        // methods for coversion of data types
        public java.util.List intSet(int[] values) 
        {
            if (values == null) 
            {
                return null;
            }
            java.util.List set = new java.util.ArrayList();
            for (int i = 0; i < values.Length; i++) 
            {
                set.add(new java.lang.Integer(values[i]));
            }
            return set;
        }

        public java.util.List doubleSet(double[] values) 
        {
            if (values == null) 
            {
                return null;
            }
            java.util.List set = new java.util.ArrayList();
            for (int i = 0; i < values.Length; i++) 
            {
                set.add(new java.lang.Double(values[i]));
            }
            return set;
        }

        // set of DvDate
        java.util.List dateSet(String[] values)
        {
            if (values == null) 
            {
                return null;
            }
            java.util.List set = new java.util.ArrayList();
            for (int i = 0; i < values.Length; i++) 
            {
                set.add(date(values[ i ]));
            }
            return set;
        }

        // set of DvDateTime
        java.util.List dateTimeSet(String[] values)
        {
            if (values == null) 
            {
                return null;
            }
            java.util.List set = new java.util.ArrayList();
            for (int i = 0; i < values.Length; i++) 
            {
                set.add(dateTime(values[ i ]));
            }
            return set;
        }

        // set of DvTime
        java.util.List timeSet(String[] values)
        {
            if (values == null) 
            {
                return null;
            }
            java.util.List set = new java.util.ArrayList();
            for (int i = 0; i < values.Length; i++) 
            {
                set.add(time(values[ i ]));
            }
            return set;
        }

        // convert with default pattern
        public org.openehr.rm.datatypes.quantity.datetime.DvDateTime dateTime(String value)
        {
            return new org.openehr.rm.datatypes.quantity.datetime.DvDateTime(value);
        }

        public org.openehr.rm.datatypes.quantity.datetime.DvTime time(String value)
        {
            return new org.openehr.rm.datatypes.quantity.datetime.DvTime(value);
        }

        public org.openehr.rm.datatypes.quantity.datetime.DvDate date(String value)
        {
            return new org.openehr.rm.datatypes.quantity.datetime.DvDate(value);
        }

        public org.openehr.rm.support.basic.Interval interval(int low, int up) 
        {
            return new org.openehr.rm.support.basic.Interval(new java.lang.Integer(low), new java.lang.Integer(up));
        }

        /* fields */
        static protected java.io.File dir = new java.io.File("res" + java.io.File.separator + "test");
    }
}
