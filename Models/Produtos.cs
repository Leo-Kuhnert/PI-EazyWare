using System;
using System.Collections.Generic;
using MySqlConnector;
using Microsoft.AspNetCore.Http;
namespace PI_EAZYWARE.Models
{
    public class Produtos
    {
      
      public int id {get; set;}
      public string nome {get; set;}
       public string descricao {get; set;}
      public double preco {get; set;}
      public int usuario {get; set;}
        
    }
}