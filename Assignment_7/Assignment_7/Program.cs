using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7
{
    internal class Program
    {
        static string connectionString = "server=DESKTOP-MFQ8M0P;database=LibraryDB;trusted_connection=true;";

        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Library Book Inventory\n");
                    Console.WriteLine("Options:");
                    Console.WriteLine("1. Display Book Inventory");
                    Console.WriteLine("2. Add New Book");
                    Console.WriteLine("3. Update Book Quantity");
                    Console.WriteLine("4. Exit");
                    Console.Write("Select an option: ");
                    string option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            DisplayBookInventory();
                            break;
                        case "2":
                            AddNewBook();
                            break;
                        case "3":
                            UpdateBookQuantity();
                            break;
                        case "4":
                            Console.WriteLine("Exiting the application.");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please select a valid option.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        static void DisplayBookInventory()
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Books";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Books");

                DataTable booksTable = dataSet.Tables["Books"];

                Console.WriteLine("Book Inventory:\n");
                Console.WriteLine("{0,-5} {1,-25} {2,-20} {3,-15} {4,-10}", "ID", "Title", "Author", "Genre", "Quantity");
                Console.WriteLine(new string('-', 75));

                foreach (DataRow row in booksTable.Rows)
                {
                    Console.WriteLine("{0,-5} {1,-25} {2,-20} {3,-15} {4,-10}",
                                      row["BookId"], row["Title"], row["Author"], row["Genre"], row["Quantity"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while displaying inventory: " + ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        static void AddNewBook()
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                Console.Write("Enter BookId: ");
                int bookId = int.Parse(Console.ReadLine());

                Console.Write("Enter Title: ");
                string title = Console.ReadLine();

                Console.Write("Enter Author: ");
                string author = Console.ReadLine();

                Console.Write("Enter Genre: ");
                string genre = Console.ReadLine();

                Console.Write("Enter Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                string query = "INSERT INTO Books (BookId,Title, Author, Genre, Quantity) " +
                               "VALUES (@BookId,@Title, @Author, @Genre, @Quantity)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookId", bookId);

                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Author", author);
                    command.Parameters.AddWithValue("@Genre", genre);
                    command.Parameters.AddWithValue("@Quantity", quantity);

                    command.ExecuteNonQuery();
                    Console.WriteLine("New book added successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding a new book: " + ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        static void UpdateBookQuantity()
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                Console.Write("Enter Title of the book to update quantity: ");
                string title = Console.ReadLine();

                Console.Write("Enter new Quantity: ");
                int newQuantity = int.Parse(Console.ReadLine());

                string query = "UPDATE Books SET Quantity = @Quantity WHERE Title = @Title";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", newQuantity);
                    command.Parameters.AddWithValue("@Title", title);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Book quantity updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("No book found with the specified title.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating book quantity: " + ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}
