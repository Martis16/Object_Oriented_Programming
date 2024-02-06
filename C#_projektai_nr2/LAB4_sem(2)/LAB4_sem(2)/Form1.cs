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

namespace LAB4_sem_2_
{
    public partial class Form1 : Form
    {

        const string CFr = "..\\..\\Results.txt";    // result's file
        const string Cffd = "..\\..\\Task.txt";
        const string Cfh = "..\\..\\user.txt";

        Ballers A; //direct
        Ballers B; //reverse
        Ballers C; //combined
        string TeamName;  //Team 1 name
        string TeamName1; //Team 2 name


        /// <summary>
        /// Read data from file and puts in reverse order
        /// </summary>
        /// <param name="fn">file name</param>
        /// <param name="TeamName">returns team's name</param>
        static Ballers ReadFileA(string fn, out string TeamName)
        {
            var a = new Ballers();
            using (var reader = new StreamReader(fn))
            {
                string line;
                line = reader.ReadLine();
                TeamName = line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    string NamSur = parts[0];
                    int Age = int.Parse(parts[1]);
                    int Height = int.Parse(parts[2]);
                    Player plr = new Player(NamSur, Age, Height);
                    a.AddDataA(plr);
                }
            }
            return a;
        }
        /// <summary>
        /// Read data from file and puts in direct order
        /// </summary>
        /// <param name="fn">file name</param>
        /// <param name="TeamName">returns team's name</param>
        static Ballers ReadFileT(string fn, out string TeamName)
        {
            var a = new Ballers();
            using (var reader = new StreamReader(fn))
            {
                string line;
                line = reader.ReadLine();
                TeamName = line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    string NamSur = parts[0];
                    int Age = int.Parse(parts[1]);
                    int Height = int.Parse(parts[2]);
                    Player plr = new Player(NamSur, Age, Height);
                    a.AddDataT(plr);
                }
            }
            return a;
        }

        /// <summary>
        /// Print's a table with a team's players
        /// </summary>
        /// <param name="fn">file name</param>
        /// <param name="Team">Teams</param>
        /// <param name="heading">heading</param>
        static void Print(string fn, Ballers B, string heading)
        {
            const string top =
                    "-----------------------------------\r\n"
                    + "Nr. |Name and Surname| Age| Height| \r\n"
                    + "-----------------------------------";
            using (var fr = new StreamWriter(File.Open(fn, FileMode.Append)))
            {
                fr.WriteLine("\n" + heading);
                fr.WriteLine(top);
                int i=0;
                for (B.Start();B.Is();B.Next())
                {
                    i++; 
                    Player plr = B.GetData();
                    fr.WriteLine("{0}     {1}",i, plr);
                }
                fr.WriteLine("-----------------------------------\n");
            }
        }

        /// <summary>
        /// Connects two Team's players into one array whose height is bigger than avarage
        /// </summary>
        /// <param name="T">Team 1</param>
        /// <param name="T1">Team 2</param>
        /// <returns>combined team of two team's</returns>
        static void ConnectByHeight(Ballers T, Ballers T2)
        {
            double avg = AvgHeight(T);
            for (T.Start(); T.Is(); T.Next())
            {
                Player plr = T.GetData();
                if (plr.Height > avg)
                {
                    T2.AddDataT(plr);
                }
            }
        }

        /// <summary>
        /// Returns team's avarage age
        /// </summary>
        /// <param name="T">Team</param>
        /// <returns>team's avarage age</returns>
        static double AvgAge(Ballers T)
        {
            int count = 0;
            double a = 0;
            double avg = 0;
            foreach (Player pl in T)
            {
                count++;
                a = a + pl.Age;
            }
            if (count != 0)
                avg = a / count;
            return avg;
        }


        /// <summary>
        /// Returns team's avarage height
        /// </summary>
        /// <param name="T">Team</param>
        /// <returns>team's avarage height</returns>
        static double AvgHeight(Ballers T)
        {
            int count = 0;
            double a = 0;
            double avg = 0;
            foreach (Player pl in T)
            {
                count++;
                a = a + pl.Height;
            }
            if (count != 0)
                avg = a / count;
            return avg;
        }

        /// <summary>
        /// Removes players which age is bigger than specified
        /// </summary>
        /// <param name="A"></param>
        /// <param name="ages"></param>
        static void Remove(Ballers A, int ages)
        {
            for (A.Start(); A.Is(); A.Next())
            {
                Player pl = A.GetData();
                if (pl.Age > ages)
                {
                    A.RemoveV(pl);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            if (File.Exists(CFr))
                File.Delete(CFr);
            removeToolStripMenuItem.Enabled = false;
            runToolStripMenuItem.Enabled = false;
        }


        /// <summary>
        /// Actions of the "Enter" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runToolStripMenuItem.Enabled = true;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Title = "Choose a file";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fv = openFileDialog1.FileName;
                A = ReadFileA(fv, out TeamName);
                results.LoadFile(fv, RichTextBoxStreamType.PlainText);
            }
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog2.Title = "Choose a file";
            DialogResult result1 = openFileDialog1.ShowDialog();
            if (result1 == DialogResult.OK)
            {
                string fv1 = openFileDialog1.FileName;
                B = ReadFileT(fv1, out TeamName1);
                results2.LoadFile(fv1, RichTextBoxStreamType.PlainText);

            }
            Print(CFr, A, TeamName + " players");
            Print(CFr, B, TeamName1 + " players");
            label1.Text = "";
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

        /// <summary>
        /// Actions of the "Run" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeToolStripMenuItem.Enabled = true;
            runToolStripMenuItem.Enabled = false;
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine(TeamName + " Avarage age is: " + AvgAge(A));
                fr.WriteLine(TeamName + " Avarage height is: " + AvgHeight(A));
                fr.WriteLine("\n\n" + TeamName1 + " Avarage age is: " + AvgAge(B));
                fr.WriteLine(TeamName1 + " Avarage height is: " + AvgHeight(B));

            }
            C = new Ballers();
            ConnectByHeight(A, C);
            ConnectByHeight(B, C);
            Print(CFr, C, "Players above avg height");
            C.Burbulas();
            Print(CFr, C, "Sorted players list");
            results3.LoadFile(CFr, RichTextBoxStreamType.PlainText);
            label1.Text = "Type player's age";
        }


        /// <summary>
        /// Actions of the "Remove" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runToolStripMenuItem.Enabled = false;
            removeToolStripMenuItem.Enabled = false;
            int age = int.Parse(Įvesti.Text);
            Remove(C, age);
            C.Start();
            if (C.GetData()!= null)
            {
                Print(CFr, C, "Removed players list");
                results3.LoadFile(CFr, RichTextBoxStreamType.PlainText);
            }
            else
            {
                results3.Text += "\n\nThere are no players with that age";
                using (var fr = File.AppendText(CFr))
                    fr.WriteLine("There are no players with that age");
            }
        }

        /// <summary>
        /// Actions of the "Task" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            results2.Clear();
            results3.Clear();
            results3.LoadFile(Cffd, RichTextBoxStreamType.PlainText);
            removeToolStripMenuItem.Enabled = false;
            runToolStripMenuItem.Enabled = false;
            enterToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Actions of the "User Instructions" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            results2.Clear();
            results3.Clear();
            results3.LoadFile(Cfh, RichTextBoxStreamType.PlainText);
            removeToolStripMenuItem.Enabled = false;
            runToolStripMenuItem.Enabled = false;
            enterToolStripMenuItem.Enabled = true;
        }
    }
}
