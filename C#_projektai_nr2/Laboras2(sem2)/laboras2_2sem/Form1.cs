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

namespace laboras2_2sem
{
    public partial class Form1 : Form
    {

        //const string CFd = "..\\..\\Players1.txt";   // data file
        //const string CFdm = "..\\..\\Players2.txt";  // data file
        const string CFr = "..\\..\\Results.csv";    // result's file
        const string Cffd = "..\\..\\Task.txt";
        const string Cfh = "..\\..\\user.txt";

        private List<Player> Team;    //Team 1
        private List<Player> Team1;   //Team 2
        private List<Player> Team2;   //Team 3 (combined Team1 with Team2)
        private List<Player> Team3;   //Team 4 (new Team)
        string TeamName;  //Team 1 name
        string TeamName1; //Team 2 name
        string TeamName2; //Team 4 (new team's agent name and surname)


        /// <summary>
        /// Read data from file
        /// </summary>
        /// <param name="fn">file name</param>
        /// <param name="TeamName">returns team's name</param>
        static List<Player> ReadFile(string fn, out string TeamName)
        {
            List<Player> Team = new List<Player>();
            using (StreamReader reader = new StreamReader(fn))
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
                    Team.Add(plr);
                }
            }
            return Team;
        }
        /// <summary>
        /// Print's a table with a team's players
        /// </summary>
        /// <param name="fn">file name</param>
        /// <param name="Team">Teams</param>
        /// <param name="heading">heading</param>
        static void Print(string fn, List<Player> Team, string heading)
        {
            const string top =
                    "-----------------------------------\r\n"
                    + "Nr.; |Name and Surname;| Age;| Height|; \r\n"
                    + "-----------------------------------";
            using (var fr = new StreamWriter(File.Open(fn, FileMode.Append)))
            {
                fr.WriteLine("\n" + heading+";");
                fr.WriteLine(top);
                for (int i = 0; i < Team.Count; i++)
                {
                    Player plr = Team[i];
                    fr.WriteLine("{0};     {1}", i + 1, plr);
                }
                fr.WriteLine("-----------------------------------\n");
            }
        }

        /// <summary>
        /// Connects two Team's players into one array whose height is bigger than avarage
        /// </summary>
        /// <param name="T">Team 1</param>
        /// /// <param name="T1">Team 2</param>
        /// <returns>combined team of two team's</returns>
        static List<Player> ConnectByHeight(List<Player> T, List<Player> T1)
        {
            List<Player> T2 = new List<Player>();
            for (int i = 0; i < T.Count; i++)
            {
                if (T[i].Height > AvgHeight(T))
                {
                    Player plr = new Player(T[i].NamSur,
                        T[i].Age, T[i].Height);
                    T2.Add(plr);
                }
            }
            for (int i = 0; i < T1.Count; i++)
            {
                if (T1[i].Height > AvgHeight(T1))
                {
                    Player plr1 = new Player(T1[i].NamSur,
                        T1[i].Age, T1[i].Height);
                    T2.Add(plr1);
                }
            }
            return T2;
        }

        /// <summary>
        /// Returns team's avarage age
        /// </summary>
        static double AvgAge(List<Player> T)
        {
            double a = 0;
            double avg = 0;
            for (int i = 0; i < T.Count; i++)
            {
                a = a + T[i].Age;
            }
            if (T.Count > 0)
                avg = a / T.Count;
            return avg;
        }

        
        /// <summary>
        /// Returns team's avarage height
        /// </summary>
        static double AvgHeight(List<Player> T)
        {
            double a = 0;
            double avg = 0;
            for (int i = 0; i < T.Count; i++)
            {
                a = a + T[i].Height;
            }
            if(T.Count > 0)
                avg = a / T.Count;
            return avg;
        }

        /// <summary>
        /// Add's players whose height is bigger than avarage to the sorted list
        /// </summary>
        /// <param name="T">Team 1</param>
        /// /// <param name="T1">Team 2</param>
        /// <returns>combined team of two team's</returns>
        static void AddPlr(List<Player> T, List<Player> T1)
        {
            double vid = T1.Average(item => item.Height);
            for (int i = 0; i < T.Count; i++)
            {
                if (T[i].Height > vid)
                {
                    Player plr = new Player(T[i].NamSur,
                       T[i].Age, T[i].Height);
                    T1.Add(plr);
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
            addToolStripMenuItem.Enabled = false;
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
                Team = ReadFile(fv, out TeamName);
                results.LoadFile(fv, RichTextBoxStreamType.PlainText);
                
            }
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog2.Title = "Choose a file";
            DialogResult result1 = openFileDialog1.ShowDialog();
            if (result1 == DialogResult.OK)
            {
                string fv1 = openFileDialog1.FileName;
                Team1 = ReadFile(fv1, out TeamName1);
                results3.LoadFile(fv1, RichTextBoxStreamType.PlainText);
                
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

        /// <summary>
        /// Actions of the "Run" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Enabled = false;
            removeToolStripMenuItem.Enabled = true;
            runToolStripMenuItem.Enabled = false;
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine(TeamName + " Avarage age is: " + AvgAge(Team));
                fr.WriteLine(TeamName + " Avarage height is: " + AvgHeight(Team));
                fr.WriteLine("\n\n" + TeamName1 + " Avarage age is: " + AvgAge(Team1));
                fr.WriteLine(TeamName1 + " Avarage height is: " + AvgHeight(Team1));
            }
            Team2 = ConnectByHeight(Team, Team1);
            Print(CFr, Team2, "Players above avg height");
            Team2.Sort();
            Print(CFr, Team2, "Sorted");
            results2.LoadFile(CFr, RichTextBoxStreamType.PlainText);
            label1.Text = "Type player's age";
        }

        /// <summary>
        /// Actions of the "Remove" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Enabled = true;
            runToolStripMenuItem.Enabled = false;
            removeToolStripMenuItem.Enabled = false;
            int age = int.Parse(Įvesti.Text);
            Team2.RemoveAll(item => item.Age > age);
            Print(CFr, Team2, "Removed list");
            if (Team2.Count < 1)
            {
                results2.Text += "There are no players with that age";
                using (var fr = File.AppendText(CFr))
                    fr.WriteLine("There are no players with that age");
            }
            else
                results2.LoadFile(CFr, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Actions of the "Add" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
                OpenFileDialog openFileDialog2 = new OpenFileDialog();
                openFileDialog2.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog2.Title = "Choose a file";
                DialogResult result1 = openFileDialog1.ShowDialog();
                if (result1 == DialogResult.OK)
                {
                    string fv1 = openFileDialog1.FileName;
                    Team3 = ReadFile(fv1, out TeamName2);
                    results4.LoadFile(fv1, RichTextBoxStreamType.PlainText);
                }

            if (Team2.Count > 0)
            {
                removeToolStripMenuItem.Enabled = false;
                runToolStripMenuItem.Enabled = false;
                addToolStripMenuItem.Enabled = false;
                double vid = Team2.Average(item => item.Height);

                if (File.Exists(CFr))
                    File.Delete(CFr);
                Print(CFr, Team, TeamName);
                Print(CFr, Team1, TeamName1);
                Print(CFr, Team3, TeamName2);
                runToolStripMenuItem_Click(sender, e);
                removeToolStripMenuItem_Click(sender, e);
                AddPlr(Team3, Team2);
                Team2.Sort();
                Print(CFr, Team2, "Combined");
                results2.LoadFile(CFr, RichTextBoxStreamType.PlainText);
                results2.Text += "Sorted list avarage height is: " + vid;
            }
            else
            {
                results2.Text += "\n\nThere are no players in the list";
                using (var fr = File.AppendText(CFr))
                    fr.WriteLine("\nThere are no players in the list");
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
            results4.Clear();
            results2.LoadFile(Cffd, RichTextBoxStreamType.PlainText);
            removeToolStripMenuItem.Enabled = false;
            runToolStripMenuItem.Enabled = false;
            addToolStripMenuItem.Enabled = false;
            enterToolStripMenuItem.Enabled = true;
        }
        /// <summary>
        /// Actions of the "User Instructions" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            results2.Clear();
            results3.Clear();
            results4.Clear();
            results2.LoadFile(Cfh, RichTextBoxStreamType.PlainText);
            removeToolStripMenuItem.Enabled = false;
            runToolStripMenuItem.Enabled = false;
            addToolStripMenuItem.Enabled = false;
            enterToolStripMenuItem.Enabled = true;
        }
    }
}
