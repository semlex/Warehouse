using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    class Warehouse : IShow
    {
        private List<ProductType> productTypes = new List<ProductType>(); // Виды товаров
        private List<StoredProduct> products = new List<StoredProduct>(); // Список продуктов
        private List<StoredProduct> discountProducts = new List<StoredProduct>(); // Уцененные продукты
        private List<DeliveryOrder> deliveryOrders = new List<DeliveryOrder>(); // Заказы на доставку
        private List<SupplyOrder> supplyOrders = new List<SupplyOrder>(); // Заказы на поставку
        private List<string> deliveryPoints = new List<string>(); // Точки доставки
        private int profit = 0; // Прибыль
        private int loss = 0; // Потери
        public Warehouse(List<Product> _products, List<ProductType> _productTypes, List<string> _deliveryPoints)
        {
            Console.WriteLine("Оптовый склад");
            Console.WriteLine("Виды товаров, которые в нем хранятся:");
            foreach (ProductType _productType in _productTypes)
            {
                Console.WriteLine("{0}", _productType.Name);
                productTypes.Add(_productType);
            }
            Console.WriteLine("\nБлизлежащие торговые точки:");
            foreach (string _deliveryPoint in _deliveryPoints)
            {
                Console.WriteLine("{0}", _deliveryPoint);
                deliveryPoints.Add(_deliveryPoint);
            }
            Console.WriteLine("\nНачальный набор продуктов на складе:");
            foreach (Product _product in _products)
            {
                Console.WriteLine("{0} {1} оптовых упаковок", _product.Name, _product.Count);
                int price = productTypes.Find(productType => productType.Name == _product.Name).Price;
                int shelfLife = productTypes.Find(productType => productType.Name == _product.Name).ShelfLife;
                products.Add(new StoredProduct(_product.Name, _product.Count, shelfLife, price));
            }
        }
        public int Profit // Получить прибыль
        {
            get { return profit; }
        }
        private int productCount(string name) // Получить количество товара определенного вида на складе
        {
            int count = 0;
            products.FindAll(product => product.Name == name).ForEach(product => {
                count += product.Count;
            });
            discountProducts.FindAll(discountProduct => discountProduct.Name == name).ForEach(discountProduct => {
                count += discountProduct.Count;
            });
            return count;
        }
        private void makeSupplyOrder() // Сделать заказ на поставку товаров
        {
            List<Product> productsToSupply = new List<Product>();
            productTypes.ForEach(productType =>
            {
                if(!supplyOrders.Exists(supplyOrder => supplyOrder.Products.Exists(product => product.Name == productType.Name)))
                {
                    int totalCount = productCount(productType.Name);
                    if (totalCount < productType.MaxCount * 0.25)
                    {
                        productsToSupply.Add(new Product(productType.Name, productType.MaxCount - totalCount));
                    }
                }
            });
            if(productsToSupply.Count > 0)
                supplyOrders.Add(new SupplyOrder(new Random().Next(1, 5), productsToSupply));
        }
        private void updateSupplies() // Обновить время поставки товаров
        {
            supplyOrders.ForEach(supplyOrder => supplyOrder.SupplyTime--);
        }
        private void deliverSupplies() { // Доставить поставляемые товары

            supplyOrders.ForEach(supplyOrder =>
            {
                if (supplyOrder.SupplyTime == 0)
                {
                    supplyOrder.Products.ForEach(product =>
                    {
                        int price = productTypes.Find(productType => productType.Name == product.Name).Price;
                        int shelfLife = productTypes.Find(productType => productType.Name == product.Name).ShelfLife;
                        products.Add(new StoredProduct(product.Name, product.Count, shelfLife, price));
                    });
                }
            });
            supplyOrders.RemoveAll(supplyOrder => supplyOrder.SupplyTime == 0);
        }
        private void makeDeliveryOrders() // Сделать заказ на доставку
        {
            List<Product> _products = new List<Product>();
            deliveryPoints.ForEach(deliveryPoint =>
            {
                _products.Clear();
                productTypes.ForEach(productType =>
                {
                    if (new Random().Next(2) == 1)
                    {
                        int count = new Random().Next(1, productType.MaxCount / 8);
                        _products.Add(new Product(productType.Name, count));
                    }
                });
                deliveryOrders.Add(new DeliveryOrder(deliveryPoint, _products));
            });
        }
        private void deliver() // Доставить товары в точки доставки
        {
            StoredProduct product;
            deliveryOrders.ForEach(deliveryOrder =>
            {
                deliveryOrder.Products.ForEach(deliveringProduct =>
                {
                    product = discountProducts.Find(discountProduct => discountProduct.Name == deliveringProduct.Name);
                    if (product != null && product.Count >= deliveringProduct.Count)
                    {
                        profit += deliveringProduct.Count * product.Price;
                        product.Count -= deliveringProduct.Count;
                        deliveringProduct.Count = 0;
                    }
                    else
                    {
                        if (product != null)
                        {
                            profit += product.Count * product.Price;
                            deliveringProduct.Count -= product.Count;
                            product.Count = 0;
                        }
                        product = products.Find(discountProduct => discountProduct.Name == deliveringProduct.Name);
                        if (product != null && product.Count >= deliveringProduct.Count)
                        {
                            profit += deliveringProduct.Count * product.Price;
                            product.Count -= deliveringProduct.Count;
                            deliveringProduct.Count = 0;
                        }
                        else if (product != null)
                        {
                            profit += product.Count * product.Price;
                            deliveringProduct.Count -= product.Count;
                            product.Count = 0;
                        }
                    }
                });
            });
        }
        private void updateDeliveryOrders() // Удалить уже выполненные заказы на доставку
        {
            deliveryOrders.RemoveAll(deliveryOrder => deliveryOrder.Products.TrueForAll(deliveringProduct => deliveringProduct.Count == 0));
        }
        public void show() // Вывод на консоль
        {
            Console.WriteLine("\nТовары на складе:");
            products.ForEach(product => product.show());
            if(discountProducts.Count > 0)
            {
                Console.WriteLine("\nУцененные товары на складе:");
                discountProducts.FindAll(discountProduct => discountProduct.ShelfLife > 0).
                    ForEach(discountProduct => discountProduct.show());
            }
            if (deliveryOrders.Count > 0)
            {
                Console.WriteLine("\nСписок заказов:");
                deliveryOrders.ForEach(deliveryOrder => deliveryOrder.show());
            }
            if (supplyOrders.Count > 0)
            {
                Console.WriteLine("\nСписок поставок:");
                supplyOrders.ForEach(supplyOrder => supplyOrder.show());
            }
            if (discountProducts.Exists(discountProduct => discountProduct.ShelfLife == 0))
            {
                Console.WriteLine("\nПросроченные товары:");
                discountProducts.FindAll(discountProduct => discountProduct.ShelfLife == 0).ForEach(expiredProduct =>
                {
                    Console.WriteLine("{0}", expiredProduct.Name);
                    loss += expiredProduct.Price;
                });
                discountProducts.RemoveAll(discountProduct => discountProduct.ShelfLife == 0);
            }
            if (loss > 0)
                Console.WriteLine("\nПотери склада: {0} рублей", loss);
        }
        public void update() // Один шаг моделирования
        {
            discountProducts.ForEach(discountProduct =>
            {
                discountProduct.ShelfLife--;
            });
            products.ForEach(product =>
            {
                product.ShelfLife--;
                if (product.ShelfLife < 3)
                {
                    loss += product.Price / 2;
                    product.Price = product.Price / 2;
                    discountProducts.Add(product);
                }
            });

            products.RemoveAll(product => product.Count == 0 || product.ShelfLife < 3);

            updateSupplies();
            deliverSupplies();
            makeSupplyOrder();
            deliver();
            updateDeliveryOrders();
            makeDeliveryOrders();
        }
    }
}
