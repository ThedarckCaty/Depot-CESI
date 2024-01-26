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
            SQLiteDbContext dbContext = new SQLiteDbContext();
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
                                Console.Clear();
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

        static void AfficherMenuEntreprise(SQLiteDbContext dbContext)
        {


            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu - Entreprise :");
                Console.WriteLine("i - Information sur l'entreprise");
                Console.WriteLine("1 - Créer un salarié");
                Console.WriteLine("2 - Créer un fournisseur");
                Console.WriteLine("3 - Créer un client");
                if (dbContext.ObtenirSalaries().Any())
                {
                    Console.WriteLine("4 - Afficher les salariés");
                }
                if (dbContext.ObtenirFournisseurs().Any())
                {
                    Console.WriteLine("5 - Afficher les fournisseurs");
                }
                if (dbContext.ObtenirClients().Any())
                {
                    Console.WriteLine("6 - Afficher les clients");
                }
                if (dbContext.ObtenirSalaries().Any())
                {
                    Console.WriteLine("7 - Supprimer un salarié");
                }
                if (dbContext.ObtenirFournisseurs().Any())
                {
                    Console.WriteLine("8 - Supprimer un fournisseur");
                }
                if (dbContext.ObtenirClients().Any())
                {
                    Console.WriteLine("9 - Supprimer un client");
                }
                Console.WriteLine("0 - Retour");
                if (dbContext.ObtenirFournisseurs().Any())
                {
                    Console.WriteLine("\nGestion Fournisseur");
                    Console.WriteLine("10 - Créer un produit");
                    if (dbContext.ObtenirProduits().Any())
                    {
                        Console.WriteLine("11 - Supprimer un produit");
                    }
                    Console.WriteLine("12 - Afficher les ventes");
                    Console.WriteLine("0 - Retour");
                }

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
                        if (dbContext.ObtenirFournisseurs().Any())
                        {
                            dbContext.AjouterProduit();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Aucun fournisseur disponible. Veuillez en créer un d'abord.\n");
                        }
                        break;
                    case "11":
                        if (dbContext.ObtenirFournisseurs().Any())
                        {
                            dbContext.SupprimerProduit();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Aucun fournisseur disponible. Veuillez en créer un d'abord.");
                            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                            Console.ReadLine();
                        }
                        break;
                    case "12":
                        if (dbContext.ObtenirFournisseurs().Any())
                        {
                            dbContext.AfficherVentes();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Aucun fournisseur disponible. Veuillez en créer un d'abord.");
                            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                            Console.ReadLine();
                        }
                        break;
                    case "0":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Choix invalide. Veuillez réessayer.\n");
                        break;
                }
            }
        }

        static void AfficherMenuClient(SQLiteDbContext dbContext)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Menu - Client :");
                if (dbContext.ObtenirProduits().Any())
                {
                    Console.WriteLine("1 - Achat d'un produit");
                    Console.WriteLine("2 - Afficher les produits");
                }
                if (dbContext.ObtenirAchats().Any())
                {
                    Console.WriteLine("3 - Afficher les achats");
                }
                Console.WriteLine("0 - Retour");
                Console.Write("Choix : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        dbContext.AchatProduit();
                        break;
                    case "2":
                        dbContext.AfficherProduits();
                        break;
                    case "3":
                        dbContext.AfficherAchats();
                        break;
                    case "0":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.\n");
                        break;
                }
            }




        }

    }
}

