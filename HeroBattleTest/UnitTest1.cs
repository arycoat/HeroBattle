using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeroBattle;
using System.Diagnostics;

namespace HeroBattleTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Trace.WriteLine("test trace");
            Debug.WriteLine("test debug");

            //
            Room room = new Room();
            room.Initialize();

            for (int i = 0; i < 100; i++)
                room.Update();
        }
    }
}
