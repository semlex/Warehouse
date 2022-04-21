using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    class SupplyOrder : Order // Класс для заказа на поставку продуктов
    {
        private int supplyTime; // Оставшееся время доставки
        public SupplyOrder(int _supplyTime, List<Product> _products) : base(_products)
        {
            supplyTime = _supplyTime;
        }
        public int SupplyTime // Получить оставшееся время доставки
        {
            get { return supplyTime; }
            set { supplyTime = value; }
        }
        public override void show() // Вывод на консоль
        {
            Console.WriteLine("Через {0} дней", supplyTime);
            products.ForEach(product => product.show());
        }
    }
}
