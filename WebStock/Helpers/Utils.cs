using System.Collections.Generic;
using WebStock.Models;

namespace WebStock.Helpers
{
    public static class Utils
    {
        public static readonly decimal[] Range = new decimal[] { -3, -2.5m, -2, -1.5m, -1, -0.5m, 0, 0.5m, 1, 1.5m, 2, 2.5m, 3 };

        public static void UpdateBatch(IEnumerable<Stock> stocks)
        {
            foreach (var stock in stocks)
            {
                stock.UpdatePrice();
            }
        }
    }
}
