using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_showsongList : System.Web.UI.Page
{
    public SqlConnection con;
    public SqlCommand cmd;
    
    SqlDataAdapter da;
    DataSet ds;
    Class1 obj = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            obj.connect();
            string cate = "Select language from category";
            SqlCommand cmd = new SqlCommand(cate, obj.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "language";
            DropDownList1.DataBind();
         
            string scate = "Select songtype from sub_category";
            SqlCommand cmd2 = new SqlCommand(scate, obj.con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            DropDownList2.DataSource = dt2;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "songtype";
            DropDownList2.DataBind();           
            GridViewshowdata();
        }
    }
    protected void GV_rowediting(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        GridViewshowdata();
    }


    public void GV_rowcancel(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridViewshowdata();
    }

    public void gvch(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridViewshowdata();
    }

    public void GV_del(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        int row = obj.delete_data(id);
        GridViewshowdata();

    }

    protected void GridViewshowdata()
    {

        ds = obj.show(2);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert ('Data Not Found');", true);
            GridView1.Visible = false;
        }

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Row = (GridViewRow)GridView1.Rows[e.RowIndex];
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        TextBox t1 = (TextBox)Row.FindControl("TextBoxtittle");
        TextBox t2 = (TextBox)Row.FindControl("TextBoxfile");
        TextBox t3 = (TextBox)Row.FindControl("TextBoxcat");
        TextBox t4 = (TextBox)Row.FindControl("TextBoxsubcat");
        TextBox t5 = (TextBox)Row.FindControl("TextBoxmovie");
        TextBox t6 = (TextBox)Row.FindControl("TextBoxsing");
        TextBox t7 = (TextBox)Row.FindControl("TextBoxyr");
        TextBox t8 = (TextBox)Row.FindControl("TextBoxcomp");
        TextBox t9 = (TextBox)Row.FindControl("TextBoxdir");
        TextBox t10 = (TextBox)Row.FindControl("TextBoxdurat");
        TextBox t11 = (TextBox)Row.FindControl("TextBoxbit");
        TextBox t12 = (TextBox)Row.FindControl("TextBoxgenr");
        TextBox t13 = (TextBox)Row.FindControl("TextBoxdesc");
        int r = obj.update_data(id,t1.Text,t2.Text,t3.Text,t4.Text,t5.Text,t6.Text,t7.Text,t8.Text,t9.Text,t10.Text,t11.Text,t12.Text,t13.Text,4);
        if (r > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Updated');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Not Updated');", true);
        }
        GridView1.EditIndex = -1;
        GridViewshowdata();
    }
    
    protected void Btnsearch_Click1(object sender, EventArgs e)
    {
        obj.connect();

        string stt = "Select * from maintable where title like '%" + Txtsearch.Text + "%' OR sub_category like '%" + Txtsearch.Text + "%' OR category like '%" + Txtsearch.Text + "%' OR albummovie like '%" + Txtsearch.Text + "%' OR singer like '%" + Txtsearch.Text + "%'";
        SqlCommand cmd = new SqlCommand(stt, obj.con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Data Not Found');", true);
            GridView1.Visible = false;
        }

    }


   
}