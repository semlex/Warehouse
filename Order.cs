using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    abstract class Order : IShow // Абстрактный класс для заказа
    {
        protected List<Product> products = new List<Product>(); // Список продуктов
        
        public Order(List<Product> _products)
        {
            products = _products;
        }
        public List<Product> Products // Получить список продуктов
        {
            get { return products; }
        }

        public abstract void show(); // Вывод на консоль
    }
}
