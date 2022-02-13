using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/*
    Charles Quigley, Thomas Gonzalez
    COSC 3351
    Due: 4/23/2021
*/


namespace ShoppersExpress
{
    public partial class ShoppersExpress : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Cookies["AdminInfo"] == null) //If the person is not logged in as an admin
            {

                if (Request.Cookies["LoginInfo"] == null) //If the person is also not a Shopper's Express member.
                {
                    loginButton.Text = "Login"; //Login button indicates that the person can log in.

                    loginImage.ImageUrl = "~/Images/Face_LoggedOut.png"; //Face logo becomes a gray color, indicating that the person is not logged in.
                    loginImage.ToolTip = "You are not logged in."; //Tool tip also let's person know they are not logged in (hover mouse over the face icon in the top-right of page).


                }
                else //The person is logged in as a member.
                {
                    loginButton.Text = "Logout"; //Button will indicate that user can click it to log out.

                    loginImage.ImageUrl = "~/Images/Face_LoggedIn.png"; //Face is a lime green color, inidicating to the person that they are logged in as a member.
                    loginImage.ToolTip = "You are logged in as " + Request.Cookies["LoginInfo"]["Email"].ToString(); //When hovering over this icon, tool tip states this as well.



                }
            }
            else //The person is logged in as an admin.
            {
                loginButton.Text = "Logout"; //Button will indicate that user can click it to log out.

                loginImage.ImageUrl = "~/Images/Face_Admin.png"; //Face is a lime green color, inidicating to the person that they are logged in as an admin.
                loginImage.ToolTip = "You are logged in as " + Request.Cookies["AdminInfo"]["Email"].ToString(); //When hovering over this icon, tool tip states this as well.

            }
           
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            Session["Search"] = searchBar.Text.Trim(); //Put whatever search bar text the user inputted into a Session object.
                                                        //Trim the front and end to get rid of any extra spaces.

            Response.Redirect("Search.aspx", false); //Go to Search page, which will use the "Search" session object to query the database.
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            //If the user is logged in as an admin, log the user out by making the admin cookie expire.
            if (Request.Cookies["AdminInfo"] != null) 
            {
                //Citing the Source that helped: https://stackoverflow.com/questions/6635349/how-to-delete-cookies-on-an-asp-net-website#:~:text=To%20delete%20a%20cookie%2C%20you,it%20from%20the%20user's%20disk.
                
                String cookieName = Request.Cookies["AdminInfo"].Name; //Get the name of the Admin cookie.
                HttpCookie cookie = new HttpCookie(cookieName); //Create a new cookie with the same name.
                cookie.Value = null; 
                cookie.Expires = DateTime.Now.AddDays(-1); //Change the expiration to yesterday.
                Response.Cookies.Add(cookie); //Add the new cookie to our cookie collection. 
                                              //Since new cookie has same name as old cookie, new cookie
                                              //replaces old cookie in the collection.

                Response.Redirect(Request.RawUrl); //Redirect to this page. Basically we are refreshing the page.
                                                    //This is so that the face icon and login button text can 
                                                    //update via Page_Load().

            }
            else
            {
                //If the user is logged in as a Memebr
                if (Request.Cookies["LoginInfo"] != null) 
                {
                    //Same steps as for admin. 
                    String cookieName = Request.Cookies["LoginInfo"].Name;
                    HttpCookie cookie = new HttpCookie(cookieName);
                    cookie.Value = null;
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);

                    Response.Redirect(Request.RawUrl);

                }
            }
            
             //If the user is not logged in as a user or admin, then we redirect to the login page.
             Response.Redirect("UserLogin.aspx", false);

            
        }

        protected void GoToCart(object sender, EventArgs e)
        {
            //Redirect to the shopping cart page.
            Response.Redirect("ShoppingCart.aspx");
        }

    }
}