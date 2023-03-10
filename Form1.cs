using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace thema1
{

    public partial class Form1 : Form
    {
        string connectionstring = @"Data Source=database\victorydata.db; Version=3; FailIfMissing=True";
        SQLiteConnection conn;
        public static Form1 instance;
        public int victories;
        public int defeats;
        OpenForm form = new OpenForm();
        Wins_Losses outcome=new Wins_Losses();
        int form2_tb1 = 0;
        int form2_tb2 = 0;
        DateTime start = DateTime.Now;
        int user_total_moves = 0;
        List<List<Control>> Usertable = new List<List<Control>>();   //user matrix buttons
        List<List<Control>> AItable = new List<List<Control>>();    //ai matrix buttons
        int AHealth_user = 5; //Aircraft carrier
        int DHealth_user = 4; //Destroyer
        int BHealth_user = 3; //Battleship
        int SHealth_user = 2; //Submarine
        int Shipsalive_user = 4;
        int AHealth_ai = 5; //Aircraft carrier
        int DHealth_ai = 4; //Destroyer
        int BHealth_ai = 3; //Battleship
        int SHealth_ai = 2; //Submarine
        int Shipsalive_ai = 4;
        bool ai_turn = false;
        Random rnd = new Random();

        public Form1()
        {

            List<Control> temp = new List<Control>();       //temporary list of the buttons in the form
            InitializeComponent();
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                if (this.Controls[i] is Button)
                {
                    temp.Add(this.Controls[i]);
                    instance = this;

                }
            }
            int counter = 0;                //sorting the buttons from the temp list to the user and ai matrices 
            for (int i = 0; i < 10; i++)
            {
                List<Control> row = new List<Control>();
                for (int j = 0; j < 10; j++)
                {
                    row.Add(temp[counter]);
                    counter++;
                }
                Usertable.Add(row);
            }
            for (int i = 0; i < 10; i++)
            {
                List<Control> row = new List<Control>();
                for (int j = 0; j < 10; j++)
                {
                    row.Add(temp[counter]);
                    counter++;
                }
                AItable.Add(row);
            }
            ShipSet(Usertable, 5, "a", Color.Red,6);
            ShipSet(AItable, 5, "a", Color.White, 6);
            ShipSet(Usertable, 4, "d", Color.Tan,7);
            ShipSet(AItable, 4, "d", Color.White, 7);
            ShipSet(Usertable, 3, "b", Color.Sienna,8);
            ShipSet(AItable, 3, "b", Color.White, 8);
            ShipSet(Usertable, 2, "s", Color.Magenta,9);
            ShipSet(AItable, 2, "s", Color.White,9);
        }
        void ShipSet(List<List<Control>> table, int squares, string shipname, Color rndColor,int bounds)
        {
            int direction =rnd.Next(0, 2);
            bool flag2 = false;

            if (direction == 0)         //sets the ship vertically 
            {
                while (flag2 == false)
                {
                    int x = rnd.Next(0, bounds);
                    int y = rnd.Next(0, bounds);
                    bool flag = true;
                    int counter = 0;
                    while (counter < squares && flag == true)
                    {
                        int i = y;
                        int j = x;
                         if ((table[i][j].Tag.ToString()).Equals("0"))
                         {
                            counter++;
                            j++;
                         }
                         else
                         {
                            flag = false;
                         }
                     
                    }
                    if (flag == true)
                    {
                        for (int j = x; j < x + squares; j++)
                        {
                            table[y][j].Tag = shipname;
                            table[y][j].BackColor = rndColor;
                            flag2 = true;               
                        }
                    }
                }
            }
            else                //sets the ship Horizontally    
            {
                while (flag2 == false)
                {
                    int x = rnd.Next(0, bounds);
                    int y = rnd.Next(0, bounds);
                    bool flag = true;
                    int counter = 0;
                    while (counter < squares && flag == true)
                    {
                        int i = y;
                        int j = x;
                        if ((table[i][j].Tag.ToString()).Equals("0"))
                        {
                            counter++;
                            i++;
                        }
                        else
                        {
                            flag = false;
                        }
 
                    }
                    if (flag == true)
                    {
                        for (int i = y; i < y + squares; i++)
                        {
                            table[i][x].Tag = shipname;
                            table[i][x].BackColor = rndColor;
                            flag2 = true;
                        }
                    }
                }
            }
        }

        void ButtonClick(object sender, EventArgs e)        //onClick event for the buttons of the ai matrix
        {
            
            ai_turn = false;
            Button button = sender as Button;
            if (button.Tag == "0")
            {
                button.BackColor = Color.Gray;
                button.ForeColor = Color.Green;
                button.Text = "-";
                button.Tag = "1";
                ai_turn = true;
            }
            else if (button.Tag == "a")
            {
                button.BackColor = Color.Black;
                button.ForeColor = Color.Red;
                button.Text = "X";
                button.Tag = "1";
                AHealth_ai--;
                ai_turn = true;
                if (AHealth_ai == 0)
                {
                    MessageBox.Show("Βυθίσατε το αντίπαλο Αεροπλανοφόρο");
                    Shipsalive_ai--;
                }
            }
            else if (button.Tag == "d")
            {
                button.BackColor = Color.Black;
                button.ForeColor = Color.Red;
                button.Text = "X";
                button.Tag = "1";
                DHealth_ai--;
                ai_turn = true;
                if (DHealth_ai == 0)
                {
                    MessageBox.Show("Βυθίσατε το αντίπαλο Αντιτορπυλικό");
                    Shipsalive_ai--;
                }
            }
            else if (button.Tag == "b")
            {
                button.BackColor = Color.Black;
                button.ForeColor = Color.Red;
                button.Text = "X";
                button.Tag = "1";
                BHealth_ai--;
                ai_turn = true;
                if (BHealth_ai == 0)
                {
                    MessageBox.Show("Βυθίσατε το αντίπαλο Πολεμικό");
                    Shipsalive_ai--; ;
                }
            }
            else if (button.Tag == "s")
            {
                button.BackColor = Color.Black;
                button.ForeColor = Color.Red;
                button.Text = "X";
                button.Tag = "1";
                SHealth_ai--;
                ai_turn = true;
                if (SHealth_ai == 0)
                {
                    MessageBox.Show("Βυθίσατε το αντίπαλο Υποβρύχιο");
                    Shipsalive_ai--;
                }
            }
            else
            {
                MessageBox.Show("!!!Διαλέξτε άλλο κουμπί!!!");
            }
            if (Shipsalive_ai == 0)
            {
                ai_turn = false;
                DateTime end = DateTime.Now;
                TimeSpan ts = (end - start);
                string str = ts.ToString(@"mm\:ss\:ff");        //time played
                conn = new SQLiteConnection(connectionstring);
                try
                {
                    using (var connection = new SQLiteConnection(connectionstring))
                    {
                        conn.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.CommandText = @"INSERT INTO Victories (Winner,Time) VALUES (@Winner , @Time)";
                        cmd.Connection = conn;
                        using (var conn = new SQLiteConnection(connectionstring))
                        {
                            cmd.Parameters.Add(new SQLiteParameter("@Winner", "User"));
                            cmd.Parameters.Add(new SQLiteParameter("@Time", str));
                            int i = cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show("Κερδίσατε");
                MessageBox.Show("Κινήσεις:" + user_total_moves.ToString() + Environment.NewLine + "Χρόνος:" + str + " λεπτά");
                Globals.victories++;
                victories = Globals.victories;
                defeats = Globals.defeats;
                form.form2 = new Form2();
                form.form2.Show();
                this.Hide();
            }
            if (ai_turn==true)
            {
                user_total_moves++;
                AI_attack();
                if (Shipsalive_user == 0)
                {
                    DateTime end = DateTime.Now;
                    TimeSpan ts = (end - start);
                    string str = ts.ToString(@"mm\:ss\:ff");           //time played
                    conn = new SQLiteConnection(connectionstring);
                    try
                    {
                        using (var connection = new SQLiteConnection(connectionstring))
                        {
                            conn.Open();
                            SQLiteCommand cmd = new SQLiteCommand();
                            cmd.CommandText = @"INSERT INTO Victories (Winner,Time) VALUES (@Winner , @Time)";
                            cmd.Connection = conn;
                            using (var conn = new SQLiteConnection(connectionstring))
                            {
                                cmd.Parameters.Add(new SQLiteParameter("@Winner", "AI"));
                                cmd.Parameters.Add(new SQLiteParameter("@Time", str));
                                int i = cmd.ExecuteNonQuery();
                            }
                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MessageBox.Show("Χάσατε");
                    MessageBox.Show("Κινήσεις:" + user_total_moves.ToString() + Environment.NewLine + "Χρόνος:" + str + " λεπτά");
                    Globals.defeats++;
                    victories = Globals.victories;
                    defeats = Globals.defeats;
                    form.form2 = new Form2();
                    form.form2.Show();
                    this.Hide();
                }
            }
        }
        void AI_attack()
        {
            {
                int i = rnd.Next(0, 10);
                int j = rnd.Next(0, 10);
                if (Usertable[i][j].Tag == "0")
                {
                    Usertable[i][j].BackColor = Color.Gray;
                    Usertable[i][j].ForeColor = Color.Green;
                    Usertable[i][j].Text = "-";
                    Usertable[i][j].Tag = "1";
                }
                else if (Usertable[i][j].Tag == "a")
                {
                    Usertable[i][j].BackColor = Color.PaleVioletRed;
                    Usertable[i][j].ForeColor = Color.Red;
                    Usertable[i][j].Text = "X";
                    Usertable[i][j].Tag = "1";
                    AHealth_user--;
                    if (AHealth_user == 0)
                    {
                        MessageBox.Show("Βυθίστηκε το Αεροπλανοφόρο μου");
                        Shipsalive_user--;
                    }
                }
                else if (Usertable[i][j].Tag == "d")
                {
                    Usertable[i][j].BackColor = Color.Beige;
                    Usertable[i][j].ForeColor = Color.Red;
                    Usertable[i][j].Text = "X";
                    Usertable[i][j].Tag = "1";
                    DHealth_user--;
                    if (DHealth_user == 0)
                    {
                        MessageBox.Show("Βυθίστηκε το Αντιτορπυλικό μου");
                        Shipsalive_user--;
                    }
                }
                else if (Usertable[i][j].Tag == "b")
                {
                    Usertable[i][j].BackColor = Color.SandyBrown;
                    Usertable[i][j].ForeColor = Color.Red;
                    Usertable[i][j].Text = "X";
                    Usertable[i][j].Tag = "1";
                    BHealth_user--;
                    if (BHealth_user == 0)
                    {
                        MessageBox.Show("Βυθίστηκε το Πολεμικό μου");
                        Shipsalive_user--; ;
                    }
                }
                else if (Usertable[i][j].Tag == "s")
                {
                    Usertable[i][j].BackColor = Color.LightPink;
                    Usertable[i][j].ForeColor = Color.Red;
                    Usertable[i][j].Text = "X";
                    Usertable[i][j].Tag = "1";
                    SHealth_user--;
                    if (SHealth_user == 0)
                    {
                        MessageBox.Show("Βυθίστηκε το Υποβρύχιο μου");
                        Shipsalive_user--;
                    }
                }
                else
                {
                    
                    AI_attack();
                }

            }
        }

    }
    class OpenForm 
    {
        public Form1 form1;
        public Form2 form2;

    }
    class Globals
    {
        public static int victories;
        public static int defeats;

    }

}
