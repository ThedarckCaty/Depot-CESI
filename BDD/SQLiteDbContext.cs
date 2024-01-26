using Entreprise.Interfaces;
using Entreprise.Services;
using System.Data.SQLite;
using Dapper;
using System.Text;
using System.Data.Entity;
using System.Net.Sockets;


namespace Entreprise
{
    public class SQLiteDbContext : IDisposable, ISQLiteDbContext
    {
        private SQLiteConnection dbConnection;

        public SQLiteDbContext()
        {
            // Initialise la connexion à la base de données SQLite, en utilisant le fichier "dbCompany.db".
            // Si le fichier n'existe pas, une nouvelle base de données sera créée.
            dbConnection = new SQLiteConnection("Data Source=dbCompany.db; Version = 3; New = True;");
            dbConnection.Open();
            // Création des tables si nécessaire.
            CreerTables();
        }


        private void CreerTables()
        {
            dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS Salaries (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nom TEXT NOT NULL,
                        Prenom TEXT NOT NULL,
                        Adresse TEXT NOT NULL,
                        Telephone TEXT NOT NULL,
                        Departement TEXT NOT NULL,
                        Poste TEXT NOT NULL,
                        Salaire REAL NOT NULL,
                        Matricule TEXT NOT NULL
                    )
                ");

            dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS Clients (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nom TEXT NOT NULL,
                        Prenom TEXT NOT NULL,
                        Adresse TEXT NOT NULL,
                        Telephone TEXT NOT NULL,
                        NumeroClient INTEGER NOT NULL
                    )
                ");

            dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS Fournisseurs (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nom TEXT NOT NULL,
                        Prenom TEXT NOT NULL,
                        Adresse TEXT NOT NULL,
                        Telephone TEXT NOT NULL,
                        NumeroFournisseur INTEGER NOT NULL
                    )
                ");

            dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS Entreprise (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nom TEXT NOT NULL,
                        SIRET TEXT NOT NULL,
                        Adresse TEXT NOT NULL
                    )
                ");

           // dbConnection.Execute("DROP TABLE IF EXISTS Achats");
           // dbConnection.Execute("DROP TABLE IF EXISTS Produits");

            dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS Produits (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Reference TEXT NOT NULL,
                        IdFournisseur INTEGER NOT NULL,
                        Nom TEXT NOT NULL,
                        Prix REAL NOT NULL,
                        Quantite INTEGER NOT NULL
                    )
                ");



            dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS Achats (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        NumeroAchat INTEGER,
                        Reference TEXT NOT NULL,
                        Nom TEXT NOT NULL,
                        IdClient INTEGER NOT NULL,
                        QuantiteProduitAchete INTEGER NOT NULL,
                        DateAchat DATETIME NOT NULL
                    )
                ");

        }

        /// <summary>
        /// Exécute une commande SQL avec des paramètres s'executant sur la base de données (paramètres en option // Peut être null).
        /// </summary>
        /// <param name="sql">La requête SQL à exécuter.</param>
        /// <param name="parameters">Les paramètres à associer à la requête (peut être null si la requête n'a pas de paramètres).</param>
        public void Execute(string sql, object parameters)
        {
            using (var command = new SQLiteCommand(sql, dbConnection))
            {
                // Vérification de la présence de paramètres
                if (parameters != null)
                {
                    foreach (var param in parameters.GetType().GetProperties())
                    {
                        command.Parameters.AddWithValue("@" + param.Name, param.GetValue(parameters, null));
                    }
                }
                // Execution du code SQL
                command.ExecuteNonQuery();
            }
        }



        /// <summary>
        /// Récupère la liste de tous les salariés depuis la base de données.
        /// </summary>
        /// <returns>Une liste d'objets de type Salarie représentant les salariés.</returns>
        public List<Salarie> ObtenirSalaries()
        {
            return dbConnection.Query<Salarie>("SELECT * FROM Salaries").AsList();
        }

        /// <summary>
        /// Récupère la liste de tous les fournisseurs depuis la base de données.
        /// </summary>
        /// <returns>Une liste d'objets de type Fournisseur représentant les fournisseurs.</returns>
        public List<Fournisseur> ObtenirFournisseurs()
        {
            return dbConnection.Query<Fournisseur>("SELECT * FROM Fournisseurs").AsList();
        }

        /// <summary>
        /// Récupère la liste de tous les clients depuis la base de données.
        /// </summary>
        /// <returns>Une liste d'objets de type Client représentant les clients.</returns>
        public List<Client> ObtenirClients()
        {
            return dbConnection.Query<Client>("SELECT * FROM Clients").AsList();
        }

        /// <summary>
        /// Récupère la liste de tous les produits depuis la base de données.
        /// </summary>
        /// <returns>Une liste d'objets de type Produit représentant les produits.</returns>
        public List<Produit> ObtenirProduits()
        {
            return dbConnection.Query<Produit>("SELECT * FROM Produits Where Quantite > 0").AsList();
        }

        /// <summary>
        /// Récupère la liste de tous les achats depuis la base de données.
        /// </summary>
        /// <returns>Une liste d'objets de type Achats représentant les achats des clients.</returns>
        public List<Achat> ObtenirAchats()
        {
            return dbConnection.Query<Achat>("SELECT * FROM Achats").AsList();
        }

        /// <summary>
        /// Récupère la liste de tous les achats d'un client spécifique depuis la base de données.
        /// </summary>
        /// <param name="idClient">L'ID du client pour lequel récupérer les achats.</param>
        /// <returns>Une liste d'objets de type Achat représentant les achats du client.</returns>
        public List<Achat> ObtenirAchats(int idClient)
        {
            return dbConnection.Query<Achat>("SELECT * FROM Achats WHERE IdClient = @IdClient", new { IdClient = idClient }).AsList();
        }


        public string GetReferenceById(long idProduit)
        {
            return dbConnection.QueryFirstOrDefault<string>(
                "SELECT Reference FROM Produits WHERE Id = @Id", new { Id = idProduit });
        }

        public List<String> GetReferencesProduits(long idFournisseur)
        {
            return dbConnection.Query<String>("SELECT Reference FROM Produits WHERE IdFournisseur = @Id", new { Id = idFournisseur }).ToList();
        }


        /// <summary>
        /// Affiche les informations de l'entreprise en interrogeant la base de données.
        /// Si aucune information n'est présente, une nouvelle entrée est créée dans la table Entreprise.
        /// </summary>
        public void AfficherEntreprise()
        {
            using (var connection = new SQLiteConnection("Data Source=dbCompany.db;Version=3;"))
            {
                connection.Open();

                // Exécution de la requête pour obtenir les informations sur l'entreprise.
                var entreprise = connection.Query<Entreprise>("SELECT * FROM Entreprise").FirstOrDefault();

                if (entreprise != null)
                {
                    Console.Clear();
                    Console.WriteLine("Informations de l'entreprise:");
                    Console.WriteLine($"Nom: {entreprise.Nom}");
                    Console.WriteLine($"SIRET: {entreprise.SIRET}");
                    Console.WriteLine($"Adresse: {entreprise.Adresse}");
                    Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                    Console.ReadLine();
                    Console.Clear(); // Effacer la console avant de revenir au menu principal
                }
                else
                {
                    // Si aucune information sur l'entreprise n'est trouvée, crée une nouvelle entrée dans la table Entreprise.
                    Entreprise infoEntreprise = new Entreprise();
                    connection.Execute(@"
                        INSERT INTO Entreprise (Nom, SIRET, Adresse)
                        VALUES (@Nom, @SIRET, @Adresse)",
                    infoEntreprise);
                }
            }
        }









        /// <summary>
        /// Affiche la liste des achats en paginant les résultats pour une meilleure lisibilité sur la console.
        /// Si le client n'existe pas ou s'il n'a pas d'achats, cela affiche un message approprié.
        /// </summary>
        public void AfficherAchats()
        {
            Console.Clear();
            var clients = ObtenirClients();

            if (clients.Count == 0)
            {
                Console.WriteLine("[Erreur] - Il n'y a pas de clients !");
                Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            Console.WriteLine("Liste des clients :");

            foreach (var client in clients)
            {
                Console.WriteLine($"{client.Id} - {client.Prenom} {client.Nom}");
            }

            Console.Write("Saisir l'ID du client pour afficher les achats : ");
            int idClient;

            while (!int.TryParse(Console.ReadLine(), out idClient) || !clients.Any(c => c.Id == idClient))
            {
                Console.WriteLine("Veuillez saisir un ID client valide.");
                Console.Write("Saisir l'ID du client pour afficher les achats : ");
            }

            // Obtenir les achats du client
            var achats = this.ObtenirAchats(idClient);

            if (achats.Count() != 0)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();

                const int itemsPerPage = 10;
                int pageCount = (int)Math.Ceiling((double)achats.Count() / itemsPerPage);

                for (int page = 1; page <= pageCount; page++)
                {
                    Console.Clear();
                    Console.WriteLine($"Page {page}/{pageCount} - Liste des achats du client :");

                    var achatsPage = achats.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

                    foreach (var achat in achatsPage)
                    {
                        Console.WriteLine($"{achat.Reference} - {achat.Nom} - Quantité : {achat.QuantiteProduitAchete} - Date : {achat.DateAchat}");
                    }

                    Console.WriteLine("\nAppuyez sur Entrée pour afficher la page suivante...");

                    Console.ReadLine();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Le client n'a pas d'achats.");
                Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                Console.ReadLine();
            }

            Console.Clear();
        }

        /// <summary>
        /// Affiche la liste des salariés en paginant les résultats pour une meilleure lisibilité sur la console.
        /// Si aucun salarié n'existe alors, cela affiche un message d'erreur.
        /// </summary>
        public void AfficherSalaries()
        {
            var salaries = this.ObtenirSalaries();

            if (salaries.Count() != 0)
            {
                Console.OutputEncoding = Encoding.UTF8; // Permet de choisir l'encodage pour l'utilisation du € qui ne passe pas sans
                Console.Clear();

                const int itemsPerPage = 10; // Nombre d'éléments par page
                int pageCount = (int)Math.Ceiling((double)salaries.Count() / itemsPerPage);

                for (int page = 1; page <= pageCount; page++)
                {
                    Console.Clear(); // Nettoyage de l'écran de la console
                    Console.WriteLine($"Page {page}/{pageCount} - Liste des salariés :");

                    var salairesPage = salaries.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

                    foreach (var salaire in salairesPage)
                    {
                        Console.WriteLine($"{salaire.Matricule} - {salaire.Prenom} {salaire.Nom} - {salaire.Salaire}€");
                    }

                    Console.WriteLine("\nAppuyez sur Entrée pour afficher la page suivante...");

                    Console.ReadLine();
                }
            }
            else
            {
                // Affiche un message d'erreur si aucun salarié n'est trouvé.
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de salariés !");
                Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                Console.ReadLine();
            }
            Console.Clear();
        }

        /// <summary>
        /// Affiche la liste des ventes en paginant les résultats pour une meilleure lisibilité sur la console.
        /// Si aucune vente n'existe alors, cela affiche un message d'erreur.
        /// </summary>
        public void AfficherVentes()
        {
            var ventes = this.ObtenirAchats();

            if (ventes.Count() != 0)
            {
                Console.OutputEncoding = Encoding.UTF8; // Permet de choisir l'encodage pour l'utilisation du € qui ne passe pas sans
                Console.Clear();

                const int itemsPerPage = 10; // Nombre d'éléments par page
                int pageCount = (int)Math.Ceiling((double)ventes.Count() / itemsPerPage);

                for (int page = 1; page <= pageCount; page++)
                {
                    Console.Clear(); // Nettoyage de l'écran de la console
                    Console.WriteLine($"Page {page}/{pageCount} - Liste des ventes :");

                    var ventesPage = ventes.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

                    foreach (var vente in ventesPage)
                    {
                        Console.WriteLine($"Id du client {vente.IdClient} - Info N° {vente.NumeroAchat} Nom produit {vente.Nom} - {vente.QuantiteProduitAchete}");
                    }

                    Console.WriteLine("\nAppuyez sur Entrée pour afficher la page suivante...");

                    Console.ReadLine();
                }
            }
            else
            {
                // Affiche un message d'erreur si aucunes ventes n'est trouvées.
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de ventes !");
                Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                Console.ReadLine();
            }
            Console.Clear();
        }


        /// <summary>
        /// Affiche la liste des clients en paginant les résultats pour une meilleure lisibilité sur la console.
        /// Si aucun clients n'existe alors, cela affiche un message d'erreur.
        /// </summary>
        public void AfficherClients()
        {
            var clients = this.ObtenirClients();

            if (clients.Count() != 0)
            {
                Console.OutputEncoding = Encoding.UTF8; // Permet de choisir l'encodage pour l'utilisation du € qui ne passe pas sans
                Console.Clear();

                const int itemsPerPage = 10; // Nombre d'éléments par page
                int pageCount = (int)Math.Ceiling((double)clients.Count() / itemsPerPage);

                for (int page = 1; page <= pageCount; page++)
                {
                    Console.Clear(); // Nettoyage de l'écran de la console
                    Console.WriteLine($"Page {page}/{pageCount} - Liste des clients :");

                    var clientsPage = clients.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

                    foreach (var client in clientsPage)
                    {
                        Console.WriteLine($"Numéro client {client.Id} - {client.Prenom} {client.Nom}");
                    }

                    Console.WriteLine("\nAppuyez sur Entrée pour afficher la page suivante...");

                    Console.ReadLine();
                }
            }
            else
            {
                // Affiche un message d'erreur si aucun client n'est trouvé.
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de clients !");
                Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                Console.ReadLine();
            }

            Console.Clear(); // Effacer la console avant de revenir au menu principal
        }

        /// <summary>
        /// Affiche la liste des fournisseurs en paginant les résultats pour une meilleure lisibilité sur la console.
        /// Si aucun fournisseurs n'existe alors, cela affiche un message d'erreur.
        /// </summary>
        public void AfficherFournisseurs()
        {
            var fournisseurs = this.ObtenirFournisseurs();

            if (fournisseurs.Count() != 0)
            {
                Console.OutputEncoding = Encoding.UTF8; // Permet de choisir l'encodage pour l'utilisation du € qui ne passe pas sans
                Console.Clear();

                const int itemsPerPage = 10; // Nombre d'éléments par page
                int pageCount = (int)Math.Ceiling((double)fournisseurs.Count() / itemsPerPage);

                for (int page = 1; page <= pageCount; page++)
                {
                    Console.Clear(); // Nettoyage de l'écran de la console
                    Console.WriteLine($"Page {page}/{pageCount} - Liste des fournisseurs :");

                    var fournisseursPage = fournisseurs.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

                    foreach (var fournisseur in fournisseursPage)
                    {
                        Console.WriteLine($"ID {fournisseur.Id} - Numéro fournisseur {fournisseur.NumeroFournisseur} - {fournisseur.Prenom} {fournisseur.Nom}");
                    }

                    Console.WriteLine("\nAppuyez sur Entrée pour afficher la page suivante...");
                    Console.ReadLine();
                }
            }
            else
            {
                // Affiche un message d'erreur si aucun fournisseur n'est trouvé.
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de fournisseurs !");
                Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                Console.ReadLine();
            }
            Console.Clear(); // Effacer la console avant de revenir au menu principal
        }


        /// <summary>
        /// Affiche la liste des produits en paginant les résultats pour une meilleure lisibilité sur la console.
        /// Si aucun Produit n'existe alors, cela affiche un message d'erreur.
        /// </summary>
        public void AfficherProduits()
        {
            var produits = this.ObtenirProduits();

            if (produits.Count() != 0)
            {
                Console.OutputEncoding = Encoding.UTF8; // Permet de choisir l'encodage pour l'utilisation du € qui ne passe pas sans
                Console.Clear();

                const int itemsPerPage = 10; // Nombre d'éléments par page
                int pageCount = (int)Math.Ceiling((double)produits.Count() / itemsPerPage);

                for (int page = 1; page <= pageCount; page++)
                {
                    Console.Clear(); // Nettoyage de l'écran de la console
                    Console.WriteLine($"Page {page}/{pageCount} - Liste des produits :");

                    var produitsPage = produits.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

                    foreach (var produit in produitsPage)
                    {
                        Console.WriteLine($"{produit.Nom} - {produit.Prix}€ - Quantité du produit {produit.Quantite}");
                    }

                    Console.WriteLine("\nAppuyez sur Entrée pour afficher la page suivante...");

                    Console.ReadLine();
                }
            }
            else
            {
                // Affiche un message d'erreur si aucun produit n'est trouvé.
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de produits !");
                Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                Console.ReadLine();
            }
            Console.Clear();
        }


        /// <summary>
        /// Affiche la liste des salariés, permet à l'utilisateur de saisir l'ID d'un salarié à supprimer,
        /// puis appelle le service de suppression pour effectuer la suppression.
        /// Affiche un message d'erreur si aucun salarié n'existe.
        /// </summary>
        public void SupprimerSalarie()
        {
            // Initialisation du service de suppression des salariés
            var suppressionSalarieService = new SuppressionSalarieService(this);

            // Obtention de la liste des salariés
            var salaries = ObtenirSalaries();

            if (salaries.Count != 0)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                Console.WriteLine("Liste des salariés:");

                foreach (var salaire in salaries)
                {
                    Console.WriteLine($"{salaire.Id} - {salaire.Prenom} {salaire.Nom}");
                }

                Console.Write("Saisir l'ID du salarié à retirer : ");
                long idSalarie;

                // Boucle de saisie sécurisée pour obtenir un ID valide
                while (!long.TryParse(Console.ReadLine(), out idSalarie) || !salaries.Any(s => s.Id == idSalarie))
                {
                    Console.WriteLine("Veuillez saisir un ID valide.");
                }

                // Appel du service de suppression avec l'ID du salarié à supprimer
                suppressionSalarieService.SupprimerSalarie(idSalarie);
            }
            else
            {
                // Affiche un message d'erreur si aucun salarié n'existe.
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de salariés !");
            }

            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
            Console.ReadLine();
            Console.Clear(); // Effacer la console avant de revenir au menu principal
        }





        /// <summary>
        /// Affiche la liste des fournisseurs, permet à l'utilisateur de saisir l'ID d'un fournisseur à supprimer,
        /// puis appelle le service de suppression pour effectuer la suppression.
        /// Affiche un message d'erreur si aucun fournisseur n'existe.
        /// </summary>
        public void SupprimerFournisseur()
        {
            // Initialisation du service de suppression des fournisseurs
            var suppressionFournisseurService = new SuppressionFournisseurService(this);

            // Obtention de la liste des fournisseurs
            var fournisseurs = ObtenirFournisseurs();

            if (fournisseurs.Count != 0)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                Console.WriteLine("Liste des fournisseurs:");

                foreach (var fournisseur in fournisseurs)
                {
                    Console.WriteLine($"{fournisseur.Id} - {fournisseur.Prenom} {fournisseur.Nom}");
                }

                Console.Write("Saisir l'ID du fournisseur à retirer : ");
                long idFournisseur;

                // Boucle de saisie sécurisée pour obtenir un ID valide
                while (!long.TryParse(Console.ReadLine(), out idFournisseur) || !fournisseurs.Any(s => s.Id == idFournisseur))
                {
                    Console.WriteLine("Veuillez saisir un ID valide.");
                }

                // Appel du service de suppression avec l'ID du fournisseur à supprimer
                suppressionFournisseurService.SupprimerFournisseur(idFournisseur);
            }
            else
            {
                // Affiche un message d'erreur si aucun fournisseur n'existe.
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de fournisseurs !");
            }

            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
            Console.ReadLine();
            Console.Clear(); // Effacer la console avant de revenir au menu principal
        }



        /// <summary>
        /// Affiche la liste des clients, permet à l'utilisateur de saisir l'ID d'un client à supprimer,
        /// puis appelle le service de suppression pour effectuer la suppression.
        /// Affiche un message d'erreur si aucun client n'existe.
        /// </summary>
        public void SupprimerClient()
        {
            // Initialisation du service de suppression des clients
            var suppressionClientService = new SuppressionClientService(this);

            // Obtention de la liste des clients
            var clients = ObtenirClients();

            if (clients.Count != 0)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                Console.WriteLine("Liste des clients:");

                foreach (var client in clients)
                {
                    Console.WriteLine($"{client.Id} - {client.Prenom} {client.Nom}");
                }

                Console.Write("Saisir l'ID du client à retirer : ");
                long idClient;

                // Boucle de saisie sécurisée pour obtenir un ID valide
                while (!long.TryParse(Console.ReadLine(), out idClient) || !clients.Any(s => s.Id == idClient))
                {
                    Console.WriteLine("Veuillez saisir un ID valide.");
                }

                // Appel du service de suppression avec l'ID du client à supprimer
                suppressionClientService.SupprimerClient(idClient);
            }
            else
            {
                // Affiche un message d'erreur si aucun client n'existe.
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de clients !");
            }

            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
            Console.ReadLine();
            Console.Clear(); // Effacer la console avant de revenir au menu principal
        }



        /// <summary>
        /// Affiche la liste des clients, permet à l'utilisateur de saisir l'ID d'un client à supprimer,
        /// puis appelle le service de suppression pour effectuer la suppression.
        /// Affiche un message d'erreur si aucun client n'existe.
        /// </summary>
        public void SupprimerProduit()
        {
            // Initialisation du service de suppression des produits
            var suppressionProduitService = new SuppressionProduitService(this);

            // Obtention de la liste des produits
            var produits = ObtenirProduits();

            if (produits.Count != 0)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                Console.WriteLine("Liste des produits:");

                foreach (var produit in produits)
                {
                    Console.WriteLine($"{produit.Id} - {produit.Nom}");
                }

                Console.Write("Saisir l'ID du produit à retirer : ");
                long idProduit;

                // Boucle de saisie sécurisée pour obtenir un ID valide
                while (!long.TryParse(Console.ReadLine(), out idProduit) || !produits.Any(s => s.Id == idProduit))
                {
                    Console.WriteLine("Veuillez saisir un ID valide.");
                }

                // Appel du service de suppression avec l'ID du produit à supprimer
                suppressionProduitService.SupprimerProduit(idProduit);
            }
            else
            {
                // Affiche un message d'erreur si aucun produit n'existe.
                Console.Clear();
                Console.WriteLine("[Erreur] - Il n'y a pas de produits !");
            }

            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
            Console.ReadLine();
            Console.Clear(); // Effacer la console avant de revenir au menu principal
        }




        /// <summary>
        /// Permet à l'utilisateur de saisir les informations d'un nouveau salarié,
        /// valide chaque champ et ajoute le salarié en utilisant le service d'ajout de salarié.
        /// </summary>
        public void AjouterSalarie()
        {

            int rowCount = dbConnection.ExecuteScalar<int>("SELECT COUNT(*) FROM Salaries;");

            // Réinitialisation de la séquence d'auto-incrémentation uniquement si la table est vide
            if (rowCount == 0)
            {
                dbConnection.Execute("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Salaries';");
            }

            // Initialisation du service d'ajout de salarié
            var ajoutSalarieService = new AjoutSalarieService(this);

            // Efface la console pour une meilleure lisibilité
            Console.Clear();

            // Initialisation des variables pour stocker les informations du salarié
            string nom = string.Empty;
            string prenom = string.Empty;
            string adresse = string.Empty;
            string num_tel = string.Empty;
            string departement = string.Empty;
            string poste = string.Empty;
            float salaire;

            // Saisie sécurisée des informations du salarié avec des boucles de validation
            while (string.IsNullOrWhiteSpace(nom))
            {
                Console.Write("Nom du salarié : ");
                nom = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nom))
                {
                    Console.WriteLine("Le nom ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(prenom))
            {
                Console.Write("Prenom du salarié : ");
                prenom = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(prenom))
                {
                    Console.WriteLine("Le prénom ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(adresse))
            {
                Console.Write("Adresse du salarié : ");
                adresse = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(adresse))
                {
                    Console.WriteLine("L'adresse ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(num_tel))
            {
                Console.Write("Numéro tel. du salarié : ");
                num_tel = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(num_tel))
                {
                    Console.WriteLine("Le numéro de tél. ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(departement))
            {
                Console.Write("Département du salarié : ");
                departement = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(departement))
                {
                    Console.WriteLine("Le département ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(poste))
            {
                Console.Write("Poste du salarié : ");
                poste = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(poste))
                {
                    Console.WriteLine("Le poste ne peut pas être vide. Veuillez réessayer.");
                }
            }

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

            // Création d'un nouveau salarié avec un matricule généré
            Salarie nouveauSalarie = new(nom, prenom, adresse, num_tel, departement, poste, salaire, "MAT" + Guid.NewGuid());

            // Appel du service d'ajout de salarié
            ajoutSalarieService.AjouterSalarie(nouveauSalarie);
        }


        /// <summary>
        /// Permet à l'utilisateur de saisir les informations d'un nouveau fournisseur,
        /// valide chaque champ et ajoute le fournisseur en utilisant le service d'ajout de fournisseur.
        /// </summary>
        public void AjouterFournisseur()
        {
            int rowCount = dbConnection.ExecuteScalar<int>("SELECT COUNT(*) FROM Fournisseurs;");

            // Réinitialisation de la séquence d'auto-incrémentation uniquement si la table est vide
            if (rowCount == 0)
            {
                dbConnection.Execute("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Fournisseurs';");
            }

            // Initialisation du service d'ajout de fournisseur
            var ajoutFournisseurService = new AjoutFournisseurService(this);

            // Efface la console pour une meilleure lisibilité
            Console.Clear();

            // Initialisation des variables pour stocker les informations du fournisseur
            string nom = string.Empty;
            string prenom = string.Empty;
            string adresse = string.Empty;
            string num_tel = string.Empty;

            // Saisie sécurisée des informations du fournisseur avec des boucles de validation
            while (string.IsNullOrWhiteSpace(nom))
            {
                Console.Write("Nom du fournisseur : ");
                nom = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nom))
                {
                    Console.WriteLine("Le nom ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(prenom))
            {
                Console.Write("Prenom du fournisseur : ");
                prenom = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(prenom))
                {
                    Console.WriteLine("Le prénom ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(adresse))
            {
                Console.Write("Adresse du fournisseur : ");
                adresse = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(adresse))
                {
                    Console.WriteLine("L'adresse ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(num_tel))
            {
                Console.Write("Numéro tel. du fournisseur : ");
                num_tel = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(num_tel))
                {
                    Console.WriteLine("Le numéro de tél. ne peut pas être vide. Veuillez réessayer.");
                }
            }

            // Requête pour obtenir le dernier ID des fournisseurs
            string query = "SELECT MAX(Id) FROM Fournisseurs";
            int? dernierId = dbConnection.QueryFirstOrDefault<int?>(query) + 1;

            // Création d'un nouveau fournisseur avec un nouvel ID ou l'ID suivant
            Fournisseur nouveauFournisseur = new(dernierId.GetValueOrDefault(1), nom, prenom, adresse, num_tel);

            // Appel du service d'ajout de fournisseur
            ajoutFournisseurService.AjouterFournisseur(nouveauFournisseur);
        }


        /// <summary>
        /// Permet à l'utilisateur de saisir les informations d'un nouveau client,
        /// valide chaque champ et ajoute le client en utilisant le service d'ajout de client.
        /// </summary>
        public void AjouterClient()
        {
            int rowCount = dbConnection.ExecuteScalar<int>("SELECT COUNT(*) FROM Clients;");

            // Réinitialisation de la séquence d'auto-incrémentation uniquement si la table est vide
            if (rowCount == 0)
            {
                dbConnection.Execute("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Clients';");
            }

            // Initialisation du service d'ajout de client
            var ajoutClientService = new AjoutClientService(this);

            // Efface la console pour une meilleure lisibilité
            Console.Clear();

            // Initialisation des variables pour stocker les informations du client
            string nom = string.Empty;
            string prenom = string.Empty;
            string adresse = string.Empty;
            string num_tel = string.Empty;

            // Saisie sécurisée des informations du client avec des boucles de validation
            while (string.IsNullOrWhiteSpace(nom))
            {
                Console.Write("Nom du client : ");
                nom = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nom))
                {
                    Console.WriteLine("Le nom ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(prenom))
            {
                Console.Write("Prenom du client : ");
                prenom = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(prenom))
                {
                    Console.WriteLine("Le prénom ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(adresse))
            {
                Console.Write("Adresse du client : ");
                adresse = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(adresse))
                {
                    Console.WriteLine("L'adresse ne peut pas être vide. Veuillez réessayer.");
                }
            }

            while (string.IsNullOrWhiteSpace(num_tel))
            {
                Console.Write("Numéro tel. du client : ");
                num_tel = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(num_tel))
                {
                    Console.WriteLine("Le numéro de tél. ne peut pas être vide. Veuillez réessayer.");
                }
            }

            // Requête pour obtenir le dernier ID des clients
            string query = "SELECT MAX(Id) FROM Clients";
            int? dernierId = dbConnection.QueryFirstOrDefault<int?>(query) + 1;

            // Création d'un nouveau client avec un nouvel ID ou l'ID suivant
            Client nouveauClient = new(nom, prenom, adresse, num_tel, dernierId.GetValueOrDefault(1));

            // Appel du service d'ajout de client
            ajoutClientService.AjouterClient(nouveauClient);
        }

        /// <summary>
        /// Permet à l'utilisateur de saisir les informations d'un nouveau produit,
        /// valide chaque champ et ajoute le produit en utilisant le service d'ajout de produit.
        /// </summary>
        public void AjouterProduit()
        {

            int rowCount = dbConnection.ExecuteScalar<int>("SELECT COUNT(*) FROM Produits;");

            // Réinitialisation de la séquence d'auto-incrémentation uniquement si la table est vide
            if (rowCount == 0)
            {
                dbConnection.Execute("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Produits';");
            }

            // Initialisation du service d'ajout de produit
            var ajoutProduitService = new AjoutProduitService(this);

            // Efface la console pour une meilleure lisibilité
            Console.Clear();

            // Initialisation des variables pour stocker les informations du produit
            string reference = string.Empty;
            int idfournisseur = 0;
            string nom = string.Empty;
            float prix;
            int quantite;

            // Saisie sécurisée des informations du produit avec des boucles de validation

            var fournisseurs = ObtenirFournisseurs();

            if (fournisseurs.Count != 0)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                Console.WriteLine("Liste des fournisseurs:");

                foreach (var fournisseur in fournisseurs)
                {
                    Console.WriteLine($"{fournisseur.Id} - {fournisseur.Prenom} {fournisseur.Nom}");
                }

                while (true)
                {
                    Console.Write("Saisir l'ID du fournisseur à retirer : ");

                    if (int.TryParse(Console.ReadLine(), out idfournisseur) && fournisseurs.Any(s => s.Id == idfournisseur))
                    {
                        break; // Sortir de la boucle si l'ID est valide
                    }
                    else
                    {
                        Console.WriteLine("Veuillez saisir un ID fournisseur valide.");
                    }
                }
            }

            while (string.IsNullOrWhiteSpace(nom))
            {
                Console.Write("Nom du produit : ");
                nom = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nom))
                {
                    Console.WriteLine("Le nom du produit ne peut pas être vide. Veuillez réessayer.");
                }
            }


            while (true)
            {
                Console.Write("Prix du produit : ");
                string prixInput = Console.ReadLine();

                if (float.TryParse(prixInput, out prix))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Veuillez saisir un prix de produit valide.");
                }
            }

            while (true)
            {
                Console.Write("Quantité du produit : ");
                string quantiteInput = Console.ReadLine();

                if (int.TryParse(quantiteInput, out quantite))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Veuillez saisir une quantité de produit valide.");
                }
            }



            // Création d'un nouveau produit avec une reference généré
            Produit nouveauProduit = new("ref" + Guid.NewGuid(), idfournisseur, nom, prix, quantite);

            // Appel du service d'ajout d'un produit
            ajoutProduitService.AjouterProduit(nouveauProduit);
        }

        /// <summary>
        /// Permet à l'utilisateur de saisir les informations pour un achat d'un produit,
        /// valide chaque champ et achete le produit en utilisant le service d'achat de produit.
        /// </summary>
        public void AchatProduit()
        {
            int rowCount = dbConnection.ExecuteScalar<int>("SELECT COUNT(*) FROM Achats;");

            // Réinitialisation de la séquence d'auto-incrémentation uniquement si la table est vide
            if (rowCount == 0)
            {
                dbConnection.Execute("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Achats';");
            }

            // Initialisation du service d'achat de produit
            var achatProduitService = new AchatProduitService(this);
            var venteProduitService = new VenteProduitService(this);

            // Efface la console pour une meilleure lisibilité
            Console.Clear();

            // Initialisation des variables pour stocker les informations du produit pour l'achat
            string referenceProduit = string.Empty;
            int numeroAchat = 0;
            string nomProduit = string.Empty;
            int quantiteProduitAchete;
            int idClient;
            int idProduit = 0;
            DateTime dateAchatProduit;
            // Vérification de l'existence de clients
            var clients = ObtenirClients();

            if (clients.Count == 0)
            {
                Console.WriteLine("[Erreur] - Il n'y a pas de clients !");
                Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                Console.ReadLine();
                return;
            }

            // Saisie sécurisée des informations du client avec des boucles de validation
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            Console.WriteLine("Liste des clients:");

            foreach (var client in clients)
            {
                Console.WriteLine($"{client.Id} - {client.Prenom} {client.Nom}");
            }

            while (true)
            {
                Console.Write("Saisir l'ID du client : ");

                if (int.TryParse(Console.ReadLine(), out idClient) && clients.Any(c => c.Id == idClient))
                {
                    break; // Sortir de la boucle si l'ID est valide
                }
                else
                {
                    Console.WriteLine("Veuillez saisir un ID client valide.");
                }
            }

            // Vérification de l'existence de produits
            var produits = ObtenirProduits();

            if (produits.Count == 0)
            {
                Console.WriteLine("[Erreur] - Il n'y a pas de produits !");
                Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("Liste des produits:");

            foreach (var produit in produits)
            {
                Console.WriteLine($"{produit.Id} - {produit.Nom} - Quantité : {produit.Quantite} - Prix : {produit.Prix}€");
            }

            while (true)
            {
                Console.Write("Saisir l'ID du produit : ");

                if (int.TryParse(Console.ReadLine(), out idProduit) && produits.Any(p => p.Id == idProduit))
                {
                    break; // Sortir de la boucle si l'ID est valide
                }
                else
                {
                    Console.WriteLine("Veuillez saisir un ID produit valide.");
                }
            }

            DateTime dateActuelle = DateTime.Now;

            string query = $"SELECT * FROM Produits WHERE Id = @Id";
            Produit produitAchat = dbConnection.QueryFirstOrDefault<Produit>(query, new { Id = idProduit });
            int quantiteDisp = produitAchat.Quantite;

            while(true)
{
                Console.Write($"Saisir la quantité (disponible : {quantiteDisp}) : ");

                if (int.TryParse(Console.ReadLine(), out quantiteProduitAchete) && quantiteProduitAchete > 0 && quantiteProduitAchete <= quantiteDisp)
                {
                    break; // Sortir de la boucle si la quantité est valide
                }
                else
                {
                    Console.WriteLine($"Veuillez saisir une quantité valide (disponible : {quantiteDisp}).");
                }
            }

            // Création d'un nouvelle achat avec une référence générée
            Achat nouvelleAchat = new(produitAchat.Reference, produitAchat.Nom, idClient, quantiteProduitAchete, dateActuelle);

            // Appel du service d'achat de produit
            achatProduitService.AcheterProduit(nouvelleAchat);
            venteProduitService.GestionAchat(nouvelleAchat, quantiteDisp);
            
        }


        /// <summary>
        /// Libère les ressources non managées utilisées par la classe, ferme la connexion à la base de données.
        /// </summary>
        public void Dispose()
        {
            // Ferme et libère la connexion à la base de données si elle est ouverte
            dbConnection?.Close();
            dbConnection?.Dispose();
        }

    }
}
