using RabbitMQClass_deneme;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace RabbitMQ_deneme1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Producer.getMessage(textBox5.Text, textBox4.Text, textBox1.Text, textBox2.Text, textBox3.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox4.UseSystemPasswordChar = false;
            pictureBox4.Visible = true;
            pictureBox3.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox4.UseSystemPasswordChar = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Consumer.main();
        }
    }
}