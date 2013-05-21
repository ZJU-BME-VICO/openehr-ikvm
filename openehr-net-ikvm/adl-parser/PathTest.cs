using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using org.openehr.am.archetype;
using org.openehr.am.archetype.constraintmodel;

namespace openehr_net_ikvm.adl_parser
{
    [TestClass]
    public class PathTest
    {
        [TestMethod]
        public void setUp()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-car.paths.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            CComplexObject definition = archetype.getDefinition();
        }
        [TestMethod]
        public void testPath()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-car.paths.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            CComplexObject definition = archetype.getDefinition();
            // root path CAR
            Assert.AreEqual("/", definition.path());

            // wheels attribute
            CAttribute wheels = (CAttribute)definition.getAttributes().get(0);
            Assert.AreEqual("/wheels", wheels.path());

            // first WHEEL node
            CObject firstWheel = (CObject)wheels.getChildren().get(0);
            Assert.AreEqual("/wheels[at0001]", firstWheel.path());

            // description and parts of first WHEEL
            CComplexObject firstWheelObj = (CComplexObject)firstWheel;
            CAttribute description = (CAttribute)firstWheelObj.getAttributes().get(0);
            Assert.AreEqual("/wheels[at0001]/description", description.path());
            CAttribute parts = (CAttribute)firstWheelObj.getAttributes().get(1);
            Assert.AreEqual("/wheels[at0001]/parts", parts.path());

            // WHEEL_PART node
            CObject wheelParts = (CObject)parts.getChildren().get(0);
            Assert.AreEqual("/wheels[at0001]/parts[at0002]",
                    wheelParts.path());

            // something and something_else of WHEEL_PART node
            //CComplexObject wheelPartsObj = (CComplexObject)wheelParts;
            //Assert.AreEqual("something of WHEEL_PART",
            //        "/wheels[at0001]/parts[at0002]/something",
            //        wheelPartsObj.getAttributes().get(0).path());

            //Assert.AreEqual("something_else of WHEEL_PART",
            //        "/wheels[at0001]/parts[at0002]/something_else",
            //        wheelPartsObj.getAttributes().get(1).path());
        }
        [TestMethod]
        public void testNodeAtPath()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-car.paths.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);
            CComplexObject definition = archetype.getDefinition();
            String[] paths = {
            		"/", 
    			"/wheels[at0001]", 
    			"/wheels[at0001]/description",
    			"/wheels[at0001]/parts[at0002]",
    			"/wheels[at0001]/parts[at0002]/something",
    			"/wheels[at0001]/parts[at0002]/something_else",
    			"/wheels[at0003]", 
    			"/wheels[at0003]/description",
    			"/wheels[at0004]",
    			"/wheels[at0004]/description",
    			"/wheels[at0005]", 
    			"/wheels[at0005]/description"
    	};

            CAttribute wheels = (CAttribute)definition.getAttributes().get(0);
            CComplexObject wheel1 = ((CComplexObject)wheels.getChildren().get(0));
            CComplexObject wheel2 = ((CComplexObject)wheels.getChildren().get(1));
            CComplexObject wheel3 = ((CComplexObject)wheels.getChildren().get(2));
            CComplexObject wheel4 = ((CComplexObject)wheels.getChildren().get(3));
            CAttribute w = (CAttribute)wheel1.getAttributes().get(1);
            CComplexObject parts = (CComplexObject)w.getChildren().get(0);
            CAttribute pt = (CAttribute)parts.getAttributes().get(0);
            CPrimitiveObject pts = (CPrimitiveObject)pt.getChildren().get(0);
            CAttribute pt2 = (CAttribute)parts.getAttributes().get(1);
            CPrimitiveObject pts2 = (CPrimitiveObject)pt2.getChildren().get(0);

            CAttribute h1 = (CAttribute) wheel1.getAttributes().get(0);
            CPrimitiveObject p1 = (CPrimitiveObject)h1.getChildren().get(0);
             CAttribute h2 = (CAttribute)wheel2.getAttributes().get(0);
             CPrimitiveObject p2 = (CPrimitiveObject)h2.getChildren().get(0);
             CAttribute h3 = (CAttribute)wheel3.getAttributes().get(0);
             CPrimitiveObject p3 = (CPrimitiveObject)h3.getChildren().get(0);
             CAttribute h4 = (CAttribute)wheel4.getAttributes().get(0);
             CPrimitiveObject p4 = (CPrimitiveObject)h4.getChildren().get(0);

            CObject[] nodes = {
                definition, 
                wheel1,
              p1,
                parts,
                pts,
               pts2,
                wheel2,
               p2,
                wheel3,
                p3,
                wheel4,
                p4,    			
        };

            for (int i = 0; i < paths.Length; i++)
            {
                Assert.AreEqual(nodes[i], archetype.node(paths[i]));

            }

        }
    }
}