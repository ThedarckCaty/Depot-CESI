// PizzeriaAdminConsoleApp.cs
using System;
using System.Globalization;

namespace PizzeriaAdminConsoleApp
{
    class PizzeriaAdminConsoleApp
    {
        static void Main()
        {
            Pizzeria pizzeria = new Pizzeria();
            pizzeria.AjouterPizza("PEP","Pépéroni",12.50f);
            pizzeria.AjouterPizza("MAR","Margherita",14.00f);
            pizzeria.AjouterPizza("REIN","La Reine",11.50f);
            pizzeria.AjouterPizza("FRO","La 4 fromages",12.00f);
            pizzeria.AjouterPizza("CAN","La cannibale",12.50f);
            pizzeria.AjouterPizza("SAV","La savoyarde",13.00f);
            pizzeria.AjouterPizza("ORI","L'orientale",13.50f);
            pizzeria.AjouterPizza("IND","L'indienne",14.00f);

            while (true)
            {
                Console.WriteLine("1. Afficher toutes les pizzas");
                Console.WriteLine("2. Ajouter une pizza");
                Console.WriteLine("3. Modifier une pizza");
                Console.WriteLine("4. Supprimer une pizza");
                Console.WriteLine("5. Quitter");

                Console.Write("Choisissez une option : ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        pizzeria.AfficherPizzas();
                        break;
                    case "2":
                        Console.Write("Entrez le déminutif de la pizza : ");
                        string deminutif = Console.ReadLine();
                        Console.Write("Entrez le nom de la pizza : ");
                        string nom = Console.ReadLine();
                        Console.Write("Entrez le prix de la pizza : ");
                        float prix;
                        if (float.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out prix))
                        {
                            pizzeria.AjouterPizza(deminutif, nom, prix);
                        }
                        else
                        {
                            Console.WriteLine("Le prix doit être un nombre valide.");
                        }
                        break;
                    case "3":
                        Console.Write("Entrez l'ID de la pizza à modifier : ");
                        int idModifier;
                        if (int.TryParse(Console.ReadLine(), out idModifier))
                        {
                            Console.Write("Entrez le nouveau déminutif de la pizza : ");
                            string deminutifModifier = Console.ReadLine();
                            Console.Write("Entrez le nouveau nom de la pizza : ");
                            string nomModifier = Console.ReadLine();
                            Console.Write("Entrez le nouveau prix de la pizza : ");
                            float prixModifier;
                            if (float.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out prixModifier))
                            {
                                pizzeria.ModifierPizza(idModifier, deminutifModifier, nomModifier, prixModifier);
                            }
                            else
                            {
                                Console.WriteLine("Le prix doit être un nombre valide.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("L'ID doit être un nombre valide.");
                        }
                        break;
                    case "4":
                        Console.Write("Entrez l'ID de la pizza à supprimer : ");
                        int id;
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            pizzeria.SupprimerPizza(id);
                        }
                        else
                        {
                            Console.WriteLine("L'ID doit être un nombre valide.");
                        }
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Option invalide. Veuillez réessayer.");
                        break;
                }
            }
        }
    }
}
