/* #############################
 * 
 * Author: Johnathon Mc Grory
 * Date :
 * Description : complete streamreading and writing guide
 * 
 * ############################# */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabSeven
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities db = new NORTHWNDEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        //exercise 1
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        group c by c.Country into g
                        orderby g.Count() descending
                        select new
                        {
                            Country = g.Key,
                            Count = g.Count()
                        };

            dvgEx1.ItemsSource = query.ToList();
        }
        //exercise 2
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        where c.Country == "Italy"
                        orderby c.CompanyName
                        select new
                        {
                            c.CompanyName,
                            c.Phone
                        };

            dvdQ2.ItemsSource = query.ToList();
        }

        //exercise 3
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var query = from p in db.Products
                        where (p.UnitsInStock - p.UnitsOnOrder > 0)
                        select new
                        {
                            Product = p.ProductName,
                            Available = p.UnitsInStock - p.UnitsOnOrder
                        };

            dvgQ3.ItemsSource = query.ToList();
        }

        //exercise 4
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var query = from od in db.Order_Details
                        orderby od.Product.ProductName
                        where od.Discount > 0
                        select new
                        {
                            ProductName = od.Product.ProductName,
                            DiscountGiven = od.Discount,
                            OrderId = od.OrderID
                        };
            dvgQ4.ItemsSource = query.ToList();
        }

        //exercise 5
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var query = from o in db.Orders
                        select o.Freight;

            //var query1 = db.Orders.Sum(o => o.Freight); - could use this too

            tblkQ5.Text = string.Format("the total value of freight for all order is {0:C}", query.Sum());
        }

        //exercise 6
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var query = from p in db.Products
                        orderby p.Category.CategoryName, p.UnitPrice descending
                        select new
                        {
                            p.CategoryID,
                            p.Category.CategoryName,
                            p.ProductName,
                            p.UnitPrice
                        };
            dgQ6.ItemsSource = query.ToList();
        }
        //exercise 7
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            var query = from o in db.Orders
                        group o by o.CustomerID into g
                        orderby g.Count() descending
                        select new
                        {
                            CustomerID = g.Key,
                            NumberOfOrders = g.Count()
                        };

            dgQ7.ItemsSource = query.ToList();
        }

        //exercise 8
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            var query = from o in db.Orders
                        group o by o.CustomerID into g
                        join c in db.Customers on g.Key equals c.CustomerID
                        orderby g.Count() descending
                        select new
                        {
                            CustomerID = c.CustomerID,
                            CompanyName = c.CompanyName,
                            NumberOfOrders = c.Orders.Count
                        };
            //.Take(10); means just the top ten rows
            dgQ8.ItemsSource = query.ToList().Take(10);

        }

        //exercise 9
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        where c.Orders.Count == 0
                        select new
                        {
                            CompanyID = c.CustomerID,
                            CompanyName = c.CompanyName,
                            NumberOfOrders = c.Orders.Count
                        };

            dgQ9.ItemsSource = query.ToList();
        }
    }
}
