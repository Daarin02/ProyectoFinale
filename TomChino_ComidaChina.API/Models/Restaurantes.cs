using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SQLite;
using System.Globalization;

namespace TomChino_ComidaChina.API.Models
{
    
    public class Restaurantes
    {
        
        public int id { get; set; }

        public string user { get; set; }

        public int Precio1 { get; set; }

        public int Precio2 { get; set; }

        public int Precio3 { get; set; }

        public int Total { get; set; }

    }

}
