using System;
using System.Data.SqlClient;

namespace Assesment_SQLQuery6
{
    internal class Program
    {
        private static SqlDataReader reader;
        private static SqlCommand cmd;
        private static SqlConnection con;
        private static string conStr = "server=SANDZONE;database=ProductInventoryDb;trusted_connection=true;";

        private static void Main(string[] args)
        {
            try
            {
                con = new SqlConnection(conStr);
                con.Open();

                Console.WriteLine("Product Inventory System");
                Console.WriteLine("1. View Product Inventory");
                Console.WriteLine("2. Add New Product");
                Console.WriteLine("3. Update Product Quantity");
                Console.WriteLine("4. Remove Product");
                Console.Write("Enter your choice: ");
                int op = int.Parse((Console.ReadLine()));

                switch (op)
                {
                    case 1:
                        ViewProductInventory();
                        break;
                    case 2:
                        AddNewProduct();
                        break;
                    case 3:
                        UpdateProductQuantity();
                        break;
                    case 4:
                        RemoveProduct();
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!!" + ex.Message);
            }
            finally
            {
                con.Close();
                Console.ReadKey();
            }
        }

        private static void ViewProductInventory()
        {
            cmd = new SqlCommand("select * from Products", con);

            reader = cmd.ExecuteReader();
            Console.WriteLine("Product ID \t Product Name \t Price \tQuantity \t MFDate \t     ExpDate ");
            while (reader.Read())
            {
                Console.Write(reader["ProductId"] + "\t\t");
                Console.Write(reader["ProductName"] + "\t");
                Console.Write(reader["Price"] + "\t");
                Console.Write(reader["Quantity"] + "\t");
                Console.Write(reader["MFDate"] + "\t");
                Console.Write(reader["ExpDate"] + "\t");
                Console.WriteLine("\n");
            }
            reader.Close();
        }

        private static void AddNewProduct()
        {
            Console.Write("Input Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Input Price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Input Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input MFDate (yyyy-mm-dd): ");
            DateTime mfDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Input ExpDate (yyyy-mm-dd): ");
            DateTime expDate = DateTime.Parse(Console.ReadLine());

            cmd = new SqlCommand("insert into Products (ProductName, Price, Quantity, MFDate, ExpDate) values (@proname, @price, @qty, @mfdate, @expdate)", con);
            cmd.Parameters.AddWithValue("@proname", productName);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@qty", quantity);
            cmd.Parameters.AddWithValue("@mfdate", mfDate);
            cmd.Parameters.AddWithValue("@expdate", expDate);

            int HasRows = cmd.ExecuteNonQuery();
            if (HasRows > 0)
            {
                Console.WriteLine("Product added successfully!");
            }
            else
            {
                Console.WriteLine("Failed to add product.");
            }
        }

        private static void UpdateProductQuantity()
        {
            Console.Write("Input Product ID: ");
            int productId = int.Parse((Console.ReadLine()));

            Console.Write("Input New Quantity: ");
            int newQty = int.Parse((Console.ReadLine()));

            cmd = new SqlCommand("update Products set Quantity = @qty where ProductId = @pid", con);
            cmd.Parameters.AddWithValue("@qty", newQty);
            cmd.Parameters.AddWithValue("@pid", productId);

            int HasRows = cmd.ExecuteNonQuery();
            if (HasRows > 0)
            {
                Console.WriteLine("Product quantity updated successfully!");
            }
            else
            {
                Console.WriteLine("Failed to update product quantity.");
            }
        }

        private static void RemoveProduct()
        {
            Console.Write("Input Product ID: ");
            int productId = int.Parse((Console.ReadLine()));

            cmd = new SqlCommand("delete from Products where ProductId = @pid", con);
            cmd.Parameters.AddWithValue("@pid", productId);

            int HasRows = cmd.ExecuteNonQuery();
            if (HasRows > 0)
            {
                Console.WriteLine("Product removed successfully!");
            }
            else
            {
                Console.WriteLine("Failed to remove product.");
            }
        }
    }
}