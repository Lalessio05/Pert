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
            Attività a = new('a', []);
            Attività b = new('b', [a]);
            Attività c = new('c', [a]);
            Attività d = new('d', [a]);
            Attività e = new('e', [c]);
            Attività f = new('f', [b]);
            Attività g = new('g', [f]);


            Attività = [a, b, c, d, e, f,g];

            var AttivitàDivisePerPriorità = Attività.GroupBy(n => string.Join(" ", n.Priorità.Select(x => x.Lettera) ?? []));
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
            int nRighe = 3;
            for (int i = 0; i < nRighe; i++)
                Tia.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));


            for (int i = 0; i < Attività.Count; i++)
                Tia.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

            Attività.Sort((x, y) => x.Priorità.Count.CompareTo(y.Priorità.Count));
            foreach (var attività in Attività)
            {
                if (attività.Priorità.Any())
                {
                    attività.NColonna = attività.Priorità.Select(x => x.NColonna).Max() + 1;
                }
                else
                {
                    attività.NColonna = 0;
                }
            }

            {
                foreach (var attività in Attività)
                {
                    if (attività.Siblings.Count == 0)
                    {
                        if (attività.Priorità.Count != 0)
                            attività.NRiga = (int)attività.Priorità.Select(x => x.NRiga).Average();
                        else
                            attività.NRiga = (int)Math.Round((double)nRighe / 2) - 1; //Return center??
                    }
                    else
                    {
                        List<Attività> lista = [attività];
                        lista.AddRange(attività.Siblings);
                        lista.Sort((x, y) => x.Center.x.CompareTo(y.Center.x));
                        attività.NRiga = lista.IndexOf(lista.First(x => x.Center.x == attività.Center.x));
                    }
                }
            }
            foreach (var attività in Attività)
            {
                var cerchio = new Ellipse() { WidthRequest = 100, HeightRequest = 100, BackgroundColor = Colors.Red, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
                //Da togliere
                Label l = new() { Text = attività.Lettera.ToString().ToUpper(), HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, TextColor = Colors.White, FontSize = 20 };
                
                    Tia.Add(cerchio, attività.NColonna, attività.NRiga);    //Se ha più fratelli allora gestisci
                    Tia.Add(l, attività.NColonna, attività.NRiga);
                
            }

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


        public static int NumeroPrimoPrecedente(int n)
        {
            return 2;
        }
    }
}


