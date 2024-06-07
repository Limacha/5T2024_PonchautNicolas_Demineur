using _5TTI_NicolasPonchaut_prosFonc;
using System.Diagnostics;

namespace _5T24_PonchautNicolas_DemineurC_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fonction fonction = new Fonction();
            Console.Title = "Demineur";
            do
            {
                bool life = true;
                List<string> list = new List<string>();
                int w;
                int h;
                string pseudo = "";
                int x = 1;
                int y = 1;
                int nb = 0;

                Console.Clear();
                Console.WriteLine("  ____    U _____ u   __  __                   _   _     U _____ u    _   _     ____     \r\n" +
                              " |  _\"\\   \\| ___\"|/ U|' \\/ '|u      ___       | \\ |\"|    \\| ___\"|/ U |\"|u| | U |  _\"\\ u  \r\n" +
                              "/| | | |   |  _|\"   \\| |\\/| |/     |_\"_|     <|  \\| |>    |  _|\"    \\| |\\| |  \\| |_) |/  \r\n" +
                              "U| |_| |\\  | |___    | |  | |       | |      U| |\\  |u    | |___     | |_| |   |  _ <    \r\n" +
                              " |____/ u  |_____|   |_|  |_|     U/| |\\u     |_| \\_|     |_____|   <<\\___/    |_| \\_\\   \r\n" +
                              "  |||_     <<   >>  <<,-,,-.   .-,_|___|_,-.  ||   \\\\,-.  <<   >>  (__) )(     //   \\\\_  \r\n" +
                              " (__)_)   (__) (__)  (./  \\.)   \\_)-' '-(_/   (_\")  (_/  (__) (__)     (__)   (__)  (__) ");
                Console.WriteLine("\nafficher le classement et ne pas jouer?\nSi oui pressez space.\nSinon n'importe quel touche.");
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    fonction.sleep(1, false);
                    Console.WriteLine("voici le classement des 10 premiers.");
                    fonction.deminReadClassement(@"D:\github\5T2024_PonchautNicolas_Demineur\5T24_PonchautNicolas_DemineurC#\data.txt");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Avec un tuto?\nSi oui pressez space.\nSinon n'importe quel touche.");
                    if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                    {
                        Console.Clear();
                        Console.WriteLine($"Votre but est de creuser toute les case ou il n'y a pas de bombe(@) et de possez un drapeau({(char)20}) sur toute les case avec une bombe.");
                        fonction.sleep(3, false);
                        Console.WriteLine($"\npour sela vous pouvez vous deplacer avec les fleche directionelle.");
                        fonction.sleep(3, false);
                        Console.WriteLine($"\npuis pressez c pour creuser et d pour posez ou enlevez un drapeau.");
                        fonction.sleep(3, false);
                        Console.WriteLine($"\nBon jeux");
                        fonction.sleep(5, false);
                    }
                    Console.WriteLine("\nmode Classer (20 par 20 avec 150 bombe)?\nSi oui pressez space.\nSinon n'importe quel touche.");
                    bool classer = Console.ReadKey().Key == ConsoleKey.Spacebar;
                    Console.Clear();
                    if (classer)
                    {
                        Console.WriteLine("Votre pseudo.");
                        pseudo = Console.ReadLine();
                        w = 20;
                        h = 20;
                    }
                    else
                    {
                        do
                        {
                            w = fonction.lireInt("quel largeur pour le plateau\nDe 2 a 20");
                        } while (w <= 1 || w > 20);
                        do
                        {
                            h = fonction.lireInt("quel hauteur pour le plateau\nDe 2 a 20");
                        } while (h <= 1 || h > 20);
                    }

                    string[,] matrice = new string[w + 2, h + 2];
                    string[,] plateau = new string[w + 2, h + 2];
                    bool win = false;

                    for (int i = 0; i < plateau.GetLength(0); i++)
                    {
                        plateau[i, 0] = i.ToString();
                        plateau[0, i] = i.ToString();
                        plateau[i, plateau.GetLength(1) - 1] = i.ToString();
                        plateau[plateau.GetLength(0) - 1, i] = i.ToString();
                    }
                    int bombe;
                    if (classer)
                    {
                        bombe = 150;
                    }
                    else
                    {
                        do
                        {
                            bombe = fonction.lireInt($"combient de bombe?\nDe 2 a {h * w - 1}");
                        } while (bombe <= 1 || bombe >= h * w);
                    }
                    matrice = fonction.calcNumber(fonction.placementBombe(bombe, matrice));
                    /*
                    Console.WriteLine("Mode dev?");
                    bool dev = Console.ReadKey().Key == ConsoleKey.Spacebar;
                    fonction.sleep(1, false);*/
                    bool dev = false;

                    fonction.ClearLastLine();
                    Console.WriteLine("Creuser automatiquement toute les case autour des 0?\nSi oui pressez space.\nSinon n'importe quel touche.");
                    bool auto = Console.ReadKey().Key == ConsoleKey.Spacebar;
                    fonction.sleep(1, false);
                    fonction.ClearLastLine();
                    Console.Clear();

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    while (life)
                    {
                        Console.SetCursorPosition(0, 0);
                        if (dev)
                        {
                            fonction.deminConcact(matrice, x, y);
                        }
                        fonction.deminConcact(plateau, x, y);
                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.UpArrow:
                                if (x - 1 >= 1)
                                {
                                    x--;
                                }
                                break;
                            case ConsoleKey.DownArrow:
                                if (x + 1 <= h)
                                {
                                    x++;
                                }
                                break;
                            case ConsoleKey.LeftArrow:
                                if (y - 1 >= 1)
                                {
                                    y--;
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                if (y + 1 <= w)
                                {
                                    y++;
                                }
                                break;
                            case ConsoleKey.D:
                                plateau = fonction.drapeau(x, y, plateau, bombe, ref nb);
                                fonction.ClearLastLine();
                                break;
                            case ConsoleKey.C:
                                if (plateau[x, y] == null)
                                {
                                    if (auto)
                                    {
                                        do
                                        {
                                            if (list.Count() > 0)
                                            {
                                                fonction.verifCase(list[0], plateau, matrice, out x, out y);
                                                list.RemoveAt(0);
                                            }
                                            life = fonction.creuserAuto(x, y, plateau, matrice, ref list);
                                        } while (list.Count() > 0);

                                    }
                                    else
                                    {
                                        life = fonction.creuser(x, y, plateau, matrice);
                                    }
                                }
                                fonction.ClearLastLine();
                                break;
                            case ConsoleKey.Q:
                                Environment.Exit(0);
                                break;
                        }
                        win = false;
                        for (int i = 1; i < matrice.GetLength(0) - 1; i++)
                        {
                            for (int j = 1; j < matrice.GetLength(1) - 1; j++)
                            {
                                if (!win)
                                {
                                    win = plateau[i, j] == null;
                                }
                            }
                        }
                        if (!win && life)
                        {
                            life = win;
                        }
                    }
                    Console.Clear();
                    stopWatch.Stop();
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                        ts.Hours, ts.Minutes, ts.Seconds);
                    if (!win)
                    {
                        Console.WriteLine($"Vous avez gagnez en {elapsedTime} bien joue.");
                        fonction.deminConcact(plateau, x, y);
                        if (classer)
                        {
                            fonction.writeinFile($"{pseudo}:{elapsedTime}", @"D:\github\5T2024_PonchautNicolas_Demineur\5T24_PonchautNicolas_DemineurC#\data.txt");
                            Console.WriteLine($"Vous avez été enregistrer avec un temps de {elapsedTime}");
                            Console.WriteLine("voici le classement des 10 premiers.");
                            fonction.deminReadClassement(@"D:\github\5T2024_PonchautNicolas_Demineur\5T24_PonchautNicolas_DemineurC#\data.txt");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Vous avez perdu en {elapsedTime} dommage.\nVotre tableau était");
                        fonction.deminConcact(plateau, x, y);
                        Console.WriteLine("Voici le tableau finalle.");
                        fonction.deminConcact(matrice, x, y);
                    }
                }
                Console.WriteLine("pour recommencez presser space.");
            } while (Console.ReadKey().Key == ConsoleKey.Spacebar);
        }
    }
}