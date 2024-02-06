using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras_2_
{
    class krepsininkas
    {
        private string vardas;
        private string pavarde;
        private int amzius;
        private int ugis;
        public krepsininkas(string vardas, string pavarde, int amzius, int ugis)
        {
            this.vardas = vardas;
            this.pavarde = pavarde;
            this.amzius = amzius;
            this.ugis = ugis;
        }
        public string ImtiVarda() { return vardas; }
        public string ImtiPavarde() { return pavarde; }
        public int ImtiAmziu() { return amzius; }
        public int ImtiUgi() { return ugis; }
    }

    class Program
    {   // konstantos
        const int Cn = 100;
        const string CFd1 = "Duom6.txt";
        const string CFd2 = "Duom7.txt";
        const string CFrez = "..\\..\\Rez.txt";

        static void Main(string[] args)
        {
            //ištrina rezultatų failą jeigu egzistuoja
            if (File.Exists(CFrez))
                File.Delete(CFrez);
            //kuriami nuskaitomi ir spausdinami masyvai
            krepsininkas[] K1 = new krepsininkas[Cn];
            int nkiek1;
            string pav1;
            Skaityti(CFd1, K1, out nkiek1, out pav1);
            Spausdinti(CFrez, K1, nkiek1, pav1);
            krepsininkas[] K2 = new krepsininkas[Cn];
            int nkiek2;
            string pav2;
            Skaityti(CFd2, K2, out nkiek2, out pav2);
            Spausdinti(CFrez, K2, nkiek2, pav2);
            SpausdintiRezultatus(CFrez, K1, pav1, nkiek1);
            SpausdintiRezultatus(CFrez, K2, pav2, nkiek2);
            //Tikrina ar nera vienodo ugio krepsininku
            using (var fr = File.AppendText(CFrez))
            {
                if (K1[Auksciausias(K1, nkiek1)].ImtiUgi() < K2[Auksciausias(K2, nkiek2)].ImtiUgi())
                    fr.WriteLine("\nAuksciausias sportininkas yra ''{0}'' mokykloje", pav2);
                else if (K1[Auksciausias(K1, nkiek1)].ImtiUgi() == K2[Auksciausias(K2, nkiek2)].ImtiUgi())
                    fr.WriteLine("\nAuksciausi sportininkai yra ir mokyklose ''{0}'' ir ''{1}''  ", pav1, pav2);
                else
                    fr.WriteLine("\nAuksciausias sportininkas yra ''{0}'' mokykloje", pav1);
            }
            krepsininkas[] K3 = new krepsininkas[Cn];
            int kiek2 = 0;
            Formuoti(K1, nkiek1, K3, ref kiek2, UgioVidurkis(K1, nkiek1));
            Formuoti(K2, nkiek2, K3, ref kiek2, UgioVidurkis(K2, nkiek2));
            //Tikrinama ar suformuotas masyvas egzistuoja
            if (kiek2 > 0)
                Spausdinti(CFrez, K3, kiek2, "\nSportininkai kuriu ugis didesnis uz vidurki");
            else
                using (var fr = File.AppendText(CFrez))
                    fr.WriteLine("\nRinkinys neformuojamas");

        }
        /// <summary>
        /// Nuskaito duomenų failus
        /// </summary>
        /// <param name="K">Masyvas į kurį skaitys</param>
        /// <param name="kiek">kintamasis kuris išsaugo masyvo ilgio reikšmę</param>
        /// <param name="fv">Duomenų failo pavadinimas</param>
        static void Skaityti(string fv, krepsininkas[] K, out int kiek, out string pav)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string vardas;
                string pavarde;
                int amzius;
                int ugis;
                string line;
                line = reader.ReadLine();
                string[] parts;
                pav = line;
                int i = 0;
                while ((line = reader.ReadLine()) != null && (i < Cn))
                {
                    parts = line.Split(';');
                    vardas = (parts[0]);
                    pavarde = (parts[1]);
                    amzius = int.Parse(parts[2]);
                    ugis = int.Parse(parts[3]);
                    K[i] = new krepsininkas(vardas, pavarde, amzius, ugis);
                    i++;
                }
                kiek = i;
            }
        }
        /// <summary>
        /// Spausdina pradinius duomenis
        /// </summary>
        /// <param name="K">Masyvas kuri spausdinsim</param>
        /// <param name="kiek">masyvo ilgis</param>
        /// <param name="fv">Rezultatų failo pavadinimas</param>
        /// <param name="pav">krepsinio mokyklos pavadinimas</param>
        public static void Spausdinti(string fv, krepsininkas[] K, int kiek, string pav)
        {
            const string virsus =
                        "|-----------------|---------------|---------------|---------|\r\n"
                      + "|     Vardas      |    Pavarde    |     Amzius    |   Ugis  | \r\n"
                      + "|-----------------|---------------|---------------|---------|";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("  {0}", pav);
                fr.WriteLine(virsus);
                krepsininkas tarp;
                for (int i = 0; i < kiek; i++)
                {
                    tarp = K[i];
                    fr.WriteLine("|{0,-17}|{1,-15}|{2,-15}|{3,-9}|",
                                      tarp.ImtiVarda(), tarp.ImtiPavarde(),
                                      tarp.ImtiAmziu(), tarp.ImtiUgi());
                    fr.WriteLine("|-----------------|---------------|---------------|---------|");
                }
            }
        }
        /// <summary>
        /// Spausdina rezultatus
        /// </summary>
        /// <param name="fv">Rezultatų failo pavadinimas</param>
        /// <param name="K">Masyvas kuriame yra visi ilgiai</param>
        /// <param name="kiek">Masyvo ilgio kintamasis</param>
        /// <param name="pav">krepsinio mokyklos pavadinimas</param>
        public static void SpausdintiRezultatus(string fv, krepsininkas[] K, string pav, int kiek)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("\nSporto mokyklos ''{0}'' busimu krepsininku amziaus vidurkis: {1,6:f2}",
                    pav, AmziausVidurkis(K, kiek));
                fr.WriteLine("Sporto mokyklos ''{0}'' busimu krepsininku ugio vidurkis: {1,9:f2}",
                    pav, UgioVidurkis(K, kiek));
            }
        }
        /// <summary>
        /// Suranda krepsininku amziaus vidurki
        /// </summary>
        /// <param name="K">Masyvas kuriame yra visi ilgiai</param>
        /// <param name="kiek">Masyvo ilgio kintamasis</param>
        public static double AmziausVidurkis(krepsininkas[] K, int kiek)
        {
            double suma = 0;
            for (int i = 0; i < kiek; i++)
                suma = suma + K[i].ImtiAmziu();
            if (kiek > 0)
                return suma / kiek;
            else return -1;
        }
        /// <summary>
        /// Suranda krepsininku vidurki
        /// </summary>
        /// <param name="K">Masyvas kuriame yra visi ilgiai</param>
        /// <param name="kiek">Masyvo ilgio kintamasis</param>
        public static double UgioVidurkis(krepsininkas[] K, int kiek)
        {

            double suma = 0;
            for (int i = 0; i < kiek; i++)
                suma = suma + K[i].ImtiUgi();
            double a = suma / kiek;
            if (kiek > 0)
                return a;
            else return -1;
        }
        /// <summary>
        /// Suranda auksciausia krepsininka
        /// </summary>
        /// <param name="K">Masyvas kuriame yra visi ilgiai</param>
        /// <param name="kiek">Masyvo ilgio kintamasis</param>
        static int Auksciausias(krepsininkas[] K, int kiek)
        {
            int n = 0;
            for (int i = 0; i < kiek; i++)
                if (K[i].ImtiUgi() > K[n].ImtiUgi())
                    n = i;
            return n;
        }
        /// <summary>
        /// Formuoja nauja masyva
        /// </summary>
        /// <param name="K">Masyvas kuriame yra visi ilgiai</param>
        /// <param name="kiek">Masyvo ilgio kintamasis</param>
        /// <param name="K3">Naujas masyvas</param>
        /// <param name="kiek2">Naujo masyvo ilgio kintamasis</param>
        /// <param name="UgioVidurkis">Kreipinys i metoda</param>
        static void Formuoti(krepsininkas[] K, int kiek, krepsininkas[] K3, ref int kiek2, double UgioVidurkis)
        {
            for (int i = 0; i < kiek; i++)
            {
                if (K[i].ImtiUgi() > UgioVidurkis)
                {
                    K3[kiek2] = K[i];
                    kiek2++;
                }
            }
        }
    }
}
