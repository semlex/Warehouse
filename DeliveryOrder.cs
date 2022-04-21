using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    class DeliveryOrder : Order // Класс для заказа на доставку
    {
        private string deliveryPoint; // Точка доставки
        public DeliveryOrder(string _deliveryPoint, List<Product> _products) : base(_products)
        {
            deliveryPoint = _deliveryPoint;
        }
        public override void show() // Вывод на консоль
        {
            Console.WriteLine("{0}:" , deliveryPoint);
            products.ForEach(product => product.show());
        }
    }
}
