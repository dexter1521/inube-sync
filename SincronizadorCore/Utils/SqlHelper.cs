
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SincronizadorCore.Models;


namespace SincronizadorCore.Utils
{
	public static class SqlHelper
	{
		// Marcar productos como exportados
		public static void MarcarProductosComoExportados(List<string> articulos, string connectionString)
		{
			if (articulos == null || articulos.Count == 0)
				return;
			using var connection = new SqlConnection(connectionString);
			connection.Open();
			foreach (var articulo in articulos)
			{
				var cmd = new SqlCommand("UPDATE prods SET exportado = 1 WHERE articulo = @articulo", connection);
				cmd.Parameters.AddWithValue("@articulo", articulo);
				cmd.ExecuteNonQuery();
			}
		}
		// Obtener todos los productos locales
		public static List<ProductoModel> ObtenerTodosProductos(string connectionString)
		{
			var productos = new List<ProductoModel>();
			using var connection = new SqlConnection(connectionString);
			connection.Open();
			var cmd = new SqlCommand("SELECT * FROM prods WHERE exportado = 0", connection);
			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				var producto = new ProductoModel
				{
					articulo = reader["articulo"].ToString() ?? string.Empty,
					descripcion = reader["descrip"].ToString() ?? string.Empty,
					marca = reader["marca"].ToString() ?? "SYS",
					linea = reader["linea"].ToString() ?? "SYS",
					unidad = reader["unidad"].ToString() ?? string.Empty,
					impuesto = reader["impuesto"].ToString() ?? string.Empty,
					costoultimo = reader["costo_u"] != DBNull.Value ? Convert.ToDecimal(reader["costo_u"]) : 0,
					precio1 = reader["precio1"] != DBNull.Value ? Convert.ToDecimal(reader["precio1"]) : 0,
					precio2 = reader["precio2"] != DBNull.Value ? Convert.ToDecimal(reader["precio2"]) : 0,
					precio3 = reader["precio3"] != DBNull.Value ? Convert.ToDecimal(reader["precio3"]) : 0,
					precio4 = reader["precio4"] != DBNull.Value ? Convert.ToDecimal(reader["precio4"]) : 0,
					precio5 = reader["precio5"] != DBNull.Value ? Convert.ToDecimal(reader["precio5"]) : 0,
					precio6 = reader["precio6"] != DBNull.Value ? Convert.ToDecimal(reader["precio6"]) : 0,
					precio7 = reader["precio7"] != DBNull.Value ? Convert.ToDecimal(reader["precio7"]) : 0,
					precio8 = reader["precio8"] != DBNull.Value ? Convert.ToDecimal(reader["precio8"]) : 0,
					precio9 = reader["precio9"] != DBNull.Value ? Convert.ToDecimal(reader["precio9"]) : 0,
					precio10 = reader["precio10"] != DBNull.Value ? Convert.ToDecimal(reader["precio10"]) : 0,
					u1 = reader["u1"] != DBNull.Value ? Convert.ToDecimal(reader["u1"]) : 0,
					u2 = reader["u2"] != DBNull.Value ? Convert.ToDecimal(reader["u2"]) : 0,
					u3 = reader["u3"] != DBNull.Value ? Convert.ToDecimal(reader["u3"]) : 0,
					u4 = reader["u4"] != DBNull.Value ? Convert.ToDecimal(reader["u4"]) : 0,
					u5 = reader["u5"] != DBNull.Value ? Convert.ToDecimal(reader["u5"]) : 0,
					u6 = reader["u6"] != DBNull.Value ? Convert.ToDecimal(reader["u6"]) : 0,
					u7 = reader["u7"] != DBNull.Value ? Convert.ToDecimal(reader["u7"]) : 0,
					u8 = reader["u8"] != DBNull.Value ? Convert.ToDecimal(reader["u8"]) : 0,
					u9 = reader["u9"] != DBNull.Value ? Convert.ToDecimal(reader["u9"]) : 0,
					u10 = reader["u10"] != DBNull.Value ? Convert.ToDecimal(reader["u10"]) : 0,
					c2 = reader["c2"] != DBNull.Value ? Convert.ToDecimal(reader["c2"]) : 0,
					c3 = reader["c3"] != DBNull.Value ? Convert.ToDecimal(reader["c3"]) : 0,
					c4 = reader["c4"] != DBNull.Value ? Convert.ToDecimal(reader["c4"]) : 0,
					c5 = reader["c5"] != DBNull.Value ? Convert.ToDecimal(reader["c5"]) : 0,
					c6 = reader["c6"] != DBNull.Value ? Convert.ToDecimal(reader["c6"]) : 0,
					c7 = reader["c7"] != DBNull.Value ? Convert.ToDecimal(reader["c7"]) : 0,
					c8 = reader["c8"] != DBNull.Value ? Convert.ToDecimal(reader["c8"]) : 0,
					c9 = reader["c9"] != DBNull.Value ? Convert.ToDecimal(reader["c9"]) : 0,
					c10 = reader["c10"] != DBNull.Value ? Convert.ToDecimal(reader["c10"]) : 0,
					paraventa = reader["paraventa"] as int? ?? 1,
					invent = reader["invent"] as int? ?? 1,
					granel = reader["granel"] as int? ?? 0,
					bajocosto = reader["bajocosto"] as int? ?? 0,
					speso = reader["speso"] as int? ?? 0
				};
				productos.Add(producto);
			}
			return productos;
		}

		// Obtener todas las líneas locales no exportadas
		public static List<LineaModel> ObtenerLineasNoExportadas(string connectionString)
		{
			var lineas = new List<LineaModel>();
			using var connection = new SqlConnection(connectionString);
			connection.Open();
			var cmd = new SqlCommand("SELECT * FROM lineas WHERE exportado = 0", connection);
			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				var linea = new LineaModel
				{
					Linea = reader["Linea"].ToString() ?? string.Empty,
					Descrip = reader["Descrip"].ToString() ?? string.Empty
				};
				lineas.Add(linea);
			}
			return lineas;
		}

		// Marcar líneas como exportadas
		public static void MarcarLineasComoExportadas(List<string> lineas, string connectionString)
		{
			if (lineas == null || lineas.Count == 0)
				return;
			using var connection = new SqlConnection(connectionString);
			connection.Open();
			foreach (var linea in lineas)
			{
				var cmd = new SqlCommand("UPDATE lineas SET exportado = 1 WHERE linea = @linea", connection);
				cmd.Parameters.AddWithValue("@linea", linea);
				int rows = cmd.ExecuteNonQuery();
				if (rows == 0)
				{
					LogService.WriteLog("Logs", $"[WARN] No se pudo marcar como exportada la línea: {linea}");
				}
			}
		}

		// Obtener todas las marcas locales no exportadas
		public static List<MarcaModel> ObtenerMarcasNoExportadas(string connectionString)
		{
			var marcas = new List<MarcaModel>();
			using var connection = new SqlConnection(connectionString);
			connection.Open();
			var cmd = new SqlCommand("SELECT * FROM marcas WHERE exportado = 0", connection);
			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				var marca = new MarcaModel
				{
					Marca = reader["Marca"].ToString() ?? string.Empty,
					Descrip = reader["Descrip"].ToString() ?? string.Empty
				};
				marcas.Add(marca);
			}
			return marcas;
		}

		// Marcar marcas como exportadas
		public static void MarcarMarcasComoExportadas(List<string> marcas, string connectionString)
		{
			if (marcas == null || marcas.Count == 0)
				return;
			using var connection = new SqlConnection(connectionString);
			connection.Open();
			foreach (var marca in marcas)
			{
				var cmd = new SqlCommand("UPDATE marcas SET exportado = 1 WHERE marca = @marca", connection);
				cmd.Parameters.AddWithValue("@marca", marca);
				int rows = cmd.ExecuteNonQuery();
				if (rows == 0)
				{
					LogService.WriteLog("Logs", $"[WARN] No se pudo marcar como exportada la marca: {marca}");
				}
			}
		}

		// Obtener todos los impuestos locales
		public static List<ImpuestoModel> ObtenerTodosImpuestos(string connectionString)
		{
			var impuestos = new List<ImpuestoModel>();
			using var connection = new SqlConnection(connectionString);
			connection.Open();
			var cmd = new SqlCommand("SELECT * FROM impuestos", connection);
			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				var impuesto = new ImpuestoModel
				{
					Impuesto = reader["Impuesto"].ToString() ?? string.Empty,
					Valor = reader["Valor"] as decimal? ?? 0
				};
				impuestos.Add(impuesto);
			}
			return impuestos;
		}

		// Comienza logical para insertar o actualizar productos desde la API
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
					costo_u = @costoultimo,
					precio1 = @precio1,
					precio2 = @precio2,
					precio3 = @precio3,
					precio4 = @precio4,
					precio5 = @precio5,
					precio6 = @precio6,
					precio7 = @precio7,
					precio8 = @precio8,
					precio9 = @precio9,
					precio10 = @precio10,
					u1 = @u1,
					u2 = @u2,
					u3 = @u3,
					u4 = @u4,
					u5 = @u5,
					u6 = @u6,
					u7 = @u7,
					u8 = @u8,
					u9 = @u9,
					u10 = @u10,
					ubicacion = @ubicacion,
					unidad = @unidad,
					bloqueado = @bloqueado,
					impuesto = @impuesto,
					fabricante = @fabricante,
					claveprodserv = @claveprodserv,
					claveunidad = @claveunidad,
					c2 = @c2,
					c3 = @c3,
					c4 = @c4,
					c5 = @c5,
					c6 = @c6,
					c7 = @c7,
					c8 = @c8,
					c9 = @c9,
					c10 = @c10,
					paraventa = @paraventa,
					invent = @invent,
					granel = @granel,
					bajocosto = @bajocosto,
					speso = @speso
				WHERE articulo = @articulo", connection);

					updateCmd.Parameters.AddWithValue("@descrip", producto.descripcion);
					updateCmd.Parameters.AddWithValue("@marca", producto.marca ?? "SYS");
					updateCmd.Parameters.AddWithValue("@linea", producto.linea ?? "SYS");
					updateCmd.Parameters.AddWithValue("@costoultimo", producto.costoultimo);
					updateCmd.Parameters.AddWithValue("@precio1", producto.precio1);
					updateCmd.Parameters.AddWithValue("@precio2", producto.precio2);
					updateCmd.Parameters.AddWithValue("@precio3", producto.precio3);
					updateCmd.Parameters.AddWithValue("@precio4", producto.precio4);
					updateCmd.Parameters.AddWithValue("@precio5", producto.precio5);
					updateCmd.Parameters.AddWithValue("@precio6", producto.precio6);
					updateCmd.Parameters.AddWithValue("@precio7", producto.precio7);
					updateCmd.Parameters.AddWithValue("@precio8", producto.precio8);
					updateCmd.Parameters.AddWithValue("@precio9", producto.precio9);
					updateCmd.Parameters.AddWithValue("@precio10", producto.precio10);
					updateCmd.Parameters.AddWithValue("@u1", producto.u1);
					updateCmd.Parameters.AddWithValue("@u2", producto.u2);
					updateCmd.Parameters.AddWithValue("@u3", producto.u3);
					updateCmd.Parameters.AddWithValue("@u4", producto.u4);
					updateCmd.Parameters.AddWithValue("@u5", producto.u5);
					updateCmd.Parameters.AddWithValue("@u6", producto.u6);
					updateCmd.Parameters.AddWithValue("@u7", producto.u7);
					updateCmd.Parameters.AddWithValue("@u8", producto.u8);
					updateCmd.Parameters.AddWithValue("@u9", producto.u9);
					updateCmd.Parameters.AddWithValue("@u10", producto.u10);
					updateCmd.Parameters.AddWithValue("@ubicacion", producto.ubicacion ?? "");
					updateCmd.Parameters.AddWithValue("@unidad", producto.unidad ?? "");
					updateCmd.Parameters.AddWithValue("@bloqueado", producto.bloqueado);
					updateCmd.Parameters.AddWithValue("@impuesto", producto.impuesto ?? "SYS");
					updateCmd.Parameters.AddWithValue("@fabricante", producto.fabricante ?? "SYS");
					updateCmd.Parameters.AddWithValue("@claveprodserv", producto.claveprodserv ?? "");
					updateCmd.Parameters.AddWithValue("@claveunidad", producto.claveunidad ?? "");
					updateCmd.Parameters.AddWithValue("@c2", producto.c2);
					updateCmd.Parameters.AddWithValue("@c3", producto.c3);
					updateCmd.Parameters.AddWithValue("@c4", producto.c4);
					updateCmd.Parameters.AddWithValue("@c5", producto.c5);
					updateCmd.Parameters.AddWithValue("@c6", producto.c6);
					updateCmd.Parameters.AddWithValue("@c7", producto.c7);
					updateCmd.Parameters.AddWithValue("@c8", producto.c8);
					updateCmd.Parameters.AddWithValue("@c9", producto.c9);
					updateCmd.Parameters.AddWithValue("@c10", producto.c10);
					updateCmd.Parameters.AddWithValue("@paraventa", producto.paraventa);
					updateCmd.Parameters.AddWithValue("@invent", producto.invent);
					updateCmd.Parameters.AddWithValue("@granel", producto.granel);
					updateCmd.Parameters.AddWithValue("@bajocosto", producto.bajocosto);
					updateCmd.Parameters.AddWithValue("@speso", producto.speso);
					updateCmd.Parameters.AddWithValue("@articulo", producto.articulo);
					updateCmd.ExecuteNonQuery();

					LogService.WriteLog("Logs", $"[SQL] Producto actualizado: Clave={producto.articulo}, Descripción={producto.descripcion}");
				}
				else
				{
					// INSERT
					var insertCmd = new SqlCommand(@"
				INSERT INTO prods (
					articulo, descrip, marca, linea, costo_u, precio1, precio2, precio3, precio4, precio5, precio6, precio7, precio8, precio9, precio10,
					u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, ubicacion, unidad, bloqueado, impuesto, fabricante, claveprodserv, claveunidad,
					c2, c3, c4, c5, c6, c7, c8, c9, c10, paraventa, invent, granel, bajocosto, speso
				) VALUES (
					@articulo, @descrip, @marca, @linea, @costoultimo, @precio1, @precio2, @precio3, @precio4, @precio5, @precio6, @precio7, @precio8, @precio9, @precio10,
					@u1, @u2, @u3, @u4, @u5, @u6, @u7, @u8, @u9, @u10, @ubicacion, @unidad, @bloqueado, @impuesto, @fabricante, @claveprodserv, @claveunidad,
					@c2, @c3, @c4, @c5, @c6, @c7, @c8, @c9, @c10, @paraventa, @invent, @granel, @bajocosto, @speso
				)", connection);

					insertCmd.Parameters.AddWithValue("@articulo", producto.articulo);
					insertCmd.Parameters.AddWithValue("@descrip", producto.descripcion);
					insertCmd.Parameters.AddWithValue("@marca", producto.marca ?? "SYS");
					insertCmd.Parameters.AddWithValue("@linea", producto.linea ?? "SYS");
					insertCmd.Parameters.AddWithValue("@costoultimo", producto.costoultimo);
					insertCmd.Parameters.AddWithValue("@precio1", producto.precio1);
					insertCmd.Parameters.AddWithValue("@precio2", producto.precio2);
					insertCmd.Parameters.AddWithValue("@precio3", producto.precio3);
					insertCmd.Parameters.AddWithValue("@precio4", producto.precio4);
					insertCmd.Parameters.AddWithValue("@precio5", producto.precio5);
					insertCmd.Parameters.AddWithValue("@precio6", producto.precio6);
					insertCmd.Parameters.AddWithValue("@precio7", producto.precio7);
					insertCmd.Parameters.AddWithValue("@precio8", producto.precio8);
					insertCmd.Parameters.AddWithValue("@precio9", producto.precio9);
					insertCmd.Parameters.AddWithValue("@precio10", producto.precio10);
					insertCmd.Parameters.AddWithValue("@u1", producto.u1);
					insertCmd.Parameters.AddWithValue("@u2", producto.u2);
					insertCmd.Parameters.AddWithValue("@u3", producto.u3);
					insertCmd.Parameters.AddWithValue("@u4", producto.u4);
					insertCmd.Parameters.AddWithValue("@u5", producto.u5);
					insertCmd.Parameters.AddWithValue("@u6", producto.u6);
					insertCmd.Parameters.AddWithValue("@u7", producto.u7);
					insertCmd.Parameters.AddWithValue("@u8", producto.u8);
					insertCmd.Parameters.AddWithValue("@u9", producto.u9);
					insertCmd.Parameters.AddWithValue("@u10", producto.u10);
					insertCmd.Parameters.AddWithValue("@ubicacion", producto.ubicacion ?? "");
					insertCmd.Parameters.AddWithValue("@unidad", producto.unidad ?? "");
					insertCmd.Parameters.AddWithValue("@bloqueado", producto.bloqueado);
					insertCmd.Parameters.AddWithValue("@impuesto", producto.impuesto ?? "SYS");
					insertCmd.Parameters.AddWithValue("@fabricante", producto.fabricante ?? "SYS");
					insertCmd.Parameters.AddWithValue("@claveprodserv", producto.claveprodserv ?? "");
					insertCmd.Parameters.AddWithValue("@claveunidad", producto.claveunidad ?? "");
					insertCmd.Parameters.AddWithValue("@c2", producto.c2);
					insertCmd.Parameters.AddWithValue("@c3", producto.c3);
					insertCmd.Parameters.AddWithValue("@c4", producto.c4);
					insertCmd.Parameters.AddWithValue("@c5", producto.c5);
					insertCmd.Parameters.AddWithValue("@c6", producto.c6);
					insertCmd.Parameters.AddWithValue("@c7", producto.c7);
					insertCmd.Parameters.AddWithValue("@c8", producto.c8);
					insertCmd.Parameters.AddWithValue("@c9", producto.c9);
					insertCmd.Parameters.AddWithValue("@c10", producto.c10);
					insertCmd.Parameters.AddWithValue("@paraventa", producto.paraventa);
					insertCmd.Parameters.AddWithValue("@invent", producto.invent);
					insertCmd.Parameters.AddWithValue("@granel", producto.granel);
					insertCmd.Parameters.AddWithValue("@bajocosto", producto.bajocosto);
					insertCmd.Parameters.AddWithValue("@speso", producto.speso);
					insertCmd.ExecuteNonQuery();

					LogService.WriteLog("Logs", $"[SQL] Producto insertado: Clave={producto.articulo}, Descripción={producto.descripcion}");
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
				var updateCmd = new SqlCommand("UPDATE lineas SET Descrip = @Descrip WHERE Linea = @Linea", connection);
				updateCmd.Parameters.AddWithValue("@Descrip", linea.Descrip);
				updateCmd.Parameters.AddWithValue("@Linea", linea.Linea);
				updateCmd.ExecuteNonQuery();
				LogService.WriteLog("Logs", $"[SQL] Línea actualizada: Clave={linea.Linea}, Descripción={linea.Descrip}");
			}
			else
			{
				var insertCmd = new SqlCommand("INSERT INTO lineas (Linea, Descrip) VALUES (@Linea, @Descrip)", connection);
				insertCmd.Parameters.AddWithValue("@Linea", linea.Linea);
				insertCmd.Parameters.AddWithValue("@Descrip", linea.Descrip);
				insertCmd.ExecuteNonQuery();
				LogService.WriteLog("Logs", $"[SQL] Línea insertada: Clave={linea.Linea}, Descripción={linea.Descrip}");
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
				var updateCmd = new SqlCommand("UPDATE marcas SET Descrip = @Descrip WHERE Marca = @Marca", connection);
				updateCmd.Parameters.AddWithValue("@Descrip", marca.Descrip);
				updateCmd.Parameters.AddWithValue("@Marca", marca.Marca);
				updateCmd.ExecuteNonQuery();
				LogService.WriteLog("Logs", $"[SQL] Marca actualizada: Clave={marca.Marca}, Descripción={marca.Descrip}");
			}
			else
			{
				var insertCmd = new SqlCommand("INSERT INTO marcas (Marca, Descrip) VALUES (@Marca, @Descrip)", connection);
				insertCmd.Parameters.AddWithValue("@Marca", marca.Marca);
				insertCmd.Parameters.AddWithValue("@Descrip", marca.Descrip);
				insertCmd.ExecuteNonQuery();
				LogService.WriteLog("Logs", $"[SQL] Marca insertada: Clave={marca.Marca}, Descripción={marca.Descrip}");
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
				LogService.WriteLog("Logs", $"[SQL] Impuesto actualizado: Clave={impuesto.Impuesto}, Valor={impuesto.Valor}");
			}
			else
			{
				var insertCmd = new SqlCommand("INSERT INTO impuestos (Impuesto, Valor) VALUES (@Impuesto, @Valor)", connection);
				insertCmd.Parameters.AddWithValue("@Impuesto", impuesto.Impuesto);
				insertCmd.Parameters.AddWithValue("@Valor", impuesto.Valor);
				insertCmd.ExecuteNonQuery();
				LogService.WriteLog("Logs", $"[SQL] Impuesto insertado: Clave={impuesto.Impuesto}, Valor={impuesto.Valor}");
			}
		}

	}
}

