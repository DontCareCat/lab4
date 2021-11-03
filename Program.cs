using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4_3axuct_wf
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    class Wocabulary
    {
        public List<char> symb = new List<char>();                        //������, � ����� ����������� ������� �������
        public List<int> count = new List<int>();                       //������, � ������ ���������� ������� ��������� �������
        public void Add(string s)                                       //����� ��� ���������� ��������
        {
            try
            {
                char[] CharsInString = s.ToCharArray();

                for (int j = 0; j < CharsInString.Length; j++)              //������� ����� ���������� ����� ������ s, � ���� �� �� ��� � � ��������
                {
                    int i = 0;
                    bool found = false;
                    do
                    {
                        if (symb.Count == i && !found)                      //���� ����, �� ���� �� ��������
                        {
                            found = true;
                            symb.Add(CharsInString[j]);
                            count.Add(1);
                        }
                        else
                        {
                            if (CharsInString[j] == symb[i])                //���� �, �� ���� �� ��������� ������� 1
                            {
                                found = true;
                                count[i]++;
                            }
                        }
                        i++;
                    } while (!found);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public List<float> GetPosiibilities()                           // (=_= ) �����
        {
            try
            {
                List<float> Poss = new List<float>();
                int total_chars = 0;
                foreach (int i in count)
                    total_chars += i;
                foreach (int i in count)
                {
                    Poss.Add((float)i / total_chars);
                }
                return Poss;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int totalChars()                                         //�� ����� ���� ������� ����, ��� � ��� �������� �� �����?
        {                                                               //(������)
            int res = 0;
            foreach (int i in count)
            {
                res += i;
            }
            return res;
        }
    }
}
