using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras_1____
{/// <summary>
/// Ši klasė skirta duomenim apie studentą
/// </summary>
    class Studentas 
    {
        private double ūgis, amžius, svoris; //studento duomenys
        public Studentas(double ūgis, double amžius, double svoris)
        {
            this.ūgis = ūgis;
            this.amžius = amžius;
            this.svoris = svoris;
        }
        public double Imtiūgį() { return ūgis; }
        public double Imtiamžių() { return amžius; }
        public double ImtiSvorį() { return svoris; }
    }
    /// <summary>
    /// Ši klasė skirta duomenim apie liftą.
    /// </summary>
    class Liftas
    {
        private double galia, talpa;
        public Liftas(double galia, double talpa) //Lifto duomenys
        {
            this.galia = galia;
            this.talpa = talpa;

        }
        public double Imtigalią() { return galia; }
        public double Imtitalpą() { return talpa; }
        public void Dėtigalią(double galia) { this.galia = galia; }
        public void Dėtitalpą(double talpa) { this.talpa = talpa; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Studentas s1, s2, s3;
            double u1, a1, kg1;
            // Pirmo studento duomenų įvedimas
            Console.WriteLine("Įveskite 1-ojo studenti ūgį centimetrais:"); 
            u1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Įveskite 1-ojo studenti amžių metais:");
            a1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Įveskite 1-ojo studenti svorį kilogramais:");
            kg1 = double.Parse(Console.ReadLine());
            //Pradiniai studentų duomenys
            s1 = new Studentas(u1, a1, kg1);
            s2 = new Studentas(180, 24, 70);
            s3 = new Studentas(190, 23, 80);
            //Lentelė pradiniams studentų duomenims įšvesti
            SpausdLentele(s1, s2, s3);
            //Tikrina ar yra studentų su vienodais ūgiais
            if (s1.Imtiūgį() == s3.Imtiūgį())
                Spausdinti2(s1, s3);
            else 
            Console.WriteLine("aukščiausio studento ūgis(cm)=" + "{0}\n"
                + " jo amžius =" +
                "{1}", SkaičAmžių(s1, s2, s3),
                Skaičiavimas((SkaičAmžių(s1, s2, s3)), s1, s2, s3));
            //Tikrina ar yra studentų su vienodu amžiumi
            if (s1.Imtiamžių() == s3.Imtiamžių())
            {
                Spausdinti1(s1, s3);
            }
            else
                Console.WriteLine("jauniausiam studentui yra =" + "{0}\n" + "jo ūgis(cm) ="
                    + "{1}", SkaičŪgį(s1, s2, s3),
                    IfŪgis((SkaičŪgį(s1, s2, s3)), s1, s2, s3));
            Liftas l1;
            //Pradiniai lifto duomenys
            l1 = new Liftas(200, 3);
            //Lentelė pradiniams lifto duomenims įšvesti
            SpausdLentele2(l1);
            //Spausdina per kiek kartu liftas pakels studentus
            Console.WriteLine("liftas studentus pakels per: " + "{0}"
                + " kartus", ELiftas(l1, s1, s2, s3));
            l1.Dėtigalią(l1.Imtigalią() * 2);
            //Spausdina per kiek kartu liftas pakels studentus jei galia 2x
            Console.WriteLine("Jei galia 2x liftas studentus pakels per: "
                + "{0}" + " kartus", ELiftas(l1, s1, s2, s3));
            l1.Dėtigalią(l1.Imtigalią() / 2);
            l1.Dėtitalpą(l1.Imtitalpą() * 2);
            //Spausdina per kiek kartu liftas pakels studentus jei talpa 2x
            Console.WriteLine("Jei talpa 2x liftas studentus pakels per: "
                + "{0}" + " kartus", ELiftas(l1, s1, s2, s3));
        }

        /// <summary>
        /// Suranda aukščiausia studentą
        /// </summary>
        /// <param name="s1"> pirmas studentas </param>
        /// <param name="s2">antras studentas</param>
        /// <param name="s3">trečias studentas</param>
        /// <returns>Gražina aukščiausią studento ūgį</returns>
        public static double SkaičAmžių(Studentas s1, Studentas s2, Studentas s3)
        {
            double a;
            a = Math.Max(Math.Max(s1.Imtiūgį(), s2.Imtiūgį()), s3.Imtiūgį());
            return a;

        }
        /// <summary>
        /// Suranda aukščiausio studento amžių
        /// </summary>
        /// <param name="s1"> pirmas studentas </param>
        /// <param name="s2">antras studentas</param>
        /// <param name="s3">trečias studentas</param>
        /// <param name="SkaičAmžių">metodas surandantis aukščiausią studentą</param>
        /// <returns>Gražina aukščiausio studento amžių</returns>
        public static double Skaičiavimas(double SkaičAmžių, Studentas s1, Studentas s2, Studentas s3)
        { double ats1;
            if (SkaičAmžių == s3.Imtiūgį())
                ats1 = s3.Imtiamžių();
            else if (SkaičAmžių == s2.Imtiūgį())
                ats1 = s2.Imtiamžių();
            else
                ats1 = s1.Imtiamžių();
            return ats1;
        }
        /// <summary>
        /// Suranda jauniausią studentą
        /// </summary>
        /// <param name="s1"> pirmas studentas </param>
        /// <param name="s2">antras studentas</param>
        /// <param name="s3">trečias studentas</param>
        /// <returns>Gražina jauniausio studento amžių</returns>
        public static double SkaičŪgį(Studentas s1, Studentas s2, Studentas s3)
        {
            double b;
            b = Math.Min(Math.Min(s1.Imtiamžių(), s2.Imtiamžių()), s3.Imtiamžių());
            return b;
        }
        /// <summary>
        /// Suranda jauniausio studento ūgį
        /// </summary>
        /// <param name="s1"> pirmas studentas </param>
        /// <param name="s2">antras studentas</param>
        /// <param name="s3">trečias studentas</param>
        /// <param name="SkaičŪgį">metodas surandantis jauniausią studentą</param>
        /// <returns>Gražina jauniausio studento ūgį</returns>
        public static double IfŪgis(double SkaičŪgį, Studentas s1, Studentas s2, Studentas s3)
        {
            double ats2;
            if (SkaičŪgį == s2.Imtiamžių())
                ats2 = s2.Imtiūgį();
            else if (SkaičŪgį == s3.Imtiamžių())
                ats2 = s3.Imtiūgį();
            else
                ats2 = s1.Imtiūgį();
            return ats2;
        }
        /// <summary>
        /// Suranda per kiek kartų liftas pakels studentus 
        /// </summary>
        /// <param name="s1"> pirmas studentas </param>
        /// <param name="s2">antras studentas</param>
        /// <param name="s3">trečias studentas</param>
        /// <param name="l1">liftas</param>
        /// <returns>Gražina jauniausio studento ūgį</returns>
        public static double ELiftas(Liftas l1, Studentas s1, Studentas s2, Studentas s3)
        {
            double Kartai;
            if (l1.Imtitalpą() >= 3 && l1.Imtigalią() >= (s1.ImtiSvorį() + s2.ImtiSvorį() + s3.ImtiSvorį()))
                Kartai = 1;
            else if (((l1.Imtigalią() < (s1.ImtiSvorį() + s2.ImtiSvorį() + s3.ImtiSvorį()))
                && (l1.Imtigalią() >= (s1.ImtiSvorį() + s2.ImtiSvorį())) ||
                     (l1.Imtigalią() >= (s1.ImtiSvorį() + s3.ImtiSvorį())) ||
                     (l1.Imtigalią() >= (s2.ImtiSvorį() + s3.ImtiSvorį()))) && l1.Imtitalpą() >= 3)
                Kartai = 2;
            else if (l1.Imtitalpą() >= 3 && (l1.Imtigalią() <= (s1.ImtiSvorį() + s2.ImtiSvorį()) ||
                    l1.Imtigalią() <= (s1.ImtiSvorį() + s3.ImtiSvorį()) ||
                    l1.Imtigalią() <= (s2.ImtiSvorį() + s3.ImtiSvorį())))
                Kartai = 3;
            else if (((l1.Imtigalią() < (s1.ImtiSvorį() + s2.ImtiSvorį() + s3.ImtiSvorį()))
                && (l1.Imtigalią() >= (s1.ImtiSvorį() + s2.ImtiSvorį())) ||
                     (l1.Imtigalią() >= (s1.ImtiSvorį() + s3.ImtiSvorį())) ||
                     (l1.Imtigalią() >= (s2.ImtiSvorį() + s3.ImtiSvorį()))) && l1.Imtitalpą() == 2)
                Kartai = 2;
            else if (l1.Imtitalpą() == 1 && (l1.Imtigalią() >= s1.ImtiSvorį())
                || (l1.Imtigalią() >= s2.ImtiSvorį()) || (l1.Imtigalią() >= s3.ImtiSvorį()))
                Kartai = 3;
            else
                Kartai = 3;
            return Kartai;
        }
        /// <summary>
        /// Metodas duomenims išvesti jei jauniausi studentai yra 1 ir 3
        /// </summary>
        /// <param name="s1"> pirmas studentas </param>
        /// <param name="s3">antras studentas</param>
        public static void Spausdinti1(Studentas s1, Studentas s3)
        {
            Console.Write("Jauniausi studentai yra 1 ir 3.\n Jų amžius yra:"
                    + "{0}" + "\n pirmo studento ūgis yra:" + "{1} " + "\n trecio studento ūgis yra:" +
                    "{2} \n", s1.Imtiamžių(), s1.Imtiūgį(), s3.Imtiūgį());
        }
         /// <summary>
         /// Metodas duomenims išvesti jei aukščiausi studentai yra 1 ir 3
         /// </summary>
         /// <param name="s1"> pirmas studentas </param>
         /// <param name="s3">antras studentas</param>
        public static void Spausdinti2(Studentas s1, Studentas s3)
        {
            Console.Write("Aukščiausi studentai yra 1 ir 3.\n Jų ūgis yra:"
               + "{0}\n" + " pirmam studentui yra:" + "{1} metų\n" +  " trečiam studentui yra:" + "{2}"
               + " metų\n", s1.Imtiūgį(), s1.Imtiamžių(), s3.Imtiamžių());
        }
        public static void SpausdLentele(Studentas s1, Studentas s2, Studentas s3)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("I    X     I  1studentas  I  2studentas  I    3studentas I");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("I Ūgis     I     {0}      I     {1}      I      {2}      I",
                s1.Imtiūgį(), s2.Imtiūgį(), s3.Imtiūgį());
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("I Amžius   I     {0}       I     {1}       I      {2}       I",
                s1.Imtiamžių(), s2.Imtiamžių(), s3.Imtiamžių());
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("I Svoris   I     {0}       I     {1}       I      {2}       I",
                s1.ImtiSvorį(), s2.ImtiSvorį(), s3.ImtiSvorį());
            Console.WriteLine("----------------------------------------------------------");
        }
        public static void SpausdLentele2(Liftas l1)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("I    X   I galia   I   talpa  I");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("I liftas I   {0}   I   {1}      I", l1.Imtigalią(),
                l1.Imtitalpą());
            Console.WriteLine("-------------------------------");
        }
            
    }
}



