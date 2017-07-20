using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class User_play : System.Web.UI.Page
{
    Connection co = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        bind();
        rel();

    }
    private void bind()
    {
        co.connect();
        co.connect();
        string id = Request.QueryString["id"].ToString();
        string stt = "select REPLACE(filename, '~', '..'),albummovie,singer,category,sub_category,img from maintable where id=" + id + "";
        SqlCommand cmd = new SqlCommand(stt, co.con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                audio_player.Attributes["src"] = dr[0].ToString();
                mov.Text = dr[1].ToString();
                //sing.Text = dr[2].ToString();
                cat.Text = dr[3].ToString();
                sub.Text = dr[4].ToString();
                Image2.ImageUrl = dr[5].ToString();


            }

        }

    }
    private void rel()
    {
        co.connect();
        string id = Request.QueryString["id"].ToString();
      
        string sql = "select top 6 id,img,title from maintable where albummovie='" + mov.Text + "' or singer='" + sing.Text + "' or category='" + cat.Text + "' or sub_category='"+sub.Text+"' and id!="+id+" ";
      SqlDataAdapter  da = new SqlDataAdapter(sql, co.con);
     DataSet   ds = new DataSet();
        da.Fill(ds);
        DataList1.DataSource = ds.Tables[0].DefaultView;
        DataList1.DataBind();
    }

}