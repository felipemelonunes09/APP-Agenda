using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    class ManipuladorArquivos
    {
        private static string CAMINHO_ARQUIVO = AppDomain.CurrentDomain.BaseDirectory + "contatos.txt";

        public static List<Contato> LerArquivo() 
        {
            List<Contato> contatos = new List<Contato>();

            if (File.Exists(@CAMINHO_ARQUIVO))
            {
                using (StreamReader sr = File.OpenText(CAMINHO_ARQUIVO)) 
                {
                    while (sr.Peek() != -1) 
                    {
                        string linha = sr.ReadLine();
                        string[] linhasSeparadas = linha.Split(';');

                        if (linhasSeparadas.Count() == 3) 
                        {
                            Contato ctt = new Contato
                            {
                                Nome = linhasSeparadas[0],
                                Email = linhasSeparadas[1],
                                Telefone = linhasSeparadas[2]
                            };

                            contatos.Add(ctt);
                        }
                    }
                }
            }

            return contatos;
        }

        public static void EscreverArquivo(List<Contato> contatos) 
        {
            using (StreamWriter sw = new StreamWriter(@CAMINHO_ARQUIVO, false)) // CLASSE PARA ESCREVER EM ARQUIVOS TEXTOS
            { 
                foreach (Contato contato in contatos)
                {
                    string linha = string.Format("{0};{1};{2}", contato.Nome, contato.Email, contato.Telefone);
                    sw.WriteLine(linha);
                }

                sw.Flush(); // LIMPA OS BUFFERS
            }
        }
    }
}
