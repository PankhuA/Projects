using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_UploadSongs : System.Web.UI.Page
{
    string str;
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
            string sngcat = "Select language from category";
            SqlCommand cmd = new SqlCommand(sngcat, obj.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DropDownCateg.DataSource = dt;
            DropDownCateg.DataBind();
            DropDownCateg.DataTextField = "language";
            DropDownCateg.DataBind();

            string scate = "Select songtype from sub_category";
            SqlCommand cmd2 = new SqlCommand(scate, obj.con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            DropDSongtype.DataSource = dt2;
            DropDSongtype.DataBind();
            DropDSongtype.DataTextField = "songtype";
            DropDSongtype.DataBind();
        }
    }
    

    protected void Btninsert_Click(object sender, EventArgs e)
    {
        obj.connect();
        string Titl, FName, CatGry, SubCat ,  AlbuMusic, Singr,Yar, Composr,Size, Formt, Directrr, Duration, BitRte,Genre, Desc;

         Titl = Txttitle.Text.Trim();
         FName = "~/music/"+FileUpload1.FileName.Trim();

         string sql = "Select id from maintable where title='" + Titl + "' and filename='" + FName + "'";
         cmd = new SqlCommand(sql, obj.con);
         SqlDataReader dr;
         dr = cmd.ExecuteReader();
         if (dr.HasRows)
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert ('This song is Already stored in database');", true);
         }
         else
         {
             dr.Close();
             Titl = Txttitle.Text.ToString();
             FName = FileUpload1.FileName;
             string img ="~/image/"+ FileUpload2.FileName;
             int a = FileUpload1.PostedFile.ContentLength;
             float AB = (float)a / (1024 * 1024);
             int li = FName.LastIndexOf(".");
             string fo = FName.Substring(li + 1);
             string newfilename = "~/music/" + FName;


             CatGry = DropDownCateg.Text.ToString();
             SubCat = DropDSongtype.Text.ToString();
             AlbuMusic = Txtmovie.Text.ToString();
             Singr = Txtsing.Text.ToString();
             Yar = Txtyr.Text.ToString();
             Composr = Txtcompos.Text.ToString();
             Size = AB.ToString() + "mb";
             Formt = fo.ToString();
             Directrr = TxtDirec.Text.ToString();
             Duration = Txtdur.Text.ToString();
             BitRte = Txtbit.Text.ToString();
             Genre = Txtgen.Text.ToString();
             Desc = Txtdes.Text.ToString();
             int p = obj.insert_data(Titl, newfilename, CatGry, SubCat , AlbuMusic, Singr, Yar, Composr, Size, Formt, Directrr, Duration, BitRte, Genre, Desc, 1,img);
             if (p > 0)
             {
                 FileUpload1.SaveAs(Server.MapPath("~/Music/") + FName);
                 FileUpload2.SaveAs(Server.MapPath("~/image/") +FileUpload2.FileName);
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Insert successfully');", true);
                 Txttitle.Text = "";
                 Txtmovie.Text = "";
                 Txtsing.Text = "";
                 Txtyr.Text = "";
                 Txtcompos.Text = "";
                 TxtDirec.Text = "";
                 Txtdur.Text = "";
                 Txtbit.Text = "";
                 Txtgen.Text = "";
                 Txtdes.Text = "";
             }
             else
             {
                 //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Not Insert');", true);
                 Response.Redirect("UploadSongs.aspx");
             }


         }
    }

    protected void Btncancel_Click(object sender, EventArgs e)
    {
        Txttitle.Text = "";
        Txtmovie.Text = "";
        Txtsing.Text = "";
        Txtyr.Text = "";
        Txtcompos.Text = "";
        TxtDirec.Text = "";
        Txtdur.Text = "";
        Txtbit.Text = "";
        Txtgen.Text = "";
        Txtdes.Text = "";


    }
}