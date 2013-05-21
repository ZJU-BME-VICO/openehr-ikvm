using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.openehr.rm.datatypes.text;
using org.openehr.rm.common.resource;
namespace openehr_net_ikvm.adl_parser
{
    /// <summary>
    /// Summary description for ArchetypeDescriptionTest
    /// </summary>
    [TestClass]
    public class ArchetypeDescriptionTest
    {
        public ArchetypeDescriptionTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void testParseFullArchetypeDescription()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_description.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            org.openehr.rm.common.resource.ResourceDescription description = archetype.getDescription();

            java.util.Map originalAuthor = description.getOriginalAuthor();
            Assert.AreEqual("Sam Heard", originalAuthor.get("name").ToString(), "name wrong");
            Assert.AreEqual("Ocean Informatics", originalAuthor.get("organisation").ToString(), "organisation wrong");
            Assert.AreEqual("23/04/2006", originalAuthor.get("date").ToString(), "date wrong");
            Assert.AreEqual("sam.heard@oceaninformatics.biz", originalAuthor.get("email").ToString(), "email wrong");
           
           // List<String> otherContributors = description.getOtherContributors();
            Assert.IsNotNull(description.getOtherContributors());
            Assert.AreEqual(1, description.getOtherContributors().size());
            Assert.AreEqual("Ian McNicoll, MD", description.getOtherContributors().get(0));

            Assert.AreEqual("AuthorDraft", description.getLifecycleState(),"lifecycleState wrong");

            Assert.AreEqual( "www.aihw.org.au/data_sets/diabetic_archetypes.html",description.getResourcePackageUri(),"resourcePackageUri");

            //Map<String, String> map = description.getOtherDetails();
            Assert.AreEqual("details 1", description.getOtherDetails().get("other 1"));
            Assert.AreEqual("details 2", description.getOtherDetails().get("other 2"));
            //List<ResourceDescriptionItem> details = description.getDetails();
            Assert.IsNotNull(description.getDetails());
            Assert.AreEqual(1, description.getDetails().size(),"details size wrong");

            ResourceDescriptionItem item = (ResourceDescriptionItem)description.getDetails().get(0);
            Assert.IsNotNull(description.getDetails().get(0));
            CodePhrase language = new CodePhrase("ISO_639-1", "en");
           Assert.AreEqual(language, item.getLanguage(),"language wrong");

           Assert.AreEqual(
                "For recording a problem, condition or"
                        + " issue that has ongoing significance to the person's health.",
                item.getPurpose(),"purpose wrong");
              

           Assert.AreEqual( "Used for recording any problem, present or"
                   + " past - so is used for recording past history as well as "
                   + "current problems. Used with changed 'Subject of care' for "
                   + "recording problems of relatives and so for family history.",
                   item.getUse(),"use wrong");

           Assert.AreEqual( "Use specialisations for medical "
                   + "diagnoses, 'openEHR-EHR-EVALUATION.problem-diagnosis' and "
                   + "histological diagnoses 'openEHR-EHR-EVALUATION.problem-"
                   + "diagnosis-histological'", item.getMisuse(),"misuse wrong");

           Assert.AreEqual("copyright (c) 2004 The openEHR "+ "Foundation", item.getCopyright(),"copyright wrong");
          
            List<String> keywords = new List<String>();
           keywords.Add("issue");
           keywords.Add("condition");
           Assert.AreEqual( keywords[0], item.getKeywords().get(0),"keywords wrong");
           Assert.AreEqual(keywords[1], item.getKeywords().get(1), "keywords wrong");
           ResourceDescriptionItem b = (ResourceDescriptionItem)description.getDetails().get(0);
           //map = description.getDetails().get(0).getOriginalResourceUri();
           Assert.AreEqual("http://guidelines.are.us/wherever/fr",
                   b.getOriginalResourceUri().get("ligne guide"));
           Assert.AreEqual("http://some%20medline%20ref", b.getOriginalResourceUri().get("medline"));
           
          // map =b.getOtherDetails();
           Assert.AreEqual("item details 1", b.getOtherDetails().get("item other 1"));
           Assert.AreEqual("item details 2", b.getOtherDetails().get("item other 2"));
        }

        [TestMethod]
        public void testParseOriginalAuthorAsLast()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.archetype_description2.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);

            try
            {
                org.openehr.am.archetype.Archetype archetype = parser.parse();
                Assert.IsNotNull(archetype);
            }
            catch (Exception e)
            {
                string a = e.Message;
                Console.Write(a);
                // fail("failed to parse mixed node types");
            }
        }

        [TestMethod]
        public void testParseEmptyOtherContributors()
        {
            string adl = System.IO.File.ReadAllText(@"..\..\..\..\java-libs\adl-parser\src\test\resources\adl-test-entry.empty_other_contributors.test.adl");

            se.acode.openehr.parser.ADLParser parser = new se.acode.openehr.parser.ADLParser(adl);
            org.openehr.am.archetype.Archetype archetype = parser.parse();
            Assert.IsNotNull(archetype);

          
            Assert.IsNull( archetype.getDescription().getOtherContributors());
        }

    }
}
