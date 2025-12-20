using CustomerManagement.Business;
using CustomerManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerManagement.WebApp
{
    public partial class CustomerForm : System.Web.UI.Page
    {
        private CustomerService _service = new CustomerService();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["id"] != null)
            {
                int id = int.Parse(Request.QueryString["id"]);
                var customer = await _service.GetByIdAsync(id);

                txtFirstName.Text = customer.FirstName;
                txtLastName.Text = customer.LastName;
                txtEmail.Text = customer.Email;
                chkActive.Checked = customer.IsActive;
            }
        }

        protected async void Save_Click(object sender, EventArgs e)
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
               await _service.UpdateAsync(customer);
            }
            else
            {
                await _service.AddAsync(customer);
            }

            foreach (DictionaryEntry item in HttpContext.Current.Cache)
            {
                if (item.Key.ToString().StartsWith("customers_"))
                {
                    HttpContext.Current.Cache.Remove(item.Key.ToString());
                }
            }

            Response.Redirect("Customers.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Customers.aspx");
        }
    }
}