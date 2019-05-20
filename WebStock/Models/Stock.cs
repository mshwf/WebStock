using System;
using WebStock.Helpers;

namespace WebStock.Models
{
    public class StockPriceEventArgs : EventArgs
    {
        public StockPriceEventArgs(decimal newPrice)
        {
            NewPrice = newPrice;
        }
        public decimal NewPrice { get; }
    }
    public class Stock
    {

        public delegate void StockPriceHandler(object stock, StockPriceEventArgs myMEA);
        public event StockPriceHandler StockPriceChanged;
        private decimal price;
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    StockPriceChanged?.Invoke(this, new StockPriceEventArgs(price));
                }
            }
        }

        public void UpdatePrice()
        {
            Random random = new Random();
            int index = random.Next(0, Utils.Range.Length - 1);
            Price += Utils.Range[index];
        }
    }
}
