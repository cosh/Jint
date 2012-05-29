using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Jint.Delegates;
using System.IO;
using System.Reflection;

namespace Jint.Tests {
    /// <summary>
    /// Summary description for SunSpider
    /// </summary>
    [TestClass, Ignore]
    public class SunSpider {
        public SunSpider() {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
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
        public void TestPerformance() {
            string[] tests = { "3d-cube", "3d-morph", "3d-raytrace", "access-binary-trees", "access-fannkuch", "access-nbody", "access-nsieve", "bitops-3bit-bits-in-byte", "bitops-bits-in-byte", "bitops-bitwise-and", "bitops-nsieve-bits", "controlflow-recursive", "crypto-aes", "crypto-md5", "crypto-sha1", "date-format-tofte", "date-format-xparb", "math-cordic", "math-partial-sums", "math-spectral-norm", "regexp-dna", "string-base64", "string-fasta", "string-tagcloud", "string-unpack-code", "string-validate-input" };

            var assembly = Assembly.GetExecutingAssembly();
            Stopwatch sw = new Stopwatch();

            foreach (var test in tests) {
                string script;

                try {
                    script = new StreamReader(assembly.GetManifestResourceStream("Jint.Tests.SunSpider." + test + ".js")).ReadToEnd();
                    if (String.IsNullOrEmpty(script)) {
                        continue;
                    }
                }
                catch {
                    Console.WriteLine("{0}: ignored", test);
                    continue;
                }

                JintEngine jint = new JintEngine()
                    //.SetDebugMode(true)
                    .DisableSecurity();

                sw.Reset();
                sw.Start();

                jint.Run(script);

                Console.WriteLine("{0}: {1}ms", test, sw.ElapsedMilliseconds);
            }
        }
    }
}
