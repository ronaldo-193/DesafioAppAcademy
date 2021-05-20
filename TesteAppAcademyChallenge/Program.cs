using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace TesteAppAcademyChallenge
{
    class Program
    {
        Pessoa pessoa = new Pessoa();
        static void Main(string[] args)
        {
            //OBS: usamos a Linq para manipulação da lista

            var pessoas = PreencheLista();            
            //1- resultado com percentual
            Console.WriteLine("Desenvolvido por Ronaldo Torres em C# \n");
            Console.WriteLine("Proporção de Candidatos por vaga:");
            Console.WriteLine("Android: "+ RetornaPorcentagem("Android", pessoas) + "%");
            Console.WriteLine("iOS:     "+ RetornaPorcentagem("iOS", pessoas) +"%");
            Console.WriteLine("QA:      "+ RetornaPorcentagem("QA", pessoas) +"%");            
           
            // 2-RETORNA MEDIA
            Console.WriteLine("\nIdade Média dos candidatos de QA: " + Math.Round(pessoas.Where(item => item.Vaga == "QA").Average(item => item.Idade),2));

            //3 -mostrar o número de estados distintos presentes na lista
            Console.WriteLine("\nNúmero de estados distintos presente na lista:" + pessoas.GroupBy(x => x.Estado).Count()) ;

            //4 - mostrar o nome do estado e a quantidade de candidatos dos 2 estados com menos ocorrências
            RankEstados(pessoas);

            //5- ordenar por ordem alfabética a lista de candidatos e salvar como Sorted_AppAcademy_Candidates.csv
            GeraListaEmOrdemAlfabetica(pessoas);

            //6- Retorna Instrutor
            RetornarProfessor(pessoas);

            Console.ReadKey();
        }
        
        //Retorna Instrutor
        public static void RetornarProfessor(List<Pessoa> pessoas)
        {
            var Ios = pessoas.Where(item => item.Vaga != "iOS"  // diferente da minha vaga
                                    && item.Estado =="SC"       // do estado de SC
                                    && item.Idade > 20          // Maior que 20 anos
                                    && item.Idade < 31          // Menor que 31 anos
                                    && item.Idade % 2 != 0      // Impar e primo
                                    ).OrderBy(x => x.Nome).ToList();

            foreach (var i in Ios)
            {
                //verifica se o ultimo nome começa com v
                var ultimo = i.Nome.Split(' ').Last();

                if(ultimo.Substring(0,1) == "V")
                {
                    Console.WriteLine("\n Instrutor de iOS : " + i.Nome);
                }
            }
        }


        public static double RetornaPorcentagem(string vaga, List<Pessoa> pessoas)
        {
            double Resultado = 0;
            double totalCanditados = pessoas.Count();
            switch (vaga)
            {
                case "Android":
                    Resultado = (pessoas.Where(item => item.Vaga == "Android").Count() * 100) / totalCanditados;
                    break;
                case "iOS":
                    Resultado = (pessoas.Where(item => item.Vaga == "iOS").Count() * 100) /totalCanditados;
                    break;
                case "QA":
                    Resultado = (pessoas.Where(item => item.Vaga == "QA").Count() * 100) / totalCanditados;
                    break;
            }

            return Math.Round(Resultado,2);
        }
        
       public static void RankEstados(List<Pessoa> pessoas)
        {
            //Primeiro pega o minimo
            string MinimoEstado = pessoas.Min(x => x.Estado).ToString();
            int MinimoCandidatoEstado = pessoas.Where(item => item.Estado == MinimoEstado).Count();

            //Depois pega o minimo menos o ultimo que achamos acima
            string PenultimoEstado = pessoas.Where(item => item.Estado != MinimoEstado).Min(x => x.Estado).ToString();
            int PenultimoCandidatoEstado = pessoas.Where(item => item.Estado == PenultimoEstado).Count();

            Console.WriteLine("\nRank do 2 estados com menos ocorrências:");
            Console.WriteLine("#1 " + MinimoEstado + " - " + MinimoCandidatoEstado + " candidato(s)");
            Console.WriteLine("#2 " + PenultimoEstado + " - " + PenultimoCandidatoEstado + " candidato(s)");
        }

        public static void GeraListaEmOrdemAlfabetica(List<Pessoa> pessoas)
        {
            var Ordenacao = pessoas.OrderBy(x => x.Nome).ToList();
            try
            {
                ExportaArquivo(Ordenacao);
                Console.WriteLine("\nLista ordenada salva como: Sorted_AppAcademy_Candidates.csv no diretorio c:");
            }
            catch
            {
                Console.WriteLine("\nErro ao salvar, verifique a permissão do diretório");
            }
        }
        public static void ExportaArquivo<T>(List<T> data)
        {            
            using (TextWriter output = File.CreateText(@"C:\Sorted_AppAcademy_Candidates.csv"))
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.DisplayName); // header
                    output.Write("\t");
                }
                output.WriteLine();
                foreach (T item in data)
                {
                    foreach (PropertyDescriptor prop in props)
                    {
                        output.Write(prop.Converter.ConvertToString(prop.GetValue(item)));
                        output.Write("\t");
                    }
                    output.WriteLine();
                }
            }
        }

        public static List<Pessoa> PreencheLista()
        {
            List<Pessoa> dados = new List<Pessoa>();
            try
            {
                dados.Add(new Pessoa("Kauê Araujo","QA",23,"SC")); 
                dados.Add(new Pessoa("Giovanna Cunha","QA",21,"SC")); 
                dados.Add(new Pessoa("Samuel Fernandes","Android",19,"SC"));
                dados.Add(new Pessoa("Kauan Ferreira","Android",30,"SC"));
                dados.Add(new Pessoa("Brenda Sousa","Android",50,"SC"));
                dados.Add(new Pessoa("Carolina Sousa","QA",20,"SC"));
                dados.Add(new Pessoa("Marcos Rodrigues","Android",33,"SC"));
                dados.Add(new Pessoa("Renan Goncalves","Android",21,"SC"));
                dados.Add(new Pessoa("Estevan Rocha","QA",21,"SC"));
                dados.Add(new Pessoa("Daniel Pinto","Android",27,"SC"));
                dados.Add(new Pessoa("Davi Barros","Android",20,"SC"));
                dados.Add(new Pessoa("Giovanna Costa","Android",22,"SC"));
                dados.Add(new Pessoa("Kauã Ferreira","iOS",19,"SC"));
                dados.Add(new Pessoa("Maria Ferreira","Android",26,"SC"));
                dados.Add(new Pessoa("Isabelle Cardoso","Android",18,"SC"));
                dados.Add(new Pessoa("Gabrielly Cunha","iOS",22,"SC"));
                dados.Add(new Pessoa("Ágatha Almeida","QA",26,"SC"));
                dados.Add(new Pessoa("Luis Dias","Android",19,"SC"));
                dados.Add(new Pessoa("Arthur Oliveira","QA",41,"SC"));
                dados.Add(new Pessoa("Isabelle Ribeiro","iOS",38,"SC"));
                dados.Add(new Pessoa("Miguel Rodrigues","QA",39 ,"SC"));
                dados.Add(new Pessoa("Leonardo Silva","iOS",19,"SC"));
                dados.Add(new Pessoa("Julia Castro","Android",19,"SC"));
                dados.Add(new Pessoa("Breno Pereira","Android",19,"SC"));
                dados.Add(new Pessoa("João Gomes", "Android",19,"SC"));
                dados.Add(new Pessoa("Leila Costa","iOS",21,"SC"));
                dados.Add(new Pessoa("Anna Costa","Android",30,"GO"));
                dados.Add(new Pessoa("Eduardo Souza","iOS",27,"SC"));
                dados.Add(new Pessoa("Davi Ferreira","QA",33,"SC"));
                dados.Add(new Pessoa("Matheus Barbosa","iOS",22,"SC"));
                dados.Add(new Pessoa("Aline Martins","iOS",33,"SC"));
                dados.Add(new Pessoa("Nicole Sousa","Android",17,"SC"));
                dados.Add(new Pessoa("Carlos Martins","QA",28,"SC"));
                dados.Add(new Pessoa("Vinícius Azevedo","QA",34,"SC"));
                dados.Add(new Pessoa("Ana Castro","Android",19,"SC"));
                dados.Add(new Pessoa("Amanda Pinto", "Android",29,"SC"));
                dados.Add(new Pessoa("Guilherme Oliveira","QA",30,"SP"));
                dados.Add(new Pessoa("Rafael Castro","Android",36,"SC"));
                dados.Add(new Pessoa("Douglas Barros","QA",30,"SC"));
                dados.Add(new Pessoa("Paulo Cunha","Android",18,"SC"));
                dados.Add(new Pessoa("Sophia Correia","Android",18,"SC"));
                dados.Add(new Pessoa("Lucas Araujo","Android",54,"SC"));
                dados.Add(new Pessoa("Emily Ferreira","Android",22,"SC"));
                dados.Add(new Pessoa("Isabelle Dias","Android",22,"SC"));
                dados.Add(new Pessoa("Camila Lima","QA",18,"SC"));
                dados.Add(new Pessoa("Isabela Araujo","Android",20,"SC"));
                dados.Add(new Pessoa("Emilly Sousa","Android",19,"SP"));
                dados.Add(new Pessoa("Kaua Ferreira","iOS",26,"SP"));
                dados.Add(new Pessoa("Alex Sousa","QA",18,"SC"));
                dados.Add(new Pessoa("Danilo Conrado","iOS",27,"SC"));
                dados.Add(new Pessoa("Miguel Fernandes","Android",27,"SC"));
                dados.Add(new Pessoa("Nicolas Araujo","iOS",28,"SP"));
                dados.Add(new Pessoa("Gabrielly Cardoso","QA",40,"SC"));
                dados.Add(new Pessoa("José Silva","QA",48,"SC"));
                dados.Add(new Pessoa("Yasmin Dias","Android",21,"SC"));
                dados.Add(new Pessoa("Nicolash Almeida","iOS",18,"SC"));
                dados.Add(new Pessoa("Fernanda Santos","QA",19,"SC"));
                dados.Add(new Pessoa("Beatrice Araujo","Android",20,"SC"));
                dados.Add(new Pessoa("Thiago Carvalho", "Android",20,"SC"));
                dados.Add(new Pessoa("Isabella Ribeiro","iOS",25,"SC"));
                dados.Add(new Pessoa("Enzo Barros","QA",24,"SC"));
                dados.Add(new Pessoa("Manuela Dias","Android",35,"SC"));
                dados.Add(new Pessoa("Nicolash Cunha","QA",27,"SC"));
                dados.Add(new Pessoa("Paulo Gomes","iOS",19,"SC"));
                dados.Add(new Pessoa("Vitória Fernandes","QA",18,"SC"));
                dados.Add(new Pessoa("Bianca Castro","Android",22,"SC"));
                dados.Add(new Pessoa("Amanda Cardoso","Android",22,"SC"));
                dados.Add(new Pessoa("Felipe Fernandes","Android",22,"SP"));
                dados.Add(new Pessoa("Livia Rodrigues","iOS",18,"SC"));
                dados.Add(new Pessoa("Kauã Cardoso","QA",18,"SC"));
                dados.Add(new Pessoa("Marisa Cardoso","iOS",27,"SC"));
                dados.Add(new Pessoa("Anna Carvalho","QA",17,"SC"));
                dados.Add(new Pessoa("Rafael Araujo","iOS",19,"SC"));
                dados.Add(new Pessoa("Matilde Barros","Android",25,"SC"));
                dados.Add(new Pessoa("Evelyn Rodrigues","QA",18,"SC"));
                dados.Add(new Pessoa("Kauan Dias","iOS",29,"SC"));
                dados.Add(new Pessoa("Camila Barros","Android",18,"SC"));
                dados.Add(new Pessoa("Diego Pereira","Android",28,"SC"));
                dados.Add(new Pessoa("Julian Cardoso","QA",40,"SC"));
                dados.Add(new Pessoa("Anna Costa","iOS",18,"SC"));
                dados.Add(new Pessoa("Anna Almeida","QA",26,"SC"));
                dados.Add(new Pessoa("Lara Fernandes","iOS",19,"SC"));
                dados.Add(new Pessoa("Mateus Silva","QA",24,"SC"));
                dados.Add(new Pessoa("Raissa Ribeiro","Android",30,"SC"));
                dados.Add(new Pessoa("Eduarda Barros","Android",21,"SC"));
                dados.Add(new Pessoa("Enzo Barros","Android",24,"SC"));
                dados.Add(new Pessoa("Kai Dias","Android",28,"SC"));
                dados.Add(new Pessoa("Carolina Goncalves","Android",22,"SC"));
                dados.Add(new Pessoa("Vitor Gomes","QA",42,"SC"));
                dados.Add(new Pessoa("Sofia Alves","QA",23,"SC"));
                dados.Add(new Pessoa("Evelyn Carvalho","QA",24,"SC"));
                dados.Add(new Pessoa("Julian Cardoso","QA",24,"SC"));
                dados.Add(new Pessoa("Rafaela Cunha","Android",18,"SC"));
                dados.Add(new Pessoa("Vitória Dias","QA",20,"SC"));
                dados.Add(new Pessoa("Anna Santos","iOS",32,"SC"));
                dados.Add(new Pessoa("Miguel Correia","QA",29,"SC"));
                dados.Add(new Pessoa("Manuela Gomes","QA",22,"SC"));
                dados.Add(new Pessoa("Fernanda Almeida","QA",26,"SC"));
                dados.Add(new Pessoa("Paulo Rocha","QA",19,"SC"));
                dados.Add(new Pessoa("Danilo Correia","QA",47,"SC"));
                dados.Add(new Pessoa("Maria Oliveira","Android",18,"SC"));
                dados.Add(new Pessoa("Bruna Martins","Android",19,"SC"));
                dados.Add(new Pessoa("Gabrielle Rodrigues","Android",18,"SC"));
                dados.Add(new Pessoa("Emilly Barros","Android",37,"SC"));
                dados.Add(new Pessoa("Ágatha Sousa","Android",28,"SC"));
                dados.Add(new Pessoa("Fernanda Barbosa","Android",31,"SC"));
                dados.Add(new Pessoa("Eduardo Oliveira","Android",28,"SC"));
                dados.Add(new Pessoa("Diego Castro","Android",22,"SC"));
                dados.Add(new Pessoa("Igor Martins","QA",21,"SC"));
                dados.Add(new Pessoa("Thaís Barbosa","QA",31,"SC"));
                dados.Add(new Pessoa("Guilherme Ribeiro","Android",29,"SC"));
                dados.Add(new Pessoa("Mariana Rodrigues","Android",27,"SC"));
                dados.Add(new Pessoa("Estevan Cavalcanti","iOS",26 ,"SP"));
                dados.Add(new Pessoa("André Souza","iOS",26,"SC"));
                dados.Add(new Pessoa("André Melo","Android",23,"SC"));
                dados.Add(new Pessoa("Isabella Carvalho","iOS",17,"SC"));
                dados.Add(new Pessoa("Ryan Barbosa","QA",17,"SC"));
                dados.Add(new Pessoa("Isabelle Rocha","Android",23,"SC"));
                dados.Add(new Pessoa("Sarah Rodrigues","QA",49,"SC"));
                dados.Add(new Pessoa("Gabrielle Costa","QA",28,"SC"));
                dados.Add(new Pessoa("Ryan Souza","iOS",20,"SC"));
                dados.Add(new Pessoa("Diego Barbosa","iOS",25,"RJ"));
                dados.Add(new Pessoa("Beatriz Alves","iOS",28,"SC"));
                dados.Add(new Pessoa("Luiza Cunha","QA",21,"SP"));
                dados.Add(new Pessoa("Marina Pinto","QA",18,"SC"));
                dados.Add(new Pessoa("Eduarda Silva","iOS",24,"SC"));
                dados.Add(new Pessoa("Tiago Oliveira","Android",30,"SC"));
                dados.Add(new Pessoa("Júlio Fernandes","Android",26,"SC"));
                dados.Add(new Pessoa("Letícia Ribeiro","Android",34,"SC"));
                dados.Add(new Pessoa("Tânia Rodrigues","Android",32,"SC"));
                dados.Add(new Pessoa("João Cunha","Android",18,"SC"));
                dados.Add(new Pessoa("Luís Lima","Android",18,"SC"));
                dados.Add(new Pessoa("Gabrielly Cunha","QA",20,"SC"));
                dados.Add(new Pessoa("Murilo Gomes","Android",22,"SC"));
                dados.Add(new Pessoa("Nicolas Almeida","QA",33,"SC"));
                dados.Add(new Pessoa("Camila Cardoso","Android",22,"SC"));
                dados.Add(new Pessoa("Larissa Ribeiro","Android",25,"SP"));
                dados.Add(new Pessoa("Antônio Cavalcanti","Android",23,"SC"));
                dados.Add(new Pessoa("Bianca Azevedo","Android",20,"SC"));
                dados.Add(new Pessoa("Carla Barros","Android",28,"SC"));
                dados.Add(new Pessoa("Arthur Barbosa","Android",28,"SC"));
                dados.Add(new Pessoa("Tiago Barros","QA",20,"SC"));
                dados.Add(new Pessoa("Marcos Souza","QA",27,"SC"));
                dados.Add(new Pessoa("Emily Cunha","QA",35,"SC"));
                dados.Add(new Pessoa("Maria Alves","QA",19,"SP"));
                dados.Add(new Pessoa("Renan Ferreira","QA",38,"SC"));
                dados.Add(new Pessoa("Mariana Oliveira","iOS",17,"SC"));
                dados.Add(new Pessoa("Rafael Costa","Android",18,"SC"));
                dados.Add(new Pessoa("José Santos","Android",27,"RJ"));
                dados.Add(new Pessoa("Livia Araujo","iOS",39,"SC"));
                dados.Add(new Pessoa("Bianca Cunha","Android",35,"SC"));
                dados.Add(new Pessoa("Alice Lima","Android",22,"SC"));
                dados.Add(new Pessoa("Leila Silva","Android",22,"SC"));
                dados.Add(new Pessoa("Ágatha Santos","Android",26,"SC"));
                dados.Add(new Pessoa("Melissa Cunha","QA",19,"SC"));
                dados.Add(new Pessoa("Gustavo Costa","QA",20,"SC"));
                dados.Add(new Pessoa("Beatrice Pereira","Android",21,"SC"));
                dados.Add(new Pessoa("Otávio Fernandes","QA",30,"SC"));
                dados.Add(new Pessoa("Daniel Melo","iOS",55,"SC"));
                dados.Add(new Pessoa("Matheus Alves","iOS",50,"SC"));
                dados.Add(new Pessoa("Paulo Goncalves","Android",24,"SC"));
                dados.Add(new Pessoa("Thaís Cavalcanti", "QA",20, "AL"));
                dados.Add(new Pessoa("Renan Cunha","Android",22,"SC"));
                dados.Add(new Pessoa("Antônio Ribeiro","QA",24,"SC"));
                dados.Add(new Pessoa("Sofia Rodrigues","iOS",20,"SC"));
                dados.Add(new Pessoa("Vitor Costa","Android",23,"SC"));
                dados.Add(new Pessoa("Rafael Pinto","Android",28,"SC"));
                dados.Add(new Pessoa("Nicole Carvalho","QA",33,"SC"));
                dados.Add(new Pessoa("Nicole Ferreira","Android",22,"SC"));
                dados.Add(new Pessoa("Kauê Souza","QA",19,"SC"));
                dados.Add(new Pessoa("Fernanda Ribeiro","QA",46,"SC"));
                dados.Add(new Pessoa("Rafael Martins","QA",32,"SC"));
                dados.Add(new Pessoa("Emilly Pereira","iOS",21,"SC"));
                dados.Add(new Pessoa("Rebeca Araujo","iOS",20,"SC"));
                dados.Add(new Pessoa("Aline Cunha","iOS",24,"SC"));
                dados.Add(new Pessoa("Julieta Pinto","Android",33,"SC"));
                dados.Add(new Pessoa("Luiz Gomes","iOS",20,"SC"));
                dados.Add(new Pessoa("Giovanna Silva","QA",24,"SC"));
                dados.Add(new Pessoa("Gabriela Correia","QA",27,"SC"));
                dados.Add(new Pessoa("Alice Gomes","Android",27,"SC"));
                dados.Add(new Pessoa("Brenda Gomes","Android",17,"SC"));
                dados.Add(new Pessoa("Arthur Goncalves","Android",20,"SC"));
                dados.Add(new Pessoa("Leonor Oliveira", "QA", 32, "SC"));
                dados.Add(new Pessoa("Pedro Barros", "Android", 20 , "SC"));
                dados.Add(new Pessoa("Gabrielle Rocha", "Android", 20 , "SC"));
                dados.Add(new Pessoa("Heitor Goncalves","QA",23,"SC"));
                dados.Add(new Pessoa("Aline Fernandes", "Android", 20, "SC"));
                dados.Add(new Pessoa("Estevan Rodrigues", "Android",19, "SC"));
                dados.Add(new Pessoa("Livia Cunha", "Android", 19, "SC"));
                dados.Add(new Pessoa("Matheus Carvalho", "Android",18, "SC"));
                dados.Add(new Pessoa("Erick Santos", "QA",18, "SC"));
                dados.Add(new Pessoa("Marina Costa", "Android", 34, "SC"));
                dados.Add(new Pessoa("José Goncalves","Android",30,"SC"));
                dados.Add(new Pessoa("Ana Correia", "QA", 24, "SC"));
                dados.Add(new Pessoa("Kauan Martins", "Android",23, "SC"));
                dados.Add(new Pessoa("Caio Melo", "Android",17, "SC"));
                dados.Add(new Pessoa("Marina Goncalves","QA",17,"SC"));
                dados.Add(new Pessoa("Leonor Gomes","Android",17,"SC"));
                dados.Add(new Pessoa("Otávio Ferreira", "Android",18, "SC"));
                dados.Add(new Pessoa("Kai Carvalho", "QA",25,"SC"));
                dados.Add(new Pessoa("Arthur Fernandes", "Android",26, "SC"));
                dados.Add(new Pessoa("Rafael Cavalcanti", "Android",19, "SC"));
                dados.Add(new Pessoa("Matilde Rodrigues", "Android",18, "SC"));
                dados.Add(new Pessoa("Ágatha Souza", "QA",23, "SC"));
                dados.Add(new Pessoa("Tomás Cunha", "QA", 19, "SC"));
                dados.Add(new Pessoa("Luana Rodrigues", "QA",29, "SC"));
                dados.Add(new Pessoa("Gabrielly Azevedo", "iOS",25, "SC"));
                dados.Add(new Pessoa("Clara Lima", "QA",23, "SC"));
                dados.Add(new Pessoa("Emilly Fernandes", "Android",24, "SC"));
                dados.Add(new Pessoa("Fernandu Vieira", "QA",29, "SC"));
                dados.Add(new Pessoa("Victor Cardoso", "Android",18, "SC"));
                dados.Add(new Pessoa("Leonardo Alves", "Android",20, "SC"));
                dados.Add(new Pessoa("Carlos Martins", "QA", 24, "SC"));
                dados.Add(new Pessoa("Rodrigo Carvalho","Android",24,"SC"));
                dados.Add(new Pessoa("Estevan Castro", "Android", 23, "SC"));
                dados.Add(new Pessoa("Giovanna Castro", "QA",32, "SC"));
                dados.Add(new Pessoa("Matilde Carvalho", "QA", 33, "SC"));
                dados.Add(new Pessoa("Martim Almeida", "QA", 24, "SC"));
                dados.Add(new Pessoa("Gabriel Costa", "Android", 20 , "SC"));
                dados.Add(new Pessoa("Fernanda Goncalves","QA",24,"SC"));
                dados.Add(new Pessoa("Júlia Azevedo", "iOS", 35 , "AM"));
                dados.Add(new Pessoa("Alex Santos", "QA", 21 , "SC"));
                dados.Add(new Pessoa("Vinícius Fernandes", "iOS", 28, "SC"));
                dados.Add(new Pessoa("Estevan Alves", "Android", 30, "PR"));
                dados.Add(new Pessoa("Luana Santos", "Android", 23 , "SC"));
                dados.Add(new Pessoa("Beatriz Rodrigues", "QA", 18, "SC"));
                dados.Add(new Pessoa("Luís Almeida", "Android", 19, "SC"));
                dados.Add(new Pessoa("Arthur Cardoso", "Android", 29 , "SP"));
                dados.Add(new Pessoa("Samuel Ribeiro","QA",25,"SC"));
                dados.Add(new Pessoa("Martim Azevedo", "Android",19, "SC"));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro " + ex);
            }
           
            return dados;
        }

    }
}
