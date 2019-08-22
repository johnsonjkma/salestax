using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesTax
{
    class Program
    {

        static void Main(string[] args)
        {
            IProductFactory productFactory = new ProductFactory();
            List<Product> cartItems = new List<Product>();

            while (true)
            {
                Console.WriteLine("----------------- Shopping Cart -------------");
                Console.WriteLine("Total Items: {" + cartItems.Sum(item => item.Quantity) + "}");
                Console.WriteLine("");
                Console.WriteLine("Add Items to Cart");
                Console.WriteLine("");

                Console.WriteLine("Name : ");
                var name = Console.ReadLine();

                Console.WriteLine("Price : ");
                var price = Console.ReadLine();
                try
                {
                    Decimal.Parse(price);
                }
                catch
                {
                    throw new Exception("Invalid price");
                }

                Console.WriteLine("Quantity : ");
                var quantity = Console.ReadLine();
                try
                {
                    Int32.Parse(quantity);
                }
                catch
                {
                    throw new Exception("Invalid quantity");
                }

                Console.WriteLine("Is imported : (Y/N) [any other key except y/Y is considered as N]");
                var isImported = Console.ReadLine().ToLower() == "y" ? true : false;

                Console.WriteLine("Type of Item (select one of below)\n1. Book\n2. Food\n3. Medical\n4. Other");
                var gTypeValues = Enum.GetValues(typeof(GoodsType)).Cast<int>().Select(x => x.ToString()).ToArray();

                var gType = Console.ReadLine();
                if (!gTypeValues.Contains(gType))
                {
                    throw new Exception("Invalid type");
                }
                var goodsType = (GoodsType)Enum.Parse(typeof(GoodsType), gType);

                Console.WriteLine("Do you want to add more items? (Y/N) [any other key except y/Y is considered as N]");
                var input = Console.ReadLine().ToLower();

                var product = productFactory.GetProduct(name, Convert.ToDecimal(price), Convert.ToInt32(quantity), goodsType, isImported);
                Product foundItem = cartItems.Find(x => (x.Name == name && x.Price == product.Price));
                if (foundItem != null)
                {
                    foundItem.Quantity = foundItem.Quantity + product.Quantity;
                }
                else
                {
                    cartItems.Add(product);
                }

                if (input == "y")
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("\n");
            Console.WriteLine("Your Billing details are below");
            Console.WriteLine("------- Date : " + DateTime.Now.ToString("dd/MM/yyyy") + " -------");
            Console.WriteLine("");

            foreach (var item in cartItems)
            {
                Console.Write(item.Name);
                Console.Write("          "
                            + item.getItemTotal().ToString("C"));
                if (item.Quantity > 1)
                {
                    Console.Write(" (" + item.Quantity + " @ " + item.getPrice() + ") ");
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("\n");

            Console.WriteLine("----------------- Total Items : {" + cartItems.Sum(item => item.Quantity) + "} -----------------\n");
            Console.WriteLine("Tax : " + cartItems.Sum(item => (item.CalculateItemTax() * item.Quantity)).ToString("C"));
            Console.WriteLine("Total : " + cartItems.Sum(item => item.getItemTotal()).ToString("C"));

            Console.ReadLine();
        }
    }
}
