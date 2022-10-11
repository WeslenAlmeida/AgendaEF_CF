using AgendaEF_CF.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaEF_CF.Models
{
    internal class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string  Email { get; set; }
        public List<Phone> Phones  { get; set; }

      
        public override string ToString()
        {
            return $"Nome: {this.Name}\nEmail: {this.Email}\n";
        }
    }
}

        