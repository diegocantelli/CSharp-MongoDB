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
            //Criando um documento
            //var doc = new BsonDocument
            //{
            //    {"Título", "Guerra dos Tronos" }
            //};

            ////Adicionand uma propriedade ao documento
            //doc.Add("Autor", "George R R Martin");
            //doc.Add("Ano", 1999);
            //doc.Add("Páginas", 899);

            ////Criando um array no json
            //var assuntoArray = new BsonArray();
            //assuntoArray.Add("Fantasia");
            //assuntoArray.Add("Ação");

            ////add o array ao documento principal
            //doc.Add("Assunto", assuntoArray);

            var livro = new Livro();
            livro.Titulo = "Sob a Redoma";
            livro.Autor = "Stephen King";
            livro.Ano = 2017;
            livro.Paginas = 679;
            livro.Assunto = new List<string> { "Ação", "Ficção", "Terro" };

            var strConexao = "mongodb://root:example@localhost:27017";
            IMongoClient client = new MongoClient(strConexao);

            //Se não existir a base Biblioteca, esta será criada
            IMongoDatabase database = client.GetDatabase("Biblioteca");
            IMongoCollection<Livro> colecao = null;
            try
            {
                //Se não existir a coleção, esta será criada
                colecao = database.GetCollection<Livro>("Livros");
            }
            catch (Exception e)
            {
                throw;
            }

            try
            {
                await colecao.InsertOneAsync(livro);
            }
            catch (Exception e)
            {
                throw;
            }

            Console.WriteLine("Documento incluído!");
        }
    }
}
