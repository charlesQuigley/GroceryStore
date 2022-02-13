using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


/*
    Charles Quigley, Thomas Gonzalez
    COSC 3351
    Due: 4/23/2021
*/


namespace ShoppersExpress
{
    public partial class ProductDescriptionPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["ProductID"] == null)
            {
                //If ProductID Session object does not exist, that means the user tried to
                //enter this page's URL into the URL bar. So, the user didn't click on a product's
                //image first. Thus, since their is no product to display in this situation, go back
                //to the default page. 
                Response.Redirect("Default.aspx", true);
                //^Response.Redirect set to true so that this page's code stops executing and
                //we immediately transfer to Default.aspx. If Response.Redirect was set to false,
                //that means this Page_Load() function would keep going, which results in an error 
                //for the very next line of code 
                // (because Session["ProductID"] does not exist yet, which means that it cannot be assigned to productID).
            }

            string productID = Session["ProductID"].ToString();


            //Get our connection string from our web.config file.
            string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
            
            //Create a new SQL connection object for the database specified by our connection string.
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

            //Open connection.
            connection.Open();

            //Crete SQL query
            System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;

            //get the product's image from the database
            command.CommandText = "SELECT PRODUCT_IMAGE FROM [Product] WHERE [PRODUCT_ID] =" + productID;
            command.ExecuteNonQuery();

            //This stuff was described in the code behind for Default.aspx
            System.Data.DataTable table = new System.Data.DataTable();
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(command);
            adapter.Fill(table);
            ProductPageView_Image.DataSource = table;
            
            ProductPageView_Image.DataBind(); //Bind the queried data to the DataList



            //get the product's name, description, and price from the database
            command.CommandText = "SELECT PRODUCT_NAME, PRODUCT_DESCRIPTION, PRODUCT_PRICE FROM [Product] WHERE [PRODUCT_ID] =" + productID;
            command.ExecuteNonQuery();

            //This stuff was desceibed in the code behind for Default.aspx.
            table = new System.Data.DataTable();
            adapter.Fill(table);
            ProductPageView_Info.DataSource = table;
       
            ProductPageView_Info.DataBind();



            //Close the connection.
            connection.Close();

        }

        protected void AddToCart(object sender, EventArgs e)
        {
            if (Request.Cookies["LoginInfo"] != null) //If the user is logged in as a member.
            {
                //Get the cookie that holds all of the user's cart information.
                if (Request.Cookies["Cart"] == null) //If this cookie doesn't exist
                {
                    //Then create it.
                    HttpCookie cookie = new HttpCookie("Cart");
                    cookie.Value = Session["ProductID"].ToString();
                    cookie.Expires = DateTime.Now.AddMinutes(10);
                    Response.Cookies.Add(cookie);

                }
                else
                {
                    //If it does exits, then modify it to include the new product using pipes.
                    HttpCookie cookie = Request.Cookies["Cart"];
                    cookie.Value = cookie.Value + "|" + Session["ProductID"].ToString();
                    cookie.Expires = DateTime.Now.AddMinutes(10); //Also, extend the expriration time by 10 minutes.
                    Response.Cookies.Add(cookie);
                }


                //Helpful Source: https://www.youtube.com/watch?v=kIsRsovyEEU
                //Call JavaScript alert box that let's the user know the product was added to the cart.
                ScriptManager.RegisterStartupScript(this, GetType(), "JavaScriptKey1", "alertBox();", true);

            }
            else
            {
                //Let the user know that only users logged in as members (not admins) can add items to the cart.
                ScriptManager.RegisterStartupScript(this, GetType(), "JavaScriptKey2", "notMember();", true);

            }

        }
    }
}