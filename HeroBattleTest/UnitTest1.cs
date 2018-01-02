using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeroBattle;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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

            for (int i = 0; i < 200; i++)
                room.Update();
        }

        [TestMethod]
        public void TestMinDistance()
        {
            List<Point> list = new List<Point>()
            {
                new Point(1, 1),
                new Point(2, 1),
                new Point(2, 0),
            };

            Point pivot = new Point(3, 1);

            Func<Point, Point, double> DistanceTo = (point1, point2) =>
            {
                var a = (double)(point2.X - point1.X);
                var b = (double)(point2.Y - point1.Y);

                return Math.Sqrt(a * a + b * b);
            };

            Point nearest = list
                .Aggregate((x, y) => DistanceTo(x, pivot) < DistanceTo(y, pivot) ? x : y);

            Debug.WriteLine(nearest);

            // sort by distance
            List<Point> sortedList = list
                .OrderBy(x => DistanceTo(x, pivot))
                .ToList();

            sortedList.ForEach(p => Debug.WriteLine(p));
        }

        [TestMethod]
        public void TestPhysics()
        {
            TestForm form = new TestForm();
            Application.Run(form);
        }
    }
}
