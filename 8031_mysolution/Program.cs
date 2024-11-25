using System;

namespace _8031_mysolution
{
    class Toy
    {
        private string name;
        private double price;

        public Toy(string name, double price)
        {
            this.name = name;
            this.price = price;
        }

        public virtual double CalculatePrice()
        {
            return price;
        }

        public override string ToString()
        {
            return $"{name} costs {CalculatePrice():F2}";
        }
    }

    class Doll : Toy //Acc = Accessories
    {
        private int numOfAcc;
        private double priceOfAcc;

        public Doll(string name, double price, int numOfAcc, double priceOfAcc)
            : base(name, price)
        {
            this.numOfAcc = numOfAcc;
            this.priceOfAcc = priceOfAcc;
        }

        public override double CalculatePrice()
        {
            return base.CalculatePrice() + (numOfAcc * priceOfAcc);
        }


        public override string ToString()
        {
            return $"{base.ToString()} with {numOfAcc} accessories";
        }
    }

    class TeddyBear : Toy
    {
        private string bearSize;

        public TeddyBear(string name, double price, string bearSize)
            : base(name, price)
        {
            this.bearSize = bearSize;
        }

        public override double CalculatePrice()
        {
            double sizeMultiplier = 1.0;

            if (bearSize == "LARGE")
                sizeMultiplier = 1.15;
            else if (bearSize == "MEDIUM")
                sizeMultiplier = 1.1;
            else if (bearSize == "SMALL")
                sizeMultiplier = 1.05;

            return base.CalculatePrice() * sizeMultiplier;

            // Modern switch expression alternative (commented out for reference):
            // double sizeMultiplier = bearSize switch
            // {
            //     "LARGE" => 1.15,
            //     "MEDIUM" => 1.1,
            //     "SMALL" => 1.05,
            //     _ => 1.0
            // };
            // return base.CalculatePrice() * sizeMultiplier;
        }

        public double CalculatePrice(double discount) //overload
        {
            return this.CalculatePrice() * (1 - discount / 100);
        }

        public override string ToString()
        {
            return $"{base.ToString()} (Size: {bearSize})";
        }

        public string GetBearSize()
        {
            return bearSize;
        }
    }

    class ToyStore
    {
        private string storeName;
        private Toy[] toys;
        private int counter; //how many toys we currently have

        public ToyStore(string storeName, int capacity)
        {
            this.storeName = storeName;
            toys = new Toy[capacity];
            counter = 0;
        }

        public void AddToy(Toy toy)
        {
            if (counter < toys.Length)
                toys[counter++] = toy;
            else
                Console.WriteLine("Store is full!");
        }

        public void PrintAllToys()
        {
            Console.WriteLine($"Toys in {storeName}:");
            for (int i = 0; i < counter; i++)
                Console.WriteLine(toys[i]);
        }

        public double CalculateTotalValue()
        {
            double totalValue = 0;
            for (int i = 0; i < counter; i++)
                totalValue += toys[i].CalculatePrice();
            return totalValue;
        }

        public int CountLargeTeddyBears()
        {
            int count = 0;
            for (int i = 0; i < counter; i++)
                if (toys[i] is TeddyBear teddy && teddy.GetBearSize() == "LARGE")
                    count++;
            return count;
        }

        public double CalculateTotalValue(double discount)
        {
            double totalValue = 0;

            for (int i = 0; i < counter; i++) // 'counter' is the current number of toys
            {
                Toy toy = toys[i]; // Explicitly declare the toy as a Toy object

                if (toy is TeddyBear)
                {
                    TeddyBear bear = (TeddyBear)toy; // Explicitly cast toy to TeddyBear
                    totalValue += bear.CalculatePrice(discount); // Use overloaded method for TeddyBear
                }
                else
                {
                    totalValue += toy.CalculatePrice(); // Use standard method for other toys
                }
            }

            return totalValue;
        }
    }

    class Program
    {
        static void Main()
        {
            ToyStore store = new ToyStore("Happy Toys", 10);

            Toy doll = new Doll("Barbie", 20.0, 3, 5.0);
            Toy teddyLarge = new TeddyBear("Teddy Large", 25.0, "LARGE");
            Toy teddySmall = new TeddyBear("Teddy Small", 15.0, "SMALL");

            store.AddToy(doll);
            store.AddToy(teddyLarge);
            store.AddToy(teddySmall);

            store.PrintAllToys();

            Console.WriteLine($"Total Value of Toys: {store.CalculateTotalValue():F2}");
            Console.WriteLine($"Total Value of Toys with 10% discount: {store.CalculateTotalValue(10):F2}");

            Console.WriteLine($"Number of Large Teddy Bears: {store.CountLargeTeddyBears()}");
        }
    }
}
