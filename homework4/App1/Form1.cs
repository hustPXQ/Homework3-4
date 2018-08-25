using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace App1
{

    public partial class Caculator : Form
    {
        
        public Caculator()
        {
            InitializeComponent();
        }

        int tab = 0;
        //记录指针
        int Precord = 0;
        //记录数
        int RecordNum = 0;
        public String text = "";
        //存储记录
        public String[] record = new String[10];
        //存储表达式
        public String[] texts = new String[10000];
        List<string> expressions = new List<string>();

        //处理表达式
        public void addComments(String s)
        {
            this.text += s;
            this.texts[tab] = s;

            this.richTextBox1.Text = this.text;
            tab++;
        }
        
        //按钮的触发事件
        private void button_number_Click(object sender, EventArgs e)
        {
            this.addComments( (sender as Button ).Text ); 
        }

        /// =按钮的触发事件,最终计算结果并显示
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {   //计算字符串表达式
                DataTable dt = new DataTable();
                string result = dt.Compute(this.text, "false").ToString();

                this.richTextBox1.Text = result;
                this.record[RecordNum] = this.text;

                expressions.Add(this.text + "=" + result);
                FileStream resultfile = new FileStream("result.txt", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(resultfile);
                foreach(string a in expressions)
                {
                    streamWriter.WriteLine(a);
                 }
                streamWriter.Close();

                this.text = result;

                this.RecordNum++;

                this.Precord = this.RecordNum;
                
            }
            catch (Exception)
            {
                this.richTextBox1.Text = "输入错误！";
                this.text = "";
                tab = 0;
            }

        }

        //清屏按钮的触发事件
        private void button20_Click(object sender, EventArgs e)
        {
            this.text = "";
            this.richTextBox1.Text = this.text;
        }

        //退格按钮的触发事件
        private void button19_Click(object sender, EventArgs e)
        {
            if (tab > 0)
            {
                tab -= 1;
            }
            this.text = "";
            for (int i = 0; i < tab; i++)
            {
                this.text += this.texts[i];
            }

            this.richTextBox1.Text = this.text;
        }

        //<-按钮的触发事件
        private void button21_Click(object sender, EventArgs e)
        {
            Precord--;
            if (Precord < 0)
            {
                Precord = RecordNum;
            }
            // this.text = this.record[Precord];
            this.richTextBox1.Text = this.record[Precord];
        }

        //->按钮的触发事件
        private void button22_Click(object sender, EventArgs e)
        {
            Precord++;
            if (Precord > RecordNum)
            {
                Precord = 0;
            }

            this.richTextBox1.Text = this.record[Precord];

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void 计算器_Load(object sender, EventArgs e)
        {

        }
    }
}
