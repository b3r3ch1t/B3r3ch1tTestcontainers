using MySql.Data.MySqlClient;

public class CustomerService
{
    private readonly string _connectionString;

    public CustomerService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void CreateTable()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS Customers (Id INT, Name VARCHAR(100));";
        command.ExecuteNonQuery();
    }

    public void AddCustomer(int id, string name)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"INSERT INTO Customers (Id, Name) VALUES ({id}, '{name}');";
        command.ExecuteNonQuery();
    }

    public void UpdateCustomer(int id, string name)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"UPDATE Customers SET Name = '{name}' WHERE Id = {id};";
        command.ExecuteNonQuery();
    }

    public void DeleteCustomer(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"DELETE FROM Customers WHERE Id = {id};";
        command.ExecuteNonQuery();
    }

    public List<string> GetCustomers()
    {
        var customers = new List<string>();
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Name FROM Customers;";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            customers.Add(reader.GetString(0));
        }
        return customers;
    }
}
