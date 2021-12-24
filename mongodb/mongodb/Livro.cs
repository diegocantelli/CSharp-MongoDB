using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace mongodb
{
    public class Livro
    {
        //deve ser mapeado para o tipo string
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Ano { get; set; }
        public int Paginas { get; set; }
        public List<string> Assunto { get; set; }
    }
}
