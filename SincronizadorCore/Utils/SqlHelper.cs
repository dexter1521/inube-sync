using System;
using System.Collections.Generic;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using SincronizadorCore.Models;
using SincronizadorCore.Utils;


namespace SincronizadorCore.Utils
{
	public static class SqlHelper
	{
		public static List<ProductoLocal> ObtenerProductos(string connectionString)
		{
			var productos = new List<ProductoLocal>();

			try
			{
				using var connection = new SqlConnection(connectionString);
				connection.Open();

				var query = @"
                    SELECT articulo, descrip, linea, marca, impuesto, ubicacion,
                           precio1, precio2, precio3, precio4, precio5,
                           c2, c3, c4, c5, u1, u2, u3, u4, u5
                    FROM prods";

				using var command = new SqlCommand(query, connection);
				using var reader = command.ExecuteReader();

				while (reader.Read())
				{
					productos.Add(new ProductoLocal
					{
						articulo = reader["articulo"]?.ToString() ?? "",
						descrip = reader["descrip"]?.ToString() ?? "",
						linea = Convert.ToInt32(reader["linea"]),
						marca = Convert.ToInt32(reader["marca"]),
						impuesto = Convert.ToInt32(reader["impuesto"]),
						ubicacion = reader["ubicacion"]?.ToString() ?? "",
						precio1 = Convert.ToDecimal(reader["precio1"]),
						precio2 = Convert.ToDecimal(reader["precio2"]),
						precio3 = Convert.ToDecimal(reader["precio3"]),
						precio4 = Convert.ToDecimal(reader["precio4"]),
						precio5 = Convert.ToDecimal(reader["precio5"]),
						c2 = reader["c2"]?.ToString() ?? "",
						c3 = reader["c3"]?.ToString() ?? "",
						c4 = reader["c4"]?.ToString() ?? "",
						c5 = reader["c5"]?.ToString() ?? "",
						u1 = reader["u1"]?.ToString() ?? "",
						u2 = reader["u2"]?.ToString() ?? "",
						u3 = reader["u3"]?.ToString() ?? "",
						u4 = reader["u4"]?.ToString() ?? "",
						u5 = reader["u5"]?.ToString() ?? ""
					});
				}
			}
			catch (Exception ex)
			{
				LogService.WriteLog("Logs", $"[SqlHelper] Error leyendo productos: {ex.Message}");
			}

			return productos;
		}
	}
}

