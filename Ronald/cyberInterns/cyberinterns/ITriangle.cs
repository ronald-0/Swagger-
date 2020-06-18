using System;
namespace cyberinterns
{
    public interface ITriangle : IDisposable 
    { 
        double Area(int Height, int Breadth);
        double perimeter(int h, int b, int hy);
        int perimeter(int h, int b);
        double trigonometry(double sine, int hypothenus);
        double Pie();
    }
}
