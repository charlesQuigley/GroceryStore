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
    public partial class Receipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If the cookie that represents member information could not be found,
            //then the user is not logged in as a member.
            if(Request.Cookies["LoginInfo"] == null)
            {
                //As such, don't show the contents of this page to the user.
                ErrorLabelReceipt.Text = "You Must Be Logged In As A Member To View This Page.";
                ErrorLabelReceipt.Visible = true;
                ReceiptContent.Visible = false;

                return;
            }


            String ProductsStr;
            String PriceStr;

            //If this Session object is null, that means that
            //the user has not bought anything yet.
            if(Session["UserReceipt_Products"] == null)
            {
                //As such, display this message.
                ErrorLabelReceipt.Text = "You Have Not Purchased Anything This Session.";
                ErrorLabelReceipt.Visible = true;
                ReceiptContent.Visible = false;
            }
            else
            {
                //2D array used to extract the item names, prices, and qunatities from Shopping Cart 
                //cookie. 
                //Cookies only hold strings, so we use pipes to differentiate between information
                //within the same cookie. So, to better represent this information,
                //going to use 2D array.
                String[,]NamePriceQuantity = new String[20,3];
                
                
                Boolean inReceipt;

                //Initialize the array.
                for(int i = 0; i < 20; i++)
                {
                    NamePriceQuantity[i, 1] = "0.0";
                    NamePriceQuantity[i,2] = "1";
                    
                }

                int k = 0;

                //ProductStr[0] will hold the name of a product whose price is 
                //PriceStr[0]. The information within these arrays matches up
                //by index.
                ProductsStr = Session["UserReceipt_Products"].ToString();
                PriceStr = Session["UserReceipt_Prices"].ToString();

                //Pipe them cookies.
                String[] ProductArray = ProductsStr.Split('|');
                String[] PriceArray = PriceStr.Split('|');

                
                float tempPrice;

                int tempQuantity;

                for(int i = 0; i < ProductArray.Length; i++)
                {
                    inReceipt = false;

                    for(int j = i+1; j < ProductArray.Length; j++)
                    {
                        //We need to find the quantity of a single item that the user purchased.
                        //So,  whenever we have a multiple instances of a product name in our array,
                        //we will go in this if-statement.
                        if(ProductArray[i] == ProductArray[j] && ProductArray[i] != "X")
                        {
                            //Product Name goes into our 2D array.
                            NamePriceQuantity[k, 0] = ProductArray[i];

                            //Place that product's price into the 2D arrat.
                            tempPrice = float.Parse(PriceArray[i]);
                            NamePriceQuantity[k, 1] = tempPrice.ToString();

                            //For each item with the same name, increase the quantity of that
                            //item in the 2D array by 1.
                            tempQuantity = int.Parse(NamePriceQuantity[k, 2]);
                            tempQuantity++;
                            NamePriceQuantity[k, 2] = tempQuantity.ToString();

                            //Change the name of the duplicate item to be "X",
                            //that way we don't count this item again.
                            ProductArray[j] = "X";

                            inReceipt = true;
                        }
                    }

                    //If we only found 1 instance of an item in the array (Quantity purchased was 1)
                    if(inReceipt == false && ProductArray[i] != "X")
                    {
                        //Then just place that item in the 2D array.
                        NamePriceQuantity[k, 0] = ProductArray[i];

                        
                        NamePriceQuantity[k, 1] = PriceArray[i];

                        //Quantity for that specific item is 1.
                        NamePriceQuantity[k, 2] = "1";
                    }

                    //If we have not landed on a duplicate item we already counted,
                    //then increment k for the 2D array's index.
                    if(ProductArray[i] != "X")
                    {
                        k++;
                    }

                }

                //Add item information into receipt page.
                String ReceiptStr = "";

                for(int i = 0; i < k; i++)
                {
                    //price will be the price of 1 item multiplied by the quantity purchased of that item
                    tempPrice = float.Parse(NamePriceQuantity[i, 1]) * int.Parse(NamePriceQuantity[i, 2]);
                    NamePriceQuantity[i, 1] = tempPrice.ToString();
                    ReceiptStr = ReceiptStr + "<br /><br />ITEM: " + NamePriceQuantity[i, 0] + "&nbsp;&nbsp;QTY: " + NamePriceQuantity[i, 2] + "&nbsp;&nbsp; PRICE: $" + NamePriceQuantity[i, 1];
                }

                ReceiptLabel.Text = ReceiptStr;

                if(Request.Cookies["Cart"] != null)
                {
                    //If the Cart cookie exists (which it should), then delete it because the user just
                    //purchased everything that was in there.
                    String cookieName = Request.Cookies["Cart"].Name;
                    HttpCookie cookie = new HttpCookie(cookieName);
                    cookie.Value = null;
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);

                }

                //Order should be ready for pickup tomorrow.
                OrderPickupWhen.Text = "Your order should be ready for pickup on " + DateTime.Now.AddDays(1).ToString("dddd, MMMM dd yyyy") + ".";

            }

        }
    }
}