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
                do
                {
                    w = fonction.lireInt("quel largeur pour le plateau\nDe 2 a 20");
                } while (w <= 1 || w > 20);
                do
                {
                    h = fonction.lireInt("quel hauteur pour le plateau\nDe 2 a 20");
                } while (h <= 1 || h > 20);
   
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
                do
                {
                    bombe = fonction.lireInt($"combient de bombe?\nDe 2 a {h*w - 1}");
                } while (bombe <= 1 || bombe >= h*w);
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
                        if(interact == "d" || interact == "D")
                        {
                            plateau = fonction.drapeau(x, y, plateau);
                        }
                        else if (interact == "c" || interact == "C")
                        {
                            if (plateau[x, y] == null)
                            {
                                if (auto) { 
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
                        for (int j = 1; j < matrice.GetLength(1) -  1; j++)
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
                }
                else
                {
                    Console.WriteLine($"Vous avez perdu en {elapsedTime} dommage.\nVotre tableau était");
                    Console.WriteLine(fonction.deminConcact(plateau));
                    Console.WriteLine("Voici le tableau finalle.");
                    Console.WriteLine(fonction.deminConcact(matrice));
                }
                
                Console.WriteLine("pour recommencez presser space.");
            } while (Console.ReadKey().Key == ConsoleKey.Spacebar);
        }
    }
}