using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    class Team //class for team characteristics
    {
        private string clubn, city, surname, name;
        private int  winpoints, teamgoals;
        public Team()
        {
            clubn = "";
            city = "";
            surname = "";
            name = "";
            winpoints = 0;
            teamgoals = 0;
        }

        public void Set(string clubn, string city,
           string surname, string name)
        {
            this.clubn = clubn;
            this.city = city;
            this.surname = surname;
            this.name = name;
        }

        public string Getclubn() { return clubn; } // returns club name
        public string Getcity() { return city; } // returns city
        public string Getsurname() { return surname; } // returns surname
        public string Getname() { return name; }  //returns name
        public int Getwinpoints() { return winpoints; } //returns win points
        public int Getteamgoals() { return teamgoals; }// returns team goals
        public void Setwinpoints(int Wpnt) { winpoints = Wpnt; } // Sets win points
        public void Setteamgoals(int Tgol) { teamgoals = Tgol; }// sets team goals
        //overloaded operator
        public override string ToString()
        {
            string line;
            line = string.Format("|{0,-20}|{1,-15}|{2,-10}|{3,-10}|",
            clubn, city, surname, name);
            return line;
        }
        //Arange operators <= and >=
        public static bool operator <=(Team st1, Team st2)
        {
            int v1, v2;
            v1 = st1.Getwinpoints(); v2 = st2.Getwinpoints();
            return (v1 > v2);
        }
        public static bool operator >=(Team st1, Team st2)
        {
            int v1, v2;
            v1 = st1.Getwinpoints(); v2 = st2.Getwinpoints();
            return (v1 < v2);
        }

    }
    //Container
    class Matrix
    {
        //Constants
        const int CMaxlin = 1000;
        const int CMaxSt = 1000;
        private int[,] A; // data matrix
        private Team[] TeamT;
        public int n { get; set; } // line number
        public int m { get; set; } // column number

        public Matrix()
        {
            n = 0;
            m = 0;
            A = new int[CMaxlin, CMaxSt];
            TeamT = new Team[CMaxlin];
        }

        public void Set(Team ob) { TeamT[n++] = ob; }
        public Team Get(int nr) { return TeamT[nr]; }
        //changes matrix values
        public void SetWWW(int i, int j, int r) { A[i, j] = r; }
        //returns matrix values
        public int GetWWW(int i, int j) { return A[i, j]; }

        //------------------------------------------------
        //Finds how many goals team scored
        public void TeamGoals()
        {
            int goal = 0;
            Team kom;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    goal = goal + GetWWW(i, j);
                }
                kom = Get(i);
                kom.Setteamgoals(goal);
                goal = 0;
            }
        }
        //Finds victory points
        public void Wins()
        {
            int point = 0;
            Team kom;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (GetWWW(i, j) > GetWWW(j, i))
                        point = point + 3;
                    else if (GetWWW(i, j) == GetWWW(j, i))
                        point = point + 1;
                }
                kom = Get(i);
                kom.Setwinpoints(point - 1);
                point = 0;
            }
        }
        //Finds which team had most matches without conceding a goal
        public string ZeroConceded(Matrix team)
        {
            int cnt = 0;
            string ats = "";
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if((GetWWW(j , i)==0)&&(GetWWW(i, j) >= 0))
                    {
                        cnt++;
                        ats = string.Format("Team: {0} has most matches without conceding a goal.",
                        team.Get(i).Getclubn());
                    }
                    else ats = "All teams have conceded a goal";
                }
            }return ats;
        }
        //Aranges teams by points
        public void Arange()
        {
            for (int i = 0; i < n - 1; i++)
            {
                Team min = TeamT[i];
                int im = i;
                for (int j = i + 1; j < n; j++)
                    if (min >= TeamT[j])
                    {
                        min = TeamT[j];
                        im = j;
                    }
                TeamT[im] = TeamT[i];
                TeamT[i] = min;
            }
        }
    }
    class Program
    {
        //data and results files
        const string CFd = "..\\..\\Duomenys.txt";
        const string CFr = "..\\..\\Rezultatai.txt";

        static void Main(string[] args)
        {
            File.Delete(CFr);
            Matrix team = new Matrix();
            Read(CFd, ref team);
            team.Wins();
            team.TeamGoals();
            Print(CFr, team);
        }

        //Reads form file
        static void Read(string fd, ref Matrix team)
        {
            int nn, points;
            string line, clubn, name, surname, city;
            using (StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts;
                nn = int.Parse(line);
                team.m = nn;
                for (int i = 0; i < nn; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    clubn = parts[0];
                    city = parts[1];
                    surname = parts[2];
                    name = parts[3];
                    Team kom;
                    kom = new Team();
                    kom.Set(clubn, city, surname, name);
                    team.Set(kom);
                }
                for (int i = 0; i < nn; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    for (int j = 0; j < nn; j++)
                    {
                        points = int.Parse(parts[j]);
                        team.SetWWW(i, j, points);
                    }
                }
                team.n = nn;
            }
        }
        //Finds which team scored the most goals
        static string MostGoals(Matrix team)
        {
            string ats = "";
            int k = 0;
            for (int i = 0; i < team.m; i++)
            {
                if (team.Get(i).Getteamgoals() > team.Get(k).
                Getteamgoals())
                    k = i;
            }
            ats = string.Format("{0} scored: {1} goals ",
            team.Get(k).Getclubn(),
            team.Get(k).Getteamgoals());
            return ats;
        }


        //Prints results to results file
        static void Print(string fv, Matrix team)
        {
            using (var fr = File.AppendText(fv))
            {
                string br = new string('-', 60);
                fr.WriteLine("First data");
                fr.WriteLine("\nTeams:");
                fr.WriteLine(br);
                for (int i = 0; i < team.n; i++)
                {
                    fr.WriteLine(team.Get(i).ToString());
                    fr.WriteLine(br);
                }
                fr.WriteLine("Data matrix");
                for (int i = 0; i < team.m; i++)
                {
                    for (int j = 0; j < team.n; j++)
                    {
                        fr.Write(team.GetWWW(i, j) + ";");
                    }
                    fr.WriteLine("");
                }
                fr.WriteLine("\n");
                team.Arange();
                fr.WriteLine(br + "-----");
                for (int i = 0; i < team.n; i++)
                {
                    fr.WriteLine(team.Get(i).ToString() +
                    team.Get(i).Getwinpoints() + "  |");
                    fr.WriteLine(br + "---");
                }
                fr.WriteLine("\n");
                fr.WriteLine("Team with most goals:");
                fr.WriteLine(MostGoals(team));
                fr.WriteLine("\nTeam With most matches without conceded goals:");
                fr.WriteLine(team.ZeroConceded(team));
            }
        }
    }
}
