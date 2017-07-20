using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class User_album : System.Web.UI.Page
{
    Connection co = new Connection();
    SqlDataAdapter da;
    SqlDataReader dr;
    DataSet ds;
    SqlCommand cmd;
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        co.connect();

        string str = "select albummovie from maintable";
        SqlCommand cmd = new SqlCommand(str, co.con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Repeater1.Visible = true;
            Repeater1.DataSource = ds.Tables[0];
            Repeater1.DataBind();

            //Response.Redirect("home.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Data Not Found');", true);
            Repeater1.Visible = false;
        }
    }
}