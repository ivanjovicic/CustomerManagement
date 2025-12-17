using CustomerManagement.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerManagement.Web
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

        private void LoadCustomers()
        {
            bool? status = string.IsNullOrEmpty(ddlStatus.SelectedValue)
                ? (bool?)null
                : bool.Parse(ddlStatus.SelectedValue);

            gvCustomers.DataSource = _service.GetCustomers(txtSearch.Text, status);
            gvCustomers.DataBind();
        }
    }