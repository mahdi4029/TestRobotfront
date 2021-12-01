using System.Text;

namespace TestRobot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        StringBuilder listtag = new StringBuilder();
        int i = 1;
        private void button1_Click(object sender, EventArgs e)
        {


            StringReader reader = new StringReader(textBox1.Text.ToString());
            StringBuilder readerNew = new StringBuilder();

            string line = "";

            while ((line = reader.ReadLine()) != null)
            {
                string s = line.ToLower();
                if (s.Contains(" href="))
                {
                    replaceGet(ref line, " href=");

                }
                if (s.Contains(" alt="))
                {
                    replaceGet(ref line, " alt=");

                }
                if (s.Contains(" src="))
                {
                    replaceGet(ref line, " src=");

                }
                readerNew.Append(line);
                
            }

            string result=readerNew.ToString();

            GetStaticWords(ref result);

            textBox1.Text = result+"\n\n";
            textBox1.Text += listtag;


        }

        private void replaceGet(ref string line,string _if)
        {
            var startIndex = line.IndexOf(_if);
            int lastindex = 0;
            bool isfirst = true;
            for (int j = startIndex; j < line.Length; j++)
            {
                char c = line[j];
                if (c == '"')
                {
                    if (isfirst)
                    {
                        isfirst = false;
                    }
                    else
                    {
                        lastindex = j;
                        break;
                    }
                }
            }

            var charArry = line.ToCharArray();
            string newHref = $"{_if}\"#tag{i}";
            i++;
            listtag.Append($"#tag{i}");

            line = line.Remove(startIndex, (lastindex - startIndex)).Insert(startIndex, newHref);

        }

        private void GetStaticWords(ref string lines)
        {
            var isfreazes = true;
            int startIndex=0;
            int lastIndex = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals('>') && isfreazes)
                {
                    isfreazes = false;
                    startIndex = i;

                }
                else if(!isfreazes)
                {
                    if (lines[i].Equals('<'))
                    {
                        isfreazes = true;
                        lastIndex = i;

                        var word=lines.Substring(startIndex,(lastIndex - startIndex));
                        if (word.Trim() != "")
                        {

                            word = "%#" + word.Trim();
                            lines = lines.Remove(startIndex, (lastIndex - startIndex)).Insert(startIndex, word);
                            listtag.Append(word);
                        }
                            
                        startIndex = 0;
                        lastIndex = 0;
                    }
                }


            }
        }
    }
}