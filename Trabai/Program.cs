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
                      "[X] - Não\n");

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
                        "Salvando alterações..."); 
                    Thread.Sleep(2000);
                    ReadLine();
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
            WriteLine("Deseja cadastrar um novo(a) aluno(a)?\n" +
                "[1] - Sim\n" +
                "[X] - Não");
            if (Perguntar_ao_usuario())
            {  
                int Id_Turma;
                Clear();
                while (true)
                {
                    EscreverCabecalho("               CADASTRAR ALUNO               ");
                    WriteLine("---------------------------------------------\n" +
                          "Em qual turma você deseja registrar um novo \n" +
                          "aluno(a)?\n" +
                          "---------------------------------------------");

                    try
                    {
                        Exibir_Lista_das_Turmas();
                        Write("[X] CANCELAR\n" +
                              "---------------------------------------------\n" +
                              "Escreva o ID da turma:");
                        Id_Turma = Convert.ToInt32(ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        CursorVisible = false;
                        WriteLine("Retornando para o Menu principal!");   
                        Thread.Sleep(1000);
                        CursorVisible = true;
                        return;
                    }

                    if (Id_Turma > NomeDasTurmas.Count - 1)
                    {
                        ForegroundColor = ConsoleColor.Red;
                        CursorVisible = false;
                        WriteLine("Esse ID não consta na nossa base de dados!");
                        ForegroundColor = ConsoleColor.Yellow;
                        Thread.Sleep(2000);
                        CursorVisible = true;
                    }
                    else
                    {
                        EscreverCabecalho("               CADASTRAR ALUNO               ");
                        WriteLine("Digite o nome do(a) aluno(a), que deseja \n" +
                                 $"inserir na turma {NomeDasTurmas[Id_Turma]}\n" +
                                  "---------------------------------------------");
                        String Aluno = ReadLine();
                        WriteLine("Digite o nome da turma: ");

                        //Criando uma lista em Lista
                        List<string> _loc_ = new List<String>();
                        Lista_das_Turmas[Id_Turma].Add(_loc_);
                        
                        int Id_Aluno = Lista_das_Turmas[Id_Turma].Count-1;
                        Lista_das_Turmas[Id_Turma][Id_Aluno].Add(Aluno);
                        Lista_das_Turmas[Id_Turma][Id_Aluno].Add("0");
                        Lista_das_Turmas[Id_Turma][Id_Aluno].Add("0");
                        WriteLine($"Nome: {Lista_das_Turmas[Id_Turma][Id_Aluno][0]} Turma: {Lista_das_Turmas[Id_Turma][Id_Aluno].Count-1} Av1: {Lista_das_Turmas[Id_Turma][Id_Aluno][1]} Av2: {Lista_das_Turmas[Id_Turma][Id_Aluno][2]}");
                        ReadLine();
                        return;
                    }
                }
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

        private static void Exibir_Lista_da_Turma()
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
                StreamWriter dadosnomes, Cu;
                string arq = @"C:\BaseDeDados\Turmas.txt";
                dadosnomes = File.CreateText(arq);

                string arq1 = @"C:\BaseDeDados\Alunos.txt";
                Cu = File.CreateText(arq1);

                foreach (var item in NomeDasTurmas)
                {
                    dadosnomes.WriteLine($"{item}");
                }
                
                for (int i = 0; i < Lista_das_Turmas.Count; i++)
                {
                    for (int x = 0; x < Lista_das_Turmas[i].Count; x++)
                    {
                        
                        foreach (var item in Lista_das_Turmas[i][x])
                        {
                            dadosnomes.WriteLine($"{item[0]}");
                        }

                        string arq2 = @"C:\BaseDeDados\Av3.txt";
                        dadosnomes = File.CreateText(arq2);

                        foreach (var item in Lista_das_Turmas[i][x])
                        {
                            dadosnomes.WriteLine($"{item[1]}");
                        }

                        string arq3 = @"C:\BaseDeDados\Av2.txt";
                        dadosnomes = File.CreateText(arq3);
                        foreach (var item in Lista_das_Turmas[i][x])
                        {
                            dadosnomes.WriteLine($"{item[2]}");
                        }
                    } 
                }
                dadosnomes.Close();
                Cu.Close();

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
            try
            {
                var _loc_1 = File.ReadAllLines(@"C:\BaseDeDados\Turmas.txt");
                for (int i = 0; i < _loc_1.Length; i++)
                {
                    NomeDasTurmas.Add(_loc_1[i]);
                    List<List<string>> _loc_2 = new List<List<String>>();
                    Lista_das_Turmas.Add(_loc_2);
                }
            }
            catch (Exception ex)
            {
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("Não foi encontrado nenhum dado salvo!");
                Thread.Sleep(2000);
            }
            
        }

        private static void EscreverCabecalho(String Titulo)
        {
            Clear();
            WriteLine($"=============================================\n{Titulo}\n=============================================");
        }




        -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            ALTERAÇAO EM REGISTRAR NOTA 

           private static void RegistrarNota()
        {
            EscreverCabecalho("REGISTRAR NOTA");
            ExibirListaAlunos(0, 10);
            WriteLine("Qual avaliação deseja registrar?\n" +
               "[1] - AV1\n" +
               "[2] - AV2\n"+
               "[x] - SAIR");

            if (Perguntar_ao_usuario())
            {
                Clear();
                EscreverCabecalho("=              REGISTRANDO NOTA              =");
                WriteLine("Digite o valor da AV1:");
                double nota = Convert.ToDouble(ReadLine());
                double nota1 = Convert.ToDouble(ReadLine());

                if (Verificar_Se_Existe_Nota(nota))
                {
                    List<List<double>> _loc_ = new List<List<double>>();
                    Clear();
                    WriteLine($"A nota {nota} foi cadastrada com sucesso!\n" +
                        "Salvando alterações...");
                    Thread.Sleep(2000);
                    ReadLine();
                    GRAVAR();
                }

                else
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    EscreverCabecalho("=                   Aviso!                  =");
                    WriteLine($"A nota {nota} já consta na nossa base de dados!\nRetornando para o Menu principal.");
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

            WriteLine("Digite o número da avaliação (1 ou 2):");
            int Op = Convert.ToInt32(ReadLine());

            WriteLine($"Digite o valor do {Op}º exame:");
            double Nota = Convert.ToDouble(ReadLine());
        }
