using CustomerManagement.Business;
using CustomerManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace CustomerManagement.WebApp
{
    public partial class Customers : System.Web.UI.Page
    {
        private readonly CustomerService _service = new CustomerService();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadCustomersAsync();
            }
        }

        protected async void Search_Click(object sender, EventArgs e)
        {
            await LoadCustomersAsync();
        }

        protected async void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
           await LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            bool? status = null;

            if (ddlStatus.SelectedValue == "true")
                status = true;
            else if (ddlStatus.SelectedValue == "false")
                status = false;

            string search = txtSearch.Text.Trim().ToLower();
            string cacheKey = $"customers_{search}_{status}";

            var customers = Cache[cacheKey] as List<Customer>;

            if (customers == null)
            {
                customers = await _service.GetCustomersAsync(search, status);

                Cache.Insert(
                    cacheKey,
                    customers,
                    null,
                    DateTime.Now.AddMinutes(5),
                    System.Web.Caching.Cache.NoSlidingExpiration);
            }

            gvCustomers.DataSource = customers;
            gvCustomers.DataBind();

            stopwatch.Stop();

            lblLoadTime.Text =
                $"Loaded {customers.Count} customers in {stopwatch.Elapsed.TotalMilliseconds} ms";
        }

        protected async void gvCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int customerId = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);
            await _service.DeleteAsync(customerId);

           
            foreach (DictionaryEntry item in HttpContext.Current.Cache)
            {
                if (item.Key.ToString().StartsWith("customers_"))
                {
                    HttpContext.Current.Cache.Remove(item.Key.ToString());
                }
            }

           await LoadCustomersAsync();
        }

        protected void AddCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerAdd.aspx");
        }

        protected async void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           gvCustomers.PageIndex = e.NewPageIndex;
           await LoadCustomersAsync();
        }

    }

}