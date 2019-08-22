using System;

namespace SalesTax
{

    public enum GoodsType
    {
        Book = 1,
        Food = 2,
        Medical = 3,
        Other = 4
    }

    public abstract class Product
    {
        public string Name { get; set; }
        public bool IsImported { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Type { get; set; }

        public Product(string name, bool isImported, decimal price, int quantity)
        {
            Name = name;
            IsImported = isImported;
            Price = price;
            Quantity = quantity;
        }

        public decimal getPrice()
        {
            decimal tax = 0;
            tax = CalculateItemTax();
            return (Price + tax);
        }

        public decimal getItemTotal()
        {
            return (getPrice() * Quantity);
        }

        public virtual decimal CalculateItemTax()
        {
            decimal tax = 0;

            if (IsImported == true)
            {
                tax = Convert.ToDecimal(Convert.ToDouble(Price) * 0.05);
                tax = Math.Ceiling(tax * 20) / 20;
            }

            return tax;
        }
    }

    public class Book : Product
    {
        public Book(string name, bool isImported, decimal price, int quantity)
            : base(name, isImported, price, quantity)
        {
        }
    }

    public class Medical : Product
    {
        public Medical(string name, bool isImported, decimal price, int quantity)
            : base(name, isImported, price, quantity)
        {
        }
    }

    public class Food : Product
    {
        public Food(string name, bool isImported, decimal price, int quantity)
            : base(name, isImported, price, quantity)
        {
        }
    }

    public class Others : Product
    {
        public Others(string name, bool isImported, decimal price, int quantity)
            : base(name, isImported, price, quantity)
        {
        }

        public override decimal CalculateItemTax()
        {
            decimal importedTax = 0;
            decimal tax = 0;

            if (IsImported == true)
            {
                importedTax = Convert.ToDecimal(Convert.ToDouble(Price) * 0.05);
            }

            var salesTax = Convert.ToDecimal(Convert.ToDouble(Price) * 0.1);
            tax = importedTax + salesTax;

            tax = Math.Ceiling(tax * 20) / 20;

            return tax;
        }

    }

}
