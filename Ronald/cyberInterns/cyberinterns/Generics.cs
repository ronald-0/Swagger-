using System;
namespace cyberinterns
{
    public class Generics
    {
        public Generics()
        {
        }

        public void display<T>(T value)
        {
            Console.WriteLine(value);
        }

        public void show<T>(string msg,T value1)
        {
            Console.WriteLine("{0} : {1}",msg, value1);
        }
       
    }
}
