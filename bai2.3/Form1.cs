using System;
using System.Windows.Forms;

namespace bai2._3
{
    public partial class Form1 : Form
    {
        List<Button> danhsachchon = new List<Button>();
        public Form1()
        {
            InitializeComponent();
        }
        int thanhtien = 0;
        private void btnChon_Click(object sender, EventArgs e)
        {
            txtThanhTien.Text = "";
            Button btn = (Button)sender;
            if (btn.BackColor != Color.Red)
            {
                if (btn.BackColor == Color.White)
                {
                    btn.BackColor = Color.Pink;
                    danhsachchon.Add(btn);
                }
                else
                {
                    btn.BackColor = Color.White;
                    danhsachchon.Remove(btn);
                }
            }
            else
            {
                MessageBox.Show("Ghế đã được chọn");
            }
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChon2_Click(object sender, EventArgs e)
        {

            foreach (Button b in danhsachchon)
            {
                int a = int.Parse(b.Text);
                if (a <= 5)
                {
                    b.BackColor = Color.Red;
                    thanhtien += 30000;
                }
                if (a > 5 && a <= 10)
                {
                    b.BackColor = Color.Red;
                    thanhtien += 40000;
                }
                if (a > 10 && a <= 15)
                {
                    b.BackColor = Color.Red;
                    thanhtien += 50000;
                }
                if (a > 15 && a <= 20)
                {
                    b.BackColor = Color.Red;
                    thanhtien += 80000;
                }
            }
            txtThanhTien.Text = thanhtien.ToString() + "VND";
            thanhtien = 0;
            danhsachchon = new List<Button>();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            for (int i = danhsachchon.Count - 1; i >= 0; i--)
            {
                Button b = danhsachchon[i];
                b.BackColor = Color.White;
                danhsachchon.RemoveAt(i);
            }
            txtThanhTien.Text = "";
            danhsachchon = new List<Button>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
