using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ApagarApp
{
    public partial class ApagarApp : Form
    {
        public ApagarApp()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            int secondsDifference = getSecondsDifference();

            if (secondsDifference < 0)
                return;

            string arguments = "/s /t " + secondsDifference;
            Process shutdownProcess = Process.Start("shutdown", arguments);
            shutdownProcess.WaitForExit();
            if (shutdownProcess.ExitCode == 1190)
            {
                Process.Start("shutdown", "/a").WaitForExit();
                acceptButton_Click(acceptButton, new EventArgs());
            }

            statusLabel.Text = "Apagado programado para " + dateTimePicker.Text;
            statusLabel.ForeColor = Color.Red;
        }

        private int getSecondsDifference()
        {
            string timeString = dateTimePicker.Text;
            System.DateTime shutdownTime = System.DateTime.Parse(timeString);
            System.DateTime actualTime = System.DateTime.Now;
            TimeSpan timeSpan = shutdownTime - actualTime;

            int secondsDifference = (int)timeSpan.TotalSeconds;

            return secondsDifference;
        }

        private void eraseButton_Click(object sender, EventArgs e)
        {
            Process shutdownProcess = Process.Start("shutdown", "/a");
            shutdownProcess.WaitForExit();

            statusLabel.Text = "No se programó ningún apagado";
            statusLabel.ForeColor = Color.Black;
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            string timeString = dateTimePicker.Text;
            System.DateTime shutdownTime = System.DateTime.Parse(timeString);
            System.DateTime actualTime = System.DateTime.Now;
            TimeSpan timeSpan = shutdownTime - actualTime;
            int secondsDifference = (int)timeSpan.TotalSeconds;

            if (secondsDifference < 0)
            {
                dateTimePicker.Text = System.DateTime.Now.ToString();
            }
        }

        private void ApagarApp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                acceptButton_Click(acceptButton, new EventArgs());
            }
        }
    }
}
