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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void SignIn_Button_Click(object sender, EventArgs e)
        {
         
            //We've seen this code explained on multiple other web pages
            string connStr = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr);

            connection.Open();


            System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;


            //See if the inputted Email is in the USER table
            command.CommandText = "SELECT * FROM [USER] WHERE [USER_EMAIL] = '" + Email_Login_Txt.Text + "' AND [USER_PASSWORD] = '" + Password_Login_Txt.Text + "'";


            int count = 0;

            var reader = command.ExecuteReader();

            while (reader.Read()) //Count how many rows we read. That number is how many rows match the query.
            {
                count++;
            }

            reader.Close(); //call close when done reading.

            if (count == 0) //If 0 rows were read, that means there were no matches in the data base.
            {

                //If the Email could not be found in the USER table, look for it in 
                //the Admin table.

                command.CommandText = "SELECT * FROM [ADMIN] WHERE [ADMIN_EMAIL] = '" + Email_Login_Txt.Text + "' AND [ADMIN_PASSWORD] = '" + Password_Login_Txt.Text + "'";

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count++;
                }

                reader.Close();

                if(count == 0)
                {
                    //If the email could not be found in the USER or ADMIN tables,
                    //then let the user know that the inputted email is not associated 
                    //with any of our accounts.
                    AccountExists.Text = "**Email or Password is Incorrect. Please Try Again.**";

                }
                else
                {
                    //If the email was found in the ADMIN table,
                    //then we have an Admin trying to sign in.

                    AccountExists.Text = "&nbsp;";

                    //Create an Admin cookie so that the browser keeps the admin 
                    //logged in.

                    HttpCookie AdminInfo = new HttpCookie("AdminInfo"); 
                    AdminInfo["Email"] = Email_Login_Txt.Text;
                    AdminInfo["Password"] = Password_Login_Txt.Text;

                    //Admin will stay signed in for 10 minutes, unless the admin logs out
                    //before then.
                    AdminInfo.Expires = DateTime.Now.AddMinutes(10);
                    Response.Cookies.Add(AdminInfo);

                    //Go to the Admin page, where the admin can add/delete rows from the Product 
                    //and USER tables.
                    Response.Redirect("AdminPage.aspx");

                }

            }
            else
            {
                //Citing helpful source about cookies: https://www.youtube.com/watch?v=Y0y_o5-9QX0

                AccountExists.Text = "&nbsp;";

                //The inputted email was found in the USER table,
                //which means we have a member trying to log in.
                //So, create a member cookie that will keep the member logged in.
                HttpCookie LoginInfo = new HttpCookie("LoginInfo"); 
                LoginInfo["Email"] = Email_Login_Txt.Text;
                LoginInfo["Password"] = Password_Login_Txt.Text;
                //Cookie expires in 10 minutes.
                LoginInfo.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(LoginInfo);

                Response.Redirect("Default.aspx");

            }


            //Close the connection.
            connection.Close();
        }

        protected void SignUp(object sender, EventArgs e)
        {
            //If the user clicks the "Register Here" button.
            Response.Redirect("SignUp.aspx", false);
        }
    }
}