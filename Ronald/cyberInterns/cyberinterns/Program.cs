using System;

namespace cyberinterns
{
    class Program
    {
        enum PropertyType { Flat, Duplex, Bungalow, studio, warehouse }
        static void Main()
        {

            //int[] numbers = new int[5];

            //var number = new int[5] { 43, 62, 8, 9, 21 };

            //string[] name = new string[3];

            //numbers[0] = 23;
            //numbers[1] = 3;
            //numbers[2] = 4;
            //numbers[3] = 30;
            //numbers[4] = 90;

            //for (int i = 0; i < name.Length; i++)
            //{
            //    Console.WriteLine("Please enter value for element at position " + i.ToString());
            //    name[i] = Console.ReadLine();
            //}

            //for (int j = 0; j < name.Length; j++)
            //{
            //    Console.WriteLine("The element at position" + j.ToString() + " is " + name[j]);
            //}

            PropertyType proptype = PropertyType.Bungalow;

            Generics dis = new Generics();

            dis.display("Welcome to generics");

            dis.display(234);

            dis.display(proptype);

            dis.display(23.10);

            dis.show("Number", 45);
            dis.show("Property type", proptype);

            Console.WriteLine("The property type is" + proptype);

            //Triangle tri = new Triangle();

            //tri.calculate
            //tri.Breadth = 8;
            //tri.Hypothenus = 12;
            //tri.Height = 10;

            ////var area = tri.Area(tri.Height, tri.Breadth);
            //var perimeter = tri.perimeter(tri.Height, tri.Breadth);
            //var per = tri.perimeter(tri.Height, tri.Breadth, tri.Hypothenus);

            //Console.WriteLine("The area of of the triangle is " + area);
            //Console.WriteLine("The perimeter of of the triangle is " + perimeter);


            Console.WriteLine("Operations for Cylinder");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Enter Radius : ");

            double radius = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter Height : ");

            double height = double.Parse(Console.ReadLine());


            // initialise the cylinder class

            Cylinder cy = new Cylinder(radius, height);

            Console.WriteLine("Volume : " + cy.Volume.ToString());


            //StoreItems it = new StoreItems();
            //it.createItem();
            //it.Describe();

            Console.ReadLine();
        }
    }
}
