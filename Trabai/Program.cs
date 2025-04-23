using static System.Console;

namespace menu
{
    class MainClass
    {
        public static void printMenu(String[] options)
        {
            Carregar();
            foreach (String option in options)
            {
                Console.WriteLine(option);
            }
            Console.Write("Escolha a sua opção : ");
        }



        public static void Main(string[] args)
        {

            String[] options =
                {
                    "1- Registrar Nova Turma",
                    "2- Registrar Novo Aluno",
                    "3- Registrar Nota",
                    "4- Exibir Aprovados",
                    "5- Exibir Recuperação",
                    "6- Exibir Reprovados",
                    "7- Exibir Lista Geral",
                    "8- Sair"
                };

            int option = 0;
            while (true)
            {
                Clear();
                ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("<<<<<<<<<<<<<<<< MENU >>>>>>>>>>>>>>>");
                ForegroundColor = ConsoleColor.White;

                printMenu(options);
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Por favor, digite uma opção entre 1 e " + options.Length);
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine("Um erro aconteceu! Tente novamente.");
                    continue;
                }
                switch (option)
                {
                    case 1:
                        CadastrarTurma();
                        break;
                    case 2:
                        CadastrarAluno();
                        break;
                    case 3:
                        RegistrarNota();
                        break;
                    case 4:
                        ExibirListaAlunos(7, 10);
                        break;
                    case 5:
                        ExibirListaAlunos(5, 6.9);
                        break;
                    case 6:
                        ExibirListaAlunos(0, 4.9);
                        break;
                    case 7:
                        ExibirListaAlunos(0, 10);
                        break;
                    case 8:
                        GRAVAR();
                        Clear();
                        WriteLine("Aperte qualquer tecla para fechar o programa!");
                        ForegroundColor = ConsoleColor.Black;
                        Write("");
                        CursorVisible = false;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Por favor, digite uma opção entre 1 e  " + options.Length);
                        break;
                }

            }
        }

        static List<List<double>> Exame1 = new List<List<double>>();
        static List<List<double>> Exame2 = new List<List<double>>();
        static List<List<String>> Turmas = new List<List<String>>();

        static List<List<string>> Lista_das_Turmas = new List<List<string>>();
        static List<string> NomeDasTurmas = new List<string>();

        private static void CadastrarTurma()
        {
            EscreverCabecalho("=              CADASTRAR TURMA              =");
            WriteLine("Deseja cadastrar uma nova turma?\n[1] - Sim\n[Qualquer valor] - Não");

            if (Perguntar_ao_usuario())
            {
                String Turma = ReadLine();
                if (Verificar_Se_Existe(Turma))
                {
                    NomeDasTurmas.Add(Turma);
                    List<String> _loc_ = new List<String>();
                    Lista_das_Turmas.Add(_loc_);
                }
                else
                {
                    WriteLine("Essa turma já consta na nossa base de dados!");
                }
            }
            else
            {
                return;
            }

        }

        private static void CadastrarAluno()
        {
            EscreverCabecalho("=              CADASTRAR ALUNO               =");
            WriteLine("Deseja cadastrar um novo(a) aluno(a)?\n[1] - Sim\n[Qualquer valor] - Não");
            if (Perguntar_ao_usuario())
            {
                int Id_Turma;

                Write("Lista das Turmas");
                Exibir_Lista_das_Turmas();
                Id_Turma = Convert.ToInt16(ReadLine());
                if (Id_Turma > NomeDasTurmas.Count - 1)
                {
                    WriteLine("Esse ID não consta na nossa base de dados!");
                }
                else
                {

                }
                WriteLine("\nDigite o nome do(a) aluno(a): ");
                String Aluno = ReadLine();
                WriteLine("Digite o nome da turma: ");
                int Turma = PegarIdTurma(ReadLine());
                Turmas[Turma].Add(Aluno);
            }
            else
            {
                return;
            }

        }

        private static void RegistrarNota()
        {
            WriteLine("LISTA DE ALUNOS");
            ExibirListaAlunos(0, 10);
            WriteLine("Digite o nome do aluno");
            string Aluno = ReadLine();

            WriteLine("Qual exame você quer alterar o valor: ");
            int Op = Convert.ToInt32(ReadLine());

            WriteLine($"Digite o valor do {Op}º exame");
            int Nota = Convert.ToInt32(ReadLine());
            int Id = PegarIdAluno(Aluno);

            if (Op == 1)
            {
                Exame1[Id].Add(Nota);
            }
            else if (Op == 2)
            {
                Exame2[Id].Add(Nota);
            }
        }

        private static void Exibir_Lista_das_Turmas()
        {
            
            for (int i = 0; i < NomeDasTurmas.Count; i++)
            {
                WriteLine($"TURMAS JA REGISTRADAS : {NomeDasTurmas[i]}");
            }
            

        }
        private static void GRAVAR() {
            EscreverCabecalho("=            GRAVAR NO ARQUIVO              =");
            try
            {
                StreamWriter dadosnomes;
                string arq = @"C:\cool\collzinho.txt";
                dadosnomes = File.CreateText(arq);
                foreach (var item in NomeDasTurmas)
                {
                    dadosnomes.WriteLine($"{item}");
                }
                dadosnomes.Close();
            }
            catch (Exception e)
            {
                WriteLine($"{e.Message}");
            }
            finally
            {
                WriteLine("<<<<<<<< DADOS GRAVADOS COM SUCESSO! >>>>>>>>>");
            }

        }

        private static void ExibirListaAlunos(double min, double max)
        {

        }

        private static int PegarIdTurma(String Nome)
        {
            return 0;
        }

        private static int PegarIdAluno(String Aluno)
        {
            return 0;
        }

        private static Boolean Verificar_Se_Existe(String turma)
        {
            Boolean repetido = NomeDasTurmas.Any(x => x.Contains(turma));
            if (repetido)
            {
                WriteLine($"A turma {turma} já consta em nossos registros!");
                return false;
            }
            else
            {
                return true;
            }
        }


        private static bool Perguntar_ao_usuario()
        {
            int Option = 0;
            try
            {
                Option = Convert.ToInt16(ReadLine());
            }
            catch (Exception ex) { }

            return Option == 1 ? true : false;
        }

        private static void Carregar()
        {


        }

        private static void EscreverCabecalho(String Titulo)
        {
            Clear();
            WriteLine($"=============================================\n{Titulo}\n=============================================");
        }
    }
}
