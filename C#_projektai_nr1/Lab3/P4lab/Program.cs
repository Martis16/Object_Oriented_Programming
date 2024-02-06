using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4lab
{
    /// <summary>
    /// Ši klasė skirta duomenims apie korteles
    /// </summary>
    class kortele
    {
        private double Suma, TarifSav,
            TarifKit, SmsTarifSav, SmsTarifKit;
        private string pav;

        public kortele(string pav ,double Suma,
            double TarifSav, double TarifKit,
            double SmsTarifSav, double SmsTarifKit)
        {
            this.pav = pav;
            this.Suma = Suma;
            this.TarifSav = TarifSav;
            this.TarifKit = TarifKit;
            this.SmsTarifSav = SmsTarifSav;
            this.SmsTarifKit = SmsTarifKit;
        }
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("|{0,9} | {1,5:f} | {2,6:f} | {3,6:f} | {4,5:f} | {5,6:f}|",
                pav, Suma, TarifSav, TarifKit, SmsTarifSav, SmsTarifKit);
            return eilute;
        }
        public double ImtiSmsTarifKit() { return SmsTarifKit; }
        public double ImtiTarifSav() { return TarifSav; }
        public double ImtiSmsTarifSav() { return SmsTarifSav; }
        /// <summary>
        /// Užklotas operatorius
        /// </summary>
        /// <param name="k1">kortele</param>
        /// <returns>grąžina palyginimą pagal SMS ir skambučių tarifus savame tinkle</returns>
        public static bool operator !(kortele k1)
        {
            if ((k1.ImtiTarifSav() == 0) && (k1.ImtiSmsTarifSav() == 0))
                return false;
            return true;
        }
        /// <summary>
        /// Užklotas operatorius
        /// </summary>
        /// <param name="kt1">pirma kortele</param>
        /// <param name="kt2"> antra kortele</param>
        /// <returns>grąžina palyginimą pagal pradinę sumą ir pavadinimą</returns>
        public static bool operator >= (kortele kt1, kortele kt2)
        {
            int p = string.Compare(kt1.pav, kt2.pav,
                StringComparison.CurrentCulture);
            if (kt1.Suma > kt2.Suma || kt1.Suma == kt2.Suma && p > 0)
                return true;
            return false;
            
        }
        public static bool operator <=(kortele kt1, kortele kt2)
        {
            int p = string.Compare(kt1.pav, kt2.pav,
                StringComparison.CurrentCulture);
            if (kt1.Suma > kt2.Suma || kt1.Suma == kt2.Suma && p < 0)
                return true;
            return false;
        }
    }
    /// <summary>
    /// Ši klasė yra konteinerinė
    /// </summary>

    class korteleskontnr
    {
        const int Cmax = 100;
        private kortele[] kort;
        private int n;

        public korteleskontnr()
        {
            n = 0;
            kort = new kortele[Cmax];
        }
        //sąsajos metodai
        public int Imti() { return n; }
        public kortele Imtikort(int i) { return kort[i]; }
        public void Deti(kortele ob) { kort[n++] = ob; }
        /// <summary>
        /// metodas skirtas surikiuoti masyvui
        /// </summary>
        public void Rikiuoti()
        {
            for(int i = 0; i < n-1; i++)
            {
                kortele min = kort[i];
                int im = i;
                for (int j =i; j<n; j++)
                    if(kort[j] <= min)
                    {
                        im = j;
                        min = kort[j];
                    }
                kort[im] = kort[i];
                kort[i] = min;
            }
        }
    }

    class Program
    {
        // konstantos
        const string CFd = "duom.txt";
        const string CFrez = "..//..//rez.txt";
        static void Main(string[] args)
        {
            //ištrina rezultatų failą jeigu egzistuoja
            if (File.Exists(CFrez))
                File.Delete(CFrez);
            korteleskontnr korteles = new korteleskontnr();
            skaityti(ref korteles, CFd);
            spausdinti(korteles, CFrez);
            korteleskontnr kortelesmin = new korteleskontnr();
            minkeli(korteles,ref kortelesmin);
            spausdintiRez(kortelesmin, CFrez);
            korteleskontnr korteles1 = new korteleskontnr();
            formuoti(korteles, ref korteles1);
            
            if (korteles1.Imti() > 0)
            {
                korteles1.Rikiuoti();
                spausdinti(korteles1, CFrez);
            }
            else
                using (var fr = File.AppendText(CFrez))
                    fr.WriteLine("\ntokiu korteliu nera");
        }
        /// <summary>
        /// Nuskaito duomenų failus
        /// </summary>
        /// <param name="korteles">konteineris</param>
        /// <param name="fv">Duomenų failo pavadinimas</param>
        static void skaityti(ref korteleskontnr korteles , string fv)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                string[] parts;
                while ((line = reader.ReadLine()) != null)
                {
                    parts = line.Split(' ');
                    string pav = parts[0];
                    double suma = double.Parse(parts[1]);
                    double tarifsav = double.Parse(parts[2]);
                    double TarifKit = double.Parse(parts[3]);
                    double SmsTarifSav = double.Parse(parts[4]);
                    double SmsTarifKit = double.Parse(parts[5]);
                    kortele kort = new kortele(pav, suma, tarifsav, TarifKit, SmsTarifSav, SmsTarifKit);
                    korteles.Deti(kort);
                }
            }
        }
        /// <summary>
        /// Spausdina duomenis lentele
        /// </summary>
        /// <param name="korteles">konteineris</param>
        /// <param name="fv">Rezultatų failo pavadinimas</param>
        static void spausdinti(korteleskontnr korteles, string fv)
        {
            string virsus = "\n|----------------------------------------------------|\r\n"
                           +"|              informacija apie korteles             |\r\n"
                           + "|----------------------------------------------------|\r\n"
                           + "|    pav   | suma  | tar sav| tar kit|smstsav|smstkit|";
                           //+ "|----------------------------------------------------|";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(virsus);
                for (int i = 0; i < korteles.Imti(); i++)
                {
                    fr.WriteLine("|----------------------------------------------------|");
                    fr.WriteLine("{0}", korteles.Imtikort(i).ToString());
                }
                fr.WriteLine("|----------------------------------------------------|\n");
            }
        }
        /// <summary>
        /// Suranda kortele su mažiausiu SMS tarifu į kitus tinklus
        /// </summary>
        /// <param name="korteles">konteineris</param>
        static double min(korteleskontnr korteles)
        {
            double min =99999;
            for (int i = 1; i < korteles.Imti(); i++)
            {
                if(korteles.Imtikort(i).ImtiSmsTarifKit() < min)
                    min = korteles.Imtikort(i).ImtiSmsTarifKit();
            }
            return min;
        }
        /// <summary>
        /// Formuoja nauja konteineri
        /// </summary>
        /// <param name="A">senas konteineris</param>
        /// <param name="B">Naujas konteineris</param>
        static void formuoti(korteleskontnr A, ref korteleskontnr B)
        {
            for (int i = 0; i < A.Imti(); i++)
                if (!A.Imtikort(i))
                    ;
                else
                    B.Deti(A.Imtikort(i));
        }
        /// <summary>
        /// Formuoja nauja konteinerį su mažiausiu SMS tarifu į kitus tinklus kortelėmis
        /// </summary>
        /// <param name="k1">senas konteineris</param>
        /// <param name="k2">Naujas konteineris</param>
        static void minkeli(korteleskontnr k1,ref korteleskontnr k2)
        {
            for (int i = 0; i < k1.Imti(); i++)
                if (k1.Imtikort(i).ImtiSmsTarifKit() == min(k1))
                    k2.Deti(k1.Imtikort(i));
        }
        /// <summary>
        /// Spausdina rezultatus lentele
        /// </summary>
        /// <param name="kortelesmin">konteineris</param>
        /// <param name="fv">Rezultatų failo pavadinimas</param>
        static void spausdintiRez(korteleskontnr kortelesmin, string fv)
        {
            using (var fr = File.AppendText(CFrez))
            {
                fr.WriteLine("kortelė, kurios SMS žinučių tarifai į kitus tinklus mažiausi.");
                for (int i = 0; i < kortelesmin.Imti(); i++)
                {
                    fr.WriteLine("|----------------------------------------------------|");
                    fr.WriteLine("{0}", kortelesmin.Imtikort(i).ToString());
                }
            }
        }
    }
}
