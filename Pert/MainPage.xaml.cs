using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;

namespace Pert
{
    public partial class MainPage : ContentPage
    {
        List<Attività> Attività = [];
        public MainPage()
        {
            InitializeComponent();
            Gianni();
            FaiComparireCerchi();
        }

        void Gianni()
        {
            float screenWidth = 500;
            Attività a = new('a', null);
            Attività b = new('b', [a]);
            Attività c = new('c', [a]);
            Attività d = new('d', [a]);


            Attività = [a, b, c, d];

            var AttivitàDivisePerPriorità = Attività.GroupBy(n => string.Join(" ", n.Priorità ?? []));
            foreach (var x in Attività)
            {
                var m = AttivitàDivisePerPriorità.Where(j => j.Contains(x)).ToList()[0];
                if (m.Count() > 1)
                {
                    x.Siblings.AddRange(m.Where(z => z != x));
                }
            }



            foreach (var attività in Attività)
            {
                if (attività.Siblings.Count == 0)
                    attività.Center = (250, 0);
                else
                {
                    if (attività.Siblings.All(x => x.Center.x == -10))
                        attività.Center = (0, 0);
                    else
                    {
                        int n = attività.Siblings.Where(x => x.Center.x != -10).Count();
                        attività.Center = (screenWidth / attività.Siblings.Count * n, 0);
                    }
                }
            }
        }

        void FaiComparireCerchi()
        {
            int nColonne = test(Attività.Select(x => x.Siblings.Count+1).ToList());
            for (int i = 0; i < nColonne; i++)
                Tia.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
            for (int i = 0; i< Attività.Count; i++)
                Tia.RowDefinitions.Add(new RowDefinition(GridLength.Star));

            //Guarda su che colonna è la più lontana delle priorità e vai nella colonna dopo
           

            //Crea un grafo in un qualche modo e infilali  in qualche modo
        }
        static int gcd(int n1, int n2)
        {
            if (n2 == 0)
            {
                return n1;
            }
            else
            {
                return gcd(n2, n1 % n2);
            }
        }

        public static int test(List<int> numbers)
        {
            return numbers.Aggregate((S, val) => S * val / gcd(S, val));
        }
    }
}


