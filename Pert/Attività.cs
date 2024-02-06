namespace Pert;
class Attività(char lettera, List<Attività>? priorità)
{
    public (float x, float y) Center { get; set; } = (-10, -10);
    public int NRiga { get
        {
            List<Attività> lista = [this];
            lista.AddRange(Siblings);
            lista.Sort((x, y) => x.Center.x.CompareTo( y.Center.x));
            return lista.IndexOf(lista.First(x=>x.Center.x == this.Center.x));
        } }
    public char Lettera => lettera;
    public List<Attività>? Priorità => priorità;
    public List<Attività> Siblings { get; set; } = [];

}