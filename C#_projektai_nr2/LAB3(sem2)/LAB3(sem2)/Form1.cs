using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3_sem2_
{
    
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private List<Cars> Cars;  //first cars list 
        private List<Cars> Cars1; //cars list of given manufacturer
        private List<Cars> Cars2; //cars list without duplicates

        /// <summary>
        /// Reads data from file
        /// </summary>
        /// <param name="fn">file name</param>
        static List<Cars> ReadFile(string fn)
        {
            List<Cars> Cars = new List<Cars>();
            using (StreamReader reader = new StreamReader(fn))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    string Nmbr = parts[0];
                    string Manuf = (parts[1]);
                    string Make = (parts[2]);
                    DateTime Year = DateTime.Parse(parts[3]);
                    DateTime TAd = DateTime.Parse(parts[4]);
                    string FuelT = (parts[5]);
                    double FuelCon = double.Parse(parts[6]);
                    Cars plr = new Cars(Nmbr, Manuf, Make, Year, TAd, FuelT, FuelCon);
                    Cars.Add(plr);
                }
            }
            return Cars;
        }
        /// <summary>
        /// Print's a table with a team's players
        /// </summary>
        /// <param name="fn">file name</param>
        /// <param name="C">Cars list</param>
        /// <param name="heading">heading</param>
        static void Print(string fn, List<Cars> C, string heading)
        {
            const string top =
                    "---------------------------------------------------------------------------------\r\n"
                    + "Nr.| Number | Manufacturer|   Model   |   Year  |  TA date  |Fuel type|Fuel cons|\r\n"
                    + "---------------------------------------------------------------------------------";
            using (var fr = new StreamWriter(File.Open(fn, FileMode.Append)))
            {
                fr.WriteLine("\n" + heading);
                fr.WriteLine(top);
                for (int i = 0; i < C.Count; i++)
                {
                    Cars cr = C[i];
                    fr.WriteLine("{0,-3}|{1}", i + 1, cr.ToString());
                }
                fr.WriteLine("---------------------------------------------------------------------------------\n");
            }
        }
        /// <summary>
        /// returns first and second newest cars from cars list
        /// </summary>
        /// <param name="Cars">Cars list</param>
        /// <param name="max1">first newest car</param>
        /// <param name="max2">second newest car</param>
        static void TwoNewestCars(List<Cars> Cars, out Cars max1, out Cars max2)
        {
            //max1 = new Cars();
            //max2 = new Cars();
            //ind1 = ind2 = 0;
            if(Cars[0].Year > Cars[1].Year)
            {
                max1 = Cars[0]; max2 = Cars[1];
            }
            else
            {
                max1 = Cars[1]; max2 = Cars[0];
            }
            for (int i = 0; i < Cars.Count; i++)
            {
                if(Cars[i].Year > max1.Year)
                {
                    max2 = max1;
                    max1 = Cars[i];
                    //ind1 = i+1;
                }
                else if(Cars[i].Year > max2.Year)
                {
                    max2 = Cars[i];
                    //ind2 = i+1;
                }
            }
        }
        /// <summary>
        /// Constructs a new list with cars of the given manufacturer
        /// </summary>
        /// <param name="C1">Cars list</param>
        /// <param name="C2">Cars1 list</param>
        /// <param name="Mk">Manufacturer's name</param>
        static void Construct(List<Cars> C1, List<Cars> C2, string Mk)
        {

            for (int i = 0; i < C1.Count; i++)
            {
                if(C1[i].Manufacturer == Mk)
                {
                    Cars cr = new Cars(C1[i].Number, C1[i].Manufacturer, C1[i].Make,
                        C1[i].Year, C1[i].TAdate, C1[i].FuelType, C1[i].FuelConsumption);
                    C2.Add(cr);
                }
            }
        }

        /// <summary>
        /// Constructs a list of cars without duplicates
        /// </summary>
        /// <param name="C1">Cars list</param>
        /// <param name="C2">Cars2 list</param>
        static void NoRepeatings(List<Cars> C1, List<Cars> C2)
        {
            for (int i = 0; i < C1.Count; i++)
            {
                int p = 0;
                Cars cr1 = new Cars(C1[i].Number, C1[i].Manufacturer, C1[i].Make,
                    C1[i].Year, C1[i].TAdate, C1[i].FuelType, C1[i].FuelConsumption);
                for (int j = 0; j < C2.Count; j++)
                {
                    if (C1[i].Equals(C2[j]))
                    {
                        p = 1;
                    }
                }
                if (p == 0)
                    {
                        C2.Add(cr1);
                    }
            }
        }

        /// <summary>
        /// Actions of the "Enter" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            runToolStripMenuItem.Enabled = true;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Title = "Choose a file";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fv = openFileDialog1.FileName;
                Cars = ReadFile(fv);
                results.LoadFile(fv, RichTextBoxStreamType.PlainText);
            }
            tekstas.Text = "Write car's manufacturer";
        }

        /// <summary>
        /// Actions of the "Run" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Title = "Pasirinkite rezultatų failą";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fv = saveFileDialog1.FileName;
                if (File.Exists(fv))
                    File.Delete(fv);
                Print(fv, Cars, "UAB Zaibas Cars");
                string mak = įvesti.Text;
                Cars1 = new List<Cars>();
                Construct(Cars, Cars1, mak);
                Cars max1 = new Cars();
                Cars max2 = new Cars();
                //int ind1, ind2;
                TwoNewestCars(Cars, out max1, out max2);
                using (var fr = File.AppendText(fv))
                {
                    fr.WriteLine("\n\nTwo newest cars:\n1. |" + max1 + "\n2. |" + max2);
                }
                Print(fv, Cars1, "\nAll " + mak + " cars");
                Cars1.Sort();
                Print(fv, Cars1, "Sorted list");
                Cars2 = new List<Cars>();
                NoRepeatings(Cars, Cars2);
                Print(fv, Cars2, "No duplicate cars");
                results.LoadFile(fv, RichTextBoxStreamType.PlainText);
                
            }
        }
        /// <summary>
        /// Actions of the "Close" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void helpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            runToolStripMenuItem.Enabled = false;
            results.Clear();
            results.Text = "1.Paspaudus 'Enter' pasirinkite duomenų failą" +
                "\n2.Užsikrovus duomenims langelyje užrašykite norimą auto gamintoją" +
                "\n3.Spauskite 'Actions' -> 'Run', pasirinkite 'results' failą ir spauskite 'Save'" +
                "\n\n\n\n\nProgramą sukūrė: Martynas Burneika";
        }
    }
}
