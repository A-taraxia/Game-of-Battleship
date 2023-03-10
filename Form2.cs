using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thema1
{
    
    public partial class Form2 : Form
    {
        OpenForm form = new OpenForm();
        public static Form2 instance;
        public TextBox tb1;
        public TextBox tb2;
        public Form2()
        {
            InitializeComponent();
            instance = this;
            tb1 = textBox1;         //victories
            tb2 = textBox2;         //defeats
            Form2.instance.tb1.Text= Form1.instance.victories.ToString();
            Form2.instance.tb2.Text = Form1.instance.defeats.ToString();
        }

        private void button1_Click(object sender, EventArgs e)          //play again button
        {
            form.form1 = new Form1();
            this.Close();
            form.form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)         //exit button
        {
            Application.Exit();
        }
    }
    class Wins_Losses
    {
        public int wins;
        public int losses;
    }
}
