using CustomerManagement.Business;
using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerManagement.WebApp
{
    public partial class CustomerForm : System.Web.UI.Page
    {
        private CustomerService _service = new CustomerService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["id"] != null)
            {
                int id = int.Parse(Request.QueryString["id"]);
                var customer = _service.GetById(id);

                txtFirstName.Text = customer.FirstName;
                txtLastName.Text = customer.LastName;
                txtEmail.Text = customer.Email;
                chkActive.Checked = customer.IsActive;
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            var customer = new Customer
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                IsActive = chkActive.Checked
            };

            if (Request.QueryString["id"] != null)
            {
                customer.Id = int.Parse(Request.QueryString["id"]);
                _service.Update(customer);
            }
            else
            {
                _service.Add(customer);
            }

            Response.Redirect("Customers.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Customers.aspx");
        }
    }
}