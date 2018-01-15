using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiddleTier;
using Unity;

namespace AuthProj
{
    public partial class Registration : Form
    {
        private IAuthenticationService _AuthenticationService;

        public Registration()
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<IAuthenticationService, AuthenticationService>();
                _AuthenticationService = container.Resolve<IAuthenticationService>();
            }

            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {

                string name = txtName.Text;
                string surname = txtSurname.Text;
                string email = txtEmail.Text;

                string username = txtUsername.Text;
                string password = txtPassword.Text;

                this._AuthenticationService.RegisterNewUser(name, surname, email, username, password, 1);

                lblResult.Text = "User registered successfully";
                lblResult.ForeColor = Color.Green;

            }
            catch(Exception ex)
            {
                lblResult.Text = ex.Message;
                lblResult.ForeColor = Color.Red;
            }
        }
    }
}
