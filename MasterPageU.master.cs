using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class User_MasterPageU : System.Web.UI.MasterPage
{
    Connection co = new Connection();
    SqlCommand cmd;
    SqlConnection con;
    SqlDataReader dr;
    DataSet ds;
    SqlDataAdapter da;

    protected void Page_Load(object sender, EventArgs e)
    {
        bind();
        bind2();
        music();
        latest();
        bind4();
        upcome();
    }
    private void music()
    {
        
        co.connect();
        //  string id = Request.QueryString["id"].ToString();
        string stt = "select top 1 REPLACE(filename, '~', '..') as filename from maintable order by newid()";
        SqlCommand cmd = new SqlCommand(stt, co.con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // GridView1.Visible = true;
            Repeater2.DataSource = ds.Tables[0];
            Repeater2.DataBind();
        }


    }
    private void bind()
    {
        co.connect();
        string sql = "select top 5 title,albummovie,img,id from maintable order by newid()";
        da = new SqlDataAdapter(sql, co.con);
        ds = new DataSet();
        da.Fill(ds);
        Repeater1.DataSource = ds.Tables[0].DefaultView;
        Repeater1.DataBind();
    }

    private void latest()
    {
        co.connect();
        string sql = "Select top 10 title,id from maintable order by newid()";
        da = new SqlDataAdapter(sql, co.con);
        ds = new DataSet();
        da.Fill(ds);
        Repeater3.DataSource = ds.Tables[0].DefaultView;
        Repeater3.DataBind();

    }

    private void upcome()
    {
        co.connect();
        string sql = "Select top 5 MovieName,id from Newmovie order by newid()";
        da = new SqlDataAdapter(sql, co.con);
        ds = new DataSet();
        da.Fill(ds);
        Repeater5.DataSource = ds.Tables[0].DefaultView;
        Repeater5.DataBind();

    }

   
    private void bind4()
    {
        co.connect();
        string sql = "select top 7 title,albummovie,img,id,singer from maintable order by newid()";
        da = new SqlDataAdapter(sql, co.con);
        ds = new DataSet();
        da.Fill(ds);
        Repeater4.DataSource = ds.Tables[0].DefaultView;
        Repeater4.DataBind();
    }
  
    private void bind2()
    {
        co.connect();
        string sql = "select top 12 id,img from maintable order by id desc";
        da = new SqlDataAdapter(sql, co.con);
        ds = new DataSet();
        da.Fill(ds);
        DataList1.DataSource = ds.Tables[0].DefaultView;
        DataList1.DataBind();
    }


    protected void BtnCAcc_Click(object sender, EventArgs e)
    {
        co.connect();
        string na, email, pass;
        string mobileno;
        na = Txtname.Text.ToString();
        mobileno = Txtmobile.Text;
        email = Txtemail.Text.ToString();
        pass = Txtpassword.Text.ToString();

        string sql = "Insert into user_regis(name,mobileno,emailid,password) values('" + na + "','" + mobileno + "','" + email + "','" + pass + "')";
        cmd = new SqlCommand(sql, co.con);
        int row = cmd.ExecuteNonQuery();
        if (row > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Registration Successful');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Not Registrated');", true);
        }
    }
    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        co.connect();
        string email, pass;
        //string myname;
        email = TxtEmailLog.Text.ToString();
        pass = TxtPasslog.Text.ToString();
        string sql = "Select name from user_regis where emailid='" + email + "' and password='" + pass + "' ";
        cmd = new SqlCommand(sql, co.con);
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                //myname = dr[0].ToString();
                Session["username"] = dr[0].ToString();
                Response.Redirect("home.aspx");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please Enter Correct details');", true);
        }
    }
    //protected void Btnsearch_Click(object sender, EventArgs e)
    //{
    //    co.connect();
    //    string str = "select * from maintable where title like '%" + search.Text + "%' OR category like '%" + search.Text + "%' OR albummovie like '%" + search.Text + "%' OR singer like '%" + search.Text + "%' ";
    //    SqlCommand cmd = new SqlCommand(str, co.con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        Response.Redirect("artist.aspx");
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Data Not Found');", true);
    //    }
    //}
    protected void LinkBtnforgot_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForgotPass.aspx");
    }
    protected void Btnsearch_Click(object sender, EventArgs e)
    {

        Session["search"] = Txtsearch.Text;
        Response.Redirect("search1.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("browse.aspx");
    }
}