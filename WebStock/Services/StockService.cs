using System;
using System.Collections.Generic;
using System.Linq;
using WebStock.Models;

namespace WebStock.Services
{
    public class StockService
    {
        private readonly IEnumerable<Stock> _stocks;
        readonly int count;
        Random random;
        public StockService(IEnumerable<Stock> stocks)
        {
            _stocks = stocks;
            count = _stocks.Count();
            random = new Random();
        }

        public void UpdateBatch()
        {
            foreach (var stock in _stocks)
            {
                stock.UpdatePrice();
            }
        }

        public void UpdateRandom()
        {
            var idx = random.Next(0, count);
            _stocks.ElementAt(idx).UpdatePrice();
        }
    }
}
