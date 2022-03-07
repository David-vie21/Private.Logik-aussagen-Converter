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
        



    }
}
