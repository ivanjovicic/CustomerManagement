using CustomerManagement.Business;
using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CustomerManagement.WebApp
{
    public partial class CustomerAdd : System.Web.UI.Page
    {
        private CustomerService _service = new CustomerService();
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            var customer = new Customer
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                IsActive = bool.Parse(ddlStatus.SelectedValue)
            };

            _service.Add(customer);

            Response.Redirect("Customers.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Customers.aspx");
        }
    }
}