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
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            //Citing a helpful source: https://www.youtube.com/watch?v=BnDC8gM6RCI

            //Pulling connection string from our web.config file
            string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
            
            //Creating a new Sql connection to our database specified by the connection string
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

            //Opening the connection for database access
            connection.Open();

            //Creating a new sql command
            System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;

            //Query the database for products whose product Id's are less than 4,
            //that should return 3 rows from our database. We will show these 3 rows as 1 column 
            //on our web page.
            command.CommandText = "SELECT * FROM [Product] WHERE [PRODUCT_ID] < 4";

            //Execute this query...might not need this because of DataAdapter...but website works and don't want to break it.
            command.ExecuteNonQuery();

            //DataList uses Datatable. Datatable is like the middle man between database table and dataList object.
            //The queried rows are placed in the dataTable, which is used to populate our data list.
            System.Data.DataTable table = new System.Data.DataTable();

            //If DataList is the middle mand, then DataAdaptor is the bouncer. It's what allows for the command 
            //to access the database.
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(command);

            //Datatable gets populated with the help of adaptor
            adapter.Fill(table);
            ProductList1.DataSource = table; //DataList is populated by DataTable.
            ProductList1.DataKeyField = "Product_ID"; //Clicking on the product's picture will send user to the product's page. 
                                                    //As such, we need to keep track of which picture corresponds to which product ID so that
                                                    //we can pass this information along to ProductDescriptionPage.aspx in a Session object.
            ProductList1.DataBind(); //Bind the data, which updates our DataList so that we can see this product information on the page.


            //Do the same thing but for 3 more rows of the database.
            command.CommandText = "SELECT * FROM Product WHERE PRODUCT_ID >= 4 AND PRODUCT_ID < 7";
            command.ExecuteNonQuery();
            table = new System.Data.DataTable();
            adapter.Fill(table);
            ProductList2.DataSource = table;
            ProductList2.DataKeyField = "Product_ID";
            ProductList2.DataBind();

            //Do the same thing but for 3 more rows of the database.
            command.CommandText = "SELECT * FROM Product WHERE PRODUCT_ID >= 7 AND PRODUCT_ID < 10";
            command.ExecuteNonQuery();
            table = new System.Data.DataTable();
            adapter.Fill(table);
            ProductList3.DataSource = table;
            ProductList3.DataKeyField = "Product_ID";
            ProductList3.DataBind();



            //Close our connection to the database.
            connection.Close();


        }


        protected void ProductList_GetProductID(object sender, DataListCommandEventArgs e)
        {
            //Citing a helpful source: https://stackoverflow.com/questions/22202372/get-selected-product-from-a-datalist-in-visual-studio-and-sql/26279818

            string productID;

            if (e.CommandName == "GetProductID_Column1") //Which DataList's product picture are we calling this function from.
            {
                //If its DataList #1's?

                //Get the Product ID that corresponds to the specific picture using the DataList item's DataKeys that are associated with that picture.
                //Product IDs were stored in Datakeys during Page_Load() function.
                productID = ProductList1.DataKeys[e.Item.ItemIndex].ToString();

                //Create a Session object called "ProductID" that will be used in ProductDescriptionPage.aspx
                //Basically, we click a product's picture in Default.aspx. Then, the product ID that corresponds to that picture
                //is put in a Session object.
                //Then, the user is redirected to the Product Description Page, which uses the picture's product ID to
                //query the data base for the specific item that the picture is associated with.
                Session["ProductID"] = productID;

            }
            else if(e.CommandName == "GetProductID_Column2")
            { 
                //If its DataList #2's?

                productID = ProductList2.DataKeys[e.Item.ItemIndex].ToString();

                System.Diagnostics.Debug.WriteLine(productID);

                Session["ProductID"] = productID;
            }
            else if(e.CommandName == "GetProductID_Column3")
            {
                //If its DataList #3's?

                productID = ProductList3.DataKeys[e.Item.ItemIndex].ToString();

                System.Diagnostics.Debug.WriteLine(productID);

                Session["ProductID"] = productID;
            }
            else
            {
                Session["ProductID"] = 0; //Inidcates that an error occcured with getting the product ID of an item.
            }



            //Go to ProductDescriptionPage.aspx
            Response.Redirect("ProductDescriptionPage.aspx", false);
        }

    }
}