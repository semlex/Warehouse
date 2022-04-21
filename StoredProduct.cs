using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    class StoredProduct : Product
    {
        private int shelfLife;
        private int price;
        public StoredProduct(string _name, int _count, int _shelfLife, int _price) : base(_name, _count)
        {
            shelfLife = _shelfLife;
            price = _price;
        }
        public int ShelfLife
        {
            get { return shelfLife; }
            set { shelfLife = value; }
        }
        public int Price
        {
            get { return price; }
            set { price = value; }
        }
        public override void show()
        {
            if (count > 0)
                Console.WriteLine("{0} {1} оптовых упаковок {2} рублей за упаковку срок годности: {3} дней", name, count, price, shelfLife);
        }
    }
}
