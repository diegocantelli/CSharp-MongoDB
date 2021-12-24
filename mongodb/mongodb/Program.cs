using System;

namespace mongodb
{
    class Program
    {
        static void Main(string[] args)
        {
            //var t = ManipulandoDocumentos.AsyncMain();
            //var t = AcessandoMongoDB.AsyncMain();
            //var t = ManipulandoClasses.ListandoDocumentosDaColecao();
            //var t = ManipulandoClasses.ListandoDocumentosDaColecaoPorFiltro();
            var t = ManipulandoClasses.ListandoDocumentosDaColecaoPorFiltroUsandoBuilder();
            Console.ReadLine();
        }
    }
}
