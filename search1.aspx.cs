using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class User_search1 : System.Web.UI.Page
{
    Connection co = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        bind();
    }
    private void bind()
    {
        co.connect();
        string value = Session["search"].ToString();
        string str = "select * from maintable where title like '%" + value + "%' OR category like '%" + value + "%' OR albummovie like '%" + value + "%' OR singer like '%" + value + "%' ";
        SqlCommand cmd = new SqlCommand(str, co.con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataListSearch.Visible = true;
            DataListSearch.DataSource = ds.Tables[0];
            DataListSearch.DataBind();

            //Response.Redirect("home.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Data Not Found');", true);
            DataListSearch.Visible = false;
        }

    }
}