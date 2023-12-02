using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace TomChino_ComidaChina.Models
{
    [Table("restaurantes")]
    public class Restaurantes
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int id { get; set; }

        public string user { get; set; }

        public int Precio1 { get; set; }

        public int Precio2 { get;set; }  

        public int Precio3 { get;set; }

        public int Total { get; set; }


    }

    
}
