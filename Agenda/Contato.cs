using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    public class Contato
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public Contato( string nome = "Sem Nome", string email = "Sem Email Definido", string telefone = "Nenhum Numero Definido") 
        {
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
        }

        public override string ToString()
        {
            return string.Format("{0}   -   {1} -   {2}", this.Nome, this.Email, this.Telefone);
        }
    }
}
