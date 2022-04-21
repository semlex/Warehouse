using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    class Product : IShow // Класс для товара
    {
        protected string name; // Название товара
        protected int count; // Количество товара
        public Product (string _name, int _count)
        {
            name = _name;
            count = _count;
        }
        public string Name // Получить название товара
        {
            get { return name; }
        }
        public int Count // Получить количество товара
        {
            get { return count; }
            set { count = value; }
        }

        public virtual void show() // Вывод на консоль
        {
            if (count > 0)
                Console.WriteLine("{0} {1} оптовых упаковок", name, count);
        }
    }
}
