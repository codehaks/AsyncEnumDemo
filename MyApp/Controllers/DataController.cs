using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    public class DataController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public DataController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<IActionResult> Upload(IFormFile file)
        {
            var counter = 1;

            await foreach (var line in ReadTextFile(file, _env))
            {
                if (line.Trim()!="hosein")
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            return Ok($"hosein found at line {counter}");
        }

        private async IAsyncEnumerable<string> ReadTextFile(IFormFile file, IWebHostEnvironment env)
        {
            using StreamReader reader = new StreamReader(file.OpenReadStream());
      
            while (reader.Peek() >= 0)
            {
                yield return await reader.ReadLineAsync();
            }

        }
    }
}