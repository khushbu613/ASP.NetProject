using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Cosmetics_Project
{
    public partial class Credit_Debit : System.Web.UI.Page
    {
        Payment p = new Payment();
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Reg;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            Label7.Text = Request.QueryString["a"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
             con.Open();
            String query = "insert into CardDetails(Cardtype,cardnumber,nameoncard,Validtill,Cvv)values('" + DropDownList2.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "')";


           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            DropDownList2.Text = "";
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            con.Close();
            Response.Redirect("Placedsuccessfully.aspx?Orderid=" + Label7.Text);
        }
    }
}