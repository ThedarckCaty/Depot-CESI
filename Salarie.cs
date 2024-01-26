namespace Entreprise
{
    public class Salarie : Personne
    {

        public Salarie() { }

        public Salarie(string nom, string prenom, string adresse, string telephone, string departement, string poste, float salaire, string matricule)
        : base(nom, prenom, adresse, telephone)
        {
            Departement = departement;
            Poste = poste;
            Salaire = salaire;
            Matricule = matricule;
        }


        private string _departement;
        public string Departement
        {
            get { return _departement; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le département ne peut pas être vide.");
                }
                _departement = value;
            }
        }

        private string _poste;
        public string Poste
        {
            get { return _poste; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le poste ne peut pas être vide.");
                }
                _poste = value;
            }
        }

        private float _salaire;
        public float Salaire
        {
            get { return _salaire; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Le salaire doit être supérieur à zéro.");
                }
                _salaire = value;
            }
        }

        public string Matricule { get; set; }
    }

}

