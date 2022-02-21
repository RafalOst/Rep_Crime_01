namespace PoliceService.Models
{
    public class PoliceOfficer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PoliceRank Rank { get; set; }

        public List<Crime> Crimes { get; set; } = new List<Crime>();
    }

    public enum PoliceRank
    {
        Constable,
        Inspector,
        Detective
    }
}