using _5TTI_NicolasPonchaut_prosFonc;

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
                int w = fonction.lireInt("quel largeur pour le plateau");
                int h = fonction.lireInt("quel hauteur pour le plateau");
                string[,] matrice = new string[w + 2, h + 2];
                string[,] plateau = new string[w + 2, h + 2];

                for (int i = 0; i < plateau.GetLength(0); i++)
                {
                    plateau[i, 0] = i.ToString();
                    plateau[0, i] = i.ToString();
                }

                matrice = fonction.calcNumber(fonction.placementBombe(fonction.lireInt("combient de bombe?"), matrice));

                while (life)
                {
                    Console.Clear();
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
                                life = fonction.creuser(x, y, plateau, matrice);
                            }
                        }
                    }
                }
                Console.WriteLine(fonction.deminConcact(plateau));
                Console.WriteLine("pour recommencez presser space.");
            } while (Console.ReadKey().Key == ConsoleKey.Spacebar);
        }
    }
}