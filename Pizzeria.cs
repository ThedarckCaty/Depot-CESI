using System;
using System.Collections.Generic;

namespace PizzeriaAdminConsoleApp
{
    public class Pizzeria
    {
        private List<Pizza> pizzas = new List<Pizza>();


        public void AfficherPizzas()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            if (pizzas.Count == 0)
            {
                Console.WriteLine("Aucune pizza disponible.");
            }
            else
            {
                foreach (var pizza in pizzas)
                {
                    Console.WriteLine(pizza);
                }
            }
        }

        public void AjouterPizza(string deminutif, string nom, float prix)
        {
            try
            {
                int newId = pizzas.Count > 0 ? pizzas.Max(p => p.Id) + 1 : 1;

                Pizza nouvellePizza = new Pizza
                {
                    Id = newId,
                    Deminutif = deminutif,
                    Nom = nom,
                    Prix = prix
                };

                pizzas.Add(nouvellePizza);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'ajout de la pizza : {ex.Message}");
            }
        }

        public void ModifierPizza(int id, string deminutif, string nom, float prix)
        {
            try
            {
                Pizza pizzaAModifier = pizzas.Find(p => p.Id == id);

                if (pizzaAModifier != null)
                {
                    pizzaAModifier.Deminutif = deminutif;
                    pizzaAModifier.Nom = nom;
                    pizzaAModifier.Prix = prix;
                    Console.WriteLine("Pizza modifiée avec succès.");
                }
                else
                {
                    Console.WriteLine("Aucune pizza trouvée avec cet ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la modification de la pizza : {ex.Message}");
            }
        }


        public void SupprimerPizza(int id)
        {
            try
            {
                Pizza pizzaASupprimer = pizzas.Find(p => p.Id == id);

                if (pizzaASupprimer != null)
                {
                    pizzas.Remove(pizzaASupprimer);
                    Console.WriteLine("Pizza supprimée avec succès.");
                }
                else
                {
                    Console.WriteLine("Aucune pizza trouvée avec cet ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression de la pizza : {ex.Message}");
            }
        }
    }
}
