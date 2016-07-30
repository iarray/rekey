using Hook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace getKeyName
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var hook = new KeyBoardHook();
            hook.KeyDown += hook_KeyDown;
        }

        bool hook_KeyDown(object sender, KeyBoardHookEventArgs e)
        {
            textBox1.Text = e.Key.ToString();
            return false;
        }
    }
}
