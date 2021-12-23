using System;

namespace mongodb
{
    class Program
    {
        static void Main(string[] args)
        {
            //var t = ManipulandoDocumentos.AsyncMain();
            var t = AcessandoMongoDB.AsyncMain();
            Console.ReadLine();
        }
    }
}
