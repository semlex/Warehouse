using System;
using System.Collections.Generic;

namespace Warehouse
{
    class Program
    {
        static void Main()
        {
            int N, M, K, shelfLife, count, maxCount, price; // Период моделирования
            string name;
            List<ProductType> productTypes = new List<ProductType>(); // Виды продуктов
            List<Product> products = new List<Product>(); // Начальный набор продуктов
            List<string> deliveryPoints = new List<string>(); // Торговые точки
            string flag = "y";
            while (true)
            {
                Console.WriteLine("Введите количество видов товаров на складе (от 12 до 20):");
                M = Convert.ToInt32(Console.ReadLine());
                if (M > 11 && M < 21) break;
            }
            Console.WriteLine("Введите количество видов товаров на складе (от 12 до 20):");
            Console.WriteLine("Введите виды товаров на складе:");
            
            for (int i = 0; i < M; i++)
            {
                Console.WriteLine("Введите название товара:");
                name = Console.ReadLine();
                Console.WriteLine("Введите стоимость товара за одну оптовую пачку:");
                price = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите срок годности товара:");
                shelfLife = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите максимальное количество этого товара на складе:");
                maxCount = Convert.ToInt32(Console.ReadLine());
                productTypes.Add(new ProductType(name, price, shelfLife, maxCount));
            }

            while(true)
            {
                Console.WriteLine("Введите количество близлежащих торговых точек(от 3 до 9): ");
                K = Convert.ToInt32(Console.ReadLine());
                if (K > 2 && K < 10) break;
            }

            for (int i = 0; i < K; i++)
            {
                Console.WriteLine("Введите название торговой точки:");
                name = Console.ReadLine();
                deliveryPoints.Add(name);
            }

            Console.WriteLine("Введите начальный набор продуктов на складе:");

            while(true)
            {
                Console.WriteLine("Хотите продолжить (y/n)?");
                flag = Console.ReadLine();
                if (flag == "n") break;
                else if (flag != "y") continue;
                while(true)
                {
                    Console.WriteLine("Введите название товара: ");
                    name = Console.ReadLine();
                    if (productTypes.Exists(productType => productType.Name == name)) break;
                }
                while (true)
                {
                    Console.WriteLine("Введите количество товара: ");
                    count = Convert.ToInt32(Console.ReadLine());
                    if (productTypes.Find(productType => productType.Name == name).MaxCount >= count) break;
                }
                products.Add(new Product(name, count));
            }

            while(true)
            {
                Console.WriteLine("Введите период моделирования(от 10 до 30):");
                N = Convert.ToInt32(Console.ReadLine());
                if (N > 9 && N < 31) break;
            }

            Warehouse warehouse = new Warehouse(products, productTypes, deliveryPoints);
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("\n{0} день", i + 1);
                warehouse.update();
                warehouse.show();
            }
            Console.WriteLine("\n{0} стоимость проданных продуктов", warehouse.Profit);
        }
    }
}
