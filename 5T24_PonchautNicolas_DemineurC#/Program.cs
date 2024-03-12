using _5TTI_NicolasPonchaut_prosFonc;
using System.Diagnostics;

namespace _5T24_PonchautNicolas_DemineurC_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fonction fonction = new Fonction();
            do
            {
                bool life = true;
                List<string> list = new List<string>();
                int w;
                int h;
                string pseudo = "";
                Console.WriteLine("afficher le classement et ne pas jouer?\nSi oui pressez space.");
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    Console.WriteLine("voici le classement des 10 premiers.");
                    fonction.deminReadClassement(@"D:\github\5T2024_PonchautNicolas_Demineur\5T24_PonchautNicolas_DemineurC#\data.txt");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Avec un tuto?\nSi oui pressez space.");
                    if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                    {
                        Console.WriteLine($"Votre but est de creuser toute les case ou il n'y a pas de bombe(@) et de possez un drapeau({(char)20}) sur toute les case avec une bombe.");
                        fonction.sleep(3, false);
                        Console.WriteLine($"pour sela vous devrez entre des coordonner comme 1 2 avec comme premier valeur pour la ligne et l'autre la column.");
                        Console.WriteLine($"| 0 | 1 | 2 |\n| 1 |   | {(char)20} |\n| 2 |   |   |");
                        fonction.sleep(3, false);
                        Console.WriteLine($"pour sela lorseque on vous demandera de choisir un interaction il sufira d'entre un des chois et si vous essayer de creuser un drapeau alors rien ne se passe.");
                        fonction.sleep(3, false);
                        Console.WriteLine($"Bon jeux");
                        fonction.sleep(3, false);
                    }
                    Console.WriteLine("mode Classer (20 par 20 avec 250 bombe)?\nSi oui pressez space.");
                    bool classer = Console.ReadKey().Key == ConsoleKey.Spacebar;
                    Console.Clear();
                    if (classer)
                    {
                        Console.WriteLine("Votre pseudo.");
                        pseudo = Console.ReadLine();
                        w = 1;
                        h = 1;
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
                        bombe = 0;
                    }
                    else
                    {
                        do
                        {
                            bombe = fonction.lireInt($"combient de bombe?\nDe 2 a {h * w - 1}");
                        } while (bombe <= 1 || bombe >= h * w);
                    }
                    matrice = fonction.calcNumber(fonction.placementBombe(bombe, matrice));

                    Console.WriteLine("Mode dev?");
                    bool dev = Console.ReadKey().Key == ConsoleKey.Spacebar;
                    fonction.ClearLastLine();
                    Console.WriteLine("Mode creusage auto si 0?\nSi oui pressez space.");
                    bool auto = Console.ReadKey().Key == ConsoleKey.Spacebar;
                    fonction.ClearLastLine();

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    while (life)
                    {
                        Console.Clear();
                        if (dev)
                        {
                            Console.WriteLine(fonction.deminConcact(matrice));
                        }
                        Console.WriteLine(fonction.deminConcact(plateau));
                        Console.WriteLine("avec quelle case interagir?\nformat:x y");
                        if (fonction.verifCase(Console.ReadLine(), plateau, matrice, out int x, out int y))
                        {
                            Console.WriteLine($"que voulez vous faire en {x} {y}?\nc: creuser\nd: drapeau");
                            string interact = Console.ReadLine();
                            if (interact == "d" || interact == "D")
                            {
                                plateau = fonction.drapeau(x, y, plateau);
                            }
                            else if (interact == "c" || interact == "C")
                            {
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
                            }
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
                    stopWatch.Stop();
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                        ts.Hours, ts.Minutes, ts.Seconds);
                    if (!win)
                    {
                        Console.WriteLine($"Vous avez gagnez en {elapsedTime} bien joue.");
                        Console.WriteLine(fonction.deminConcact(plateau));
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
                        Console.WriteLine(fonction.deminConcact(plateau));
                        Console.WriteLine("Voici le tableau finalle.");
                        Console.WriteLine(fonction.deminConcact(matrice));
                    }
                }
                Console.WriteLine("pour recommencez presser space.");
            } while (Console.ReadKey().Key == ConsoleKey.Spacebar);
        }
    }
}