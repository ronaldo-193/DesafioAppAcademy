using System;
using System.Collections.Generic;
using System.Text;

namespace TesteAppAcademyChallenge
{
   public class Pessoa
    {
        public Pessoa() { }

        public string Nome { get; set; }
        public string Vaga { get; set; }
        public int Idade { get; set; }
        public string Estado { get; set; }

        public Pessoa(string nome,string vaga,int idade,string estado)
        {
            this.Nome = nome;
            this.Idade = idade;
            this.Vaga = vaga;
            this.Estado = estado;
        }
    }
}
