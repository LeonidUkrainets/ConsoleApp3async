using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace AsyncExample
{
    class Program
    {
        static async Task<int> GetPageSizeAsync(string url)
        {
            var httpClient   = new HttpClient();
            var response     = await httpClient.GetAsync(url);
            var pageContents = await response.Content.ReadAsStringAsync();
            Console.WriteLine(pageContents);
            return pageContents.Length;
        }
        static async Task Main(string[] args)
        {
            var urls = new List<string>
            {

                "https://www.example.com",
                "https://www.microsoft.com",
                "https://www.google.com"
              
            };
            var tasks = new List<Task<int>>();
            foreach (var url in urls)
            {
                tasks.Add(GetPageSizeAsync(url));
            }
            var totalSize = 0;
            while (tasks.Count > 0)
            {
                Task<int> finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                totalSize += await finishedTask;
            }
            Console.WriteLine($"Total page size: {totalSize} bytes");
        }
    }
}