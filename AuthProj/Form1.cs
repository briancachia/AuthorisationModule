using MiddleTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace AuthProj
{
    public partial class Form1 : Form
    {
        private readonly IAuthenticationService _AuthenticationService;

        public Form1()
        {
            using (UnityContainer container = new UnityContainer())
            {
                container.RegisterType<IAuthenticationService, AuthenticationService>();
                _AuthenticationService = container.Resolve<IAuthenticationService>();
            }

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Login Form";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._AuthenticationService.Login(txtUsername.Text, txtPassword.Text))
                {
                    lblResult.Text = "Login Successfull";
                }
                else
                {
                    lblResult.Text = "Login not successfull";
                }
            }
            catch(Exception ex)
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = ex.Message;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            txtPassword.Text = "";
            txtUsername.Text = "";
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Registration regForm = new Registration();
            regForm.Show();
        }
    }
}
