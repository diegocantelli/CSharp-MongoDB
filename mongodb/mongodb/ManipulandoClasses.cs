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
            livro.Título = "Teste";
            livro.Autor = "Autor Teste";
            livro.Ano = 2019;
            livro.Páginas = 259;
            livro.Assunto = new List<string> { "Ação", "Ficção", "Terror" };

            
            var mongoConn = new MongoDBConnection("mongodb://root:example@localhost:27017", "Biblioteca", "Livros");
            var database = mongoConn.Connect();
            var colection = database.GetCollection<Livro>("Livros");
            await colection.InsertOneAsync(livro);

            Console.WriteLine("Documento incluído!");
        }

        public static async Task ListandoDocumentosDaColecao()
        {
            var mongoConn = new MongoDBConnection("mongodb://root:example@localhost:27017", "Biblioteca", "Livros");
            var database = mongoConn.Connect();
            var colection = database.GetCollection<Livro>("Livros");

            // Ao passar new BsonDocument(), significa que não será usado nenhum parâmetro de busca

            try
            {
                var livros = await colection.Find(new BsonDocument()).ToListAsync();

                foreach (var livro in livros)
                {
                    Console.WriteLine($"{livro.ToJson<Livro>()}");
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static async Task ListandoDocumentosDaColecaoPorFiltro()
        {
            var mongoConn = new MongoDBConnection("mongodb://root:example@localhost:27017", "Biblioteca", "Livros");
            var database = mongoConn.Connect();
            var colection = database.GetCollection<Livro>("Livros");

            // Ao passar new BsonDocument(), significa que não será usado nenhum parâmetro de busca

            try
            {
                var filtro = new BsonDocument()
                {
                    {"Título", "Livro teste" }
                };

                var livros = await colection.Find(filtro).ToListAsync();

                foreach (var livro in livros)
                {
                    Console.WriteLine($"{livro.ToJson<Livro>()}");
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static async Task ListandoDocumentosDaColecaoPorFiltroUsandoBuilder()
        {
            var mongoConn = new MongoDBConnection("mongodb://root:example@localhost:27017", "Biblioteca", "Livros");
            var database = mongoConn.Connect();
            var colection = database.GetCollection<Livro>("Livros");

            // Ao passar new BsonDocument(), significa que não será usado nenhum parâmetro de busca

            try
            {
                var construtor = Builders<Livro>.Filter;

                // Busca os livros cujo título seja igual ao parâmetro informado
                var condicao = construtor.Eq(x => x.Título, "Livro teste");

                // Gte -> Greater Than or equal
                var condicao2 = construtor.Gte(x => x.Ano, 2000);

                // Utilizando critérios de busca compostos
                var condicao3 = construtor.Gte(x => x.Ano, 2000) & construtor.Gte(x => x.Páginas, 50);

                // Realizando a busca dentro de um array existente no documento
                var condicao4 = construtor.AnyEq(x => x.Assunto, "Terror");

                var livros = await colection.Find(condicao).ToListAsync();

                foreach (var livro in livros)
                {
                    Console.WriteLine($"{livro.ToJson<Livro>()}");
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static async Task ListandoDocumentosDaColecaoOrdenados()
        {
            var mongoConn = new MongoDBConnection("mongodb://root:example@localhost:27017", "Biblioteca", "Livros");
            var database = mongoConn.Connect();
            var colection = database.GetCollection<Livro>("Livros");

            // Ao passar new BsonDocument(), significa que não será usado nenhum parâmetro de busca

            try
            {
                var construtor = Builders<Livro>.Filter;

                // Busca os livros cujo título seja igual ao parâmetro informado
                var condicao = construtor.Eq(x => x.Título, "Livro teste");

                // Ordenando os resultados com base no Autor
                var livros = await colection.Find(condicao)
                    .SortBy(x => x.Autor)
                    .Limit(2) // Retorna apenas os 2 primeiros resultados
                    .ToListAsync();

                foreach (var livro in livros)
                {
                    Console.WriteLine($"{livro.ToJson<Livro>()}");
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static async Task AlterandoDocumento()
        {
            var mongoConn = new MongoDBConnection("mongodb://root:example@localhost:27017", "Biblioteca", "Livros");
            var database = mongoConn.Connect();
            var colection = database.GetCollection<Livro>("Livros");

            try
            {
                var construtor = Builders<Livro>.Filter;

                // Busca os livros cujo título seja igual ao parâmetro informado
                var condicao = construtor.Eq(x => x.Título, "Livro teste");

                // Ordenando os resultados com base no Autor
                var livros = await colection.Find(condicao).ToListAsync();

                foreach (var livro in livros)
                {
                    livro.Ano = 2021;
                    livro.Páginas = 400;

                    // Substituindo os dados do livro com base na condição do filtro
                    await colection.ReplaceOneAsync(condicao, livro);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            Console.WriteLine("Documentos alterados com sucesso!");
        }

        public static async Task AlterandoDocumentoComBuilder()
        {
            var mongoConn = new MongoDBConnection("mongodb://root:example@localhost:27017", "Biblioteca", "Livros");
            var database = mongoConn.Connect();
            var colection = database.GetCollection<Livro>("Livros");

            try
            {
                var construtor = Builders<Livro>.Filter;

                // Busca os livros cujo título seja igual ao parâmetro informado
                var condicao = construtor.Eq(x => x.Título, "Livro teste");

                // Ordenando os resultados com base no Autor
                var livros = await colection.Find(condicao).ToListAsync();

                //Alterando um documento no mongoDB com utilizando builder
                var builderAlteracao = Builders<Livro>.Update;
                var condicaoAlteracao = builderAlteracao.Set(x => x.Ano, 2001);
                await colection.UpdateOneAsync(condicao, condicaoAlteracao);

                foreach (var livro in livros)
                {
                    livro.Ano = 2021;
                    livro.Páginas = 400;

                    // Substituindo os dados do livro com base na condição do filtro
                    await colection.ReplaceOneAsync(condicao, livro);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            Console.WriteLine("Documentos alterados com sucesso!");
        }

        public static async Task ExcluindoDocumentos()
        {
            var mongoConn = new MongoDBConnection("mongodb://root:example@localhost:27017", "Biblioteca", "Livros");
            var database = mongoConn.Connect();
            var colection = database.GetCollection<Livro>("Livros");

            try
            {
                var construtor = Builders<Livro>.Filter;

                // Busca os livros cujo título seja igual ao parâmetro informado
                var condicaoFiltro = construtor.Eq(x => x.Título, "Livro teste");

                // Ordenando os resultados com base no Autor
                var livros = await colection.Find(condicaoFiltro).ToListAsync();

                await colection.DeleteManyAsync(condicaoFiltro);
               
            }
            catch (Exception e)
            {

                throw;
            }

            Console.WriteLine("Documentos excluídos com sucesso!");
        }
    }
}
