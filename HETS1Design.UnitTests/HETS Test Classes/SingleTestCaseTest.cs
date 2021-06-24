using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HETS1Design
{
    [TestClass]
    public class SingleTestCaseTest
    {
        SingleTestCase s1, s2, s3;

        [TestInitialize]
        public void Initialize()
        {
            Submissions.ResetSubmissions();
            TestCases.ResetTestCases();
            s1 = new SingleTestCase("7 6", "12", true);
            s2 = new SingleTestCase("__[Bound]3 5 10", "10", true);
            s3 = new SingleTestCase("__[EP]3 9 17", "19", true);
        }



        [TestMethod]
        public void SingleTestCase_CreatedSuccessfully()
        {
            Assert.IsNotNull(s1);
            Assert.IsNotNull(s2);
            Assert.IsNotNull(s3);
            Assert.IsTrue(s2.hasBoundInText);
            Assert.IsTrue(s3.hasEPInText);
        }

        [TestMethod]
        public void AppendTestCase_Appended()
        {
            //TODO
        }

        [TestMethod]
        public void CompareOutput_IsTrue()
        {
            var resultOutput = "12";
            var result = s1.CompareOutput(resultOutput);
            Assert.IsTrue(result);
        }

        //[TestMethod]
        //public void BoundaryScan_HasBound()
        //{
        //    It's a private method
        //}

        //[TestMethod]
        //public void EPScan_HasEP()
        //{
        //    It's a private method
        //}

        [TestMethod]
        public void ReturnBoundaryTestCases_Test()
        {
            //375,376,3687,6999,7000
            string input = "__[Bound] 375 7000";
            SingleTestCase sp1 = new SingleTestCase(input, "kokoriko", true);
            //foreach (SingleTestCase s in s1.ReturnBoundaryTestCases())
            //{
            //    System.Windows.Forms.MessageBox.Show("Input: " + s.input + " Output: " + s.output,"SingleTestCaseTest");
            //}
            List<SingleTestCase> list1 = sp1.ReturnBoundaryTestCases();
            Assert.AreEqual(5, list1.Count);
            Assert.AreEqual("375", list1[0].input);
            Assert.AreEqual("376", list1[1].input);
            Assert.AreEqual("3687", list1[2].input);
            Assert.AreEqual("6999", list1[3].input);
            Assert.AreEqual("7000", list1[4].input);

            input = "__[Bound] 7000 7000";
            SingleTestCase sp2 = new SingleTestCase(input, "kokoriko", true);
            List<SingleTestCase> list2 = sp2.ReturnBoundaryTestCases();
            Assert.AreEqual(1, list2.Count);
            Assert.AreEqual("7000", list2[0].input);

            input = "__[Bound] 7000 7001";
            SingleTestCase sp3 = new SingleTestCase(input, "kokoriko", true);
            List<SingleTestCase> list3 = sp3.ReturnBoundaryTestCases();
            Assert.AreEqual(2, list3.Count);
            Assert.AreEqual("7000", list3[0].input);
            Assert.AreEqual("7001", list3[1].input);

            input = "__[Bound] 9000 7000";
            SingleTestCase sp4 = new SingleTestCase(input, "kokoriko", true);
            var ex = Assert.ThrowsException<Exception>(()=>sp4.ReturnBoundaryTestCases());
        }


        [TestMethod]
        public void ReturnEPTestCases_Test()
        {
            //374F, 375T, 376T, 3687T, 6999T, 7000T, 7001F 
            string input = "9\r\n__[EP] 375 7000";
            SingleTestCase sp1 = new SingleTestCase(input, "kokoriko", true);
            List<SingleTestCase> list1 = sp1.ReturnEPTestCases();
            //foreach (SingleTestCase s in list1)
            //{
            //    System.Windows.Forms.MessageBox.Show("Input: \n" + s.input + " \nOutput: " + s.output + ", TC? " + s.equal, "SingleTestCaseTest");
            //}
            Assert.AreEqual(7, list1.Count);
            Assert.AreEqual("9\r\n374", list1[0].input);
            Assert.AreEqual("9\r\n375", list1[1].input);
            Assert.AreEqual("9\r\n376", list1[2].input);
            Assert.AreEqual("9\r\n3687", list1[3].input);
            Assert.AreEqual("9\r\n6999", list1[4].input);
            Assert.AreEqual("9\r\n7000", list1[5].input);
            Assert.AreEqual("9\r\n7001", list1[6].input);
            Assert.IsFalse(list1[0].equal);
            Assert.IsTrue(list1[1].equal);
            Assert.IsTrue(list1[2].equal);
            Assert.IsTrue(list1[3].equal);
            Assert.IsTrue(list1[4].equal);
            Assert.IsTrue(list1[5].equal);
            Assert.IsFalse(list1[6].equal);

            input = "__[EP] 7000 7000";
            SingleTestCase sp2 = new SingleTestCase(input, "kokoriko", true);
            List<SingleTestCase> list2 = sp2.ReturnEPTestCases();
            Assert.AreEqual(3, list2.Count);
            Assert.AreEqual("6999", list2[0].input);
            Assert.AreEqual("7000", list2[1].input);
            Assert.AreEqual("7001", list2[2].input);


            input = "__[EP] 7000 7001";
            SingleTestCase sp3 = new SingleTestCase(input, "kokoriko", false);
            List<SingleTestCase> list3 = sp3.ReturnEPTestCases();
            Assert.AreEqual(4, list3.Count);
            Assert.AreEqual("6999", list3[0].input);
            Assert.AreEqual("7000", list3[1].input);
            Assert.AreEqual("7001", list3[2].input);
            Assert.AreEqual("7002", list3[3].input);
            Assert.IsTrue(list3[0].equal);
            Assert.IsTrue(list3[3].equal);


            input = "__[EP] 9000 7000";
            SingleTestCase sp4 = new SingleTestCase(input, "kokoriko", true);
            var ex = Assert.ThrowsException<Exception>(() => sp4.ReturnEPTestCases());

            
        }
    }
}
