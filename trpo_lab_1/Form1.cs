using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace trpo_lab_1
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        HashSet<char> setOfSigns = new HashSet<char>() { '+', '-', '/', '*' };
        HashSet<char> setOfDif = new HashSet<char>() { '<', '>', '=' };
        HashSet<char> setOfBrakes = new HashSet<char>() { '(', ')', '[', ']','{','}'};
        HashSet<string> setOfReservedWords = new HashSet<string> { "if", "else", "then", "and", "or", "while", "repeat", "do", "until" };
        List<string> answer = new List<string>();
        private void btnGoAutomat_Click(object sender, EventArgs e)
        {
            runAutomat(txbxExpression.Text);
            txbxAnswer.Text = "";            
            foreach (string item in answer)
            {               
                txbxAnswer.Text += item + Environment.NewLine;
            }
            answer.Clear();
        }
        void runAutomat(string s)
        {
            int i = 0;
            StringBuilder tmp = new StringBuilder(s[i]);
            while (i < s.Length)
            {
                tmp.Append(s[i]);
                if (s[i] == ' ')
                    i++;
                else
                    if (Char.IsLetter(s[i]))//если буква
                    {
                        i++;
                        //пока буква или цифра
                        while (i < s.Length && (Char.IsLetter(s[i]) || Char.IsDigit(s[i])))
                        {
                            tmp.Append(s[i]);
                            i++;
                        }
                        if (setOfReservedWords.Contains(tmp.ToString()))
                        {
                            answer.Add(tmp.ToString() + "\t служебное слово");
                        }
                        else //это не служебное слово, проверим, идет ли после оператор присваивания
                        {
                            answer.Add(tmp.ToString() + "\t идентификатор");
                            while (i < s.Length && s[i] == ' ')
                                i++;
                            if (i < s.Length - 2 && s[i] == ':' && s[i + 1] == '=')
                            {
                                answer.Add(s[i].ToString() + s[i + 1].ToString() + "\t оператор присваивания");
                                i += 2;
                            }

                        }

                    }
                    else
                        if (Char.IsDigit(s[i]))//если цифра
                        {
                            i++;
                            while (i < s.Length && Char.IsDigit(s[i]))//пока цифры
                            {
                                tmp.Append(s[i]);
                                i++;
                            }
                            if (i < s.Length - 2 && s[i] == '.' && Char.IsDigit(s[i + 1]))//если нашли точку и после нее цифра
                            {
                                tmp.Append(s[i]);
                                i++;
                                while (i < s.Length && Char.IsDigit(s[i]))
                                {
                                    tmp.Append(s[i]);
                                    i++;
                                }
                                answer.Add(tmp.ToString() + "\t вещественное число");
                            }
                            else
                                answer.Add(tmp.ToString() + "\t целое число");
                        }
                        else
                            if (setOfSigns.Contains(s[i]))
                            {
                                answer.Add(s[i] + "\t арифметическая операция");
                                i++;
                            }
                            else
                                if (setOfDif.Contains(s[i]))
                                {
                                    answer.Add(s[i] + "\t операция сравнения");
                                    i++;
                                }
                                else
                                    if (s[i] == '.')
                                    {
                                        answer.Add(s[i] + "\t точка");
                                        i++;
                                    }
                                    else
                                        if (setOfBrakes.Contains(s[i]))
                                        {
                                            answer.Add(s[i] + "\t скобка");
                                            i++;
                                        }
                                        else
                                        {
                                            answer.Add(s[i] + "\t неизвестный символ");
                                            i++;
                                        }
                tmp.Clear();
            }

        }

        private void btnClearExpr_Click(object sender, EventArgs e)
        {
            txbxExpression.Text = "";
            txbxAnswer.Text="";

        }
    }
}
