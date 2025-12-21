using CustomerManagement.Business;
using CustomerManagement.Models;
using System;
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
                try
                {
                    await LoadCustomersAsync();
                }
                catch (Exception ex)
                {
                    HandleError(ex, "Error loading customers on initial page load.");
                }
            }

            // If DB is not available, app runs in demo/read-only mode
            if (!AppState.IsDatabaseAvailable)
            {
                btnAddCustomer.Enabled = false;

                // Hide delete column (last column is Delete template field)
                if (gvCustomers.Columns.Count > 0)
                {
                    gvCustomers.Columns[gvCustomers.Columns.Count - 1].Visible = false;
                }

                lblInfo.Text = "Database is not available. Application is running in read-only demo mode. Add/Edit/Delete are disabled.";
            }
        }

        protected async void Search_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadCustomersAsync();
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error loading customers on search.");
            }
        }

        protected async void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadCustomersAsync();
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error loading customers on status filter change.");
            }
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

            try
            {
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
            catch (Exception ex)
            {
                stopwatch.Stop();
                HandleError(ex, "Error loading customers.");
                lblLoadTime.Text = "An error occurred while loading customers.";
            }
        }

        protected async void gvCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (!AppState.IsDatabaseAvailable)
            {
                // Read-only demo mode; ignore delete
                return;
            }

            try
            {
                int customerId = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);
                await _service.DeleteAsync(customerId);

                CacheHelper.ClearCustomersCache();

                await LoadCustomersAsync();
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error deleting customer.");
            }
        }

        protected void AddCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerAdd.aspx");
        }

        protected async void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCustomers.PageIndex = e.NewPageIndex;
                await LoadCustomersAsync();
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error changing page index for customers grid.");
            }
        }

        private void HandleError(Exception ex, string message)
        {
            Debug.WriteLine($"{message} {ex}");
            HttpContext.Current.Items["LastError"] = ex;
        }
    }
}