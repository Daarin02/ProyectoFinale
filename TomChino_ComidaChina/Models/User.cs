﻿using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomChino_ComidaChina.Models
{
    
    public class User
    {
        
        public string Username { get; set; }

        public string Password { get; set; }

     
    }
}
