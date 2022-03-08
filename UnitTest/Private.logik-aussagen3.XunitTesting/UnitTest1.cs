using System;
using Xunit;
using Private.logik_aussagen;
using Private.logik_aussagen.converter;


namespace Private.logik_aussagen3.XunitTesting
{
    //UnitTest 
    public class UnitTest1
    {
        public YesNoToBoolConverter bC = new YesNoToBoolConverter();

        [Fact]
        public void Test1()
        {            
            object res = bC.ConverterBackUT(bC.ConverterUT("True & False"));

            Assert.Equal("False", res);    
        }

        [Fact]
        public void Test2()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("( True | False )"));
            Assert.Equal("True", res);
        }
        [Fact]
        public void Test3()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("( True | False ) & True"));
            Assert.Equal("True", res);
        }
        [Fact]
        public void Test4()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("( True & False )"));
            Assert.Equal("False", res);
        }
        [Fact]
        public void Test5()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("( True | False ) & True & True &   True |   False"));
            Assert.Equal("True", res);
        }

        [Fact]
        public void Test6()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("( True | False ( & True & True &   True |   False"));
            Assert.Equal("false Syntax => falsche Klammersetzung", res);
        }
        [Fact]
        public void Test7()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("( True | False (() & True & True &   True |   False"));
            Assert.Equal("false Syntax => falsche Klammersetzung", res);
        }
        [Fact]
        public void Test8()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("( True | False ) & True & True &   True |   False &        True"));
            Assert.Equal("True", res);
        }
        //Test XOr
        [Fact]
        public void Test_XOR1()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("True ^ False"));
            Assert.Equal("True",res);
        }

        //Teste Implikation
        [Fact]
        public void Test_Imp1()
        {
            bool res = bC.implies(true, true);
            Assert.True(res);
        }
        [Fact]
        public void Test_Imp2()
        {
            bool res = bC.implies(true, false);
            Assert.False(res);
        }
        [Fact]
        public void Test_Imp3()
        {
            bool res = bC.implies(false, true);
            Assert.True(res);
        }
        [Fact]
        public void Test_Imp4()
        {
            bool res = bC.implies(false, false);
            Assert.True(res);
        }
        //Test Äquivalenz
        [Fact]
        public void Test_Eql1()
        {
            bool res = bC.equivalence(true, true);
            Assert.True(res);
        }
        [Fact]
        public void Test_Eql2()
        {
            bool res = bC.equivalence(false, true);
            Assert.False(res);
        }
        [Fact]
        public void Test_Eql3()
        {
            bool res = bC.equivalence(true, false);
            Assert.False(res);
        }
        [Fact]
        public void Test_Eql4()
        {
            bool res = bC.equivalence(false, false);
            Assert.True(res);
        }

        //Test IMP in Converter
        [Fact]
        public void TestImpC1()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("True imp True"));
            Assert.Equal("True", res);
        }
        [Fact]
        public void TestImpC2()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("True imp False"));
            Assert.Equal("False", res);
        }
        [Fact]
        public void TestImpC3()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("( True imp True ) eql True"));
            Assert.Equal("True", res);
        }
        [Fact]
        public void TestGesamt()
        {
            object res = bC.ConverterBackUT(bC.ConverterUT("True imp True eql true ^ false"));
            Assert.Equal("True", res);
        }
        



    }
}
