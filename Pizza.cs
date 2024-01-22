namespace PizzeriaAdminConsoleApp
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Deminutif { get; set; }
        public string Nom { get; set; }
        public float Prix { get; set; }

        public override string ToString()
        {
            return $"{Deminutif} -> {Nom} ({Prix.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("fr-FR"))})";
        }
    }
}
