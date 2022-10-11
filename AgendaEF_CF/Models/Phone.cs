using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaEF_CF.Models
{
    internal class Phone
    {
        public int Id { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public int PersonId { get; set; }

        public override string ToString()
        {
            return $"Telefone Residêncial: {Telephone}\nCelular: {Mobile}\n";
        }
    }
}
