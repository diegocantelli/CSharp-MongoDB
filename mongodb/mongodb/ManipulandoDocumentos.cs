using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mongodb
{
    public class ManipulandoDocumentos
    {

        public static async Task AsyncMain() 
        {
            //Criando um documento
            var doc = new BsonDocument
            {
                {"Título", "Guerra dos Tronos" }
            };

            //Adicionand uma propriedade ao documento
            doc.Add("Autor", "George R R Martin");
            doc.Add("Ano", 1999);
            doc.Add("Páginas", 899);

            //Criando um array no json
            var assuntoArray = new BsonArray();
            assuntoArray.Add("Fantasia");
            assuntoArray.Add("Ação");

            //add o array ao documento principal
            doc.Add("Assunto", assuntoArray);

            Console.WriteLine(doc);
        }
    }
}
