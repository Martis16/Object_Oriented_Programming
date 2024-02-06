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

namespace Laboras1_2sem_
{
    public partial class Form1 : Form
    {

        const string CFd = "..\\..\\Players1.txt"; // data file
        const string CFdm = "..\\..\\Players2.txt";// data file
        const string CFr = "..\\..\\Results.txt";    // result's file

        TeamCont Team;    //Team 1
        TeamCont Team1;   //Team 2
        TeamCont Team2;   //Team 3 (combined)
        string TeamName;  //Team 1 name
        string TeamName1; //Team 2 name

        /// <summary>
        /// Read data from file
        /// </summary>
        /// <param name="fn">file name</param>
        /// <param name="TeamName">returns team's name</param>
        static TeamCont ReadFile(string fn, out string TeamName)
        {
            TeamCont Team = new TeamCont();
            using(StreamReader reader = new StreamReader(fn))
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
                    Team.SetPlayer(plr);
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
        static void Print(string fn, TeamCont Team, string heading)
        {
            const string top =
                    "-----------------------------------\r\n"
                    + " Nr. |Name and Surname| Age| Height| \r\n"
                    + "-----------------------------------";
            using (var fr = new StreamWriter(File.Open(fn, FileMode.Append)))
            {
                fr.WriteLine("\n\n " + heading);
                fr.WriteLine(top);
                for (int i = 0; i < Team.Count; i++)
                {
                    Player plr = Team.GetPlayer(i);
                    fr.WriteLine("{0}     {1}",i+1, plr);
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
        static TeamCont ConnectByHeight(TeamCont T, TeamCont T1)
        {
            TeamCont T2 = new TeamCont();
            for (int i = 0; i < T.Count; i++)
            {
                if (T.GetPlayer(i).Height > T.AvgHeight())
                {
                    Player plr = new Player(T.GetPlayer(i).NamSur,
                        T.GetPlayer(i).Age, T.GetPlayer(i).Height);
                    T2.SetPlayer(plr);
                }
            }
            for (int i = 0; i < T1.Count; i++)
            {
                if (T1.GetPlayer(i).Height > T1.AvgHeight())
                {
                    Player plr1 = new Player(T1.GetPlayer(i).NamSur,
                        T1.GetPlayer(i).Age, T1.GetPlayer(i).Height);
                    T2.SetPlayer(plr1);
                }
            }return T2;
        }

        public Form1()
        {
            InitializeComponent();

            if (File.Exists(CFr))
                File.Delete(CFr);
        }

        /// <summary>
        /// Actions of the "close" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Actions of the "run" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void run_Click(object sender, EventArgs e)
        {
            Team = ReadFile(CFd, out TeamName);
            Print(CFr, Team, TeamName);
            Team1 = ReadFile(CFdm, out TeamName1);
            Print(CFr, Team1, TeamName1);
            results.LoadFile(CFr, RichTextBoxStreamType.PlainText);
            count.Enabled = true;
            run.Enabled = false;
        }

        /// <summary>
        /// Actions of the "count" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void count_Click(object sender, EventArgs e)
        {
            sort.Enabled = true;
            results.Clear();
            results.LoadFile(CFr, RichTextBoxStreamType.PlainText);
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine(TeamName + " Avarage age is: " + Team.AvgAge());
                fr.WriteLine("\n" + TeamName + " Avarage height is: " + Team.AvgHeight());
                fr.WriteLine("\n\n" + TeamName1 + " Avarage age is: " + Team1.AvgAge());
                fr.WriteLine("\n" + TeamName1 + " Avarage height is: " + Team1.AvgHeight());
            }
            results.Text += TeamName + " Avarage age is: " + Team.AvgAge();
            results.Text += "\n" + TeamName + " Avarage height is: " + Team.AvgHeight();
            results.Text += "\n\n" + TeamName1 + " Avarage age is: " + Team1.AvgAge();
            results.Text += "\n" + TeamName1 + " Avarage height is: " + Team1.AvgHeight();
            count.Enabled = false;
        }

        /// <summary>
        /// Actions of the "sort" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sort_Click(object sender, EventArgs e)
        {

            Team2 = ConnectByHeight(Team, Team1);
            Print(CFr, Team2, "Players above avg Height");
            Team2.Sort();
            Print(CFr, Team2, "Sorted List");
            remove.Enabled = true;
            results2.LoadFile(CFr, RichTextBoxStreamType.PlainText);
            sort.Enabled = false;

        }

        /// <summary>
        /// Actions of the "remove" menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void remove_Click(object sender, EventArgs e)
        {
            //sort_Click(sender, e);
            int age = int.Parse(Įvesti.Text);
            if (age % 1 == 0)
            {
                Team2.Remove(age);
                Print(CFr, Team2, "Removed List");
                results.Clear();
                if (Team2.Count < 1)
                {
                    results.Text += "There are no players with that age";
                    using (var fr = File.AppendText(CFr))
                        fr.WriteLine("There are no players with that age");
                }
                else
                    results.LoadFile(CFr, RichTextBoxStreamType.PlainText);
            }
            else results.Text += "Please retype age";
            remove.Enabled = false;
        }
    }
}
