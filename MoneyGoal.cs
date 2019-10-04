using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace DarkDemo
{
    public partial class MoneyGoal : Form
    {/// <summary>
    /// variable area
    /// </summary>
        private readonly string rootPath = @"G:\interesting shit\MoneyManagement";
        private string fileName="MoneyGoal.txt";

        private void FileUpdate()
        {
            File.WriteAllText(Path.Combine(rootPath, fileName), string.Empty);
            File.AppendAllText(Path.Combine(rootPath, fileName), label10.Text + "\n" + label7.Text + "\n" + label8.Text + "\n");
        }
        public MoneyGoal()
        {
            InitializeComponent();
          
            FileInfo file = new FileInfo(Path.Combine(rootPath, fileName));
            if (file.Length != 0 && file.Length != 3) //this means there is a goal 0 cand e complet gol si 3 cand are doar empty string in ea
            { //ascundem textboxurile unde poti adauga alta treaba
                textBox1.Hide();

                textBox3.Hide();

                textBox4.Hide();
                
                label10.Text = File.ReadAllText(Path.Combine(rootPath, fileName)).Split('\n')[0];
                label7.Text = File.ReadAllText(Path.Combine(rootPath, fileName)).Split('\n')[1];
                label8.Text = File.ReadAllText(Path.Combine(rootPath, fileName)).Split('\n')[2];
                //LABEL 9 STUFF
                double price = Convert.ToDouble(label7.Text);
                double gathered = Convert.ToDouble(label8.Text);

                label9.Text = (price - gathered).ToString();
            }
            else
            {
                textBox1.Show();

                textBox3.Show();

                textBox4.Show();

                label10.Text = string.Empty;
                label7.Text = string.Empty;
                label8.Text = string.Empty;

            }
           
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty) ///gata ultima eroare !!!!!!
                if (Double.TryParse(textBox3.Text, out double n) && Double.TryParse(textBox4.Text, out double m)) 
                if (e.KeyCode == Keys.Enter)
            {
                File.AppendAllText(Path.Combine(rootPath, fileName), textBox1.Text + "\n" + textBox3.Text + "\n" + textBox4.Text + "\n");
                //ascundem textboxurile unde poti adauga alta treaba
                    textBox1.Hide();

                    textBox3.Hide();

                    textBox4.Hide();
                    ///rezolv hardcodat o problema  im sorry im done with *this

                    int wordCounter = 0;
                    string[] actualWords = new string[3];
                    string[] file = File.ReadAllLines(Path.Combine(rootPath, fileName));
                    foreach (var item in file)
                    {
                        if(item!="\n")
                        {
                            actualWords[wordCounter++] = item;
                        }
                        if (wordCounter == 3)
                            break;
                    }
                label10.Text=actualWords[0];
                label7.Text=actualWords[1];
                label8.Text = actualWords[2] ;
                double price = Convert.ToDouble(label7.Text);
                double gathered = Convert.ToDouble(label8.Text);

                label9.Text = (price - gathered).ToString();
            }

        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //inseamna ca avem o valoare de adaugat :D
            {
                if (textBox2.Text != string.Empty)
                {
                    if (Double.TryParse(textBox2.Text, out double n))
                    {
                        double value = Convert.ToDouble(textBox2.Text);
                        double actualValue = Convert.ToDouble(label8.Text);
                        label8.Text = (value + actualValue).ToString();
                        double price = Convert.ToDouble(label7.Text);

                        double gathered = Convert.ToDouble(label8.Text);
                        
                        label9.Text = (price - gathered).ToString();

                        if ((price - gathered) < 0)   //yey am strans rublele
                        {

                            textBox1.Show();

                            textBox3.Show();

                            textBox4.Show();

                            label10.Text = string.Empty;
                            label7.Text = string.Empty;
                            label8.Text = string.Empty;
                            label9.Text = string.Empty;
                        }
                    }
                }
            }
            FileUpdate();
        }

        private void MoneyGoal_Load(object sender, EventArgs e)
        {

        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }
    }
}
