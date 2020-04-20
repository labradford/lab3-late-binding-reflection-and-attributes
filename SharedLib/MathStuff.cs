using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib
{
    [SpecialClass(2)]
    public static class MathStuff
    {
        public static double RectangleArea(double length, double width)
        {
            return length * width;
        }

        public static double RectanglePerimeter(double length, double width)
        {
            return (length * 2) + (width * 2);
        }

        public static double CircleCircumference(double radius)
        {
            return Math.PI * (radius * 2);
        }

        public static double CircleArea(double radius)
        {
            return Math.PI * (radius * radius);
        }

        public static double PointDistance(double x1, double y1, double x2, double y2)
        {
            double exes = x2 - x1;
            double whys = y2 - y1;

            return Math.Sqrt((exes * exes) + (whys * whys));
        }
    }
}
