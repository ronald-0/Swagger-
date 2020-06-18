using System;
namespace cyberinterns
{
    public class StoreItems
    {
        public StoreItems()
        {
        }

        public int itemNumber;
        public string itemName;
        public string size;
        public double unitPrice;

        public void createItem()
        {
            itemNumber = 4562;
            itemName = "Hand Bag";
            size = "12";
            unitPrice = 2000;
        }

        public void Describe()
        {
            Console.WriteLine("Welcome to my store");

            Console.Write("Item Number # : ");
            Console.WriteLine(itemNumber);
            Console.Write("Item Name : ");
            Console.WriteLine(itemName);
            Console.Write("Item Size : ");
            Console.WriteLine(size);
            Console.Write("Item price # : ");
            Console.WriteLine(unitPrice);

        }

      
    }
}
