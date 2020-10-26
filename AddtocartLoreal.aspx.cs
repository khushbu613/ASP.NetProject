using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Cosmetics_Project
{
    public partial class AddtocartLoreal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable dt=new DataTable();
                DataRow dr;
                dt.Columns.Add("sno");
                dt.Columns.Add("Product_id");
                dt.Columns.Add("Product_Name");
                dt.Columns.Add("quantity");
                dt.Columns.Add("Price");
                dt.Columns.Add("totalprice");
                dt.Columns.Add("Product_Image");
                if(Request.QueryString["id"]!=null)
                {
                   if(Session["Buyitems"]==null)
                   {
                       dr=dt.NewRow();
                       String mycon="Data Source=.;Initial Catalog=Reg;Integrated Security=True";
                       SqlConnection scon=new SqlConnection(mycon);
                       String myquery ="select * from Loreal2 where Product_id="+Request.QueryString["id"];
                       SqlCommand cmd=new SqlCommand();
                       cmd.CommandText=myquery;
                       cmd.Connection=scon;
                       SqlDataAdapter da=new SqlDataAdapter();
                       da.SelectCommand=cmd;
                       DataSet ds1=new DataSet();
                       da.Fill(ds1);
                       dr["sno"] = 1;
                       dr["Product_id"]=ds1.Tables[0].Rows[0]["Product_id"].ToString();
                       dr["Product_Name"]=ds1.Tables[0].Rows[0]["Product_Name"].ToString();
                       dr["Product_Image"] = ds1.Tables[0].Rows[0]["Product_Image"].ToString();
                       dr["quantity"]=Request.QueryString["quantity"];
                       dr["Price"]=ds1.Tables[0].Rows[0]["Price"].ToString();
                       int price =Convert.ToInt16(ds1.Tables[0].Rows[0]["Price"].ToString());
                       int quantity=Convert.ToInt16(Request.QueryString["quantity"].ToString());
                       int totalprice = price * quantity;
                       dr["totalprice"]=totalprice;
                       dt.Rows.Add(dr);
                       GridView1.DataSource=dt;
                       GridView1.DataBind();
                       Session["buyitems"]=dt;
                       GridView1.FooterRow.Cells[5].Text="Total Amount";
                       GridView1.FooterRow.Cells[6].Text=grandtotal().ToString();
                       Response.Redirect("AddtocartLoreal.aspx");
                   }
                   else
                   {

                       dt = (DataTable)Session["buyitems"];
                       int sr;
                       sr = dt.Rows.Count;

                       dr = dt.NewRow();
                       String mycon = " Data Source=.;Initial Catalog=Reg;Integrated Security=True";
                       SqlConnection scon = new SqlConnection(mycon);
                       String myquery = "select * from Loreal2  where Product_id=" + Request.QueryString["id"];
                       SqlCommand cmd = new SqlCommand();
                       cmd.CommandText = myquery;
                       cmd.Connection = scon;
                       SqlDataAdapter da = new SqlDataAdapter();
                       da.SelectCommand = cmd;
                       DataSet ds1 = new DataSet();
                       da.Fill(ds1);
                       dr["sno"] = sr + 1;
                       dr["Product_id"] = ds1.Tables[0].Rows[0]["Product_id"].ToString();
                       dr["Product_Name"] = ds1.Tables[0].Rows[0]["Product_Name"].ToString();
                       dr["Product_Image"] = ds1.Tables[0].Rows[0]["Product_Image"].ToString();
                       dr["quantity"] = Request.QueryString["quantity"];
                       dr["Price"] = ds1.Tables[0].Rows[0]["Price"].ToString();
                       int price = Convert.ToInt16(ds1.Tables[0].Rows[0]["Price"].ToString());
                       int quantity = Convert.ToInt16(Request.QueryString["quantity"].ToString());
                       int totalprice = price * quantity;
                       dr["totalprice"] = totalprice;
                       dt.Rows.Add(dr);
                       GridView1.DataSource = dt;
                       GridView1.DataBind();

                       Session["buyitems"] = dt;
                       GridView1.FooterRow.Cells[5].Text = "Total Amount";
                       GridView1.FooterRow.Cells[6].Text = grandtotal().ToString();
                       Response.Redirect("AddtocartLoreal.aspx");

                   }
                }
                else
                {
                    dt = (DataTable)Session["buyitems"];
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    if (GridView1.Rows.Count > 0)
                    {
                        GridView1.FooterRow.Cells[5].Text = "Total Amount";
                        GridView1.FooterRow.Cells[6].Text = grandtotal().ToString();

                    }


                }
                

            }
        }
        public int grandtotal()
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["buyitems"];
        int nrow = dt.Rows.Count;
        int i = 0;
        int gtotal = 0;
        while(i<nrow)
        {
            gtotal =gtotal+ Convert.ToInt32(dt.Rows[i]["totalprice"].ToString());

            i = i + 1;
        }
        return gtotal;
    }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("EditOrder.aspx?sno=" + GridView1.SelectedRow.Cells[0].Text);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlaceOrder.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
              DataTable dt = new DataTable();
        dt = (DataTable)Session["buyitems"];


        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            int sr;
            int sr1;
            string qdata;
            string dtdata;
            sr = Convert.ToInt32(dt.Rows[i]["sno"].ToString());
            TableCell cell = GridView1.Rows[e.RowIndex].Cells[0];
            qdata = cell.Text ;
            dtdata = sr.ToString();
            sr1 = Convert.ToInt32(qdata);

            if (sr == sr1)
            {
                dt.Rows[i].Delete();
                dt.AcceptChanges();
                //Label1.Text = "Item Has Been Deleted From Shopping Cart";
                break;

            }
        }

        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            dt.Rows[i - 1]["sno"] = i;
            dt.AcceptChanges();
        }

        Session["buyitems"] = dt;
        Response.Redirect("AddtocartLoreal.aspx");
    }
    
        
    }
}