using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMaquila1
{
    internal class BaseDatos
    {
        string[] tablas = new string[]
{
    // CLIENTES 
    @"CREATE TABLE IF NOT EXISTS TClientes (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Nombre TEXT,
        Telefono TEXT,
        Correo TEXT
    )",

    // EMPLEADOS 
    @"CREATE TABLE IF NOT EXISTS TEmpleados (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Nombre TEXT,
        Telefono TEXT,
        Cargo TEXT, -- Bodeguero, EncargadoVentas, Administrador
        Correo TEXT,
        Salario REAL
    )",

    // MATERIALES 
    @"CREATE TABLE IF NOT EXISTS TMateriales (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Nombre TEXT,
        Tipo TEXT,
        Color TEXT,
        UnidadMedida TEXT,
        CantidadDisponible REAL,
        StockMinimo REAL,
        CostoUnitario REAL
    )",

    // PRENDAS 
    @"CREATE TABLE IF NOT EXISTS TPrendas (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Tipo TEXT,
        Talla TEXT,
        Descripcion TEXT
    )",

    // PEDIDOS 
    @"CREATE TABLE IF NOT EXISTS TPedidos (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        ClienteId INTEGER,
        EmpleadoId INTEGER,
        FechaInicio TEXT,
        FechaEntrega TEXT,
        Anticipo REAL,
        Total REAL,
        Estado TEXT,
        FOREIGN KEY (ClienteId) REFERENCES TClientes(Id),
        FOREIGN KEY (EmpleadoId) REFERENCES TEmpleados(Id)
    )",

    // DETALLE PEDIDO 
    @"CREATE TABLE IF NOT EXISTS TDetallePedido (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        PedidoId INTEGER,
        PrendaId INTEGER,
        Cantidad INTEGER,
        PrecioUnitario REAL,
        FOREIGN KEY (PedidoId) REFERENCES TPedidos(Id),
        FOREIGN KEY (PrendaId) REFERENCES TPrendas(Id)
    )",

    // RELACIÓN PRENDA - MATERIAL
    @"CREATE TABLE IF NOT EXISTS TPrendaMaterial (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        PrendaId INTEGER,
        MaterialId INTEGER,
        Cantidad REAL,
        FOREIGN KEY (PrendaId) REFERENCES TPrendas(Id),
        FOREIGN KEY (MaterialId) REFERENCES TMateriales(Id)
    )",

};
    }
}
