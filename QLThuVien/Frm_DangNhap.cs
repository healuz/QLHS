using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLThuVien
{
    public partial class Frm_DangNhap : Form
    {
        public Frm_DangNhap()
        {
            InitializeComponent();
        }

        private void Frm_DangNhap_Load(object sender, EventArgs e)
        {
            txtServerName.Text = "lolocalhost";
            cbbAuthentication.SelectedIndex = 0;
        }

        private void cbbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hide = true;
            if (cbbAuthentication.SelectedIndex == 0)
                hide = false;
            txtUserName.Enabled = hide;
            txtPassword.Enabled = hide;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(cbbAuthentication.SelectedIndex==0)
                
        }
    }
}
