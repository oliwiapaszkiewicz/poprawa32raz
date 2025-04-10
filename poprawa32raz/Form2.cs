using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;

namespace poprawa32raz
{
    public partial class Form2 : Form
    {
        public Osoba NewEmployee { get; private set; }
        public Form2()
        {
            InitializeComponent();
            comboBox1.Items.Add("Manager");
            comboBox1.Items.Add("Programista");
            comboBox1.Items.Add("Tester");
            comboBox1.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateLabelInfo();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateLabelInfo();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            UpdateLabelInfo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void UpdateLabelInfo()
        {
            string imie = textBox1.Text;
            string nazwisko = textBox2.Text;
            string wiek = textBox3.Text;
            string stanowisko = comboBox1.SelectedItem.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int id = new Random().Next(1, 1000);
            string imie = textBox1.Text;
            string nazwisko = textBox2.Text;
            int wiek;
            bool isWiekValid = int.TryParse(textBox3.Text, out wiek);
            string stanowisko = comboBox1.SelectedItem.ToString();
            NewEmployee = new Osoba(id, imie, nazwisko, wiek, stanowisko);
            this.DialogResult = DialogResult.OK; 
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
