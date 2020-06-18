using System;

namespace cyberinterns
{
    public class Triangle : ITriangle
    {
        public Triangle()
        {
        }

        public int Height { get; set; }
        public int Breadth { get; set; }
        public int Hypothenus{get; set;}

        public double Area(int Height, int Breadth)
        {
            return (Breadth / 2) * Height;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public double perimeter(int h, int b, int hy)
        {
            return h + b + hy;
        }

        public int perimeter(int h, int b)
        {
            return h + b;
        }

        public double Pie()
        {
            return Math.PI;
        }

        public double trigonometry(double sine, int hypothenus)
        {
            return Math.Sin(sine) * hypothenus;
        }

        public double calculate()
        {
            return 0.0;
        }
    }
}
