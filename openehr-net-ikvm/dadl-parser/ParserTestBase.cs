using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using java.io;

namespace openehr_net_ikvm.dadl_parser
{
    [TestClass]
    public class ParserTestBase 
    {
        public ParserTestBase()
        {
        }
//         protected InputStream loadFromClasspath(String adl)
//         {
//             return this.getClass().getClassLoader().getResourceAsStream(adl);
//         }
        /* fields */
        static protected File dir = new File("res" + File.separator + "test");
      
    }
}
