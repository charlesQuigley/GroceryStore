using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ShoppersExpress
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If the user is not currently logged in as Admin
            if (Request.Cookies["AdminInfo"] == null) 
            {
                //Then just redirected to Default.aspx.
                Response.Redirect("Default.aspx", true);
            }
        }

        
        protected void AddRowProduct_Click(object sender, EventArgs e)
        {
            int ProductID;

            //Make sure what the admin enetered for ProductID is a valid integer value.
            int.TryParse(AddRow_ProductID.Text, out ProductID);


            if(ProductID > 0 && ProductID < 1000) //TryParse Returns 0 if the conversion failed.
            {
                //Get the connection string from our web.config file
                string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;

                //Create a new SQL connection
                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

                //Open this connection
                connection.Open();

                //Query the Product table for ProductID's that match the one the admin entered
                System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;

                command.CommandText = "SELECT * FROM [Product] WHERE [PRODUCT_ID] = " + AddRow_ProductID.Text;


                //ExecuteReader() creates a SQL Data Reader object that reads the queried rows one row at a time.
                var reader = command.ExecuteReader();

                int count = 0;

                //So, we can call Read() to move the SQL Data Reader to the next record.
                //Read() returns false when there are no more records to read.
                while (reader.Read())
                {
                    //We Count how many rows we read. That number is how many rows match the query.
                    count++;
                }

                reader.Close(); //call close when done reading because when Sql Data Reader is being used,
                               //No other operations can be perfomed on the database via the Sql connection
                               //Until the Data Reader is closed.

                //If 1 match was returned, that means the Product ID inputted by the admin already exists.
                //This is erroneous, as Product ID is the primary key for the Product table.
                if (count != 0) 
                {
                    AddRow_NoExist_Product.Text = "**Sorry, That Product ID Already Exists.";
                    AddRow_NoExist_Product.Visible = true;
                }
                else 
                {
                    //If no matches were found

                    AddRow_NoExist_Product.Visible = false;

                    //Add the admin-inputted new product stuff into the Product table.
                    command.CommandText = "INSERT INTO [Product] (PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESCRIPTION, PRODUCT_IMAGE, PRODUCT_PRICE) " +
                          "VALUES ('" + AddRow_ProductID.Text + "','" + AddRow_ProductName.Text + "','" + AddRow_ProductDescription.Text + "','" +
                                   AddRow_ProductImage.Text + "','" + AddRow_ProductPrice.Text + "')";

                    command.ExecuteNonQuery();


                }

                //Close the sql connection
                connection.Close();

                //Update the grid so that we can clearly see how the database is effected.
                ProductGridView.DataBind();
            }
            else
            {
                //If the inputted product ID is not an integer or is less than 1 or is greater than 1000.
                AddRow_NoExist_Product.Text = "**Product ID Must Be An Integer Between 1 and 1000.";
                AddRow_NoExist_Product.Visible = true;
            }



        }

        protected void DeleteRowProduct_Click(object sender, EventArgs e)
        {

            int ProductID;

            //Make sure what the admin enetered for ProductID is a valid integer value.
            int.TryParse(DeleteRow_ProductID.Text, out ProductID);


            if (ProductID > 0 && ProductID < 1000) //Returns 0 if the conversion failed.
            {
                //Get the connection string from our web.config file
                string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;

                //Create a new SQL connection
                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

                //Open the connection to the Store database.
                connection.Open();


                //Create sql command.
                System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;

                //Query the Product table for ProductID's that match the one the admin entered
                command.CommandText = "SELECT * FROM [Product] WHERE [PRODUCT_ID] = " + DeleteRow_ProductID.Text;

                //ExecuteReader() creates a SQL Data Reader object that reads the queried rows one row at a time.
                var reader = command.ExecuteReader();

                int count = 0;

                while (reader.Read()) //Count how many rows we read. That number is how many rows match the query.
                {
                    //So, we can call Read() to move the SQL Data Reader to the next record.
                    //Read() returns false when there are no more records to read.
                    count++;
                }

                reader.Close(); //call close when done reading.

                if (count == 0) //If 0 rows were read, that means there were no matches in the data base.
                {
                    DeleteRow_NoExist_Product.Text = "**Sorry, That Product ID Does Not Exist.";
                    DeleteRow_NoExist_Product.Visible = true;
                }
                else
                {
                    DeleteRow_NoExist_Product.Visible = false;

                    command.CommandText = "DELETE FROM [Product] WHERE [PRODUCT_ID] = " + DeleteRow_ProductID.Text;

                    command.ExecuteNonQuery();

                }
                connection.Close();
                //Update the grid so that we can clearly see how the database is effected.
                ProductGridView.DataBind();
            }
            else
            {
                DeleteRow_NoExist_Product.Text = "**Product ID Must Be An Integer Between 1 and 1000.";
                DeleteRow_NoExist_Product.Visible = true;
            }

        }


        protected void AddRowMember_Click(object sender, EventArgs e)
        {
            int MemberID;

            int.TryParse(AddRow_MemberID.Text, out MemberID);


            if (MemberID > 0 && MemberID < 1000) //Try Parse Returns 0 if the conversion failed.
            {
                string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

                connection.Open();


                System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;


                command.CommandText = "SELECT * FROM [USER] WHERE [USER_ID] = " + AddRow_MemberID.Text;

                var reader = command.ExecuteReader();

                int count = 0;

                while (reader.Read()) //Count how many rows we read. That number is how many rows match the query.
                {
                    count++;
                }

                reader.Close(); //call close when done reading because when Sql Data Reader is being used,
                                //No other operations can be perfomed on the database via the Sql connection
                                //Until the Data Reader is closed.

                if (count != 0) //If more than 0 rows were read, that means there were matches in the data base.
                {
                    AddRow_NoExist_Member.Text = "**Sorry, That Member ID Already Exists.";
                    AddRow_NoExist_Member.Visible = true;
                }
                else
                {
                    //If no rows match the admin-inputted Product ID, then add the new row into the Product table.

                    //Membership lasts for 1 year. It starts today and ends on the same date 1 year later.
                    DateTime current = DateTime.Now;
                    DateTime yearLater = current.AddYears(1);

                    AddRow_NoExist_Member.Visible = false;

                    //Add the admin-inputted new member account/row into the Product table.
                    command.CommandText = "INSERT INTO [USER] (USER_ID, USER_EMAIL, USER_PASSWORD, USER_CARD_NUMBER, USER_CARD_EXPIRATION_DATE, USER_CARD_SECURITY_CODE, USER_MEMBERSHIP_START, USER_MEMBERSHIP_END)" +
                          "VALUES ('" + AddRow_MemberID.Text + "','" + AddRow_MemberEmail.Text + "','" + AddRow_MemberPassword.Text + "','" +
                                   AddRow_MemberCardNumber.Text + "','" + AddRow_MemberCardExpirationDate.Text + "','" + AddRow_MemberCardSecurityCode.Text +
                                   "','" + current + "','" + yearLater +"')";

                    command.ExecuteNonQuery();


                }

                //Close the connection.
                connection.Close();

                //Update the grid so that we can clearly see how the database is effected.
                MemberGridView.DataBind();
            }
            else
            {
                //If the inputted Member ID is not an integer or is less than 1 or is greater than 1000.

                AddRow_NoExist_Member.Text = "**Member ID Must Be An Integer Between 1 and 1000.";
                AddRow_NoExist_Member.Visible = true;
            } 

            
        }

        protected void DeleteRowMember_Click(object sender, EventArgs e)
        {
            int MemberID;


            //Make sure what the admin enetered for USER ID (which is member ID) is a valid integer value.
            int.TryParse(DeleteRow_MemberID.Text, out MemberID);


            if (MemberID > 0 && MemberID < 1000) //Returns 0 if the conversion failed.
            {
                string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

                connection.Open();


                System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;


                command.CommandText = "SELECT * FROM [USER] WHERE [USER_ID] = " + DeleteRow_MemberID.Text;

                var reader = command.ExecuteReader();

                int count = 0;

                while (reader.Read()) //Count how many rows we read. That number is how many rows match the query.
                {
                    count++;
                }

                reader.Close(); //call close when done reading.

                if (count == 0) //If 0 rows were read, that means there were no matches in the data base.
                {
                    DeleteRow_NoExist_Member.Text = "**Sorry, That Member ID Does Not Exist.";
                    DeleteRow_NoExist_Member.Visible = true;
                }
                else
                {
                    DeleteRow_NoExist_Member.Visible = false;

                    command.CommandText = "DELETE FROM [USER] WHERE [USER_ID] = " + DeleteRow_MemberID.Text;

                    command.ExecuteNonQuery();

                }
                connection.Close();
                //Update the grid so that we can clearly see how the database is effected.
                MemberGridView.DataBind();
            }
            else
            {
                //If the inputted Member ID is not an integer or is less than 1 or is greater than 1000.

                DeleteRow_NoExist_Member.Text = "**USER ID Must Be An Integer Between 1 and 1000.";
                DeleteRow_NoExist_Member.Visible = true;
            }
        }
    }

}