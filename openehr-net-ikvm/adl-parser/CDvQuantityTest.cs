using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class CDvQuantityTest
    {
        //[TestMethod]
        public CDvQuantityTest()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.c_dv_quantity_full.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            archetype = parser.parse();
        }
        [TestMethod]
        public void testParseFullCDvQuantityStartsWithProperty()
        {
    	    node = archetype.node("/types[at0001]/items[at10005]/value");
    	    verifyCDvQuantityValue(node);      
        }
        [TestMethod]
        public void testParseFullCDvQuantityStartsWithList()
        {
    	    node = archetype.node("/types[at0001]/items[at10005]/value");
            verifyCDvQuantityValue(node);       
        }

        [TestMethod]
        public void testParseFullCDvQuantityStartsWithAssumedValue()
        {
    	    node = archetype.node("/types[at0001]/items[at10005]/value");
            verifyCDvQuantityValue(node);       
        }
    
        private void verifyCDvQuantityValue(org.openehr.am.archetype.constraintmodel.ArchetypeConstraint node) 
        {
    	    Assert.IsTrue(node is org.openehr.am.openehrprofile.datatypes.quantity.CDvQuantity, "CDvQuantity expected");
        
    	    org.openehr.am.openehrprofile.datatypes.quantity.CDvQuantity cdvquantity = (org.openehr.am.openehrprofile.datatypes.quantity.CDvQuantity) node;
        
            // verify property 
            org.openehr.rm.datatypes.text.CodePhrase property = cdvquantity.getProperty();
            Assert.IsNotNull(property, "property is null");
            Assert.AreEqual("openehr", property.getTerminologyId().name());
            Assert.AreEqual("128", property.getCodeString());
        
            // verify item list
            java.util.List list = cdvquantity.getList();
            Assert.AreEqual(2, list.size(), "unexpected size of list");
            java.lang.Double temp1 = new java.lang.Double(0.0);
            java.lang.Double temp2 = new java.lang.Double(200.0);
            java.lang.Integer temp3 = new java.lang.Integer(2);
            assertCDvQuantityItem((org.openehr.am.openehrprofile.datatypes.quantity.CDvQuantityItem)list.get(0), "yr", new org.openehr.rm.support.basic.Interval(temp1, temp2), new org.openehr.rm.support.basic.Interval(temp3, temp3));
            temp1 = new java.lang.Double(1.0);
            temp2 = new java.lang.Double(36.0);
            assertCDvQuantityItem((org.openehr.am.openehrprofile.datatypes.quantity.CDvQuantityItem)list.get(1), "mth", new org.openehr.rm.support.basic.Interval(temp1, temp2), new org.openehr.rm.support.basic.Interval(temp3, temp3));
        
            org.openehr.rm.support.measurement.MeasurementService ms = org.openehr.rm.support.measurement.SimpleMeasurementService.getInstance();
            org.openehr.rm.datatypes.quantity.DvQuantity expected = new org.openehr.rm.datatypes.quantity.DvQuantity("yr", new java.lang.Double(8.0), new java.lang.Integer(2), ms);
            Assert.AreEqual(expected, 
        		    cdvquantity.getAssumedValue(), "assumed value wrong");        
        }

        private void assertCDvQuantityItem(org.openehr.am.openehrprofile.datatypes.quantity.CDvQuantityItem item, String units,
    		    org.openehr.rm.support.basic.Interval magnitude, org.openehr.rm.support.basic.Interval precision) 
        {
            Assert.AreEqual(units, item.getUnits(), "unexpected units");
            Assert.AreEqual(magnitude, item.getMagnitude(), "unexpected magnitude interval");
            Assert.AreEqual(precision, item.getPrecision(), "unexpected precision interval");    	
        }

        [TestMethod]
        public void testParseCDvQuantityOnlyWithProperty()
        {
    	    string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.c_dv_quantity_property.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            archetype = parser.parse();
            Assert.IsNotNull(archetype);
         }

        [TestMethod]
        public void testParseCDvQuantityOnlyWithList()
        {
    	    string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.c_dv_quantity_list.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            archetype = parser.parse();
            Assert.IsNotNull(archetype);
        }

        [TestMethod]
        public void testParseCDvQuantityReversed()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.c_dv_quantity_reversed.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            archetype = parser.parse();
            Assert.IsNotNull(archetype);
    	 }

        [TestMethod]
        public void testParseCDvQuantityItemWithOnlyUnits()
        {
    	    string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.c_dv_quantity_item_units_only.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            archetype = parser.parse();
            Assert.IsNotNull(archetype);
        }

        [TestMethod]
        public void testParseEmptyCDvQuantity()
        {
    	    string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.c_dv_quantity_empty.test.adl");
            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            archetype = parser.parse();
            node = archetype.node("/types[at0001]/items[at10005]/value");
            Assert.IsTrue(node is org.openehr.am.openehrprofile.datatypes.quantity.CDvQuantity, "CDvQuantity expected");

            org.openehr.am.openehrprofile.datatypes.quantity.CDvQuantity cdvquantity = (org.openehr.am.openehrprofile.datatypes.quantity.CDvQuantity)node;
    	    Assert.IsNull(cdvquantity.getList());
            Assert.IsNull(cdvquantity.getProperty());
            Assert.IsNull(cdvquantity.getAssumedValue());
            Assert.IsTrue(cdvquantity.isAnyAllowed());
        }

        private org.openehr.am.archetype.Archetype archetype;
        private org.openehr.am.archetype.constraintmodel.ArchetypeConstraint node;
    }
}
