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
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean AllowedToViewCart = false;

            //If the user is not logged in as an admin
            if (Request.Cookies["AdminInfo"] == null) 
            {
                //If the user is also not logged in as a member
                if (Request.Cookies["LoginInfo"] == null) 
                {
                    //Then show error message and do not display the user's cart.
                    EmptySearch_Label.Text = "You Must Be Logged In To View Your Cart.";
                    EmptySearch_Label.Visible = true;

                    CartTitle.Visible = false;

                    HorizontalLine1.Visible = false;
                    HorizontalLine2.Visible = false;
                    HorizontalLine3.Visible = false;

                    PurchaseCartButton.Visible = false;
                    DeleteCartButton.Visible = false;
                }
                else
                {
                    //If the user is logged in as a member, display the user's cart
                    //with no error labels.
                    EmptySearch_Label.Text = "";
                    EmptySearch_Label.Visible = false;

                    AllowedToViewCart = true;

                }
            }
            else
            {
                //If the user is logged in as an admin, display the user's cart with no error labels.
                EmptySearch_Label.Text = "";
                EmptySearch_Label.Visible = false;

                AllowedToViewCart = true;
            }


            if (AllowedToViewCart == true)
            {
                //If the user's cart is empty.
                if (Request.Cookies["Cart"] == null)
                {
                    EmptySearch_Label.Text = "You Do Not Have Any Items In Your Cart.";
                    EmptySearch_Label.Visible = true;
                    CartTitle.Visible = false;

                    HorizontalLine1.Visible = false;
                    HorizontalLine2.Visible = false;
                    HorizontalLine3.Visible = false;

                    PurchaseCartButton.Visible = false;
                    DeleteCartButton.Visible = false;
                }
                else
                {

                    //Citing a Helpful Source: https://stackoverflow.com/questions/9845787/get-and-set-cookie-values-in-array-position
                   
                    //Extract information from the Cart cookie by splitting its pipes.
                    HttpCookie cookie = Request.Cookies["Cart"];
                    var cookieVal = cookie.Value;
                    var cartArray = cookieVal.Split('|');

                    //Grab connection string from web.config file.
                    string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
                    System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

                    //Open the connection.
                    connection.Open();

                    //Create SQL command.
                    System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
                    command.CommandType = System.Data.CommandType.Text;
 

                    string CartString = "";
                    float totalPrice = 0;
                  
                    //Display the name and price of each item the user has in his/her cart.
                    //Duplicate items will be displayed as well...which is why we do this.
                    //DataList will not display duplicate item entries, but users should be able to buy
                    //more than 1 of the same product.
                    for (int i = 0; i < cartArray.Length; i++)
                    {

                        command.CommandText = "SELECT [PRODUCT_NAME] FROM [Product] WHERE [PRODUCT_ID] = " + cartArray[i].ToString();
                        CartString = CartString + "<br />*1 &nbsp;" + command.ExecuteScalar().ToString();

                        command.CommandText = "SELECT [PRODUCT_PRICE] FROM [Product] WHERE [PRODUCT_ID] = " + cartArray[i].ToString();
                        totalPrice = totalPrice + float.Parse(command.ExecuteScalar().ToString());

                    }

                    //Show the total price if the user bought the entire cart.
                    TotalPurchase_Label.Text = CartString;
                    TotalPrice_Label.Text = "Total :&nbsp;&nbsp;&nbsp;$&nbsp;" + String.Format("{0:.##}", totalPrice);
                    String ProductIDs = "";

                    //
                    for (int i = 0; i < cartArray.Length; i++)
                    {
                        if (i == 0)
                        {
                            ProductIDs = cartArray[i].ToString();
                        }
                        else
                        {
                            ProductIDs = ProductIDs + " OR [PRODUCT_ID] = " + cartArray[i].ToString();

                        }
                    }


                    //Display the database picture, name, and description and price of each distinct item the user
                    //has in his/her cart.
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = "SELECT * FROM [Product] WHERE [PRODUCT_ID] = " + ProductIDs;


                    int count = 0;

                    var reader = command.ExecuteReader();

                    while (reader.Read()) //Count how many rows we read. That number is how many rows match the query.
                    {
                        count++;
                    }

                    reader.Close(); //call close when done reading.

                    if (count == 0) //If 0 rows were read, that means there were no matches in the data base.
                    {
                        EmptySearch_Label.Text = "Sorry, It Looks Like The Items You're Looking For Were Removed From Our Store.";
                        EmptySearch_Label.Visible = true;


                    }
                    else
                    {
                        //If at least 1 row did match, display all matching rows.
                        //This code has been explained on multiple different code behind pages.
                        //Look at Default.aspx.cs for reference.
                        command.ExecuteNonQuery();
                        System.Data.DataTable table = new System.Data.DataTable();
                        System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                        adapter.Fill(table);
                        SearchList.DataSource = table;
                        SearchList.DataBind();
                    }

                    //Close the connection.
                    connection.Close();

                }

            }
        }

        protected void DeleteCart(object sender, EventArgs e)
        {
            //Modify the Cart cookie to expire yesterday.

            String cookieName = Request.Cookies["Cart"].Name; //Get the name of the cart cookie
            HttpCookie cookie = new HttpCookie(cookieName); //create new cookie with same name.
            cookie.Value = null; 
            cookie.Expires = DateTime.Now.AddDays(-1); //Expires yesterday
            Response.Cookies.Add(cookie); //Replaces old cookie in our cookie collection. 

            Response.Redirect("ShoppingCart.aspx");
        }

        protected void PurchaseCart(object sender, EventArgs e)
        {

            //Citing Helpful Source: https://stackoverflow.com/questions/9845787/get-and-set-cookie-values-in-array-position
           
            //Split cookie string by pipe to get the information.
            HttpCookie cookie = Request.Cookies["Cart"];
            var cookieVal = cookie.Value;
            var cartArray = cookieVal.Split('|');

            //Get connection string from web.config
            string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

            //Open connection
            connection.Open();

            //Create sql command
            System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;

            string CartString = "";
            string totalPrice = "";


            //We need to send the product names and prices of the cart to the Receipt page. As such
            //Let's create 2 piped strings that hold each product name and product price respectively.
            //This will let us extract this information fairly easily in the Receipt.aspx page.
            for (int i = 0; i < cartArray.Length; i++)
            {
                //Get Product name based on Product ID from the cart.
                command.CommandText = "SELECT [PRODUCT_NAME] FROM [Product] WHERE [PRODUCT_ID] = " + cartArray[i].ToString();
                
                //Each product name entry will be piped.
                if(i != 0)
                {
                    //All subsequent item names will be added to the string with a pipe in the front.
                    CartString = CartString + "|" + command.ExecuteScalar().ToString();
                }
                else
                {
                    //The first item name will be added to the string without a pipe in the front.
                    CartString = command.ExecuteScalar().ToString();

                }

                //Get Product Price based on Product ID from the cart.
                command.CommandText = "SELECT [PRODUCT_PRICE] FROM [Product] WHERE [PRODUCT_ID] = " + cartArray[i].ToString();

                if(i != 0)
                {
                    //All subsequent item prices will be added to the string with a pipe in the front.
                    totalPrice = totalPrice + "|" + (command.ExecuteScalar().ToString());

                }
                else
                {
                    //The first item name will be added to the string without a pipe in the front.
                    totalPrice = command.ExecuteScalar().ToString();

                }

            }

            //Create 2 Session Objects. One contains the item names from the cart.
            Session["UserReceipt_Products"] = CartString;

            //The other contains the item prices from the cart.
            Session["UserReceipt_Prices"] = totalPrice;

            //Call alertBox() javaScript function that is defined in the HTML page.
            ScriptManager.RegisterStartupScript(this, GetType(), "JavaScriptKey", "alertBox();", true);


        }

    }
}
