using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    class ProductType // Класс для вида товара
    {
        private string name; // Название товара
        private int shelfLife; // Срок годности
        private int price; // Цена
        private int maxCount; // Максимальное количество на складе
        public ProductType(string _name, int _price, int _shelfLife, int _maxCount)
        {
            name = _name;
            shelfLife = _shelfLife;
            price = _price;
            maxCount = _maxCount;
        }
        public string Name // Получить название
        {
            get { return name; }
        }
        public int Price // Получить цену
        {
            get { return price; }
        }
        public int ShelfLife // Получить срок годности
        {
            get { return shelfLife; }
        }
        public int MaxCount // Получить максимальное количество вида товара на складе
        {
            get { return maxCount; }
        }
    }
}
