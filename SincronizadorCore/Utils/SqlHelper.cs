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

		public static void InsertarOActualizarProducto(ProductoModel producto, string connectionString)
		{
			try
			{
				using var connection = new SqlConnection(connectionString);
				connection.Open();

				string existeQuery = "SELECT COUNT(*) FROM prods WHERE articulo = @articulo";
				using var cmdExiste = new SqlCommand(existeQuery, connection);
				cmdExiste.Parameters.AddWithValue("@articulo", producto.articulo);
				int count = (int)cmdExiste.ExecuteScalar();

				if (count > 0)
				{
					// UPDATE
					var updateCmd = new SqlCommand(@"
                UPDATE prods SET
                    descrip = @descrip,
                    marca = @marca,
                    linea = @linea,
                    precio1 = @precio,
                    impuesto = @impuesto
                WHERE articulo = @articulo", connection);

					updateCmd.Parameters.AddWithValue("@descrip", producto.descripcion);
					updateCmd.Parameters.AddWithValue("@marca", producto.marca ?? "SYS");
					updateCmd.Parameters.AddWithValue("@linea", producto.linea ?? "SYS");
					updateCmd.Parameters.AddWithValue("@precio", producto.precio1);
					updateCmd.Parameters.AddWithValue("@impuesto", producto.impuesto ?? "SYS");
					updateCmd.Parameters.AddWithValue("@articulo", producto.articulo);
					updateCmd.ExecuteNonQuery();

					LogService.WriteLog("Logs", $"[SQL] Producto actualizado: {producto.articulo}");
				}
				else
				{
					// INSERT
					var insertCmd = new SqlCommand(@"
                INSERT INTO prods (articulo, descrip, marca, linea, precio1, impuesto)
                VALUES (@articulo, @descrip, @marca, @linea, @precio, @impuesto)", connection);

					insertCmd.Parameters.AddWithValue("@articulo", producto.articulo);
					insertCmd.Parameters.AddWithValue("@descrip", producto.descripcion);
					insertCmd.Parameters.AddWithValue("@marca", producto.marca ?? "SYS");
					insertCmd.Parameters.AddWithValue("@linea", producto.linea ?? "SYS");
					insertCmd.Parameters.AddWithValue("@precio", producto.precio1);
					insertCmd.Parameters.AddWithValue("@impuesto", producto.impuesto ?? "SYS");
					insertCmd.ExecuteNonQuery();

					LogService.WriteLog("Logs", $"[SQL] Producto insertado: {producto.articulo}");
				}

				// Validar que la línea existe exactamente antes de actualizar/insertar producto
				var lineaVal = (producto.linea ?? "").Trim();
				var lineaQuery = "SELECT COUNT(*) FROM lineas WHERE RTRIM(LTRIM(Linea)) = @Linea";
				using var cmdLinea = new SqlCommand(lineaQuery, connection);
				cmdLinea.Parameters.AddWithValue("@Linea", lineaVal);
				int lineaCount = (int)cmdLinea.ExecuteScalar();
				if (lineaCount == 0)
				{
					var insertLineaCmd = new SqlCommand("INSERT INTO lineas (Linea) VALUES (@Linea)", connection);
					insertLineaCmd.Parameters.AddWithValue("@Linea", lineaVal);
					insertLineaCmd.ExecuteNonQuery();
					LogService.WriteLog("Logs", $"[SQL] Línea insertada: {lineaVal}");
				}
				
			}
			catch (Exception ex)
			{
				LogService.WriteLog("Logs", $"[SQL] Error con producto {producto.articulo}: {ex.Message}");
			}
		}

        public static void InsertarOActualizarLinea(LineaModel linea, string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            string existeQuery = "SELECT COUNT(*) FROM lineas WHERE Linea = @Linea";
            using var cmdExiste = new SqlCommand(existeQuery, connection);
            cmdExiste.Parameters.AddWithValue("@Linea", linea.Linea);
            int count = (int)cmdExiste.ExecuteScalar();
            if (count > 0)
            {
                var updateCmd = new SqlCommand("UPDATE lineas SET Descripcion = @Descripcion WHERE Linea = @Linea", connection);
                updateCmd.Parameters.AddWithValue("@Descripcion", linea.Descrip);
                updateCmd.Parameters.AddWithValue("@Linea", linea.Linea);
                updateCmd.ExecuteNonQuery();
            }
            else
            {
                var insertCmd = new SqlCommand("INSERT INTO lineas (Linea, Descripcion) VALUES (@Linea, @Descripcion)", connection);
                insertCmd.Parameters.AddWithValue("@Linea", linea.Linea);
                insertCmd.Parameters.AddWithValue("@Descripcion", linea.Descrip);
                insertCmd.ExecuteNonQuery();
            }
        }

        public static void InsertarOActualizarMarca(MarcaModel marca, string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            string existeQuery = "SELECT COUNT(*) FROM marcas WHERE Marca = @Marca";
            using var cmdExiste = new SqlCommand(existeQuery, connection);
            cmdExiste.Parameters.AddWithValue("@Marca", marca.Marca);
            int count = (int)cmdExiste.ExecuteScalar();
            if (count > 0)
            {
                var updateCmd = new SqlCommand("UPDATE marcas SET Descripcion = @Descripcion WHERE Marca = @Marca", connection);
                updateCmd.Parameters.AddWithValue("@Descripcion", marca.Descrip);
                updateCmd.Parameters.AddWithValue("@Marca", marca.Marca);
                updateCmd.ExecuteNonQuery();
            }
            else
            {
                var insertCmd = new SqlCommand("INSERT INTO marcas (Marca, Descripcion) VALUES (@Marca, @Descripcion)", connection);
                insertCmd.Parameters.AddWithValue("@Marca", marca.Marca);
                insertCmd.Parameters.AddWithValue("@Descripcion", marca.Descrip);
                insertCmd.ExecuteNonQuery();
            }
        }

        public static void InsertarOActualizarImpuesto(ImpuestoModel impuesto, string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            string existeQuery = "SELECT COUNT(*) FROM impuestos WHERE Impuesto = @Impuesto";
            using var cmdExiste = new SqlCommand(existeQuery, connection);
            cmdExiste.Parameters.AddWithValue("@Impuesto", impuesto.Impuesto);
            int count = (int)cmdExiste.ExecuteScalar();
            if (count > 0)
            {
                var updateCmd = new SqlCommand("UPDATE impuestos SET Valor = @Valor WHERE Impuesto = @Impuesto", connection);
                updateCmd.Parameters.AddWithValue("@Valor", impuesto.Valor);
                updateCmd.Parameters.AddWithValue("@Impuesto", impuesto.Impuesto);
                updateCmd.ExecuteNonQuery();
            }
            else
            {
                var insertCmd = new SqlCommand("INSERT INTO impuestos (Impuesto, Valor) VALUES (@Impuesto, @Valor)", connection);
                insertCmd.Parameters.AddWithValue("@Impuesto", impuesto.Impuesto);
                insertCmd.Parameters.AddWithValue("@Valor", impuesto.Valor);
                insertCmd.ExecuteNonQuery();
            }
        }

	}
}

