using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aehyok.Dapper.GuideLine;

namespace aehyok.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        //编写获取指标定义
        [TestMethod]
        public void TestMethod1()
        {
            GuidelineAccessor.GetGuidelineDefine("7018951012565");
        }
    }
}
