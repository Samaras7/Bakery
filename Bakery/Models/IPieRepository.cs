namespace Bakery.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }
        IEnumerable<Pie> PiesOfTheWeek { get; }
        Pie? GetPieById(int pieId);
        Pie AddPie(Pie pie);
        bool UpdatePie(Pie pie);
        bool DeletePie(int pieId);
    }
}
