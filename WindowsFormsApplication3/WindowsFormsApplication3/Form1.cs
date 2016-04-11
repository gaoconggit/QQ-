using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebBrowserTest;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        // 计数器
        int counter = 0;
        bool b = true;
        object o = new object();
        //首次登录
        bool flag = true;
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
            if (counter == 0)
            {
                if (flag == true)
                {
                    var doc = this.webBrowser1.Document;
                    var frames = doc.Window.Frames;
                    IHTMLDocument3 baiduDoc = CorssDomainHelper.GetDocumentFromWindow(frames[0].DomWindow as IHTMLWindow2);
                    foreach (IHTMLElement item in baiduDoc.documentElement.all)
                    {
                        if (item.className == "face")
                        {
                            item.click();
                            break;
                        }
                    }
                    flag = false;
                }

            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.comboBox1
            //构造时间
            //(H)00-23 : (M)00-59 : (S)00-59

            for (int i = 0; i <= 23; i++)
            {
                string H = i + "";
                if (i < 10)
                {
                    H = "0" + H;
                }
                this.comboBox1.Items.Add(H);
            }
            for (int j = 0; j <= 59; j++)
            {

                string M = j + "";


                if (j < 10)
                {
                    M = "0" + M;

                }


                this.comboBox2.Items.Add(M);
            }
            this.comboBox1.Text = this.comboBox1.Items[0].ToString();
            this.comboBox2.Text = this.comboBox2.Items[0].ToString();


        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            if (0 == counter)
            {

                // 加载完毕
                string content = this.textBox2.Text;
                //string js = "document.getElementById('tgb').contentWindow.document.getElementById('veditor1_Iframe').contentWindow.document.getElementsByTagName('div')[0].innerHTML='" + content + "';";
                //string action = "document.getElementById('tgb').contentWindow.document.getElementById('btnPostMsg').click();";
                //string refresh = "window.location=window.location;";
                //webBrowser1.Document.InvokeScript("eval", new object[] { js + action + refresh });
                var doc = this.webBrowser1.Document;
                HtmlWindowCollection frames = doc.Window.Frames;
                IHTMLDocument3 baiduDoc = CorssDomainHelper.GetDocumentFromWindow(frames[0].Frames[0].DomWindow as IHTMLWindow2);
                IHTMLElement item = baiduDoc.getElementsByTagName("div").item(0) as IHTMLElement;
                item.innerHTML = content;
                IHTMLDocument3 baiduDoc1 = CorssDomainHelper.GetDocumentFromWindow(frames[0].DomWindow as IHTMLWindow2);
                baiduDoc1.getElementById("btnPostMsg").click();
                b = false;

            }





        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (b == true)
            {
                if (DateTime.Now.ToString("HH:mm") == this.comboBox1.Text + ":" + this.comboBox2.Text)
                {
                    btnStart_Click(null, null);
                }
            }
            else
            {
                if (DateTime.Now.ToString("HH:mm") != this.comboBox1.Text + ":" + this.comboBox2.Text)
                {
                    b = true;
                }

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

            //this.button1.Enabled = true;
            //this.button1.Text = "已停止";
            //this.timer1.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            b = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            b = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            MessageBox.Show("当前留言任务已经停止");
            this.button1.Enabled = true;
            this.button1.Text = "启动";
            webBrowser1.Navigate("");
        }
    }
}
