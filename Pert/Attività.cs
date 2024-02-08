namespace Pert;
class Attività(char lettera, List<Attività> priorità)
{
    public (float x, float y) Center { get; set; } = (-10, -10);
    public int NRiga { get; set; }
    public int NColonna { get; set; }
    public char Lettera => lettera;
    public List<Attività> Priorità => priorità;
    public List<Attività> Siblings { get; set; } = [];
    public override string ToString()
    {
        return $"{Lettera}";
    }

}