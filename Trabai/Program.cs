using static System.Console;

namespace menu
{
    class MainClass
    {
        public static void printMenu(String[] options)
        {
            foreach (String option in options)
            {
                Console.WriteLine(option);
            }
            Console.Write("Escolha a sua opção : ");
        }



        public static void Main(string[] args)
        {
            Carregar();

            String[] options =
                {
                    "[1]- Registrar Nova Turma",
                    "[2]- Registrar Novo Aluno",
                    "[3]- Registrar Nota",
                    "[4]- Exibir Aprovados",
                    "[5]- Exibir Recuperação",
                    "[6]- Exibir Reprovados",
                    "[7]- Exibir Lista Geral",
                    "[8]- Sair"
                };

            int option = 0;
            while (true)
            {
                Clear();
                ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("<<<<<<<<<<<<<<<< MENU >>>>>>>>>>>>>>>");
                ForegroundColor = ConsoleColor.Yellow;

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

        static List<List<List<string>>> Lista_das_Turmas = new List<List<List<string>>>();
        static List<string> NomeDasTurmas = new List<string>();

        private static void CadastrarTurma()
        {
            EscreverCabecalho("             TURMAS CADASTRADAS");
            Exibir_Lista_das_Turmas();
            WriteLine("---------------------------------------------\n"+
                      "      Deseja cadastrar uma nova turma?\n" +
                      "---------------------------------------------\n" +
                      "[1] - Sim\n" +
                      "[Qualquer valor] - Não\n");

            if (Perguntar_ao_usuario())
            {
                Clear();
                EscreverCabecalho("=              CADASTRAR TURMA              =");
                WriteLine("Digite o nome/código da nova turma:");
                String Turma = ReadLine();
                if (!Verificar_Se_Existe(Turma))
                {
                    NomeDasTurmas.Add(Turma);
                    List<List<string>> _loc_ = new List<List<String>>();
                    Lista_das_Turmas.Add(_loc_);
                    Clear();
                    WriteLine($"A turma {Turma} foi cadastrada com sucesso!\n" +
                        "Salvando alterações"); 
                    Thread.Sleep(2000);
                    GRAVAR();
                }
                else
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    EscreverCabecalho("=                   Aviso!                  =");   
                    WriteLine($"A turma {Turma} já consta na nossa base de dados!\nRetornando para o Menu princípal.");
                    ForegroundColor = ConsoleColor.Yellow;
                    Thread.Sleep(2000);
                }
            }
            else
            {
                Clear();
                CursorVisible = false;
                WriteLine("Retornando para Menu principal.");
                Thread.Sleep(2000);
                CursorVisible = true;
                return;
            }

        }

        private static void CadastrarAluno()
        {
            EscreverCabecalho("               CADASTRAR ALUNO               ");
            WriteLine("Deseja cadastrar um novo(a) aluno(a)?\n[1] - Sim\n[Qualquer valor] - Não");
            if (Perguntar_ao_usuario())
            {
                int Id_Turma;
                Clear();
                EscreverCabecalho("               CADASTRAR ALUNO               ");
                WriteLine("---------------------------------------------\n" +
                      "Em qual turma você deseja registrar um novo \n" +
                      "aluno(a)?\n" +
                      "---------------------------------------------");
                Exibir_Lista_das_Turmas();
                WriteLine("[X] CANCELAR");
                try
                {
                    Id_Turma = Convert.ToInt16(ReadLine());
                }
                catch (Exception ex)
                {
                    Clear();
                    WriteLine("Retornando para o Menu principal!");
                    Thread.Sleep(1000);
                    return;
                }
                
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
                //Turmas[Turma].Add(Aluno);
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

            if (Op == 1)
            {
                //Exame1[Id].Add(Nota);
            }
            else if (Op == 2)
            {
                //Exame2[Id].Add(Nota);
            }
        }

        private static void Exibir_Lista_das_Turmas()
        {
            for (int i = 0; i < NomeDasTurmas.Count; i++)
            {
                WriteLine($"[{i}] - {NomeDasTurmas[i]}");
            }
        }
        private static void GRAVAR()
        {
            try
            {
                StreamWriter dadosnomes;

                string arq = @"C:\BaseDeDados\Turmas.txt";
                dadosnomes = File.CreateText(arq);
                foreach (var item in NomeDasTurmas)
                {
                    dadosnomes.WriteLine($"{item}");
                }
                dadosnomes.Close();

                /*
                for (int i = 0; i < Lista_das_Turmas.Count; i++)
                {
                    arq = @$"C:\BaseDeDados\Alunos{i}.txt";
                    foreach (var item in Lista_das_Turmas[i])
                    {
                        dadosnomes.WriteLine($"{item[0]}");
                    }

                    arq = @$"C:\BaseDeDados\Av1{i}.txt";
                    foreach (var item in Lista_das_Turmas[i])
                    {
                        dadosnomes.WriteLine($"{item[1]}");
                    }

                    arq = @$"C:\BaseDeDados\Av2{i}.txt";
                    foreach (var item in Lista_das_Turmas[i])
                    {
                        dadosnomes.WriteLine($"{item[2]}");
                    }
                }
                */
                
            }
            catch (Exception e)
            {
                WriteLine($"{e.Message}");
            }
            finally
            {
                Clear();
                ForegroundColor = ConsoleColor.Green;
                WriteLine("DADOS GRAVADOS COM SUCESSO!");
                ForegroundColor = ConsoleColor.Yellow;
            }
            CursorVisible = false;
            Thread.Sleep(1000);
            CursorVisible = true;
        }

        private static void ExibirListaAlunos(double min, double max)
        {

        }

        private static Boolean Verificar_Se_Existe(String Value)
        {
           return NomeDasTurmas.Any(x => x.Contains(Value));
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
            var _loc_ = File.ReadAllLines(@"C:\BaseDeDados\Turmas.txt");
            for (int i = 0; i < _loc_.Length; i++)
            {
                NomeDasTurmas.Add(_loc_[i]);
            }
        }

        private static void EscreverCabecalho(String Titulo)
        {
            Clear();
            WriteLine($"=============================================\n{Titulo}\n=============================================");
        }
    }
}
