using CustomerManagement.Business;
using CustomerManagement.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CustomerManagement.WebApp
{
    public partial class CustomerAdd : System.Web.UI.Page
    {
        private CustomerService _service = new CustomerService();
        protected async void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppState.IsDatabaseAvailable)
            {
                // Read-only demo mode; block saving
                return;
            }

            if (!Page.IsValid)
                return;

            var customer = new Customer
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                IsActive = bool.Parse(ddlStatus.SelectedValue)
            };

            try
            {
                await _service.AddAsync(customer);

                CacheHelper.ClearCustomersCache();

                Response.Redirect("Customers.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (InvalidOperationException ex)
            {
                lblError.Text = ex.Message;
            }
            catch (Exception)
            {
                lblError.Text = "An error occurred while saving the customer.";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Customers.aspx");
        }

        protected void ValidateFirstLetterUppercase(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Value))
            {
                args.IsValid = true;
                return;
            }

            char firstChar = args.Value.Trim()[0];
            args.IsValid = char.IsUpper(firstChar);
        }
    }
}