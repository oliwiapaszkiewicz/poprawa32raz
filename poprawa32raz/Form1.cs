using System.Text.Json;
using System.Xml.Serialization;

namespace poprawa32raz
{
    public partial class Form1 : Form
    {
        private List<Osoba> listaPracownikow = new List<Osoba>();
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("Id", "ID");
            dataGridView1.Columns.Add("Imie", "Imię");
            dataGridView1.Columns.Add("Nazwisko", "Nazwisko");
            dataGridView1.Columns.Add("Wiek", "Wiek");
            dataGridView1.Columns.Add("Stanowisko", "Stanowisko");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 addForm = new Form2();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                listaPracownikow.Add(addForm.NewEmployee);
                RefreshDataGrid();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < listaPracownikow.Count)
                {
                    listaPracownikow.RemoveAt(selectedIndex);
                    RefreshDataGrid();
                }
            }
        }
        private void RefreshDataGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaPracownikow;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files|*.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (var emp in listaPracownikow)
                    {
                        sw.WriteLine($"{emp.Id},{emp.Imie},{emp.Nazwisko},{emp.Wiek},{emp.Stanowisko}");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files|*.csv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                listaPracownikow.Clear();
                using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var parts = line.Split(',');
                        var id = int.Parse(parts[0]);
                        var imie = parts[1];
                        var nazwisko = parts[2];
                        var wiek = int.Parse(parts[3]);
                        var stanowisko = parts[4];

                        listaPracownikow.Add(new Osoba(id, imie, nazwisko, wiek, stanowisko));
                    }
                }
                RefreshDataGrid();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { Filter = "XML Files|*.xml" };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Osoba>));
                    serializer.Serialize(fs, listaPracownikow);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = "XML Files|*.xml" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Osoba>));
                        List<Osoba> wczytaneOsoby = (List<Osoba>)serializer.Deserialize(fs);

                        if (wczytaneOsoby != null)
                        {
                            listaPracownikow = wczytaneOsoby;
                            RefreshDataGrid();
                        }
                     
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas wczytywania XML: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { Filter = "JSON Files|*.json" };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string jsonString = JsonSerializer.Serialize(listaPracownikow, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(saveFileDialog.FileName, jsonString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas zapisywania do JSON: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button8_Click(object sender, EventArgs e) { }
    }
}