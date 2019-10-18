using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MyApp.Hubs;

namespace MyApp.Controllers
{
    public class DataController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHubContext<ChartHub, IChartHub> _chartHub;

        public DataController(IWebHostEnvironment env, IHubContext<ChartHub, IChartHub> chartHub)
        {
            _env = env;
            _chartHub = chartHub;
        }


        public IActionResult Index()
        {
            return Ok("ok");
        }
        public async Task<IActionResult> Upload(IFormFile file)
        {
            await foreach (var line in ReadLines(file))
            {           
                var data = line.Split(',');
                var date = data[0];
                var price = Convert.ToDouble(data[1]);
                await _chartHub.Clients.All.SendBitcoinData(date, price);
                await Task.Delay(2);
            }

            return Ok("Done");
        }

        private async IAsyncEnumerable<string> ReadLines(IFormFile file)
        {
            using StreamReader reader = new StreamReader(file.OpenReadStream());

            while (reader.Peek() >= 0)
            {
                yield return await reader.ReadLineAsync();
            }

        }
    }
}