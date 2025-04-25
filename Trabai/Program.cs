using static System.Console;

namespace menu
{
    class MainClass
    {
        //Essa função é responsável por escrever as opções do nosso Menu Principal
        public static void printMenu(String[] options)
        {
            foreach (String option in options)
            {
                Console.WriteLine(option);
            }
            Console.Write("Escolha a sua opção : ");
        }

        //Essa função é responsável por escrever o cabeçalho da nossa aplicação.
        private static void EscreverCabecalho(String Titulo)
        {
            Clear();
            WriteLine("=============================================\n" +
                                        $"{Titulo}" +
                    "\n=============================================");
        }

        //Essa função é responsável por verificar se já existe uma turma específica na nossa base de dados
        private static Boolean Verificar_Se_Existe(String Value)
        {
            return NomeDasTurmas.Any(x => x.Contains(Value));
        }

        //Essa função é responsável por exibir uma lista das turma e seus IDs
        private static void Exibir_Lista_das_Turmas()
        {
            WriteLine(
                "Turmas Registradas\n" +
                "---------------------------------------------");
            for (int i = 0; i < NomeDasTurmas.Count; i++)
            {
                WriteLine($"[{i}] - {NomeDasTurmas[i]}");
            }
        }

        private static void Retornando_Para_O_Menu_Principal()
        {
            Clear();
            CursorVisible = false;
            WriteLine("Retornando para Menu principal!");
            Thread.Sleep(2000);
            CursorVisible = true;
        }

        //
        private static void Exibir_Lista_Alunos(double min, double max, string cabecalho)
        {
            EscreverCabecalho(cabecalho);
            WriteLine("| NOME | AV1 | AV2 |");
            foreach (var item in Lista_das_Turmas)
            {
                foreach (var item1 in item)
                {
                    WriteLine($"| {item1[0]} | {item1[1]} | {item1[2]} |");
                }
            }
            ReadLine();
        }

        //Essa função é responsável por fazer uma pergunta ao usuário com uma estrutura genérica
        private static bool Perguntar_ao_usuario(string questão)
        {
            int Option = 0;

            WriteLine(
                "---------------------------------------------\n" +
                $"{questão}\n" +
                "---------------------------------------------\n" +
                "[1] - Sim\n" +
                "[X] - Não"
                );
            try
            {
                Option = Convert.ToInt16(ReadLine());
            }
            catch (Exception ex) { }

            return Option == 1 ? true : false;
        }

        //Aqui é onde criamos nossas listas
        static List<List<List<string>>> Lista_das_Turmas = new List<List<List<string>>>();
        static List<string> NomeDasTurmas = new List<string>();

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
                    "[8]- Sair",
                    "---------------------------------------------"
                };

            int option = 0;
            while (true)
            {
                Clear();
                ForegroundColor = ConsoleColor.Red;
                EscreverCabecalho("                     MENU");
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
                        Exibir_Lista_Alunos(7, 10, "            ALUNOS ARPOVADOS");
                        break;
                    case 5:
                        Exibir_Lista_Alunos(5, 6.9, "           ALUNOS DE RECUPERAÇÃO");
                        break;
                    case 6:
                        Exibir_Lista_Alunos(0, 4.9, "           ALUNOS REPROVADOS");
                        break;
                    case 7:
                        Exibir_Lista_Alunos(0, 10, "            TODOS OS ALUNOS");
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

        
        private static void CadastrarTurma()
        {
            string Cabecalho = "             REGISTRAR NOVA TURMA", Turma;

            EscreverCabecalho(Cabecalho);
            Exibir_Lista_das_Turmas();

            if (Perguntar_ao_usuario("Deseja cadastrar uma nova turma?"))
            {
                Clear();
                EscreverCabecalho(Cabecalho);
                Exibir_Lista_das_Turmas();
                
                WriteLine(
                    "---------------------------------------------\n" +
                    "Digite o nome/código da nova turma\n" +
                    "---------------------------------------------");
                Turma = ReadLine();

                if (!Verificar_Se_Existe(Turma))
                {
                    NomeDasTurmas.Add(Turma);
                    List<List<string>> _loc_ = new List<List<String>>();
                    Lista_das_Turmas.Add(_loc_);
                    Clear();
                    WriteLine(
                        $"A turma {Turma} foi cadastrada com sucesso!\n" +
                        "Salvando alterações...");
                    Thread.Sleep(2000);
                    GRAVAR();
                }
                else
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    EscreverCabecalho("                    Aviso!");
                    WriteLine($"A turma {Turma} já consta na nossa base de dados!\nRetornando para o Menu princípal.");
                    ForegroundColor = ConsoleColor.Yellow;
                    Thread.Sleep(2000);
                }
            }
            else
            {
                Retornando_Para_O_Menu_Principal();
                return;
            }

        }

        private static void CadastrarAluno()
        {
            EscreverCabecalho("               CADASTRAR ALUNO");

            if (Perguntar_ao_usuario("Deseja cadastrar um novo(a) aluno(a) ?"))
            {
                int Id_Turma;
                Clear();
                while (true)
                {
                    EscreverCabecalho("               CADASTRAR ALUNO               ");
                    WriteLine(
                        "---------------------------------------------\n" +
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
                        Retornando_Para_O_Menu_Principal();
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

                        int Id_Aluno = Lista_das_Turmas[Id_Turma].Count - 1;
                        Lista_das_Turmas[Id_Turma][Id_Aluno].Add(Aluno);
                        Lista_das_Turmas[Id_Turma][Id_Aluno].Add("0");
                        Lista_das_Turmas[Id_Turma][Id_Aluno].Add("0");
                        WriteLine($"Nome: {Lista_das_Turmas[Id_Turma][Id_Aluno][0]} Turma: {Lista_das_Turmas[Id_Turma][Id_Aluno].Count - 1} Av1: {Lista_das_Turmas[Id_Turma][Id_Aluno][1]} Av2: {Lista_das_Turmas[Id_Turma][Id_Aluno][2]}");
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
            Clear();
            Exibir_Lista_Alunos(0, 10, "              LISTA DE ALUNOS");
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
                StreamWriter dadosturmas, dadosalunos, dadosav1, dadosav2;

                dadosturmas = File.CreateText(@"C:\BaseDeDados\Turmas.txt");

                foreach (var item in NomeDasTurmas)
                {
                    dadosturmas.WriteLine($"{item}");
                }

                int i = 0;
                foreach (var item in Lista_das_Turmas)
                {
                    foreach (var item1 in item)
                    {
                        dadosalunos = File.CreateText(@"C:\BaseDeDados\Alunos" + i + ".txt");
                        dadosalunos.WriteLine($"{item1[0]}\n{item1[1]}\n{item1[2]}");
                        dadosalunos.Close();
                    }
                    i++;
                }
                dadosturmas.Close();
            }
            catch (Exception e)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"{e.Message}");
                ForegroundColor = ConsoleColor.Yellow;
                ReadLine();
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

                for (int i = 0; i < NomeDasTurmas.Count; i++)
                {
                    var _loc_ = File.ReadAllLines(@"C:\BaseDeDados\Alunos" + i + ".txt");
                    for (int x = 0; i+3 < _loc_.Length; i += 3)
                    {
                        List<string> Lista = new List<string>();
                        Lista.Add(Lista[i]);
                        Lista.Add(Lista[i+1]);
                        Lista.Add(Lista[i+2]);
                        Lista_das_Turmas[i].Add(Lista);
                    }
                }

            }
            catch (Exception ex)
            {
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("Não foi encontrado nenhum dado salvo!");
                Thread.Sleep(2000);
            }

        }

    }
}
