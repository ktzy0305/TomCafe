using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TomCafe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Variables 
        string textveri = "";
        int receiptno = 1;
        OrderItem CustomBundle;
        //Lists
        List<OrderItem> CartList = new List<OrderItem>();
        List<MenuItem> BundleMealList = new List<MenuItem>();
        List<MenuItem> SidesList = new List<MenuItem>();
        List<MenuItem> BeverageList = new List<MenuItem>();
        List<MenuItem> ValueMealsList = new List<MenuItem>();
        List<Product> ProductList = new List<Product>();
        //To Show TradeIns
        List<Beverage> TradeInList = new List<Beverage>();
        //RecommendedFeature
        List<MenuItem> CompareBundleList = new List<MenuItem>();
        List<String> checkbundle = new List<String>();

        public void InitPage()
        {

            //There is a way to shorten this
            //Example
            //ProductList.Add(new Beverage("Cola", 2.85, 0));
            //Beverages
            Beverage cola = new Beverage("Cola", 2.85, 0);
            Beverage greentea = new Beverage("Green Tea", 3.70, 0.85);
            Beverage coffee = new Beverage("Coffee", 2.70, 0);
            Beverage tea = new Beverage("Tea", 2.70, 0);
            Beverage rootbeer = new Beverage("Tom's Brew Root Beer", 9.70, 6.85);
            Beverage mocktail = new Beverage("Mocktail", 15.90, 13.05);
            ProductList.Add(cola);
            ProductList.Add(greentea);
            ProductList.Add(coffee);
            ProductList.Add(tea);
            ProductList.Add(rootbeer);
            ProductList.Add(mocktail);
            //Trade Ins
            TradeInList.Add(cola);
            TradeInList.Add(greentea);
            TradeInList.Add(coffee);
            TradeInList.Add(tea);
            TradeInList.Add(rootbeer);
            TradeInList.Add(mocktail);
            //Sides
            Side hashbrown = new Side("Hash Brown", 2.10);
            Side trufflefries = new Side("Truffle Fries", 3.70);
            Side calamari = new Side("Calamari", 3.40);
            Side salad = new Side("Caesar Salad", 4.30);
            ProductList.Add(hashbrown);
            ProductList.Add(trufflefries);
            ProductList.Add(calamari);
            ProductList.Add(salad);
            //Value Meal
            ValueMeal hotcake = new ValueMeal("Hotcakes with sausage", 6.90, Convert.ToDateTime("07:00AM"), Convert.ToDateTime("02:00PM"));
            ValueMeal hamburger = new ValueMeal("Hamburger", 7.50, Convert.ToDateTime("10:00AM"), Convert.ToDateTime("07:00PM"));
            ValueMeal ribseye = new ValueMeal("Ribeye Steak", 10.20, Convert.ToDateTime("04:00PM"), Convert.ToDateTime("10:00PM"));
            ValueMeal nasilemak = new ValueMeal("Nasi Lemak", 5.40, Convert.ToDateTime("00:00AM"), Convert.ToDateTime("11:59:59PM"));
            ProductList.Add(hotcake);
            ProductList.Add(hamburger);
            ProductList.Add(nasilemak);
            ProductList.Add(ribseye);

            //MenuItems
            //Downcasting them into different products
            //Each menu item has a product list containing products that are being sold.
            foreach (Product p in ProductList)
            {
                if (p is Side)
                {
                    MenuItem side = new MenuItem(p.Name, p.Price);
                    side.ProductList = new List<Product>();
                    side.ProductList.Add(p);
                    SidesList.Add(side);
                }

                else if (p is Beverage)
                {
                    MenuItem beverage = new MenuItem(p.Name, p.Price);
                    beverage.ProductList = new List<Product>();
                    beverage.ProductList.Add(p);
                    BeverageList.Add(beverage);
                }

                else
                {
                    ValueMeal valueMeal = (ValueMeal)p;
                    if (valueMeal.IsAvailable() == true) //This is to check whether the valuemeal is available at the current time.
                    {
                        MenuItem value_meal = new MenuItem(valueMeal.Name, valueMeal.Price);
                        value_meal.ProductList = new List<Product>();
                        value_meal.ProductList.Add(valueMeal);
                        ValueMealsList.Add(value_meal);
                    }
                }
            }


            //Bundle Meal
            MenuItem breakfastset = new MenuItem("Breakfast Set\n(Hotcakes with sausage, Hash brown)", 7.90);
            MenuItem hamburgercombo = new MenuItem("Hamburger combo\n(Hamburger, Fries, Cola)", 10.20);
            MenuItem dinnerset = new MenuItem("Dinner set\n(Ribeye steak, Fries, Caesar salad, Coffee)", 18.50);
            breakfastset.ProductList = new List<Product>();
            breakfastset.ProductList.Add(hotcake);
            breakfastset.ProductList.Add(hashbrown);
            hamburgercombo.ProductList = new List<Product>();
            hamburgercombo.ProductList.Add(hamburger);
            hamburgercombo.ProductList.Add(trufflefries);
            hamburgercombo.ProductList.Add(cola);
            dinnerset.ProductList = new List<Product>();
            dinnerset.ProductList.Add(ribseye);
            dinnerset.ProductList.Add(trufflefries);
            dinnerset.ProductList.Add(salad);
            dinnerset.ProductList.Add(coffee);
            //Since bundlemeal is not a value meal it does not have the method is available so we have to do this.
            if (DateTime.Now >= Convert.ToDateTime("7:00AM") && DateTime.Now <= Convert.ToDateTime("2:00PM"))
            {
                BundleMealList.Add(breakfastset);
            }
            if (DateTime.Now >= Convert.ToDateTime("10:00AM") && DateTime.Now <= Convert.ToDateTime("7:00PM"))
            {
                BundleMealList.Add(hamburgercombo);
            }
            if (DateTime.Now >= Convert.ToDateTime("4:00PM") && DateTime.Now <= Convert.ToDateTime("10:00PM"))
            {
                BundleMealList.Add(dinnerset);
            }

        }
        //Recommended Feature
        //The CompareBundleList is a list containing bundlemeals where it's product data is not edited.
        public void InitCompareBundleList()
        {
            try
            {
                foreach (MenuItem m in BundleMealList)
                {
                    CompareBundleList.Add(m);
                }
            }
            catch
            {
                //In the event where there are no bundlemeals available at the current time and null reference error occurs
            }
        }

        //Standard welcome text for Tom's Cafe to be added each time a user adds or removes an item from the cart, cancel or confirms an order.
        public string WelcomeText()
        {
            return "Welcome to Tom's Cafe!\n\nChoose your item from the menu.";
        }

        //A method to calculate the current total price in the cart to be displayed each time a user adds or removes item from the cart
        public double CalculateCartTotalPrice()
        {
            double subtotal = 0;
            foreach (OrderItem o in CartList)
            {
                subtotal += o.Item.Price;
            }
            return subtotal;
        }


        public MainPage()
        {
            InitPage();
            InitCompareBundleList();
            this.InitializeComponent();
            displayText.Text = WelcomeText();
            //Default value is bundlemeal
            itemsListView.ItemsSource = BundleMealList;

        }

        //This method is to check the word count during the reciept display to align the words correctly.
        public int CheckWordCount(string text)
        {
            text.Trim(); //Trim to remove whitespace at the end of the text
            int wordCount = 0, index = 0;

            while (index < text.Length)
            {
                // check if current char is part of a word
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;

                // skip whitespace until next word
                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;
            }
            return wordCount;
        }

        public void CheckBundle()
        {
            textveri = "";
            foreach (OrderItem c in CartList)
            {
                checkbundle.Add(c.Item.Name);
            }
            if (checkbundle.Contains("Hotcakes with sausage") && checkbundle.Contains("Hash Brown"))
            {
                textveri += "Breakfast set & ";
            }
            if (checkbundle.Contains("Hamburger") && checkbundle.Contains("Truffle Fries") && checkbundle.Contains("Cola"))
            {
                textveri += "Hamburger set & ";
            }
            if (checkbundle.Contains("Ribeye Steak") && checkbundle.Contains("Truffle Fries") && checkbundle.Contains("Coffee") && checkbundle.Contains("Caesar Salad"))
            {
                textveri += "Dinner set & ";
            }
            if (textveri != "")
            {
                textveri = textveri.Substring(0, textveri.Length - 2);
            }
            checkbundle.Clear();
        }

        //This method is to clear all the lists containing the MenuItems and re-initialises every product.
        public void RefreshMenuLists()
        {
            BundleMealList.Clear();
            ValueMealsList.Clear();
            SidesList.Clear();
            BeverageList.Clear();
            ProductList.Clear();
            TradeInList.Clear();
            InitPage();
        }

        //Menu Display Buttons
        //Every list except the CartList and TradeIn will be cleared and products are re-initialised each time a button is clicked.
        private void mainsButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshMenuLists();
            itemsListView.ItemsSource = ValueMealsList;
        }

        private void sidesButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshMenuLists();
            itemsListView.ItemsSource = SidesList;
        }

        private void beveragesButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshMenuLists();
            itemsListView.ItemsSource = BeverageList;
        }

        private void bundlesButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshMenuLists();
            itemsListView.ItemsSource = BundleMealList;
        }
        //Add to Cart
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            if (itemsListView.SelectedItems.Count == 0)
            {
                displayText.Text = "Welcome to Tom's Cafe!\nChoose your item from the menu.";
            }
            //if bundle meal is selected
            else if (itemsListView.ItemsSource == BundleMealList)
            {
                OrderItem newbundle = new OrderItem(BundleMealList[itemsListView.SelectedIndex]);
                int index = newbundle.Item.Name.IndexOf('('); //Finds the index of '(' to get the bundle meal name
                if (newbundle.Item.Name == "Breakfast Set\n(Hotcakes with sausage, Hash brown)")
                {
                    bool found = false;
                    foreach (OrderItem o in CartList) //Finding out if the bundle exists in cart.
                    {
                        if (newbundle.Item.Name == o.Item.Name)
                        {
                            found = true;
                            o.AddQty();
                            o.Item.Price = o.GetItemTotalAmount();
                            displayText.Text = $"{newbundle.Item.Name.Substring(0, index - 1)} added.\n";
                            break;
                        }
                    }
                    //If the MenuItem is not found in the Cart, the bundle will be added into the cart
                    if (!found)
                    {
                        newbundle.AddQty();
                        CartList.Add(newbundle);
                        displayText.Text = $"{newbundle.Item.Name.Substring(0, index - 1)} added.\n";
                    }
                    displayText.Text += $"Total: ${CalculateCartTotalPrice():0.00}\n\n" + WelcomeText();
                    cartsListView.ItemsSource = null;
                    cartsListView.ItemsSource = CartList;
                }
                else //This is for the Hamburger Combo and Dinner Set
                {
                    foreach (Beverage b in TradeInList) //This chunk of code calculates the current trade in price for each drink.
                    {
                        if ((b.GetPrice() - newbundle.Item.ProductList[newbundle.Item.ProductList.Count - 1].GetPrice()) < 0) //The drink is the last product in the productlist for hamburger set and dinner set
                        {
                            b.TradeIn = 0; //If the calculated trade in value is negative, it will be rounded to 0.
                        }
                        else
                        {
                            b.TradeIn = b.GetPrice() - newbundle.Item.ProductList[newbundle.Item.ProductList.Count - 1].GetPrice();
                        }
                    }
                    CustomBundle = newbundle; //This is to store either the Hamburger Set or Dinner Set into a MenuItem variable CustomBundle. 
                    itemsListView.ItemsSource = null;
                    itemsListView.ItemsSource = TradeInList;
                }

            }
            else if (itemsListView.ItemsSource == TradeInList)
            {
                CustomBundle.Item.ProductList.RemoveAt(CustomBundle.Item.ProductList.Count - 1); // Remove the drinks from the CustomBundle object
                int index = CustomBundle.Item.Name.IndexOf('('); //Get the index of '('
                double price = CustomBundle.Item.GetTotalPrice(); //Base price of the Hamburger Set or Dinner Set
                Beverage newBeverage; //Beverage variable newBeverage to store the new drink
                newBeverage = TradeInList[itemsListView.SelectedIndex];
                CustomBundle.Item.ProductList.Add(newBeverage); //Add the new beverage into the custombundle variable that stores the current hamburger set or dinner set that the customer orders
                price += newBeverage.TradeIn; //Add the trade in price of the drink to the base price
                string NewMenuItemName = CustomBundle.Item.Name.Substring(0, index) + "("; //Removes everything from '('
                foreach (Product p in CustomBundle.Item.ProductList) //Add the current customized bundle product names onto the name of the bundle.
                {
                    if (p.Name == "Truffle Fries") //The reciept on the assignment sheet calls Truffle fries as Fries
                    {
                        NewMenuItemName += "Fries, ";
                    }
                    else
                    {
                        NewMenuItemName += $"{p.Name}, ";
                    }
                }
                //This chunk of code removes the ',' and whitespace.
                if (NewMenuItemName.EndsWith(", "))
                {
                    NewMenuItemName = NewMenuItemName.Substring(0, NewMenuItemName.Length - 2);
                }
                NewMenuItemName += ")"; //Adds the ')' at the end to finish the name of the customized bundle.
                //Creates the new Custom Bundle object
                MenuItem CustomBundleItem = new MenuItem(NewMenuItemName, price);
                CustomBundleItem.ProductList = new List<Product>();
                //Add each product into the Custom Bundle.
                foreach (Product p in CustomBundle.Item.ProductList)
                {
                    CustomBundleItem.ProductList.Add(p);
                }
                //Check if the Custom Bundle exists in the Cart by matching the name of the items
                bool found = false;
                foreach (OrderItem o in CartList)
                {
                    if (CustomBundleItem.Name == o.Item.Name)
                    {
                        found = true;
                        o.AddQty();
                        o.Item.Price = o.GetItemTotalAmount() + (o.Quantity * newBeverage.TradeIn); //Base price + Trade In Quantity
                        displayText.Text = $"{CustomBundleItem.Name.Substring(0, index - 1)} added.\n";
                        break;
                    }
                }
                if (!found) //If the item is not found, we create the OrderItem object to be added into the cart.
                {
                    OrderItem OI = new OrderItem(CustomBundleItem);
                    OI.AddQty();
                    CartList.Add(OI);
                    displayText.Text = $"{CustomBundleItem.Name.Substring(0, index - 1)} added.\n";
                }
                displayText.Text += $"Total: ${CalculateCartTotalPrice():0.00}\n\n" + WelcomeText();
                RefreshMenuLists();
                itemsListView.ItemsSource = null;
                itemsListView.ItemsSource = BundleMealList;
                cartsListView.ItemsSource = null;
                cartsListView.ItemsSource = CartList;
            }
            //For the rest of the items
            //We check if the name of new OrderItem is the same as any of the existing OrderItems in the cart.
            //If it is we add the quantity
            //If not we add the new OrderItem into the cart
            else if (itemsListView.ItemsSource == BeverageList)
            {
                OrderItem newbev = new OrderItem(BeverageList[itemsListView.SelectedIndex]);
                bool found = false;
                foreach (OrderItem o in CartList)
                {
                    if (newbev.Item.Name == o.Item.Name)
                    {
                        found = true;
                        o.AddQty();
                        o.Item.Price = o.GetItemTotalAmount();
                        displayText.Text = $"{newbev.Item.Name} added.\n";
                        break;
                    }
                }
                if (!found)
                {
                    newbev.AddQty();
                    CartList.Add(newbev);
                    displayText.Text = $"{newbev.Item.Name} added.\n";
                }
                displayText.Text += $"Total: ${CalculateCartTotalPrice():0.00}\n\n" + WelcomeText();
                CheckBundle();
            }
            else if (itemsListView.ItemsSource == SidesList)
            {
                OrderItem newside = new OrderItem(SidesList[itemsListView.SelectedIndex]);
                bool found = false;
                foreach (OrderItem o in CartList)
                {
                    if (newside.Item.Name == o.Item.Name)
                    {
                        found = true;
                        o.AddQty();
                        o.Item.Price = o.GetItemTotalAmount();
                        displayText.Text = $"{newside.Item.Name} added.\n";
                        break;
                    }
                }
                if (!found)
                {
                    newside.AddQty();
                    CartList.Add(newside);
                    displayText.Text = $"{newside.Item.Name} added.\n";
                }
                displayText.Text += $"Total: ${CalculateCartTotalPrice():0.00}\n\n" + WelcomeText();
                CheckBundle();
            }
            else if (itemsListView.ItemsSource == ValueMealsList)
            {
                OrderItem newvaluemeal = new OrderItem(ValueMealsList[itemsListView.SelectedIndex]);
                bool found = false;
                foreach (OrderItem o in CartList)
                {
                    if (newvaluemeal.Item.Name == o.Item.Name)
                    {
                        found = true;
                        o.AddQty();
                        o.Item.Price = o.GetItemTotalAmount();
                        displayText.Text = $"{newvaluemeal.Item.Name} added.\n";
                        break;
                    }
                }
                if (!found)
                {
                    newvaluemeal.AddQty();
                    CartList.Add(newvaluemeal);
                    displayText.Text = $"{newvaluemeal.Item.Name} added.\n";
                }
                displayText.Text += $"Total: ${CalculateCartTotalPrice():0.00}\n\n" + WelcomeText();
                CheckBundle();
            }
            //CheckBundle();
            //The chunk of code below displays what bundle exists with the following items in the cart.
            //try
            //{
            if (textveri != "" && CartList.Count >= 1)
            {
                //textveri = textveri.Substring(0, textveri.IndexOf('('));
                //textveri = textveri.Substring(0, textveri.IndexOf('\n'));
                displayText.Text += "\n\n" + textveri + " bundle exists.\nYou can save money by selecting the bundle from the Bundle Meals option.";
            }
            //}
            //catch
            //{
            //Null exception
            //}

            cartsListView.ItemsSource = null;
            cartsListView.ItemsSource = CartList;

        }
        //Confirm Order
        //A new Order object is created each time an order is confirmed.
        //If an order consists of items, the receipt number increases by 1.
        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            order.ItemList = new List<OrderItem>();
            order.OrderNo = receiptno;
            if (CartList.Count == 0)
            {
                displayText.Text = "No order to confirm!\n\n" + WelcomeText();
            }
            else
            {
                string productreceipt = "";
                receiptno++;
                displayText.Text = "Tom's Cafe\n\n" + "Receipt #" + $"{order.OrderNo:00000}" + "\n" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\n\n";
                foreach (OrderItem p in CartList)
                {
                    order.Add(p);
                    //For advanced bundlemeal receipt display
                    //This chunk of code settles the number of tabs depending on the length of the name of the bundle.
                    if (p.Item.ProductList.Count > 1) //Bundlemeal name length checking
                    {
                        int index = p.Item.Name.IndexOf('(');
                        if (p.Item.Name.Substring(0, index - 1).Length >= 15)
                        {
                            productreceipt += $"{p.Quantity}\t{p.Item.Name.Substring(0, index - 1)}\t\t${p.Item.Price:0.00}\n";
                        }
                        else
                        {
                            productreceipt += $"{p.Quantity}\t{p.Item.Name.Substring(0, index - 1)}\t\t\t${p.Item.Price:0.00}\n";
                        }
                        foreach (Product pr in p.Item.ProductList)
                        {
                            if (pr.Name == "Truffle Fries")
                            {
                                productreceipt += $"\t        Fries\n";
                            }
                            else
                            {
                                productreceipt += $"\t        {pr.Name}\n";
                            }

                        }
                    }
                    //This is for ala carte item. The word count and string length of each item in the order is checked. The appropriate amount of tabs is then given
                    else if (CheckWordCount(p.Item.Name) == 4)
                    {
                        productreceipt += $"{p.Quantity}\t{p.Item.Name}\t\t${p.Item.Price:0.00}\n";
                    }
                    else if (CheckWordCount(p.Item.Name) == 3 || p.Item.Name.Length >= 15)
                    {
                        productreceipt += $"{p.Quantity}\t{p.Item.Name}\t\t${p.Item.Price:0.00}\n";
                    }
                    else if (CheckWordCount(p.Item.Name) == 2 || p.Item.Name.Length >= 9)
                    {
                        productreceipt += $"{p.Quantity}\t{p.Item.Name}\t\t\t${p.Item.Price:0.00}\n";
                    }
                    else
                    {
                        productreceipt += $"{p.Quantity}\t{p.Item.Name}\t\t\t\t${p.Item.Price:0.00}\n";
                    }
                }
                displayText.Text += productreceipt + "\n\n";
                displayText.Text += $"Total\t\t\t\t\t${order.GetTotalAmt():0.00}";
                CartList.Clear();
                RefreshMenuLists();
                checkbundle.Clear();
                cartsListView.ItemsSource = null;
                cartsListView.ItemsSource = CartList;
                itemsListView.ItemsSource = null;
                textveri = "";
            }


        }
        //Cancel Button
        //Cancel only works if the cart has orderitems inside.
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (CartList.Count == 0) //When there are no items in the cart and you still press the cancel button
            {
                itemsListView.ItemsSource = null;
                itemsListView.ItemsSource = BundleMealList;
                displayText.Text = "There is nothing in your cart.\n\n" + WelcomeText();
            }
            else //If there are items to cancel then clear the CartList and all the other lists and initialise all the products
            {
                CartList.Clear();
                RefreshMenuLists();
                cartsListView.ItemsSource = null;
                cartsListView.ItemsSource = CartList;
                displayText.Text = "Your order has been cancelled\n\n" + WelcomeText();
            }
            checkbundle.Clear();
        }
        //Remove Item
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (displayText.Text.Contains("Receipt")) //This is when the order is confirmed and the removed button is pressed.
            {
                displayText.Text = "There is nothing in your cart.\n\n" + WelcomeText();
                itemsListView.ItemsSource = null;
                itemsListView.ItemsSource = BundleMealList;
            }
            else if (cartsListView.SelectedIndex == -1 && CartList.Count > 0)
            {
                displayText.Text = "Please select an item from the cart to remove";
            }
            else if (CartList.Count == 0) //In the event where there is no items in the cart
            {
                displayText.Text = "There is nothing in your cart.\n\n" + WelcomeText();
            }

            else if (CartList[cartsListView.SelectedIndex].RemoveQty() == true) //In the event where the quantity of the orderitem object is more than 1
            {
                if (CartList[cartsListView.SelectedIndex].Item.ProductList.Count > 1)
                {
                    double price = CartList[cartsListView.SelectedIndex].Item.Price;
                    CartList[cartsListView.SelectedIndex].Item.Price = (price / Convert.ToDouble(CartList[cartsListView.SelectedIndex].Quantity + 1)) * CartList[cartsListView.SelectedIndex].Quantity;
                    //We removed the quantity by one but the price has not been updated so we need to add one back to the quantity to 
                    //divide the price to get the price of one bundle meal and multiply by the current quantity.
                    displayText.Text = $"{CartList[cartsListView.SelectedIndex].Item.Name.Substring(0, CartList[cartsListView.SelectedIndex].Item.Name.IndexOf('(') - 1)} has been removed.\n";
                }
                else
                {
                    CartList[cartsListView.SelectedIndex].Item.Price = CartList[cartsListView.SelectedIndex].GetItemTotalAmount(); //Update the new price after the quantity is reduced by 1
                    displayText.Text = $"{CartList[cartsListView.SelectedIndex].Item.Name} has been removed.\n";
                }
                displayText.Text += $"Total: ${CalculateCartTotalPrice():0.00}\n\n" + WelcomeText(); //Displays pricing
                cartsListView.ItemsSource = null;
                cartsListView.ItemsSource = CartList;
            }
            else //In the event the quantity of the cart orderitem is 1
            {
                if (CartList[cartsListView.SelectedIndex].Item.ProductList.Count > 1)
                {
                    displayText.Text = $"{CartList[cartsListView.SelectedIndex].Item.Name.Substring(0, CartList[cartsListView.SelectedIndex].Item.Name.IndexOf('(') - 1)} has been removed.\n\n";
                }
                else
                {
                    displayText.Text = $"{CartList[cartsListView.SelectedIndex].Item.Name} has been removed.\n";
                }

                try //This is to eliminate the case if checkitems has no items inside.
                {
                    foreach (string i in checkbundle) //This code removes the ala carte item from the checkbundle list once the user removes an ala carte item that is part of a bundle meal
                    {
                        if (CartList[cartsListView.SelectedIndex].Item.Name == i)
                        {
                            checkbundle.Remove(i);
                        }
                    }
                }
                catch
                {
                    //prevents null reference error
                }
                CartList.Remove(CartList[cartsListView.SelectedIndex]); //Removes the OrderItem from the cart
                displayText.Text += $"Total: ${CalculateCartTotalPrice():0.00}\n\n"; //Shows pricing
                displayText.Text += WelcomeText();
                cartsListView.ItemsSource = null;
                cartsListView.ItemsSource = CartList;
            }
        }

        private void itemsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void displayText_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void cartsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
