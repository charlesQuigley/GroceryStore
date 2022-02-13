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
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string searchQuery;

            //If the Search Session Object is null,
            //then the user never typed anything into the search bar.
            if (Session["Search"] == null)
            {
                searchQuery = "";
            }
            else
            {
                searchQuery = Session["Search"].ToString();

            }


            if(searchQuery == "")
            {
                //Dsiplay a message on the screen that let's the user know nothing
                //was searched for. 
                EmptySearch_Label.Visible = true;
            }
            else
            {
                //Get the connection string from our web.config file.
                string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
               
                //Create a new sql connection
                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

                //open the connectiom. 
                connection.Open();

                //Create SQL command
                System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;

                //Use LIKE keyword with % in beginning and end of query to match the query to any part of the actual product's name.
                //For example, if someone entered "at" into the search bar, 'wATer bottles' and 'potATo chips' would be returned by
                //the database.
                command.CommandText = "SELECT * FROM [Product] WHERE [PRODUCT_NAME] LIKE " + "'%" + searchQuery + "%'";
                

                int count = 0;

                var reader = command.ExecuteReader();

                //Count how many rows we read. That number is how many rows match the query.
                while(reader.Read()) 
                {
                    count++;
                }

                reader.Close(); //call close when done reading.

                if (count == 0) //If 0 rows were read, that means there were no matches in the data base.
                {
                    EmptySearch_Label.Text = "Sorry, we couldn't find what you were looking for.";
                    EmptySearch_Label.Visible = true;

                 
                }
                else
                {
                    //If at least 1 row did match, display all matching rows.
                    command.ExecuteNonQuery();
                    System.Data.DataTable table = new System.Data.DataTable();
                    System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                    adapter.Fill(table);
                    SearchList.DataSource = table;
                    //DataKeyField containing Product_ID allows us to send this product ID info to ProductDescriptionPage.aspx
                    //When the user clicks on a product's image, that DataList entry contains the product_ID in its datakeys,
                    //So, we can create a Session object to send that product ID to the Product Description page, which can use it
                    //to query the database and render the desired product.
                    SearchList.DataKeyField = "Product_ID";

                    //Bind the database data to the DataList.
                    SearchList.DataBind();
                }


                //Close the connection.
                connection.Close();


            }



        }

        protected void SearchList_GetProductID(object sender, DataListCommandEventArgs e)
        {
            string productID;

            if (e.CommandName == "GetProductID") //Clicked on picture
            {
                //Get the appropriate Product ID that corresponds to the product picture that was clicked.
                productID = SearchList.DataKeys[e.Item.ItemIndex].ToString();

                //System.Diagnostics.Debug.WriteLine(productID);
                //^^Can be used to make sure we're getting the correct product ID's when we click the pictures.
                //Writes the product ID to the debug window.

                //New Session object containing the desired productID.
                Session["ProductID"] = productID;

                //Go to ProductDescriptionPage.aspx
                Response.Redirect("ProductDescriptionPage.aspx", false);

            }
        }

    }
}