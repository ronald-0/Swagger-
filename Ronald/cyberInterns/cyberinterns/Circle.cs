using System;
namespace cyberinterns
{
    public class Circle
    {
        protected double radius;
        public Circle()
        {
            radius = 0.0;
        }

        public double Area => radius * radius * Math.PI;
    

        public double circumference
        {
            get {
                 return (radius * 2) * Math.PI;
            }
        }
    }
}
