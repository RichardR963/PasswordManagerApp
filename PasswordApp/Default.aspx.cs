using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PasswordApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        private void LoadFile()
        {
            // read file contents
            // decrypt file content
            // put that data into passwordGV

        }

        protected void passwordGV_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void passwordGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void passwordGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void passwordGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}