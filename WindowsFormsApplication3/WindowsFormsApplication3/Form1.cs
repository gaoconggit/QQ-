using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        // 计数器
        int counter = 0;
        bool b = false;
        public Form1()
        {
            InitializeComponent();
            // 添加事件响应函数
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(webBrowser_Navigated);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
        }
        // 事件响应函数
        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            counter++;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            counter--;


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.comboBox1
            //构造时间
            //(H)00-23 : (M)00-59 : (S)00-59

            for (int i = 0; i <= 23; i++)
            {
                for (int j = 0; j <= 59; j++)
                {
                    string H = i + "";
                    string M = j + "";
                    string time = "";
                    if (i < 10)
                    {
                        H = "0" + H;
                    }
                    if (j < 10)
                    {
                        M = "0" + M;

                    }
                    time = H + ":" + M;
                    this.comboBox1.Items.Add(time);
                }
            }
            this.comboBox1.Text = this.comboBox1.Items[0].ToString();


        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            if (0 == counter)
            {
                // 加载完毕
                string content=this.textBox2.Text;
                string js = "document.getElementById('tgb').contentWindow.document.getElementById('veditor1_Iframe').contentWindow.document.getElementsByTagName('div')[0].innerHTML='" + content + "';";
                string action = "document.getElementById('tgb').contentWindow.document.getElementById('btnPostMsg').click();";
                string refresh = "window.location=window.location;";
                webBrowser1.Document.InvokeScript("eval", new object[] { js + action + refresh });

            }


        }



        private void timer1_Tick(object sender, EventArgs e)
        {

            if (DateTime.Now.ToString("HH:mm:ss") == this.comboBox1.Text + ":" + "00")
            {

                btnStart_Click(null, null);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("QQ号必填");
                return;
            }
            webBrowser1.Navigate("http://user.qzone.qq.com/" + this.textBox1.Text + "/334");
            webBrowser1.ScriptErrorsSuppressed = true;
            this.timer1.Enabled = true;
            this.button1.Text = "已启动";
            this.button1.Enabled = false;
        }

    }
}
