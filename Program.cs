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
        public List<char> symb = new List<char>();                        //список, в €кому збер≥гаютьс€ ун≥кальн≥ символи
        public List<int> count = new List<int>();                       //список, в €каому збер≥гаЇтьс€ к≥льк≥сть повторень символа
        public void Add(string s)                                       //метод дл€ заповненн€ словника
        {
            try
            {
                char[] CharsInString = s.ToCharArray();

                for (int j = 0; j < CharsInString.Length; j++)              //наступн≥ цикли перев≥р€ють кожен символ s, а саме чи в≥н уже Ї в словнику
                {
                    int i = 0;
                    bool found = false;
                    do
                    {
                        if (symb.Count == i && !found)                      //якщо немаЇ, то додаЇ до словника
                        {
                            found = true;
                            symb.Add(CharsInString[j]);
                            count.Add(1);
                        }
                        else
                        {
                            if (CharsInString[j] == symb[i])                //якщо Ї, то додаЇ до повторень символа 1
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
        public List<float> GetPosiibilities()                           // (=_= ) назва
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
        public int totalChars()                                         //ћб варто було зробити поле, €ке в €ке записане це число?
        {                                                               //(впадлу)
            int res = 0;
            foreach (int i in count)
            {
                res += i;
            }
            return res;
        }
    }
}
