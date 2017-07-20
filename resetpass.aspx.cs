using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class User_resetpass : System.Web.UI.Page
{
    Connection co = new Connection();
    SqlCommand cmd;
    SqlConnection con;
    SqlDataReader dr;
    DataSet ds;
    SqlDataAdapter da;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btnresetpass_Click(object sender, EventArgs e)
    {

        co.connect();
        string oldpass;
        int row;
        oldpass = TxtOldpass.Text.Trim();
        string sql = "Select password from  user_regis where password='" + oldpass + "'";
        cmd = new SqlCommand(sql, co.con);
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                TxtOldpass.Text = dr[0].ToString();
            }
            if (oldpass == TxtOldpass.Text)
            {
                string newpass;
                cmd.Dispose();
                dr.Close();
                newpass = TxtNewpass.Text.Trim();
                sql = "update user_regis set password='" + newpass + "' where password='" + oldpass + "'";
                cmd = new SqlCommand(sql, co.con);
                row = cmd.ExecuteNonQuery();

                if (row > 0)
                {
                    Response.Redirect("DefaultM.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Password Not Updated');", true);
                }


            }
        }
    }
}