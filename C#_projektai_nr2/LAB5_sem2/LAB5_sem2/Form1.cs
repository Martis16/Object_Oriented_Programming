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

namespace LAB5_sem2
{
    public partial class Form1 : Form
    {
        const string Cfd = "..\\..\\U2a.txt";
        const string Cff = "..\\..\\U2b.txt";
        const string Cfr = "..\\..\\Results.txt";

        Routes<Route> A;   //Routes List
        Routes<Tickets> B; //Tickets List
        Routes<Route> C;   //No Reapeatings List
        Routes<Route> D;   //Used Routes List

        public Form1()
        {
            InitializeComponent();

            if (File.Exists(Cfr))
                File.Delete(Cfr);

            runToolStripMenuItem.Enabled = false;
            formToolStripMenuItem.Enabled = false;
            findToolStripMenuItem.Enabled = false;
        }


        /// <summary>
        /// Reads data from tickets file
        /// </summary>
        /// <param name="fn">file name</param>
        /// <returns></returns>
        static Routes<Tickets> ReadT(string fn)
        {
            var a = new Routes<Tickets>();
            using(var reader = new StreamReader(fn))
            {
                string line;
                while((line=reader.ReadLine())!= null)
                {
                    string[] parts = line.Split(';');
                    string NamSur = parts[0];
                    string day = parts[1];
                    DateTime Dtime = DateTime.Parse(parts[2]);
                    string RNmbr = parts[3];
                    Tickets Tck = new Tickets(NamSur, day, Dtime, RNmbr);
                    a.AddDataA(Tck);
                }
            }return a;
        }


        /// <summary>
        /// Reads data from routes file
        /// </summary>
        /// <param name="fn">file name</param>
        /// <returns></returns>
        static Routes<Route> ReadR(string fn)
        {
            var a = new Routes<Route>();
            using (var reader = new StreamReader(fn))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    int Price = int.Parse(parts[3]);
                    string day = parts[1];
                    DateTime Dtime = DateTime.Parse(parts[2]);
                    string RNmbr = parts[0];
                    Route rt = new Route(RNmbr, day, Dtime, Price);
                    a.AddDataA(rt);
                }
            }
            return a;
        }


        /// <summary>
        /// Prints tickets data
        /// </summary>
        /// <param name="fn">file name</param>
        /// <param name="B">tickets list</param>
        /// <param name="heading">heading</param>
        static void PrintT(string fn, Routes<Tickets> B, string heading)
        {
            const string top =
                    "-----------------------------------------------------------------------\r\n"
                    + "Nr. |  Name and Surname  |      Day      |Departing Time| Route Number |\r\n"
                    + "-----------------------------------------------------------------------";
            using (var fr = new StreamWriter(File.Open(fn, FileMode.Append)))
            {
                fr.WriteLine("\n" + heading);
                fr.WriteLine(top);
                int i = 0;
                for (B.Start(); B.Is(); B.Next())
                {
                    i++;
                    Tickets plr = B.GetData();
                    fr.WriteLine("{0,3} {1}", i, plr);
                }
                fr.WriteLine("----------------------------------------------------------------------\n");
            }
        }


        /// <summary>
        /// Prints routes data
        /// </summary>
        /// <param name="fn">file name</param>
        /// <param name="B">routes list</param>
        /// <param name="heading">heading</param>
        static void PrintR(string fn, Routes<Route> B, string heading)
        {
            const string top = "------------------------------------------------------------\r\n"
                             + "Nr. | Route Number|      Day      |Departing Time|  Price  |\r\n"
                    + "------------------------------------------------------------";
            using (var fr = new StreamWriter(File.Open(fn, FileMode.Append)))
            {
                fr.WriteLine("\n" + heading);
                fr.WriteLine(top);
                int i = 0;
                for (B.Start(); B.Is(); B.Next())
                {
                    i++;
                    Route plr = B.GetData();
                    fr.WriteLine("{0,3} {1}", i, plr);
                }
                fr.WriteLine("----------------------------------------------------------\n");
            }
        }


        /// <summary>
        /// Forms a list of used routes
        /// </summary>
        /// <param name="RT">Routes list</param>
        /// <param name="TC">Tickets List</param>
        /// <returns>Fromed list</returns>
        static Routes<Route> Form(Routes<Route> RT, Routes<Tickets> TC)
        {
            var A = new Routes<Route>();
            for(RT.Start(); RT.Is(); RT.Next())
            {
                Route tr = RT.GetData();
                for(TC.Start(); TC.Is(); TC.Next())
                {
                    if(RT.GetData().RouteNumber == TC.GetData().RouteNumber &&
                        RT.GetData().DepartingTime == TC.GetData().DepartingTime && RT.GetData().Day == TC.GetData().Day)
                    {
                        A.AddDataT(tr);
                    }
                }
            }return A;
        }


        /// <summary>
        /// Forms a list without reapeating routes
        /// </summary>
        /// <param name="RT">Routes list</param>
        /// <param name="TC">Tickets list</param>
        /// <returns>Formed list</returns>
        static Routes<Route> NoReapeatings(Routes<Route> RT, Routes<Tickets> TC)
        {
            Routes<Route> C = new Routes<Route>();
            Routes<Route> A = Form(RT, TC);
            for (A.Start(); A.Is(); A.Next())
            {
                int p = 0;
                Route tr = A.GetData();
                for (C.Start(); C.Is(); C.Next())
                {
                    if (tr.RouteNumber == C.GetData().RouteNumber &&
                        tr.DepartingTime == C.GetData().DepartingTime && tr.Day == C.GetData().Day)
                    {
                        p = 1;
                    }
                }
                if (p == 0)
                {
                    C.AddDataT(tr);
                }
            }
            return C;
        }


        /// <summary>
        /// Finds sum of one route
        /// </summary>
        /// <param name="RT">Routes list</param>
        /// <param name="A">Route</param>
        /// <returns>sum of every route</returns>
        static int Sum(Routes<Route> RT, Route A)
        {
            
            int sum = 0;
            for (RT.Start(); RT.Is(); RT.Next())
            {
                
                if (RT.GetData().Day == A.Day && RT.GetData().DepartingTime == A.DepartingTime && RT.GetData().RouteNumber == A.RouteNumber)
                {
                    sum += RT.GetData().Price;
                    
                }
            }return sum;
        }


        /// <summary>
        /// Finds which route was most profitable
        /// </summary>
        /// <param name="C">List without reapeatings</param>
        /// <param name="A">List of used routes</param>
        /// <param name="At">most profitable route</param>
        /// <param name="ind">place in list</param>
        static void Find(Routes<Route> C, Routes<Route> A, out Route At, out int ind)
        {
            At = new Route();
            int max = 0; int ind1 = 0;
            ind = 0;
            using (var fr = new StreamWriter(File.Open(Cfr, FileMode.Append)))
            {
                for (C.Start(); C.Is(); C.Next())
                {
                    ind1++;
                    if (Sum(A, C.GetData()) > max)
                    {
                        max = Sum(A, C.GetData());
                        At = C.GetData();
                        ind = ind1;
                    }
                }
            }
        }


        /// <summary>
        /// actions of close click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// actions of enter click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runToolStripMenuItem.Enabled = true;
            formToolStripMenuItem.Enabled = false;
            findToolStripMenuItem.Enabled = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Title = "Choose a file";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fv = openFileDialog1.FileName;
                A = ReadR(fv);
                results.LoadFile(fv, RichTextBoxStreamType.PlainText);
            }
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog2.Title = "Choose a file";
            DialogResult result1 = openFileDialog1.ShowDialog();
            if (result1 == DialogResult.OK)
            {
                string fv1 = openFileDialog1.FileName;
                B = ReadT(fv1);
                results2.LoadFile(fv1, RichTextBoxStreamType.PlainText);

            }
            PrintT(Cfr, B," Bought tickets");
            PrintR(Cfr, A," Routes");
        }


        /// <summary>
        /// actions of run click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runToolStripMenuItem.Enabled = false;
            formToolStripMenuItem.Enabled = true;
            findToolStripMenuItem.Enabled = false;
            results3.LoadFile(Cfr, RichTextBoxStreamType.PlainText);
        }


        /// <summary>
        /// actions of form click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runToolStripMenuItem.Enabled = false;
            formToolStripMenuItem.Enabled = false;
            findToolStripMenuItem.Enabled = true;
            D = Form(A, B);
            C = NoReapeatings(A, B);
            //PrintR(Cfr, D, "Used");
            C.Start();
            if (C.Is())
            {
                PrintR(Cfr, C, "Used routes");
            }
            else
            {
                using (var fr = File.AppendText(Cfr))
                    fr.WriteLine("There are no used routes");
            }
            results3.LoadFile(Cfr, RichTextBoxStreamType.PlainText);
        }


        /// <summary>
        /// actions of find click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ind = 0;
            Route At = new Route();
            Find(C, D, out At, out ind);
            if (At.RouteNumber != "" && At.Price != 0)
            {
                using(var fr = File.AppendText(Cfr))
                {
                    fr.WriteLine("Most profitable route");
                    fr.WriteLine("------------------------------------------------------------\r\n"
                        +        "Nr. | Route Number|      Day      |Departing Time|  Price  |\r\n"
                        +        "------------------------------------------------------------");
                    fr.WriteLine("{0,3} {1}" , ind , At);
                }
            }
            else
            {
                using (var fr = File.AppendText(Cfr))
                    fr.WriteLine("There is no profitable route");
            }

            results3.LoadFile(Cfr, RichTextBoxStreamType.PlainText);
        }
    }
}
