using System.Collections;
using System.ComponentModel.Design;
using System.Data;
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
                WriteLine(option);
            }
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("Escolha uma das opções digitando um valor entre 0 à 8.");
            ForegroundColor = ConsoleColor.Yellow;
            Write("Escolha a sua opção : ");
        }

        public static void Exibir_Barra()
        {
            WriteLine("-----------------------------------------------------");
        }

        //Essa função é responsável por escrever o cabeçalho da nossa aplicação.
        private static void EscreverCabecalho(String Titulo)
        {
            Clear();
            WriteLine("=====================================================\n" +
                                        $"{Titulo}" +
                    "\n=====================================================");
        }

        //Essa função é responsável por verificar se já existe uma turma específica na nossa base de dados
        private static Boolean Verificar_Se_Existe_Turma(String Value)
        {
            return NomeDasTurmas.Any(x => x.Contains(Value));
        }

        private static Boolean Verificar_Se_Existe_Aluno(int ID_Turma, int ID_Aluno, String Nome)
        {
            return Lista_das_Turmas[ID_Turma][ID_Aluno].Any(x => x.Contains(Nome));
        }

        //Essa função é responsável por exibir uma lista das turma e seus IDs
        private static void Exibir_Lista_das_Turmas()
        {
            WriteLine("Turmas Registradas");
            Exibir_Barra();
            for (int i = 0; i < NomeDasTurmas.Count; i++)
            {
                WriteLine($"[{i}] - {NomeDasTurmas[i]}");
            }
        }

        private static void Retornando_Para_O_Menu_Principal()
        {
            Clear();
            CursorVisible = false;
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("Retornando para Menu principal!");
            Thread.Sleep(2000);
            ForegroundColor = ConsoleColor.Yellow;
            CursorVisible = true;
        }

        //Esse código é responsável por exibir uma lista dos alunos com o filtro por notas
        private static void Exibir_Lista_Alunos(double min, double max, string cabecalho)
        {
            EscreverCabecalho(cabecalho);
            BackgroundColor = ConsoleColor.DarkYellow;
            ForegroundColor = ConsoleColor.Black;
            WriteLine("|         NOME          | AV1 | AV2 | MÉDIA | TURMA |");
            WriteLine("|---------------------------------------------------|");
            int i = 0;
            bool Lista_Vazia = true;
            bool AlternarCor = true;
            foreach (var item in Lista_das_Turmas)
            {
                foreach (var item1 in item)
                {
                    string Nome = item1[0], N1, N2, M;
                    double Nota_1 = Convert.ToDouble(item1[1]);
                    double Nota_2 = Convert.ToDouble(item1[2]);
                    double Media = (Nota_1 + Nota_2) / 2;
                    N1 = Convert.ToString($"{Nota_1:F1}");
                    N2 = Convert.ToString($"{Nota_2:F1}");
                    M = Convert.ToString($"{Media:F1}");

                    if (Media >= min && Media <= max)
                    {
                        if (AlternarCor)
                        {
                            BackgroundColor = ConsoleColor.Yellow;
                            AlternarCor = false;
                        }
                        else
                        {
                            BackgroundColor = ConsoleColor.DarkYellow;
                            AlternarCor = true;
                        }
                        Write($"| {Nome.PadRight(22)}| " + (Nota_1 < 10 ? N1 : Nota_1 + " ") + $" | " + (Nota_2 < 10 ? N2 : Nota_2 + " ") + $" |  " + (Media < 10 ? M : Media + " ") + $"  | {NomeDasTurmas[i].PadRight(5)} |\n");
                        Lista_Vazia = false;
                    }
                    
                }
                
                i++;
            }
            ResetColor();
            ForegroundColor = ConsoleColor.Yellow;
            if (Lista_Vazia)
            {
                WriteLine("|".PadRight(52)+"|\n"+"|".PadRight(10)+"Está tabela parece estar vazia..."+"|".PadLeft(10)+"\n|".PadRight(53)+"|");
            }
            WriteLine("=====================================================");
        }

        //Pega o valor da nota
        private static string Pegar_Nota(string pergunta)
        {
            while (true)
            {
                Write($"{pergunta}");
                double Avaliacao = -1;
                try
                {
                    Avaliacao = Convert.ToDouble(ReadLine());
                }
                catch (Exception ex) { }
                if (Avaliacao >= 0 && Avaliacao <= 10)
                {
                    return Convert.ToString(Avaliacao);
                }
                else
                {
                    SetCursorPosition(0, CursorTop - 1);
                }
            }
        }

        //Essa função é responsável por fazer uma pergunta ao usuário com uma estrutura genérica
        private static bool Perguntar_ao_usuario(string questão)
        {
            int Option = 0;
            Exibir_Barra();
            WriteLine($"{questão}");
            Exibir_Barra();
            WriteLine("[1] - Sim\n[X] - Não");
            try
            {
                Option = Convert.ToInt16(ReadLine());
            }
            catch (Exception ex) { }

            return Option == 1 ? true : false;
        }

        private static string Perguntar_ao_usuario_Dados(string questão)
        {
            string Option;
            Exibir_Barra();
            WriteLine($"{questão}");
            Exibir_Barra();
            WriteLine("[1] - NOME\n[2] - NOTAS\n[X] - Não");
            try
            {
                Option = ReadLine();
                return Option;
            }
            catch (Exception ex) 
            { 
            
            }

            return "x";
        }

        private static void Enter_To_Continue()
        {
            WriteLine("Aperte ENTER para continuar...");
            CursorVisible = false;
            ReadLine();
            CursorVisible = true;
        }

        //Aqui é onde criamos nossas listas
        static List<List<List<string>>> Lista_das_Turmas = new List<List<List<string>>>();
        static List<string> NomeDasTurmas = new List<string>();

        public static void Main(string[] args)
        {
            Title = "Gerenciamento de Turmas";
            ForegroundColor = ConsoleColor.Yellow;
            Carregar();
            String[] options =
                {
                    "[1]- Registrar Nova Turma",
                    "[2]- Registrar Novo Aluno(a)",
                    "[3]- Editar Dados",
                    "[4]- Exibir Aprovados",
                    "[5]- Exibir Recuperação",
                    "[6]- Exibir Reprovados",
                    "[7]- Exibir Lista Geral",
                    "[8]- Sair",
                    "-----------------------------------------------------"
                };

            int option = 0;
            while (true)
            {
                Clear();
                EscreverCabecalho("                     MENU");

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
                        Editar_Dados();
                        break;
                    case 4:
                        Exibir_Lista_Alunos(7, 10, "            ALUNOS ARPOVADOS");
                        Enter_To_Continue();
                        break;
                    case 5:
                        Exibir_Lista_Alunos(5, 6.9, "           ALUNOS DE RECUPERAÇÃO");
                        Enter_To_Continue();
                        break;
                    case 6:
                        Exibir_Lista_Alunos(0, 4.9, "           ALUNOS REPROVADOS");
                        Enter_To_Continue();
                        break;
                    case 7:
                        Exibir_Lista_Alunos(0, 10, "            TODOS OS ALUNOS");
                        Enter_To_Continue();
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

                Exibir_Barra();
                WriteLine("Digite o nome/código da nova turma");
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("Aviso: o sistema ira aceitar turmas/códigos \ncom apenas 3 dígitos, exemplo: 1-A");
                ForegroundColor = ConsoleColor.Yellow;
                Exibir_Barra();
                Turma = ReadLine().ToUpper();

                if (Turma.Length != 3)
                {
                    Clear();
                    EscreverCabecalho("                    Aviso!");
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteLine("Tamanho do nome da turma não aceito pelo sistema.\nPor favor, digite um valor com apenas 3 dígitos.\nExemplo: 1-A, 2-B...");
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("Aperte ENTER para continuar...");
                    CursorVisible = false;
                    ReadLine();
                    CursorVisible = true;
                    Retornando_Para_O_Menu_Principal();
                    return;
                }

                if (!Verificar_Se_Existe_Turma(Turma))
                {
                    NomeDasTurmas.Add(Turma);
                    List<List<string>> _loc_ = new List<List<String>>();
                    Lista_das_Turmas.Add(_loc_);
                    Clear();
                    EscreverCabecalho("                    Aviso!");
                    CursorVisible = false;
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteLine(
                        $"A turma {Turma} foi cadastrada com sucesso!");
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("Aperte ENTER para continuar e salvar...");
                    CursorVisible = false;
                    ReadLine();
                    CursorVisible = true;
                    GRAVAR();
                }
                else
                {
                    Clear();
                    EscreverCabecalho("                    Aviso!");
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteLine($"A turma {Turma} já consta na nossa base de dados!\nRetornando para o Menu princípal.");
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("Aperte ENTER para continuar e salvar...");
                    CursorVisible = false;
                    ReadLine();
                    CursorVisible = true;
                    Retornando_Para_O_Menu_Principal();
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
            EscreverCabecalho("               CADASTRAR ALUNO(A)");

            if (Perguntar_ao_usuario("Deseja cadastrar um novo(a) aluno(a) ?"))
            {
                int Id_Turma;
                Clear();
                while (true)
                {
                    EscreverCabecalho("               CADASTRAR ALUNO               ");
                    WriteLine("Em qual turma você deseja registrar um novo \naluno(a)?");

                    try
                    {
                        Exibir_Barra();
                        Exibir_Lista_das_Turmas();
                        Write("[X] CANCELAR\n");
                        Exibir_Barra();
                        Write("Escreva o ID da turma:");
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
                                 $"inserir na turma {NomeDasTurmas[Id_Turma]}");
                        Exibir_Barra();
                        Write("NOME: ");
                        String Aluno = ReadLine().ToUpper(), Avaliacao_1 = "0", Avaliacao_2 = "0";

                        if (Aluno.Length < 3)
                        {
                            Clear();
                            EscreverCabecalho("                    Aviso!");
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteLine("Tamanho do nome do Aluno(a) não aceito pelo sistema.\nPor favor, digite um valor com ou mais de 3 dígitos.");
                            ForegroundColor = ConsoleColor.Yellow;
                            WriteLine("Aperte ENTER para continuar...");
                            CursorVisible = false;
                            ReadLine();
                            CursorVisible = true;
                            Retornando_Para_O_Menu_Principal();
                            return;
                        }

                        foreach (var item in Lista_das_Turmas[Id_Turma])
                        {
                            if (item[0] == Aluno)
                            {
                                ForegroundColor = ConsoleColor.Cyan;
                                WriteLine($"O aluno(a) {Aluno} já existe na turma {NomeDasTurmas[Id_Turma]}!\nAperte ENTER para continuar...");
                                CursorVisible = false;
                                ReadLine();
                                ForegroundColor = ConsoleColor.Yellow;
                                Retornando_Para_O_Menu_Principal();
                                return;
                            }
                        }

                        List<string> _loc_ = new List<String>();
                        Lista_das_Turmas[Id_Turma].Add(_loc_);

                        int Id_Aluno = Lista_das_Turmas[Id_Turma].Count - 1;
                        Lista_das_Turmas[Id_Turma][Id_Aluno].Add(Aluno);
                        Lista_das_Turmas[Id_Turma][Id_Aluno].Add(Convert.ToString(Avaliacao_1));
                        Lista_das_Turmas[Id_Turma][Id_Aluno].Add(Convert.ToString(Avaliacao_2));
                        WriteLine($"TURMA: {NomeDasTurmas[Lista_das_Turmas[Id_Turma][Id_Aluno].Count - 1]}");
                        ForegroundColor = ConsoleColor.Cyan;
                        WriteLine($"O aluno(a) {Aluno} foi adicionado(a) à turma {NomeDasTurmas[Id_Turma]} com sucesso!\nAperte ENTER para continuar...");
                        CursorVisible = false;
                        ReadLine();
                        GRAVAR();
                        return;
                    }
                }
            }
            else
            {
                Retornando_Para_O_Menu_Principal();
                return;
            }

        }

        private static void Editar_Dados(int ID_Turma, int ID_Aluno, int ID_Nota)
        {
            while (true)
            {
                try
                {
                    if (ID_Nota == 0)
                    {
                        Write("Escolha o novo nome para esse aluno(a): ");
                        string Novo_Nome = ReadLine().ToUpper();
                        if (!Verificar_Se_Existe_Aluno(ID_Turma, ID_Aluno, Novo_Nome))
                        {
                            Lista_das_Turmas[ID_Turma][ID_Aluno][ID_Nota] = Novo_Nome;
                            CursorVisible = false;
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteLine($"Nome atualizado com sucesso para {Novo_Nome}!");
                            CursorVisible = false;
                            ReadLine();
                            CursorVisible = true;
                            return;
                        } 
                        else
                        {
                            ForegroundColor = ConsoleColor.Red;
                            WriteLine($"O nome {Novo_Nome}, já está cadastrado!");
                            ForegroundColor = ConsoleColor.Yellow;
                        }   
                    }
                    else
                    {
                        Write($"Escolha o valor da AV{ID_Nota}, digite um valor\n entre 0 e 10\nNova nota: ");
                        int Nota = Convert.ToInt32(ReadLine());
                        if (Nota < 0 || Nota > 10)
                        {
                            ForegroundColor = ConsoleColor.Red;
                            WriteLine($"Valor inválido {Nota}, por favor digite um valor entre 0 e 10");
                            ForegroundColor = ConsoleColor.Yellow;
                            CursorVisible = false;
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Exibir_Barra();
                            Lista_das_Turmas[ID_Turma][ID_Aluno][ID_Nota] = Convert.ToString(Nota);
                            return;
                        }
                    }
                    
                }
                catch (Exception)
                {
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteLine($"Essa nota não será atualizada");
                    ForegroundColor = ConsoleColor.Yellow;
                    return;
                }
            }
        }
        private static void Editar_Dados()
        {
            Clear();
            Exibir_Lista_Alunos(0, 10, "              EDITAR DADOS");
            
            switch (Perguntar_ao_usuario_Dados("Você deseja editar algum dado?"))
            {
                case "1":
                    Editar_Nomes();
                    break;
                case "2":
                    Editar_Nota();
                    break;

                default:
                    Retornando_Para_O_Menu_Principal();
                    break;
            }
            
        }

        public static void Editar_Nomes()
        {
            string Aluno, Turma;
            int ID_Aluno = -1, ID_Turma = -1;

            Exibir_Lista_Alunos(0, 10, "              EDITAR NOMES");

            bool _loc_ = true;
            while (_loc_) {
                Clear();
                Exibir_Lista_Alunos(0, 10, "              EDITAR NOMES");
                Write("Digite a turma desse aluno(a): ");
                Turma = ReadLine().ToUpper();
                ID_Turma = Procurar_ID_Turma(Turma);
                _loc_ = ID_Turma == -1 ? true : false;
            }

            _loc_ = true;
            while (_loc_)
            {
                Clear();
                Exibir_Lista_Alunos(0, 10, "              EDITAR NOMES");
                Write("Digite o nome atual do aluno(a): ");
                Aluno = ReadLine().ToUpper();
                ID_Aluno = Procurar_ID_Aluno(Aluno, ID_Turma);
                _loc_ = ID_Aluno == -1 ? true : false;
            }

            Editar_Dados(ID_Turma, ID_Aluno, 0);
            Retornando_Para_O_Menu_Principal();
            GRAVAR();
            return;
        }

        public static int Procurar_ID_Turma(string Nome_Turma)
        {
            for (int i = 0; i < NomeDasTurmas.Count; i++)
            {
                if (Nome_Turma == NomeDasTurmas[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public static int Procurar_ID_Aluno(string Nome_Aluno, int i) 
        {
            for (int j = 0; j < Lista_das_Turmas[i].Count; j++)
            {
                if (Nome_Aluno == Lista_das_Turmas[i][j][0])
                {
                    return j;
                }
            }
            return -1;
        }

        public static void Editar_Nota()
        {
            string Aluno, Turma;
            int ID_Aluno = -1, ID_Turma = -1;

            Exibir_Lista_Alunos(0, 10, "              EDITAR NOTAS");

            Write("Digite o nome do aluno(a): ");
            Aluno = ReadLine().ToUpper();

            Write("Digite a turma desse aluno(a): ");
            Turma = ReadLine().ToUpper();
            Exibir_Barra();

            for (int i = 0; i < NomeDasTurmas.Count; i++)
            {
                if (Turma == NomeDasTurmas[i])
                {
                    ID_Turma = i;
                    for (int j = 0; j < Lista_das_Turmas[i].Count; j++)
                    {
                        if (Aluno == Lista_das_Turmas[i][j][0])
                        {
                            ID_Aluno = j;
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteLine("Atenção: Caso não queira mudar o valor \né só não digitar um valor numérico e apertar Enter");
                            ForegroundColor = ConsoleColor.Yellow;
                            Editar_Dados(ID_Turma, ID_Aluno, 1);
                            Editar_Dados(ID_Turma, ID_Aluno, 2);
                            ForegroundColor = ConsoleColor.Cyan;
                            CursorVisible = false;
                            WriteLine("As notas do aluno(a) foram atualizadas\nAperte enter para salvar.");
                            ReadLine();
                            GRAVAR();
                            Retornando_Para_O_Menu_Principal();
                            return;
                        }
                    }
                }
            }
            Clear();
            WriteLine("Não foi encontrado nenhum aluno(a) correspondente\na turma ou não foi possível encontrar a turma");
            Thread.Sleep(2000);
            Retornando_Para_O_Menu_Principal();
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
                    dadosalunos = File.CreateText(@"C:\BaseDeDados\Alunos" + i + ".txt");
                    foreach (var item1 in item)
                    {
                        dadosalunos.WriteLine($"{item1[0]}\n{item1[1]}\n{item1[2]}");
                    }
                    dadosalunos.Close();
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
                WriteLine("DADOS ATUALIZADOS COM SUCESSO!");
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

                    for (int x = 0; x + 2 <= _loc_.Length; x += 3)
                    {
                        List<string> Lista = new List<string>();
                        Lista.Add(_loc_[x]);
                        Lista.Add(_loc_[x + 1]);
                        Lista.Add(_loc_[x + 2]);
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
