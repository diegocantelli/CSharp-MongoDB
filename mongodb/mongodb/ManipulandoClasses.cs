using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mongodb
{
    public class ManipulandoClasses
    {
        public static async Task AsyncMain()
        {
            var livro = new Livro();
            livro.Titulo = "Teste";
            livro.Autor = "Autor Teste";
            livro.Ano = 2019;
            livro.Paginas = 259;
            livro.Assunto = new List<string> { "Ação", "Ficção", "Terror" };

            
            var mongoConn = new MongoDBConnection("mongodb://root:example@localhost:27017", "Biblioteca", "Livros");
            var colection = mongoConn.ConnectAndReturnCollection<Livro>();
            await colection.InsertOneAsync(livro);

            Console.WriteLine("Documento incluído!");
        }
    }
}
