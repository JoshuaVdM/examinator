using System;
using TextAnalyticsApi;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    class Program
    { 
        static void Main(string[] args)
        {
            LingDemo demo = new LingDemo();
            demo.Start();
            Console.ReadKey();
        }
    }
}