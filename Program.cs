using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

using System;
using Entreprise.Services;
using System.ComponentModel.Design;

namespace Entreprise
{
    class Program
    {
        static void Main()
        {
            LiteDbContext dbContext = new LiteDbContext();
                while (true)
                    {
                     AfficherMenu();

                        // Message de choix et lecture du choix de l'utilisateur
                        Console.Write("Choix : ");
                        string choix = Console.ReadLine();

                        // Choix de l'utilisateur en fonction du choix
                        switch (choix)
                        {
                            case "1":
                                AfficherMenuEntreprise(dbContext);
                                break;
                            case "2":
                                AfficherMenuClient(dbContext);
                                break;
                            case "0":
                                return;
                            default:
                                Console.Clear();
                                Console.WriteLine("Choix invalide. Veuillez réessayer.\n");
                                break;
                        }
            }
        }
        static void AfficherMenu()
        {
            Console.WriteLine("Menu :");
            Console.WriteLine("1 - Connexion Entreprise");
            Console.WriteLine("2 - Connexion Client");
            Console.WriteLine("0 - Quitter");
        }

        static void AfficherMenuEntreprise(LiteDbContext dbContext)
        {


            while (true)
            {
                Console.WriteLine("Menu - Entreprise :");
                Console.WriteLine("i - Information sur l'entreprise");
                Console.WriteLine("1 - Créer un salarié");
                Console.WriteLine("2 - Créer un fournisseur");
                Console.WriteLine("3 - Créer un client");
                Console.WriteLine("4 - Afficher les salariés");
                Console.WriteLine("5 - Afficher les fournisseurs");
                Console.WriteLine("6 - Afficher les clients");
                Console.WriteLine("7 - Supprimer un salarié");
                Console.WriteLine("8 - Supprimer un fournisseur");
                Console.WriteLine("9 - Supprimer un client");
                //Vérification a faire en cas de présence d'un fournisseurs sinon ne pas afficher le 10 et 11
                Console.WriteLine("10 - Créer un produit");
                Console.WriteLine("11 - Supprimer un produit");
                Console.WriteLine("0 - Retour");
                Console.Write("Choix : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "i":
                        dbContext.AfficherEntreprise();
                        break;
                    case "1":
                        dbContext.AjouterSalarie();
                        break;
                    case "2":
                        dbContext.AjouterFournisseur();
                        break;
                    case "3":
                        dbContext.AjouterClient();
                        break;
                    case "4":
                        dbContext.AfficherSalaries();
                        break;
                    case "5":
                        dbContext.AfficherFournisseurs();
                        break;
                    case "6":
                        dbContext.AfficherClients();
                        break;
                    case "7":
                        dbContext.SupprimerSalarie();
                        break;
                    case "8":
                        dbContext.SupprimerFournisseur();
                        break;
                    case "9":
                        dbContext.SupprimerClient();
                        break;
                    case "10":
                        //
                        break;
                    case "11":
                        //
                        break;
                    case "0":
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Choix invalide. Veuillez réessayer.\n");
                        break;
                }
            }
        }

        static void AfficherMenuClient(LiteDbContext dbContext)
        {
            while (true)
            {
                Console.WriteLine("Menu - Client :");
                Console.WriteLine("1 - Créer un client");
                Console.WriteLine("2 - Afficher les produits");
                Console.WriteLine("0 - Quitter");
                Console.Write("Choix : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        //
                        break;
                    case "2":
                        //
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.\n");
                        break;
                }
            }




        }

    }
}

