using Microsoft.Data.Sqlite;

namespace GestionProductos;

internal static class Program
{
    // Archivo de la base de datos: se crea solo si no existe.
    private const string CadenaConexion = "Data Source=productos.db";

    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Registro de productos con ADO.NET y SQLite ===\n");

        // 1. Conexión: 'using' garantiza que se cierre al terminar el bloque.
        using var conexion = new SqliteConnection(CadenaConexion);
        conexion.Open();
        Console.WriteLine("Conexión abierta con productos.db\n");

        CrearTabla(conexion);
        LimpiarTabla(conexion); // Para que cada ejecución muestre el mismo resultado
        InsertarProductos(conexion);
        MostrarProductos(conexion);

        Console.WriteLine("\nPrograma finalizado.");
    }

    private static void CrearTabla(SqliteConnection conexion)
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS Productos (
                Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombre    TEXT    NOT NULL,
                Categoria TEXT    NOT NULL,
                Precio    REAL    NOT NULL,
                Stock     INTEGER NOT NULL
            );";

        using var comando = new SqliteCommand(sql, conexion);
        comando.ExecuteNonQuery();
        Console.WriteLine("Tabla 'Productos' lista (5 columnas).");
    }

    private static void LimpiarTabla(SqliteConnection conexion)
    {
        using var comando = new SqliteCommand("DELETE FROM Productos;", conexion);
        comando.ExecuteNonQuery();
        // Reinicia el contador del Id autoincremental.
        using var reinicio = new SqliteCommand(
            "DELETE FROM sqlite_sequence WHERE name = 'Productos';", conexion);
        reinicio.ExecuteNonQuery();
    }

    private static void InsertarProductos(SqliteConnection conexion)
    {
        var productos = new (string Nombre, string Categoria, double Precio, int Stock)[]
        {
            ("Teclado mecánico",  "Periféricos", 189000, 25),
            ("Mouse inalámbrico", "Periféricos",  75000, 40),
            ("Monitor 24 pulgadas", "Pantallas",  620000, 12),
            ("Audífonos Bluetooth", "Audio",     145000, 30)
        };

        const string sql = @"
            INSERT INTO Productos (Nombre, Categoria, Precio, Stock)
            VALUES (@nombre, @categoria, @precio, @stock);";

        Console.WriteLine("\nInsertando registros...");

        foreach (var p in productos)
        {
            using var comando = new SqliteCommand(sql, conexion);

            // Parámetros: separan el SQL de los datos y evitan inyección SQL.
            comando.Parameters.AddWithValue("@nombre", p.Nombre);
            comando.Parameters.AddWithValue("@categoria", p.Categoria);
            comando.Parameters.AddWithValue("@precio", p.Precio);
            comando.Parameters.AddWithValue("@stock", p.Stock);

            int filas = comando.ExecuteNonQuery();
            Console.WriteLine($"  + {p.Nombre} (filas afectadas: {filas})");
        }
    }

    private static void MostrarProductos(SqliteConnection conexion)
    {
        const string sql = "SELECT Id, Nombre, Categoria, Precio, Stock FROM Productos ORDER BY Id;";

        using var comando = new SqliteCommand(sql, conexion);
        using SqliteDataReader lector = comando.ExecuteReader();

        Console.WriteLine("\nProductos almacenados:");
        Console.WriteLine(new string('-', 78));
        Console.WriteLine($"{"Id",-4}{"Nombre",-24}{"Categoría",-14}{"Precio",14}{"Stock",8}");
        Console.WriteLine(new string('-', 78));

        // Read() avanza fila por fila hasta que no quedan más registros.
        while (lector.Read())
        {
            int id = lector.GetInt32(0);
            string nombre = lector.GetString(1);
            string categoria = lector.GetString(2);
            double precio = lector.GetDouble(3);
            int stock = lector.GetInt32(4);

            Console.WriteLine($"{id,-4}{nombre,-24}{categoria,-14}{precio,14:C0}{stock,8}");
        }

        Console.WriteLine(new string('-', 78));
    }
}
