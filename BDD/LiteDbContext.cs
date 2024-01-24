using Entreprise.Interfaces;
using Entreprise.Services;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entreprise
{
    public class LiteDbContext : IDisposable
    {
        private LiteDatabase liteDatabase;

        public LiteDbContext()
        {
            InitialiserBaseDeDonnees();
        }

        public ILiteCollection<Salarie> Salaries => liteDatabase.GetCollection<Salarie>("salaries");
        public ILiteCollection<Client> Clients => liteDatabase.GetCollection<Client>("clients");
        public ILiteCollection<Achat> Achats => liteDatabase.GetCollection<Achat>("achats");
        public ILiteCollection<Fournisseur> Fournisseurs => liteDatabase.GetCollection<Fournisseur>("fournisseurs");
        public ILiteCollection<Produit> Produits => liteDatabase.GetCollection<Produit>("produits");

        // Utilisation de entreprise directement
        public Entreprise Entreprise
        {
            get => liteDatabase.GetCollection<Entreprise>("entreprises").FindAll().FirstOrDefault();
            set => liteDatabase.GetCollection<Entreprise>("entreprises").Update(value);
        }

        private void InitialiserBaseDeDonnees()
        {
            liteDatabase = new LiteDatabase("entreprise.db");

            if (Entreprise == null)
            {
                var entreprise = new Entreprise
                {
                    Nom = "Cesi (CESI)",
                    SIRET = "77572257201109",
                    Adresse = "TOUR PB5, 1 AVENUE DU GENERAL DE GAULLE, 92800 PUTEAUX",
                };

                liteDatabase.GetCollection<Entreprise>("entreprises").Insert(entreprise);
            }
        }

        
        public List<Salarie> ObtenirSalaries()
        {

            return Salaries.FindAll().ToList();
        }



        public List<Fournisseur> ObtenirFournisseurs()
        {

            return Fournisseurs.FindAll().ToList();
        }

        public List<Client> ObtenirClients()
        {

            return Clients.FindAll().ToList();
        }

        public int ObtenirProchainIdDisponible<T>()
        {
            // Obtenez la collection correspondante au type T
            var collection = liteDatabase.GetCollection<T>(typeof(T).Name.ToLower());
            var test = collection.FindOne(Query.All("Id", Query.Descending));
            Console.WriteLine(test.ToString());
            Console.ReadLine();
            int prochainId = 6;
            return prochainId;
        }













        public void AfficherEntreprise()
        {
            var entreprise = this.Entreprise;
            Console.Clear();
            Console.WriteLine("Informations de l'entreprise:");
            Console.WriteLine($"Nom: {entreprise.Nom}");
            Console.WriteLine($"SIRET: {entreprise.SIRET}");
            Console.WriteLine($"Adresse: {entreprise.Adresse}");
            Console.ReadLine();

        }


        public void AfficherSalaries()
        {
            if (this.ObtenirSalaries().Count() != 0)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                Console.WriteLine("Liste des salariés :");
                foreach (var salaire in this.ObtenirSalaries())
                {
                    Console.WriteLine($"{salaire.Matricule} - {salaire.Prenom} {salaire.Nom} - {salaire.Salaire}€");
                }
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de salariés !");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public void AfficherClients()
        {
            if (this.ObtenirClients().Count() != 0)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                Console.WriteLine("Liste des clients :");
                foreach (var client in this.ObtenirClients())
                {
                    Console.WriteLine($"{client.NumeroClient} - {client.Prenom} {client.Nom}");
                }
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de clients !");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public void AfficherFournisseurs()
        {
            if (this.ObtenirFournisseurs().Count() != 0)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                Console.WriteLine("Liste des fournisseurs :");
                foreach (var fournisseur in this.ObtenirFournisseurs())
                {
                    Console.WriteLine($"{fournisseur.NumeroFournisseur} - {fournisseur.Prenom} {fournisseur.Nom}");
                }
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de fournisseurs !");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public void SupprimerSalarie()
        {
            var suppressionSalarieService = new SuppressionSalarieService(this);
            if (ObtenirSalaries().Count() != 0)
            {
                Console.Clear();
                Console.WriteLine("Liste des salariés");
                foreach (var salaire in ObtenirSalaries())
                {
                    Console.WriteLine($"{salaire.Id} - {salaire.Prenom} {salaire.Nom}");
                }

                Console.WriteLine("Saisir l'id du salarié à retirer : ");
                long idSalarie;

                while (true)
                {
                    if (long.TryParse(Console.ReadLine(), out idSalarie))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Veuillez saisir un ID valide.");
                    }
                }


                suppressionSalarieService.SupprimerSalarie(idSalarie);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de salariés !");
                Console.ReadLine();
                Console.Clear();
            }
        }



        public void SupprimerFournisseur()
        {
            var suppressionFournisseurService = new SuppressionFournisseurService(this);
            if (ObtenirFournisseurs().Count() != 0)
            {
                Console.Clear();
                Console.WriteLine("Liste des clients :");
                foreach (var fournisseur in ObtenirFournisseurs())
                {
                    Console.WriteLine($"{fournisseur.Id} - {fournisseur.Prenom} {fournisseur.Nom}");
                }

                Console.WriteLine("Saisir l'id du fournisseur à retirer : ");
                long idFournisseur;

                while (true)
                {
                    if (long.TryParse(Console.ReadLine(), out idFournisseur))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Veuillez saisir un ID valide.");
                    }
                }


                suppressionFournisseurService.SupprimerFournisseur(idFournisseur);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de fournisseurs !");
                Console.ReadLine();
                Console.Clear();
            }
        }


        public void SupprimerClient()
        {
            var suppressionClientService = new SuppressionClientService(this);
            if (ObtenirClients().Count() != 0)
            {
                Console.Clear();
                Console.WriteLine("Liste des clients :");
                foreach (var client in ObtenirClients())
                {
                    Console.WriteLine($"{client.Id} - {client.Prenom} {client.Nom}");
                }

                Console.WriteLine("Saisir l'id du client à retirer : ");
                long idClient;

                while (true)
                {
                    if (long.TryParse(Console.ReadLine(), out idClient))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Veuillez saisir un ID valide.");
                    }
                }


                suppressionClientService.SupprimerClient(idClient);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de clients !");
                Console.ReadLine();
                Console.Clear();
            }
        }


        public void AjouterSalarie()
        {
            var ajoutSalarieService = new AjoutSalarieService(this);
            Console.Clear();
            Console.Write("Nom du salarié : ");
            string nom = Console.ReadLine();

            Console.Write("Prenom du salarié : ");
            string prenom = Console.ReadLine();

            Console.Write("Adresse du salarié : ");
            string adresse = Console.ReadLine();

            Console.Write("Numéro tel. du salarié : ");
            string num_tel = Console.ReadLine();

            Console.Write("Département du salarié : ");
            string departement = Console.ReadLine();

            float salaire;
            while (true)
            {
                Console.Write("Salaire du salarié : ");
                string salaireInput = Console.ReadLine();

                if (float.TryParse(salaireInput, out salaire))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Veuillez saisir un salaire valide.");
                }
            }

            Console.Write("Poste du salarié : ");
            string poste = Console.ReadLine();
            int prochainId = ObtenirProchainIdDisponible<Salarie>();

            Salarie nouveauSalarie = new Salarie
            {
                Nom = nom,
                Prenom = prenom,
                Adresse = adresse,
                Telephone = num_tel,
                Departement = departement,
                Salaire = salaire,
                Poste = poste,
                Matricule = "MAT" + prochainId,
                Id = prochainId


            };

            ajoutSalarieService.AjouterSalarie(nouveauSalarie);
        }

        public void AjouterFournisseur()
        {
            var ajoutFournisseurService = new AjoutFournisseurService(this);
            Console.Clear();
            Console.Write("Nom du Fournisseur : ");
            string nom = Console.ReadLine();

            Console.Write("Prenom du Fournisseur : ");
            string prenom = Console.ReadLine();

            Console.Write("Adresse du Fournisseur : ");
            string adresse = Console.ReadLine();

            Console.Write("Numéro tel. du Fournisseur : ");
            string num_tel = Console.ReadLine();

            int prochainId = ObtenirProchainIdDisponible<Fournisseur>();
            List<Produit> produitsFournis = new List<Produit>();
            Fournisseur nouveauFournisseur = new Fournisseur
            {
                Nom = nom,
                Prenom = prenom,
                Adresse = adresse,
                Telephone = num_tel,
                ProduitsFournis = produitsFournis,
                NumeroFournisseur = prochainId,
                Id = prochainId


            };

            ajoutFournisseurService.AjouterFournisseur(nouveauFournisseur);
        }

        public void AjouterClient()
        {
            var ajoutClientService = new AjoutClientService(this);
            Console.Clear();
            Console.Write("Nom du Client : ");
            string nom = Console.ReadLine();

            Console.Write("Prenom du Client : ");
            string prenom = Console.ReadLine();

            Console.Write("Adresse du Client : ");
            string adresse = Console.ReadLine();

            Console.Write("Numéro tel. du Client : ");
            string num_tel = Console.ReadLine();

            int prochainId = ObtenirProchainIdDisponible<Client>();
            List<Achat> historiqueAchats = new List<Achat>();
            Client nouveauClient = new Client
            {
                Nom = nom,
                Prenom = prenom,
                Adresse = adresse,
                Telephone = num_tel,
                HistoriqueAchats = historiqueAchats,
                NumeroClient = prochainId,
                Id = prochainId


            };

            ajoutClientService.AjouterClient(nouveauClient);
        }






        public void Dispose()
        {
            liteDatabase?.Dispose();
        }
    }
}
