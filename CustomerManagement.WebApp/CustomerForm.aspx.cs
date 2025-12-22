using CustomerManagement.Business;
using CustomerManagement.Models;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace CustomerManagement.WebApp
{
    public partial class CustomerForm : System.Web.UI.Page
    {
        private readonly CustomerService _service = new CustomerService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(async () =>
                {
                    try
                    {
                        if (int.TryParse(Request.QueryString["id"], out int id))
                        {
                            await LoadCustomerAsync(id);
                        }
                        else
                        {
                            Response.Redirect("Customers.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                            return;
                        }
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Error loading customer data.");
                    }
                    
                }));
            }

            if (!AppState.IsDatabaseAvailable)
            {
                // Disable save in demo/read-only mode
                btnSave.Enabled = false;
            }
        }

        private async Task LoadCustomerAsync(int id)
        {
            try
            {
                var customer = await _service.GetByIdAsync(id);
                if (customer == null)
                {
                    Response.Redirect("Customers.aspx");
                    return;
                }

                txtFirstName.Text = customer.FirstName;
                txtLastName.Text = customer.LastName;
                txtEmail.Text = customer.Email;
                chkActive.Checked = customer.IsActive;
            }
            catch
            {
                // If something goes wrong (e.g., DB error), return to list
                Response.Redirect("Customers.aspx");
            }
        }

        protected async void Save_Click(object sender, EventArgs e)
        {
            if (!AppState.IsDatabaseAvailable)
            {
                // Read-only demo mode
                return;
            }

            if (!int.TryParse(Request.QueryString["id"], out int id))
            {
                Response.Redirect("Customers.aspx");
                return;
            }

            var customer = new Customer
            {
                Id = id,
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                IsActive = chkActive.Checked
            };

            await _service.UpdateAsync(customer);

            CacheHelper.ClearCustomersCache();

            Response.Redirect("Customers.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Customers.aspx");
        }
    }
}