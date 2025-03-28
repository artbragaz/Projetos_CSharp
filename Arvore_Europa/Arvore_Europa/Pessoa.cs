using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvore_Europa
{
    public class Pessoa
    {
        public string Nome { get; set; }
        private Pessoa Pai { get; set; }
        private Pessoa Conjuge { get; set; }
        private List<Pessoa> Filhos = new List<Pessoa>();

        public Pessoa(string nome)
        {
            this.Nome = nome;
        }
        public Pessoa(string nome, Pessoa pai)
        {
            this.Nome = nome;
            this.Pai = pai;
            pai.AdicionarFilho(this);
        }
        public void AdicionaConjuge(Pessoa nome)
        {
            this.Conjuge = nome;
        }
        public void AdicionarFilho(Pessoa nome)
        {
            Filhos.Add(nome);
        }
        public void ImprimeArvore(int nivel = 0)
        {
            if (Conjuge == null)
            {
                Console.WriteLine(new string(' ', nivel * 2) + "-->" + Nome + " é solteiro(a).");
            }

            if (Conjuge != null)
            {
                Console.WriteLine(new string(' ', nivel * 2) + "-->" + Nome + " é casado(a) com " + Conjuge.Nome + " filhos: ");
            }
            if (Filhos.Count > 0)
            {               
                foreach (Pessoa filho in Filhos)
                {
                    filho.ImprimeArvore(nivel + 2); // Aumenta o nível de indentação para os filhos
                }
            }
        }
    }
}