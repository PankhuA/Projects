using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_adminLogin : System.Web.UI.Page
{
    string sql, str;
    SqlCommand cmd;
    SqlConnection con;
    SqlDataReader dr;
    string myname = ""; string mypass = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        str = "Data Source=DESKTOP-67HA26C\\SQLEXPRESS;Initial Catalog=musicproject;Integrated Security=True";
        con = new SqlConnection(str);
        con.Open();
    }

    protected void Button_login_Click(object sender, EventArgs e)
    {
        string name, password;
        name = Txtname.Text.ToString();
        password = Txtpass.Text.ToString();

        //to database....
        sql = "Select admin_name from adminLog where  admin_name='" + name + "' and admin_pass='" + password + "'";
        cmd = new SqlCommand(sql, con);
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                myname = dr[0].ToString();

                Session["Admin"] = dr[0].ToString();
                Response.Redirect("DefaultAdmin.aspx");
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please Enter Correct details');", true);
        }

    }
}