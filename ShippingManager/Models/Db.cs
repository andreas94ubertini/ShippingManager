using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Web;

namespace ShippingManager.Models
{
    public class Db
    {
        public static Users GetUserByUsername(string username)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Users where Username = @username", conn);
                cmd.Parameters.AddWithValue("username", username);
                SqlDataReader sqlDataReader;

                conn.Open();
                sqlDataReader = cmd.ExecuteReader();

                Users user = new Users();
                while (sqlDataReader.Read())
                {
                    user.IdUser = Convert.ToInt32(sqlDataReader["IdUser"]);
                    user.Username = sqlDataReader["Username"].ToString();
                    user.Psw = sqlDataReader["Psw"].ToString();
                    user.Role = sqlDataReader["Role"].ToString();
                }
                return user;
            }
            catch { }
            finally
            {
                conn.Close();

            }
            return null;

        }
        public static Customer GetCustomerById(int id)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Customers where IdCustomer = @id", conn);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataReader sqlDataReader;

                conn.Open();
                sqlDataReader = cmd.ExecuteReader();

                Customer c = new Customer();
                while (sqlDataReader.Read())
                {

                    c.IdCustomer = Convert.ToInt32(sqlDataReader["IdCustomer"]);
                    c.Name = sqlDataReader["Name"].ToString();
                    c.Surname = sqlDataReader["Surname"].ToString();
                    c.IsAzienda = Convert.ToBoolean(sqlDataReader["IsAzienda"]);
                    c.Cf = sqlDataReader["Cf"].ToString();
                    c.PIva = sqlDataReader["PIva"].ToString();


                }

                return c;
            }
            catch { }
            finally
            {
                conn.Close();

            }
            return null;

        }
        public static void NewCustomer(string Name, string Surname, bool IsAzienda, string Cf = "", string PIva = "")
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Customers VALUES(@Name,@Surname,@IsAzienda,@Cf,@PIva)";
                cmd.Parameters.AddWithValue("Name", Name);
                cmd.Parameters.AddWithValue("Surname", Surname);
                cmd.Parameters.AddWithValue("IsAzienda", IsAzienda);
                cmd.Parameters.AddWithValue("Cf", Cf);
                cmd.Parameters.AddWithValue("PIva", PIva);
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch
            {

            }
            finally
            {
                conn.Close();
            }
        }
        public static void NewShipping(DateTime ShippingDate, double Weight, string Destination, string Address, string Addressee, double Price, DateTime ExDelivery, int IdCustomer)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Shipments VALUES(@ShippingDate,@Weight,@Destination,@Address,@Addressee,@Price,@ExDelivery,@IdCustomer, @CurrentState)";
                cmd.Parameters.AddWithValue("ShippingDate", ShippingDate);
                cmd.Parameters.AddWithValue("Weight", Weight);
                cmd.Parameters.AddWithValue("Destination", Destination);
                cmd.Parameters.AddWithValue("Address", Address);
                cmd.Parameters.AddWithValue("Addressee", Addressee);
                cmd.Parameters.AddWithValue("Price", Price);
                cmd.Parameters.AddWithValue("ExDelivery", ExDelivery);
                cmd.Parameters.AddWithValue("IdCustomer", IdCustomer);
                cmd.Parameters.AddWithValue("CurrentState", "Spedizione Registrata");
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch
            {

            }
            finally
            {
                conn.Close();
            }
        }
        public static void NewUpdate(string State, string Location, string Description, DateTime Date, int IdShipping)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Updates VALUES(@State,@Location,@Description,@Date,@IdShipping)";
                cmd.Parameters.AddWithValue("State", State);
                cmd.Parameters.AddWithValue("Location", Location);
                cmd.Parameters.AddWithValue("Description", Description);
                cmd.Parameters.AddWithValue("Date", Date);
                cmd.Parameters.AddWithValue("IdShipping", IdShipping);

                int IsOk = cmd.ExecuteNonQuery();

            }

            catch
            {

            }
            finally
            {
                conn.Close();
            }
        }
        public static List<Update> GetShippingInfo(int IdShipping, string CustomerIdentify, bool IsAzienda)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            string queryPrivate = "Select IdUpdate, State, Location, Description, Date, Shipments.IdShipping, Shipments.ExDelivery, Customers.Cf, Customers.PIva from Updates as U Inner Join Shipments on U.IdShipping = Shipments.IdShipping inner join Customers on Shipments.IdCustomer = Customers.IdCustomer where Customers.Cf = @Cf and Shipments.IdShipping =@IdShipping order By Date Desc";
            string queryAzienda = "Select  IdUpdate, State, Location, Description, Date, Shipments.IdShipping, Shipments.ExDelivery, Customers.Cf, Customers.PIva from Updates as U Inner Join Shipments on U.IdShipping = Shipments.IdShipping inner join Customers on Shipments.IdCustomer = Customers.IdCustomer where Customers.PIva = @PIva and Shipments.IdShipping =@IdShipping order By Date Desc";
            SqlCommand cmd = new SqlCommand();
            if (IsAzienda == false)
            {

                cmd = new SqlCommand(queryPrivate, conn);
                cmd.Parameters.AddWithValue("Cf", CustomerIdentify);
                cmd.Parameters.AddWithValue("IdShipping", IdShipping);

            }
            else
            {
                cmd = new SqlCommand(queryAzienda, conn);
                cmd.Parameters.AddWithValue("PIva", CustomerIdentify);
                cmd.Parameters.AddWithValue("IdShipping", IdShipping);
            }

            SqlDataReader sqlDataReader;
            conn.Open();
            sqlDataReader = cmd.ExecuteReader();

            List<Update> updates = new List<Update>();
            while (sqlDataReader.Read())
            {
                Update u = new Update();
                u.IdUpdate = Convert.ToInt32(sqlDataReader["IdUpdate"]);
                u.State = sqlDataReader["State"].ToString();
                u.Location = sqlDataReader["Location"].ToString();
                u.Description = sqlDataReader["Description"].ToString();
                u.Date = Convert.ToDateTime(sqlDataReader["Date"]);
                u.IdShipping = Convert.ToInt32(sqlDataReader["IdShipping"]);
                updates.Add(u);
            }

            conn.Close();
            return updates;
        }
        public static Shipping GetShippingById(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from Shipments where IdShipping = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            SqlDataReader sqlDataReader;

            conn.Open();
            sqlDataReader = cmd.ExecuteReader();

            Shipping s = new Shipping();
            while (sqlDataReader.Read())
            {
                s.IdShipping = Convert.ToInt32(sqlDataReader["IdShipping"]);
                s.ShippingDate = Convert.ToDateTime(sqlDataReader["ShippingDate"]);
                s.Weight = Convert.ToDouble(sqlDataReader["Weight"]);
                s.Destination = sqlDataReader["Destination"].ToString();
                s.Address = sqlDataReader["Address"].ToString();
                s.Addressee = sqlDataReader["Addressee"].ToString();
                s.Price = Convert.ToDouble(sqlDataReader["Price"]);
                s.ExDelivery = Convert.ToDateTime(sqlDataReader["ExDelivery"]);
                s.IdCustomer = Convert.ToInt32(sqlDataReader["IdCustomer"]);
                s.State = sqlDataReader["CurrentState"].ToString();
            }

            conn.Close();
            return s;
        }
        public static List<Shipping> GetAllShipments()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from Shipments", conn);
            SqlDataReader sqlDataReader;

            conn.Open();
            sqlDataReader = cmd.ExecuteReader();

            List<Shipping> shipments = new List<Shipping>();
            while (sqlDataReader.Read())
            {
                Shipping s = new Shipping();
                s.IdShipping = Convert.ToInt32(sqlDataReader["IdShipping"]);
                s.ShippingDate = Convert.ToDateTime(sqlDataReader["ShippingDate"]);
                s.Weight = Convert.ToDouble(sqlDataReader["Weight"]);
                s.Destination = sqlDataReader["Destination"].ToString();
                s.Address = sqlDataReader["Address"].ToString();
                s.Addressee = sqlDataReader["Addressee"].ToString();
                s.Price = Convert.ToDouble(sqlDataReader["Price"]);
                s.ExDelivery = Convert.ToDateTime(sqlDataReader["ExDelivery"]);
                s.IdCustomer = Convert.ToInt32(sqlDataReader["IdCustomer"]);
                s.State = sqlDataReader["CurrentState"].ToString();
                shipments.Add(s);
            }

            conn.Close();
            return shipments;
        }
        public static void DeleteShipping(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Shipments where IdShipping=@id";
            cmd.Parameters.AddWithValue("id", id);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public static List<Customer> GetAllCustomers()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from Customers", conn);
            SqlDataReader sqlDataReader;

            conn.Open();
            sqlDataReader = cmd.ExecuteReader();

            List<Customer> customers = new List<Customer>();
            while (sqlDataReader.Read())
            {
                Customer c = new Customer();
                c.IdCustomer = Convert.ToInt32(sqlDataReader["IdCustomer"]);
                c.Name = sqlDataReader["Name"].ToString();
                c.Surname = sqlDataReader["Surname"].ToString();
                c.IsAzienda = Convert.ToBoolean(sqlDataReader["IsAzienda"]);
                c.Cf = sqlDataReader["Cf"].ToString();
                c.PIva = sqlDataReader["PIva"].ToString();
                customers.Add(c);

            }

            conn.Close();
            return customers;
        }
        public static List<ShippingOrderedByDest> GetAllShippingOrderedByDest()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select count(Destination) as totaleVerso, Destination from Shipments group by Destination", conn);
            SqlDataReader sqlDataReader;

            conn.Open();
            sqlDataReader = cmd.ExecuteReader();

            List<ShippingOrderedByDest> listOrdered = new List<ShippingOrderedByDest>();
            while (sqlDataReader.Read())
            {
                ShippingOrderedByDest l = new ShippingOrderedByDest();
                l.totaleVerso = Convert.ToInt32(sqlDataReader["totaleVerso"]);
                l.Destination = sqlDataReader["Destination"].ToString();
                listOrdered.Add(l);

            }

            conn.Close();
            return listOrdered;
        }
        public static List<Shipping> GetTotalOnDelivery()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * from Shipments where CurrentState='In Consegna'", conn);
            SqlDataReader sqlDataReader;
            conn.Open();
            sqlDataReader = cmd.ExecuteReader();

            List<Shipping> shipments = new List<Shipping>();
            while (sqlDataReader.Read())
            {
                Shipping s = new Shipping();
                s.IdShipping = Convert.ToInt32(sqlDataReader["IdShipping"]);
                s.ShippingDate = Convert.ToDateTime(sqlDataReader["ShippingDate"]);
                s.Weight = Convert.ToDouble(sqlDataReader["Weight"]);
                s.Destination = sqlDataReader["Destination"].ToString();
                s.Address = sqlDataReader["Address"].ToString();
                s.Addressee = sqlDataReader["Addressee"].ToString();
                s.Price = Convert.ToDouble(sqlDataReader["Price"]);
                s.ExDelivery = Convert.ToDateTime(sqlDataReader["ExDelivery"]);
                s.IdCustomer = Convert.ToInt32(sqlDataReader["IdCustomer"]);
                shipments.Add(s);
            }

            conn.Close();
            return shipments;
        }
        public static void SetShippingStatus(string state, int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update Shipments SET CurrentState=@state where IdShipping=@id";
            cmd.Parameters.AddWithValue("state", state);
            cmd.Parameters.AddWithValue("id", id);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public static List<Shipping> GetNotDelivered()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            string date = DateTime.Now.ToShortDateString();
            SqlCommand cmd = new SqlCommand("Select * from Shipments where not Shipments.CurrentState = 'Consegnato'", conn);
            SqlDataReader sqlDataReader;
            conn.Open();
            sqlDataReader = cmd.ExecuteReader();

            List<Shipping> shipments = new List<Shipping>();
            while (sqlDataReader.Read())
            {
                Shipping s = new Shipping();
                s.IdShipping = Convert.ToInt32(sqlDataReader["IdShipping"]);
                s.ShippingDate = Convert.ToDateTime(sqlDataReader["ShippingDate"]);
                s.Weight = Convert.ToDouble(sqlDataReader["Weight"]);
                s.Destination = sqlDataReader["Destination"].ToString();
                s.Address = sqlDataReader["Address"].ToString();
                s.Addressee = sqlDataReader["Addressee"].ToString();
                s.Price = Convert.ToDouble(sqlDataReader["Price"]);
                s.ExDelivery = Convert.ToDateTime(sqlDataReader["ExDelivery"]);
                s.IdCustomer = Convert.ToInt32(sqlDataReader["IdCustomer"]);
                shipments.Add(s);
            }

            conn.Close();
            return shipments;
        }
        public static void DeleteUpdate(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Updates where IdUpdate=@id";
            cmd.Parameters.AddWithValue("id", id);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public static void DeleteCustomer(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Customers where IdCustomer=@id";
            cmd.Parameters.AddWithValue("id", id);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}