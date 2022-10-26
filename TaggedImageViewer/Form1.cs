using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaggedImageViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // 検索ボタン クリック
        private void button1_Click(object sender, EventArgs e)
        {
            var text = SearchText.Text;

        }

        // テキストボックス 変更
        private void SearchText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
