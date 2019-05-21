using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using WebStock.Helpers;
using WebStock.Models;
using WebStock.Services;
using WebStock.SignalrMan;

namespace WebStock.Pages
{
    public class IndexModel : PageModel
    {
        Timer _timer;
        IHubContext<StockHub> _context;
        StockService _stockService;
        public List<Stock> Stocks { get; set; }
        public string StocksData { get; set; }

        public IndexModel(IHubContext<StockHub> context)
        => _context = context;

        public void OnGet()
        {
            SeedData();
            _stockService = new StockService(Stocks);
            SetTimer();
        }
        private void SetTimer()
        {
            _timer = new Timer(new TimerCallback(Update));
            var period = TimeSpan.FromMilliseconds(1000);
            _timer.Change(TimeSpan.Zero, period);
        }

        private void SeedData()
        {
            Stocks = new List<Stock>
            {
                new Stock{Id = 1, Name= "Orascom", Price= 19.50m},
                new Stock{Id = 2, Name= "Raya", Price= -5.08m},
                new Stock{Id = 3, Name= "Petrojet", Price= 20.00m},
                new Stock{Id = 4, Name= "Wuzzuf", Price= 2.06m},
                new Stock{Id = 5, Name= "EZ DK", Price= 1.50m}
            };
            StocksData = JsonConvert.SerializeObject(Stocks);
            foreach (var stock in Stocks)
            {
                stock.StockPriceChanged += Item_StockPriceChanged;
            }
        }

        private async void Item_StockPriceChanged(object stock, StockPriceEventArgs e)
        {
            await _context.Clients.All.SendAsync("update", stock);
        }

        private void Update(object state)
        {
            _stockService.UpdateRandom();
        }

    }
}
