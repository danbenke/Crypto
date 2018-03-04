using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto
{
    public partial class Decrypt : Form
    {
        char[] code;
        char[] trans;
        int ch;
        public Decrypt()
        {
            InitializeComponent();
        }
        private void getinput()//gets text from textbox and puts it into a char array
        {
            trans = textBox1.Text.ToCharArray();
        }

        private string getinputstring()//gets text from textbox and puts it into a char array
        {
            return textBox1.Text;
        }

        private void sendoutput()// puts trans into text box
        {
            textBox1.Text = new string(trans);
        }

        private void sendoutputstring(string output)// put string into text box
        {
            textBox1.Text = output;
        }

        private bool ischar(char c)//is c a-Z?
        {
            int ch = (int)c;
            if (((ch >= 65 && ch <= 90) || (ch >= 97 && ch <= 122)))
                return true;
            return false;
        }
        private void invert_word(int start, int fin)
        {
            char temp;
            for (int j = 0; j < (fin - start) / 2; j++)
            {
                temp = trans[start + j];
                trans[start + j] = trans[fin - j];
                trans[fin - j] = temp;
            }
        }
        private void Decoder()
        {
            char cha;
            for(int j=code.Length-1;j>=0;j--)
            {
                cha = code[j];
                switch(cha)
                {
                    case ('A')://shifts left
                        {
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                ch = (int)trans[i];
                                if ((ch > 65 && ch <= 90) || (ch > 97 && ch <= 122))
                                    trans[i] = (char)(((int)trans[i]) - 1);
                                else if (ch == 65 || ch == 97)
                                    trans[i] = (char)(((int)trans[i]) + 25);
                            }
                            sendoutput();
                            break;
                        }
                    case ('B')://shifts right
                        {
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                ch = (int)trans[i];
                                if ((ch >= 65 && ch < 90) || (ch >= 97 && ch < 122))
                                    trans[i] = (char)(((int)trans[i]) + 1);
                                else if (ch == 90 || ch == 122)
                                    trans[i] = (char)(((int)trans[i]) - 25);
                            }
                            sendoutput();
                            break;
                        }
                    case ('C')://inverts alphabet
                        {
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                ch = (int)trans[i];
                                if (ch >= 65 && ch <= 90)
                                    trans[i] = (char)(90 - (((int)trans[i]) - 65));
                                else if (ch >= 97 && ch <= 122)
                                    trans[i] = (char)(122 - (((int)trans[i]) - 97));
                            }
                            sendoutput();
                            break;
                        }
                    case ('D')://bivide alphabet
                        {
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                ch = (int)trans[i];
                                if (ch >= 65 && ch < 78)
                                    trans[i] = (char)(77 - (((int)trans[i]) - 65));
                                else if (ch >= 78 && ch <= 90)
                                    trans[i] = (char)(90 - (((int)trans[i]) - 78));
                                else if (ch >= 97 && ch < 110)
                                    trans[i] = (char)(109 - (((int)trans[i]) - 97));
                                else if (ch >= 110 && ch <= 122)
                                    trans[i] = (char)(122 - (((int)trans[i]) - 110));
                            }
                            sendoutput();
                            break;
                        }
                    case ('E')://shift line up
                        {
                            string output = "";
                            string holding = "";
                            string input = getinputstring();
                            int chop = 0;
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                if (i != trans.Length - 1 && trans[i] == '\r' && trans[i + 1] == '\n')
                                {
                                    holding = input.Substring(0, i);
                                    chop = i + 2;
                                    break;
                                }
                            }
                            if (holding != "")
                            {
                                output = input.Substring(chop) + "\r\n" + holding;
                                sendoutputstring(output);
                            }
                            else
                            {
                                sendoutputstring(input);
                            }
                            break;
                        }
                    case ('F')://shift line down
                        {
                            string output = "";
                            string holding = "";
                            string input = getinputstring();
                            int chop = 0;
                            getinput();
                            for (int i = trans.Length - 1; i >= 0; i--)
                            {
                                if (i != 0 && trans[i] == '\n' && trans[i - 1] == '\r')
                                {
                                    holding = input.Substring(i + 1);
                                    chop = i - 1;
                                    break;
                                }
                            }
                            if (holding != "")
                            {
                                output = holding + "\r\n" + input.Substring(0, chop);
                                sendoutputstring(output);
                            }
                            else
                            {
                                sendoutputstring(input);
                            }
                            break;
                        }
                    case ('G')://flip words
                        {
                            int start = 0;
                            int fin = 0;
                            bool inword = false;
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                if (!inword && ischar(trans[i]))
                                {
                                    start = i;
                                    inword = true;
                                }
                                else if (inword && !ischar(trans[i]))
                                {
                                    fin = i - 1;
                                    inword = false;
                                    invert_word(start, fin);
                                }
                            }
                            fin = trans.Length - 1;
                            invert_word(start, fin);
                            sendoutput();
                            break;
                        }
                    case ('H')://shift letter 1 left
                        {
                            getinput();
                            if (trans.Length == 0)
                            {
                                break;
                            }
                            char temp = trans[0];
                            char temp2;
                            for (int i = trans.Length - 2; i > 0; i--)
                            {
                                if (trans[i - 1] == ' ' || trans[i - 1] == '\n' || trans[i - 1] == '\t')
                                {
                                    temp2 = trans[i];
                                    trans[i] = temp;
                                    temp = temp2;
                                }
                            }
                            trans[0] = temp;
                            sendoutput();
                            break;
                        }
                    case ('I'):// shift letter 1 right
                        {
                            getinput();
                            if (trans.Length == 0)
                            {
                                break;
                            }
                            char temp = trans[0];
                            char temp2;
                            for (int i = 1; i < trans.Length; i++)
                            {
                                if (trans[i - 1] == ' ' || trans[i - 1] == '\n' || trans[i - 1] == '\t')
                                {
                                    temp2 = trans[i];
                                    trans[i] = temp;
                                    temp = temp2;
                                }
                            }
                            trans[0] = temp;
                            sendoutput();
                            break;
                        }
                    case ('J')://shift letter n left
                        {
                            getinput();
                            if (trans.Length == 0)
                            {
                                break;
                            }
                            char temp = trans[trans.Length - 1];
                            char temp2;
                            for (int i = trans.Length - 2; i > 0; i--)
                            {
                                if (trans[i + 1] == ' ' || trans[i + 1] == '\r' || trans[i + 1] == '\t')
                                {
                                    temp2 = trans[i];
                                    trans[i] = temp;
                                    temp = temp2;
                                }
                            }
                            trans[trans.Length - 1] = temp;
                            sendoutput();
                            break;
                        }
                    case ('K')://shift letter n right
                        {
                            getinput();
                            if (trans.Length == 0)
                            {
                                break;
                            }
                            char temp = trans[trans.Length - 1];
                            char temp2;
                            for (int i = 0; i < trans.Length - 1; i++)
                            {
                                if (trans[i + 1] == ' ' || trans[i + 1] == '\r' || trans[i + 1] == '\t')
                                {
                                    temp2 = trans[i];
                                    trans[i] = temp;
                                    temp = temp2;
                                }
                            }
                            trans[trans.Length - 1] = temp;
                            sendoutput();
                            break;
                        }
                    case ('L'):
                        {
                            int count = 1, count2 = 0;
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                if(trans[i]=='\r'&&trans[i+1]=='\n')
                                {
                                    count++;
                                }
                            }
                            count = count / 2;
                            for (int i = 0; i < trans.Length; i++)
                            {
                                if (trans[i] == '\r' && trans[i + 1] == '\n')
                                {
                                    count2++;
                                }
                                if(count2==count)
                                {
                                    sendoutputstring(new string(trans).Substring(0,i));
                                    break;
                                }
                            }
                            break;
                        }
                    case ('M'):// invert caps
                        {
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                ch = (int)trans[i];
                                if (ch >= 65 && ch <= 90)
                                    trans[i] = (char)((((int)trans[i]) + 32));
                                else if (ch >= 97 && ch <= 122)
                                    trans[i] = (char)((((int)trans[i]) - 32));
                            }
                            sendoutput();
                            break;
                        }
                        case ('N'):
                        {
                            getinput();
            for (int i = 0; i < trans.Length; i++)
            {
                                char c = trans[i];
                if (c == 'w') trans[i] = 'q'; else if (c == 'e') trans[i] = 'w'; else if (c == 'r') trans[i] = 'e'; else if (c == 't') trans[i] = 'r'; 
                else if (c == 'y') trans[i] = 't'; else if (c == 'u') trans[i] = 'y'; else if (c == 'i') trans[i] = 'u'; else if (c == 'o') trans[i] = 'i'; else if (c == 'p') trans[i] = 'o';
                else if (c == 'q') trans[i] = 'p'; else if (c == 's') trans[i] = 'a'; else if (c == 'd') trans[i] = 's'; else if (c == 'f') trans[i] = 'd'; else if (c == 'g') trans[i] = 'f';
                else if (c == 'h') trans[i] = 'g'; else if (c == 'j') trans[i] = 'h'; else if (c == 'k') trans[i] = 'j'; else if (c == 'l') trans[i] = 'k'; else if (c == 'a') trans[i] = 'l';
                else if (c == 'x') trans[i] = 'z'; else if (c == 'c') trans[i] = 'x'; else if (c == 'v') trans[i] = 'c'; else if (c == 'b') trans[i] = 'v'; else if (c == 'n') trans[i] = 'b';
                else if (c == 'm') trans[i] = 'n'; else if (c == 'z') trans[i] = 'm'; else if (c == 'W') trans[i] = 'Q'; else if (c == 'E') trans[i] = 'W'; else if (c == 'R') trans[i] = 'E';
                else if (c == 'T') trans[i] = 'R'; else if (c == 'Y') trans[i] = 'T'; else if (c == 'U') trans[i] = 'Y'; else if (c == 'I') trans[i] = 'U'; else if (c == 'O') trans[i] = 'I';
                else if (c == 'P') trans[i] = 'O'; else if (c == 'Q') trans[i] = 'P'; else if (c == 'S') trans[i] = 'A'; else if (c == 'D') trans[i] = 'S'; else if (c == 'F') trans[i] = 'D';
                else if (c == 'G') trans[i] = 'F'; else if (c == 'H') trans[i] = 'G'; else if (c == 'J') trans[i] = 'H'; else if (c == 'K') trans[i] = 'J'; else if (c == 'L') trans[i] = 'K';
                else if (c == 'L') trans[i] = 'A'; else if (c == 'X') trans[i] = 'Z'; else if (c == 'C') trans[i] = 'X'; else if (c == 'V') trans[i] = 'C'; else if (c == 'B') trans[i] = 'V';
                else if (c == 'N') trans[i] = 'B'; else if (c == 'M') trans[i] = 'N'; else if (c == 'Z') trans[i] = 'M';
            }
            sendoutput();
                            break;
                        }
                    case ('O'):
                        {
                            getinput();
                for (int i = 0; i < trans.Length; i++)
                {
                                char c = trans[i];
                    if (c == 'q') trans[i] = 'w'; else if (c == 'w') trans[i] = 'e'; else if (c == 'e') trans[i] = 'r'; else if (c == 'r') trans[i] = 't';
                    else if (c == 't') trans[i] = 'y';else if (c == 'y') trans[i] = 'u';else if (c == 'u') trans[i] = 'i';else if (c == 'i') trans[i] = 'o';else if (c == 'o') trans[i] = 'p';
                    else if (c == 'p') trans[i] = 'q';else if (c == 'a') trans[i] = 's';else if (c == 's') trans[i] = 'd';else if (c == 'd') trans[i] = 'f';else if (c == 'f') trans[i] = 'g';
                    else if (c == 'g') trans[i] = 'h';else if (c == 'h') trans[i] = 'j';else if (c == 'j') trans[i] = 'k';else if (c == 'k') trans[i] = 'l';else if (c == 'l') trans[i] = 'a';
                    else if (c == 'z') trans[i] = 'x';else if (c == 'x') trans[i] = 'c';else if (c == 'c') trans[i] = 'v';else if (c == 'v') trans[i] = 'b';else if (c == 'b') trans[i] = 'n';
                    else if (c == 'n') trans[i] = 'm';else if (c == 'm') trans[i] = 'z';else if (c == 'Q') trans[i] = 'W';else if (c == 'W') trans[i] = 'E';else if (c == 'E') trans[i] = 'R';
                    else if (c == 'R') trans[i] = 'T';else if (c == 'T') trans[i] = 'Y';else if (c == 'Y') trans[i] = 'U';else if (c == 'U') trans[i] = 'I';else if (c == 'I') trans[i] = 'O';
                    else if (c == 'O') trans[i] = 'P';else if (c == 'P') trans[i] = 'Q';else if (c == 'A') trans[i] = 'S';else if (c == 'S') trans[i] = 'D';else if (c == 'D') trans[i] = 'F';
                    else if (c == 'F') trans[i] = 'G';else if (c == 'G') trans[i] = 'H';else if (c == 'H') trans[i] = 'J';else if (c == 'J') trans[i] = 'K';else if (c == 'K') trans[i] = 'L';
                    else if (c == 'A') trans[i] = 'L';else if (c == 'Z') trans[i] = 'X';else if (c == 'X') trans[i] = 'C';else if (c == 'C') trans[i] = 'V';else if (c == 'V') trans[i] = 'B';
                    else if (c == 'B') trans[i] = 'N'; else if (c == 'N') trans[i] = 'M'; else if (c == 'M') trans[i] = 'Z';
                }
                sendoutput();
                            break;
                        }
                    case ('P'):
                        {
                            int alt = 0;
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                ch = (int)trans[i];
                                if (ch >= 65 && ch <= 90 && alt == 1)
                                    trans[i] = (char)(90 - (((int)trans[i]) - 65));
                                else if (ch >= 97 && ch <= 122 && alt == 1)
                                    trans[i] = (char)(122 - (((int)trans[i]) - 97));
                                if (alt == 0)
                                    alt = 1;
                                else
                                    alt = 0;
                            }
                            sendoutput();
                            break;
                        }
                    case ('Q'):
                        {
                            int alt = 1;
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                if (alt == 0)
                                {
                                    ch = (int)trans[i];
                                    if (ch >= 65 && ch < 78)
                                        trans[i] = (char)(77 - (((int)trans[i]) - 65));
                                    else if (ch >= 78 && ch <= 90)
                                        trans[i] = (char)(90 - (((int)trans[i]) - 78));
                                    else if (ch >= 97 && ch < 110)
                                        trans[i] = (char)(109 - (((int)trans[i]) - 97));
                                    else if (ch >= 110 && ch <= 122)
                                        trans[i] = (char)(122 - (((int)trans[i]) - 110));
                                }
                                if (alt == 0) alt = 1;
                                else alt = 0;
                            }
                            sendoutput();
                            break;
                        }
                    case ('R'):
                        {
                            int alt = 0;
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                if (alt == 0)
                                {
                                    ch = (int)trans[i];
                                    if (ch >= 65 && ch <= 90)
                                        trans[i] = (char)((((int)trans[i]) + 32));
                                    else if (ch >= 97 && ch <= 122)
                                        trans[i] = (char)((((int)trans[i]) - 32));
                                }
                                if (alt == 0) alt = 1;
                                else alt = 0;
                            }
                            sendoutput();
                            break;
                        }
                    case ('S'):
                        {
                            int alt = 1;
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                ch = (int)trans[i];
                                if (ch >= 65 && ch <= 90 && alt == 1)
                                    trans[i] = (char)(90 - (((int)trans[i]) - 65));
                                else if (ch >= 97 && ch <= 122 && alt == 1)
                                    trans[i] = (char)(122 - (((int)trans[i]) - 97));
                                if (alt == 0)
                                    alt = 1;
                                else
                                    alt = 0;
                            }
                            sendoutput();
                            break;
                        }
                    case ('T'):
                        {
                            int alt = 0;
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                if (alt == 0)
                                {
                                    ch = (int)trans[i];
                                    if (ch >= 65 && ch < 78)
                                        trans[i] = (char)(77 - (((int)trans[i]) - 65));
                                    else if (ch >= 78 && ch <= 90)
                                        trans[i] = (char)(90 - (((int)trans[i]) - 78));
                                    else if (ch >= 97 && ch < 110)
                                        trans[i] = (char)(109 - (((int)trans[i]) - 97));
                                    else if (ch >= 110 && ch <= 122)
                                        trans[i] = (char)(122 - (((int)trans[i]) - 110));
                                }
                                if (alt == 0) alt = 1;
                                else alt = 0;
                            }
                            sendoutput();
                            break;
                        }
                    case ('U'):
                        {
                            int alt = 1;
                            getinput();
                            for (int i = 0; i < trans.Length; i++)
                            {
                                if (alt == 0)
                                {
                                    ch = (int)trans[i];
                                    if (ch >= 65 && ch <= 90)
                                        trans[i] = (char)((((int)trans[i]) + 32));
                                    else if (ch >= 97 && ch <= 122)
                                        trans[i] = (char)((((int)trans[i]) - 32));
                                }
                                if (alt == 0) alt = 1;
                                else alt = 0;
                            }
                            sendoutput();
                            break;
                        }
                    case ('V'):
                        {
                            getinput();
                            string output = "";
                            for (int i = trans.Length - 1; i >= 0; i--)
                            {
                                output += trans[i].ToString();
                            }
                            sendoutputstring(output);
                            break;
                        }
                    case ('W'):
                        {
                            getinput();
                            string output = "";
                            int placeholder = 0;
                            getinput();
                            for (int i = trans.Length - 1; i > 0; i--)
                            {
                                if (trans[i - 1] == ' ' || trans[i - 1] == '\n' || trans[i - 1] == '\t')
                                {
                                    output += new string(trans).Substring(i, placeholder + 1);
                                    placeholder = 0;
                                }
                                else
                                    placeholder++;
                            }
                            output += new string(trans).Substring(0, placeholder);
                            sendoutputstring(output);
                            break;
                        }
                    case ('X'):
                        {
                            getinput();
                            int ch;
                            for (int i = 0; i < trans.Length; i++)
                            {
                                ch = (int)Char.GetNumericValue(trans[i]);
                                if (ch > 0 && ch <= 9)
                                    trans[i] = (ch - 11).ToString()[0];
                                else if (ch == 0)
                                    trans[i] = (9).ToString()[0];
                            }
                            sendoutput();
                            break;
                        }
                    case ('Y'):
                        {
                            getinput();
                            int ch;
                            for (int i = 0; i < trans.Length; i++)
                            {
                                ch = (int)Char.GetNumericValue(trans[i]);
                                if (ch >= 0 && ch < 9)
                                    trans[i] = (ch + 1).ToString()[0];
                                else if (ch == 9)
                                    trans[i] = (0).ToString()[0];
                            }
                            sendoutput();
                            break;
                        }
                    case ('Z'):
                        {
                            getinput();
                            int ch;
                            for (int i = 0; i < trans.Length; i++)
                            {
                                ch = (int)Char.GetNumericValue(trans[i]);
                                if (ch >= 0 && ch <= 9)
                                    trans[i] = (9 - ch).ToString()[0];
                            }
                            sendoutput();
                            break;
                        }

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           textBox2.Text=textBox2.Text.ToUpper();
            string input = textBox2.Text;
            foreach(char c in input)
            {
                if ((int)c < 65 || (int)c >= 90)
                {
                    label3.Visible = true;
                    label4.Visible = true;
                    return;
                }  
            }
            code = input.ToCharArray();
            label3.Visible = false;
            label4.Visible = false;
            Decoder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)//import
        {
            openFileDialog1.Filter = "Text|*.txt|All|*.*";
            openFileDialog1.ShowDialog();
            string[] dump=File.ReadAllLines(openFileDialog1.FileName);
            textBox1.Text = "";
            foreach(string s in dump)
            {
                textBox1.Text += s+"\r\n";
            }
        }

        private void button4_Click(object sender, EventArgs e)//export
        {
           saveFileDialog1.Filter = "Text|*.txt|All|*.*";
            saveFileDialog1.ShowDialog();
            File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
