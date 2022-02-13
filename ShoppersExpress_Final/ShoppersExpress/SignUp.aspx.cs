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
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterMember(object sender, EventArgs e)
        {
            //Get the connection string from the web.config file.
            string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
            
            //We've seen all of this explained on multiple different pages.
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

            connection.Open();


            System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;

            //Make sure the inputted email does not already exist in the database.
            command.CommandText = "SELECT * FROM [USER] WHERE [USER_EMAIL] = '" + SignUp_Email.Text +"'";

            var reader = command.ExecuteReader();

            int count = 0;

            bool success = false;

            while (reader.Read()) //Count how many rows we read. That number is how many rows match the query.
            {
                count++;
            }

            reader.Close(); //call close when done reading.

            if (count != 0) 
            {
                //If more than 0 rows were read, that means the email already exists in the database,
                //Which is erroneous.
                EmailAlreadyExists.Visible = true;
            }
            else
            {
                //Inputted Email does not exist in the database

                EmailAlreadyExists.Visible = false;

                //Find the largest User ID number
                command.CommandText = "SELECT MAX(USER_ID) FROM [USER]";

                int largestID = int.Parse(command.ExecuteScalar().ToString());

                //Increase it by 1 since this is going to be the next entry in the USER table.
                largestID++;


                //Membership starts today and ends on the same date next year.
                DateTime current = DateTime.Now;
                DateTime yearLater = current.AddYears(1);

                //Add the new member row into the USER (member) table.
                command.CommandText = "INSERT INTO [USER] (USER_ID, USER_EMAIL, USER_PASSWORD, USER_CARD_NUMBER, USER_CARD_EXPIRATION_DATE, USER_CARD_SECURITY_CODE, USER_MEMBERSHIP_START, USER_MEMBERSHIP_END)" +
                                       "VALUES ('" + largestID.ToString() + "','" + SignUp_Email.Text + "','" + SignUp_Password.Text + "','" +
                                       SignUp_CardNumber.Text + "','" + SignUp_CardExpiration.Text + "','" + SignUp_CardSecurity.Text +
                                       "','" + current + "','" + yearLater + "')";

                command.ExecuteNonQuery();

                success = true;

            }

            connection.Close();

            if(success == true)
            {
                //Call JavaScript function alertxBox(), which can be found in the HTML page.
                ScriptManager.RegisterStartupScript(this, GetType(), "JavaScriptKey", "alertBox();", true);

            }

        }
    }
}