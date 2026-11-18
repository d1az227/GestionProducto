# GestionProductos — ADO.NET con SQLite

Programa de consola en C# (.NET 8) que demuestra el acceso a datos con **ADO.NET** y **SQLite**
mediante el paquete `Microsoft.Data.Sqlite`.

Actividad de la asignatura *Programación en C# con .NET* — Semana 12.

## Qué hace

1. Crea (si no existe) la base de datos `productos.db`.
2. Abre una conexión con `SqliteConnection`.
3. Crea la tabla `Productos` (Id, Nombre, Categoria, Precio, Stock).
4. Inserta 4 productos con `SqliteCommand` y parámetros.
5. Consulta y muestra los registros con `SqliteDataReader`.
6. Usa `using` para liberar conexión, comandos y lector.

## Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) o superior
- Conexión a internet la primera vez (para restaurar el paquete NuGet)

## Cómo ejecutarlo

```bash
git clone <URL-DEL-REPOSITORIO>
cd GestionProductos
dotnet restore
dotnet run
```

El archivo `productos.db` se crea en la carpeta desde la que se ejecuta el programa.
Cada ejecución limpia la tabla y vuelve a insertar los datos, así el resultado es siempre el mismo.

## Salida esperada

```
=== Registro de productos con ADO.NET y SQLite ===

Conexión abierta con productos.db

Tabla 'Productos' lista (5 columnas).

Insertando registros...
  + Teclado mecánico (filas afectadas: 1)
  ...

Productos almacenados:
Id  Nombre                  Categoría         Precio   Stock
...
```

## Estructura

```
GestionProductos/
├── GestionProductos.csproj
├── Program.cs
├── README.md
└── .gitignore
```

## Autor

<TU NOMBRE>
