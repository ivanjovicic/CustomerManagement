using CustomerManagement.Business;
using CustomerManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace CustomerManagement.WebApp
{
    public partial class Customers : System.Web.UI.Page
    {
        private CustomerService _service = new CustomerService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadCustomers();
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            bool? status = null;
            if (ddlStatus.SelectedValue == "true") status = true;
            else if (ddlStatus.SelectedValue == "false") status = false;

            string cacheKey = $"customers_{txtSearch.Text}_{status}";
            var customers = Cache[cacheKey] as List<Customer>;

            if (customers == null)
            {
                customers = _service.GetCustomers(txtSearch.Text, status);

                Cache.Insert(
                    cacheKey,
                    customers,
                    null,
                    DateTime.Now.AddMinutes(5),
                    System.Web.Caching.Cache.NoSlidingExpiration);
            }

            gvCustomers.DataSource = _service.GetCustomers(txtSearch.Text, status);
            gvCustomers.DataBind();
        }

        protected void gvCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int customerId = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);
            _service.Delete(customerId);

            string cacheKeyPattern = $"customers_*";
            foreach (DictionaryEntry item in HttpContext.Current.Cache)
            {
                if (item.Key.ToString().StartsWith("customers_"))
                {
                    HttpContext.Current.Cache.Remove(item.Key.ToString());
                }
            }

            LoadCustomers();
        }
        protected void AddCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerAdd.aspx");
        }
    }
}