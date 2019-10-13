using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Hubs
{
    public class ChartHub:Hub<IChartHub>
    {
    }

    public interface IChartHub
    {
        Task SendBitcoinData(string date, double price);
    }
}
