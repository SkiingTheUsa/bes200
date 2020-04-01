using System;
using CacheCow.Client;
using CacheCow.Client.RedisCacheStore;

namespace ApiClient
{
    public class Program
    {
        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            var client = new RedisStore("localhost:6379").CreateClient();
            client.BaseAddress = new Uri("http://localhost:1337");
            while (true)
            {
                var response = client.GetAsync("/time").Result;
                var data = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(data);
                Console.WriteLine(response.Headers.CacheControl.ToString());
                if (Console.ReadLine() == "done") break;
            }
        }
    }
}
