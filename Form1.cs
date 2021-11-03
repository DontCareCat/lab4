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

namespace lab4_3axuct_wf
{
    public partial class Form1 : Form
    {
        int current = 0;
        static Wocabulary w = new Wocabulary();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.CheckFileExists = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            listBox1.Items.Add(openFileDialog1.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            button7.Enabled = false;
            dataGridView1.Enabled = true;
            for(int i=0;i<listBox1.Items.Count;i++)
            {
                string file = File.ReadAllText(listBox1.Items[i].ToString());
                file= new string(file.Where(c => !char.IsPunctuation(c)).ToArray());     //exclude punctuation.
                string s = "";
                for(int j=0;j<file.Length;j++)
                {
                    if (file[j] == '\\')
                    {
                        i += 2;
                    }
                    else
                        s += file[j];
                }
                file = s;
                w.Add(file);
            }
            List<float> possib = w.GetPosiibilities();
            DataTable dt = new DataTable("Fucking data");
            dt.Columns.Add("char", typeof(string));
            dt.Columns.Add("possibility",typeof(float));
            for(int i=0;i<possib.Count;i++)
            {
                DataRow dr = dt.NewRow();
                dr["char"] = w.symb[i].ToString();
                dr["possibility"] = (float)possib[i];
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
            label3.Text = dt.Rows.Count.ToString();
            current = 1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if(w.symb.Count==0)
            {
                button3_Click(sender, e);
            }
            dataGridView1.Enabled = true;
            string file = null;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                file = File.ReadAllText(listBox1.Items[i].ToString());
                file = new string(file.Where(c => !char.IsPunctuation(c)).ToArray());     //exclude punctuation.
                string ss = "";
                for (int j = 0; j < file.Length; j++)
                {
                    if (file[j] == '\\')
                    {
                        i += 2;
                    }
                    else
                        ss += file[j];
                }
                file = ss;
            }
            List<string> bigrams = new List<string>();
            List<int> count = new List<int>();
            List<float> possib = new List<float>();
            //string s = null;
            for(int i=0;i< file.Length-1;i++)
            {
                bool found = false;
                int j = 0;
                string bigram =$"{file[i]}{file[i + 1]}";
                do
                {
                    if(j==bigrams.Count&&!found)
                    {
                        found = true;
                        bigrams.Add(bigram);
                        count.Add(1);
                    }
                    else
                    {
                        if (bigram == bigrams[j])
                        {
                            found = true;
                            count[j]++;
                        }
                    }
                    j++;
                } while (!found);
            }
            float length = Convert.ToSingle(file.Length);
            foreach(int c in count)
            {
                possib.Add(c / length);
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("bigram", typeof(string));
            dt.Columns.Add("possibility", typeof(float));
            for (int i = 0; i < possib.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["bigram"] = bigrams[i];
                dr["possibility"] = possib[i];
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
            label3.Text = dt.Rows.Count.ToString();
            button7.Enabled = true;
            current = 2;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(this.Width - 40, this.Height - 260);
            label2.Location = new Point(label2.Location.X,183+dataGridView1.Size.Height);
            label3.Location = new Point(label3.Location.X, label2.Location.Y);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            w = new Wocabulary();
            current = 0;
            button10_Click(sender,e);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            button7.Enabled = false;
            dataGridView1.Enabled = true;
            string file = null;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                file = File.ReadAllText(listBox1.Items[i].ToString());
                file = new string(file.Where(c => !char.IsPunctuation(c)).ToArray());     //exclude punctuation.
                string ss = "";
                for (int j = 0; j < file.Length; j++)
                {
                    if (file[j] == '\\')
                    {
                        i += 2;
                    }
                    else
                        ss += file[j];
                }
                file = ss;
            }
            List<string> threegrams = new List<string>();
            List<int> count = new List<int>();
            List<float> possib = new List<float>();
            //string s = null;
            for (int i = 0; i < file.Length - 2; i++)
            {
                bool found = false;
                int j = 0;
                string threegram = $"{file[i]}{file[i + 1]}{file[i+2]}";
                do
                {
                    if (j == threegrams.Count && !found)
                    {
                        found = true;
                        threegrams.Add(threegram);
                        count.Add(1);
                    }
                    else
                    {
                        if (threegram == threegrams[j])
                        {
                            found = true;
                            count[j]++;
                        }
                    }
                    j++;
                } while (!found);
            }
            float length = Convert.ToSingle(file.Length);
            foreach (int c in count)
            {
                possib.Add(c / length);
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("threegram", typeof(string));
            dt.Columns.Add("possibility", typeof(float));
            for (int i = 0; i < possib.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["threegram"] = threegrams[i];
                dr["possibility"] = possib[i];
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
            label3.Text = dt.Rows.Count.ToString();
            current = 3;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            //dataGridView1.DataSource = dt;
            DataView dv = dt.DefaultView;
            dv.Sort = "possibility desc";
            dt = dv.ToTable();
            float minP = (float)dt.Rows[dt.Rows.Count - 1][1];
            float maxP = (float)dt.Rows[0][1];
            DataTable m = new DataTable();
            m.Columns.Add("   ");
            for(int i=0;i<w.symb.Count;i++)
            {
                m.Columns.Add($"{w.symb[i]}");
            }
            for (int i = 0; i < w.symb.Count; i++)
            {
                m.Rows.Add($"{w.symb[i]}");
            }
            dataGridView1.DataSource = m;
            dataGridView1.Columns[0].Frozen = true;
            foreach(DataGridViewColumn c in dataGridView1.Columns )
            {
                c.Width = 20;
            }
            
            
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                for(int j=1;j<dataGridView1.Columns.Count;j++)
                {
                    float p = 0;
                    string bigram = $"{w.symb[i]}{w.symb[j-1]}";
                    for(int k =0;k<dt.Rows.Count;k++)
                    {
                        if ((string)dt.Rows[k][0] == bigram)
                            p = (float)dt.Rows[k][1];
                    }
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    if (p != 0)
                    {
                        string ss = GetColour(minP, maxP, p);
                        style.BackColor = Color.FromArgb(int.Parse("FF"+GetColour(minP, maxP, p), System.Globalization.NumberStyles.HexNumber));
                    }
                    else
                    {
                        style.BackColor = Color.Black;
                    }
                    dataGridView1.Rows[i].Cells[j].Style = style;
                }
            }
            dataGridView1.Refresh();
        }

        static int CalcMainColour(float lowerProbability, float higherProbability, float inputProbability, int lowerProbabilityColour, int higherProbabilityClolour)
        {
            return Convert.ToInt32((lowerProbabilityColour * higherProbability + inputProbability * (higherProbabilityClolour - lowerProbabilityColour) - lowerProbability * higherProbabilityClolour) / (higherProbability - lowerProbability));
        }
        static string GetColour(float lowestProbability, float highestProbability, float inputProbability)
        {
            string str = "";
            if (inputProbability >= lowestProbability && inputProbability < (highestProbability - lowestProbability) / 2)
            {
                string r = CalcMainColour(lowestProbability, highestProbability / 2, inputProbability, 10, 120).ToString("X");
                if (r.Length < 2)
                    r = "0" + r;
                str += r + "0A76";
            }
            else
            {
                string b = CalcMainColour(highestProbability / 2, highestProbability, inputProbability, 120, 10).ToString("X");
                if (b.Length < 2)
                    b = "0" + b;
                str = "760A" + b;
            }
            return str;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            DataView dv = dt.DefaultView;
            dv.Sort = "possibility desc";
            dt = dv.ToTable();
            if (dt.Columns.Count == 2)
            {
                switch (current)
                {
                    case 1:
                        EncodeAthens1(dt);
                        break;
                    /*case 2:
                        EncodeAthens2(dt);
                        break;
                    case 3:
                        EncodeAthens3(dt);
                        break;*/
                }
            }
        }

        private void EncodeAthens1(DataTable dt)
        {
            foreach(string path in listBox1.Items)
            {
                string file = File.ReadAllText(path);
                file = new string(file.Where(c => !char.IsPunctuation(c)).ToArray());
                string wpath = PathToEncoded(path);

                DataView dv = dt.DefaultView;
                dv.Sort = "char";
                dt = dv.ToTable();
                dataGridView1.DataSource = dt;
                List<char> chars = new List<char>();
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    chars.Add(Convert.ToChar(dt.Rows[i][0]));
                }
                int a = 0;
                for (int x = chars.Count; x != 1; x--)
                {
                    if (gcb(x, chars.Count) == 1)
                        a = x;
                }
                using (StreamWriter sw = new StreamWriter(wpath))
                {
                    foreach (char c in file)
                    {
                        int x = chars.IndexOf(c);
                        int encIndex = (a * x + 5) % dt.Rows.Count;
                        sw.Write(dt.Rows[encIndex][0]);
                    }
                }
            }
        }

        private string PathToEncoded(string ReadingPath)
        {
            string[] tmp = ReadingPath.Split("\\");
            string filename = tmp[tmp.Length - 1].Split('.')[0];
            string wpath = null;
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                wpath += tmp[i] + "\\";
            }
            wpath += filename + "_encoded.txt";
            return wpath;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            DataView dv = dt.DefaultView;
            dv.Sort = "possibility desc";
            dt = dv.ToTable();
            if (dt.Columns.Count == 2)
            {
                switch (current)
                {
                    case 1:
                        DecodeAthens1(dt);
                        break;
                    /*case 2:
                        DecodeAthens2(dt);
                        break;
                    case 3:
                        DecodeAthens3(dt);
                        break;*/
                }
            }
        }

        private void DecodeAthens1(DataTable dt)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = "char";
            dt = dv.ToTable();
            List<char> chars = new List<char>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chars.Add(Convert.ToChar(dt.Rows[i][0]));
            }
            int a = 0;
            for (int x = chars.Count; x !=1; x--)
            {
                if (gcb(x, chars.Count) == 1)
                    a = x;
            }
            foreach (string path in listBox1.Items)
            {
                string wpath = PathToDecoded(path);
                string file = File.ReadAllText(path);
                file = new string(file.Where(c => !char.IsPunctuation(c)).ToArray());

                using (StreamWriter sw = new StreamWriter(wpath))
                {
                    int aInvers = MultiplicativeInverse(a);
                    foreach (char c in file)
                    {
                        int x = chars.IndexOf(c);
                        if (x - 5 < 0)
                            x += chars.Count;
                        int decIndex = (aInvers * (x - 5)) % chars.Count;
                        sw.Write(chars[decIndex]);
                    }
                }
            }
        }
        private string PathToDecoded(string ReadingPath)
        {
            string[] tmp = ReadingPath.Split("\\");
            string filename = tmp[tmp.Length - 1].Split('.')[0];
            string wpath = null;
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                wpath += tmp[i] + "\\";
            }
            wpath += filename + "_decoded.txt";
            return wpath;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
        public int MultiplicativeInverse(int a)
        {
            for (int i = 1; i < dataGridView1.Rows.Count + 1; i++)
            {
                if ((a * i) % dataGridView1.Rows.Count == 1)
                {
                    return i;
                }
            }
            throw new Exception("No Multiplicative Inverse Found");
        }
        int gcb(int a, int b)
        {
            if (b == 0) return a;
            return gcb(b, a % b);
        }
    }
}
