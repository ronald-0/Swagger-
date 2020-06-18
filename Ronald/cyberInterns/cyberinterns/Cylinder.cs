using System;
namespace cyberinterns
{
    public class Cylinder : Circle 
    {
       

        public double Height { get; set; }
        public Cylinder(double baseRadius = 0.0, double height = 0.0)
        {
            radius = baseRadius;
            Height = height;
        }

        public double Volume
        {
            get
            {
                return Area * Height;
            }
        }


    }
}
