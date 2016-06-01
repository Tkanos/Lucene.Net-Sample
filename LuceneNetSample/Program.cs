using System;
using System.Linq;

namespace LuceneNetSample
{
    class Program
    {
        static void Main(string[] args)
        {
            LuceneService service = new LuceneService();

            var result = service.Search("Norrin").FirstOrDefault(); ;

            Console.WriteLine("Id : {0} \t\tName : {1}", result.Id, result.Name);

            Console.ReadKey();
        }
    }
}
