﻿CREATE DATABASE Ferreteria;
GO

USE [master]
GO
CREATE LOGIN [usrferreteria] WITH PASSWORD = N'123456',
	DEFAULT_DATABASE = [Ferreteria],
	CHECK_EXPIRATION = OFF,
	CHECK_POLICY = ON
GO
USE [Ferreteria]
GO
CREATE USER [usrferreteria] FOR LOGIN [usrferreteria]
GO
ALTER ROLE [db_owner] ADD MEMBER [usrferreteria]
GO

DROP TABLE VentaDetalle;
DROP TABLE Venta;
DROP TABLE Usuario;
DROP TABLE Empleado;
DROP TABLE Cliente;
DROP TABLE Producto;
DROP TABLE Proveedor;
DROP TABLE Categoria;

CREATE TABLE Categoria (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(100) NOT NULL,
	descripcion VARCHAR(250),
);

CREATE TABLE Proveedor (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nit BIGINT NOT NULL,
	razonSocial VARCHAR(150) NOT NULL,
	telefono VARCHAR(30),
	direccion VARCHAR(250),
	representante VARCHAR(100),
);

CREATE TABLE Producto (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idCategoria INT NOT NULL,
	idProveedor INT NOT NULL,
	codigo VARCHAR(20) NOT NULL,
	descripcion VARCHAR(250) NOT NULL,
	precioVenta DECIMAL NOT NULL,
	saldo DECIMAL NOT NULL DEFAULT 0,
	unidadMedida VARCHAR(20) NOT NULL,
	CONSTRAINT fk_Producto_Categoria FOREIGN KEY(idCategoria) REFERENCES Categoria(id),
	CONSTRAINT fk_Producto_Proveedor FOREIGN KEY(idProveedor) REFERENCES Proveedor(id)
);

CREATE TABLE Cliente (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(150) NOT NULL,
	nit VARCHAR(20) NOT NULL,
);

CREATE TABLE Empleado (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	cedulaIdentidad VARCHAR(12) NOT NULL,
	nombre VARCHAR(30) NOT NULL,
	primerApellido VARCHAR(30),
	segundoApellido VARCHAR(30),
	direccion VARCHAR(250),
	celular BIGINT,
	cargo VARCHAR(50),
);

CREATE TABLE Usuario (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idEmpleado INT NOT NULL,
	usuario VARCHAR(20) NOT NULL,
	contraseña VARCHAR(250) NOT NULL,
	CONSTRAINT fk_Usuario_Empleado FOREIGN KEY(idEmpleado) REFERENCES Empleado(id)
);

CREATE TABLE Venta(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idCliente INT NOT NULL,
	idUsuario INT NOT NULL,
	fecha DATE NOT NULL DEFAULT GETDATE(),
	transaccion INT NOT NULL,
	CONSTRAINT fk_Venta_Cliente FOREIGN KEY(idCliente) REFERENCES Cliente(id),
	CONSTRAINT fk_Venta_Usuario FOREIGN KEY(idUsuario) REFERENCES Usuario(id)
);

CREATE TABLE VentaDetalle(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idVenta INT NOT NULL,
	idProducto INT NOT NULL,
	cantidad DECIMAL NOT NULL CHECK (cantidad>0),
	precioUnitario DECIMAL NOT NULL,
	total DECIMAL NOT NULL,
	CONSTRAINT fk_VentaDetalle_Venta FOREIGN KEY(idVenta) REFERENCES Venta(id),
	CONSTRAINT fk_VentaDetalle_Producto FOREIGN KEY(idProducto) REFERENCES Producto(id)
);


ALTER TABLE Categoria ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Categoria ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Categoria ADD estado SMALLINT NOT NULL DEFAULT 1;  -- -1ELIMINADO 0= INACTIVI  1= ACTIVO
 
ALTER TABLE Proveedor ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Proveedor ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Proveedor ADD estado SMALLINT NOT NULL DEFAULT 1;  -- -1ELIMINADO 0= INACTIVI  1= ACTIVO

ALTER TABLE Producto ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Producto ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Producto ADD estado SMALLINT NOT NULL DEFAULT 1;  -- -1ELIMINADO 0= INACTIVI  1= ACTIVO

ALTER TABLE Cliente ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT 'nlimachi';
ALTER TABLE Cliente ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Cliente ADD estado SMALLINT NOT NULL DEFAULT 1;  -- -1ELIMINADO 0= INACTIVI  1= ACTIVO

ALTER TABLE Empleado ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Empleado ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Empleado ADD estado SMALLINT NOT NULL DEFAULT 1;  -- -1ELIMINADO 0= INACTIVI  1= ACTIVO

ALTER TABLE Usuario ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Usuario ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Usuario ADD estado SMALLINT NOT NULL DEFAULT 1;  -- -1ELIMINADO 0= INACTIVI  1= ACTIVO

ALTER TABLE Venta ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Venta ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Venta ADD estado SMALLINT NOT NULL DEFAULT 1;  -- -1ELIMINADO 0= INACTIVI  1= ACTIVO

ALTER TABLE VentaDetalle ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE VentaDetalle ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE VentaDetalle ADD estado SMALLINT NOT NULL DEFAULT 1;  -- -1ELIMINADO 0= INACTIVI  1= ACTIVO
 --
GO
ALTER PROC paProductoListar @parametro VARCHAR(100)
AS
BEGIN
    SELECT *
    FROM Producto
    WHERE estado <> -1
      AND (
            codigo LIKE '%' + @parametro + '%'
            OR descripcion LIKE '%' + @parametro + '%'
            OR unidadMedida LIKE '%' + @parametro + '%'
          )
    ORDER BY estado DESC, descripcion ASC;
END
GO

ALTER PROC paEmpleadoListar @parametro VARCHAR(100)
AS
	SELECT e.*, u.usuario
	FROM Empleado e
	LEFT JOIN Usuario u ON e.id = u.idEmpleado
	WHERE e.estado <> -1
	  AND (cedulaIdentidad + nombre + primerApellido + segundoApellido) LIKE '%' + @parametro + '%' + REPLACE(@parametro, ' ', '%') + '%'
	ORDER BY e.estado DESC, e.nombre ASC, e.primerApellido ASC;
GO
EXEC paProductoListar'Clavos';

EXEC paEmpleadoListar'na';

-- Insertar empleado
INSERT INTO Empleado(cedulaIdentidad, nombre, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES ('1234567', 'Juan', 'Perez', 'Muñoz', 'Calle Loa N° 50', 71717171, 'Cajero');

INSERT INTO Empleado(cedulaIdentidad, nombre, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES ('7513756', 'Wilder Handel', '', 'Arciénega', 'Av. Circunvalación S/N', 67654550, 'Contador');

INSERT INTO Empleado(cedulaIdentidad, nombre, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES ('10347355', 'Nayeli', 'Limachi', 'Peréz', 'Calle Loa N° 50', 71717171, 'Ejecutivo');

-- Crear usuario para ese empleado
INSERT INTO Usuario(idEmpleado, usuario, contraseña)
VALUES (1, 'jperez', '123456'); -- Contraseña simple para pruebas

INSERT INTO Usuario(idEmpleado, usuario, contraseña)
VALUES (2, 'wharcienega', '123456'); -- Contraseña simple para pruebas

INSERT INTO Usuario(idEmpleado, usuario, contraseña)
VALUES (3, 'nlimachi', 'Bwk06E2GcnE7m+nHi+A3IA=='); -- Contraseña simple para pruebas

UPDATE Usuario SET contraseña = 'Bwk06E2GcnE7m+nHi+A3IA==' WHERE id = 2; -- Actualizar usuario
INSERT INTO Categoria(nombre, descripcion)
VALUES ('Herramienta','Herramientas de construccion');

-- Insertar categorías
INSERT INTO Categoria(nombre, descripcion)
VALUES ('Electricidad','Productos eléctricos y cables');
INSERT INTO Categoria(nombre, descripcion)
VALUES ('Fontanería','Artículos para instalaciones de agua y desagüe');
INSERT INTO Categoria(nombre, descripcion)
VALUES ('Pinturas','Pinturas, brochas y accesorios');

 
-- Insertar proveedores
INSERT INTO Proveedor(nit, razonSocial, telefono, direccion, representante)
VALUES (123456789, 'Ferretería El Tornillo', '76543210', 'Av. América #100', 'Luis Salazar');
INSERT INTO Proveedor(nit, razonSocial, telefono, direccion, representante)
VALUES (987654321, 'Materiales El Constructor', '77788899', 'Calle Principal #200', 'Ana Gomez');
INSERT INTO Proveedor(nit, razonSocial, telefono, direccion, representante)
VALUES (456789123, 'Suministros Industriales S.A.', '61234567', 'Zona Industrial #300', 'Carlos Ramirez');
INSERT INTO Proveedor(nit, razonSocial, telefono, direccion, representante)
VALUES (112233445, 'Distribuidora Ferretera del Sur', '70011223', 'Av. del Libertador #50', 'Marta Torres');

-- Insertar productos
INSERT INTO Producto(idCategoria, idProveedor, codigo, descripcion, unidadMedida, saldo, precioVenta)
VALUES (1, 1, 'PL0254', 'Martillo Mango de Madera', 'Unidad', 0, 36);
INSERT INTO Producto(idCategoria, idProveedor, codigo, descripcion, unidadMedida, saldo, precioVenta)
VALUES(1, 1, 'HB7985', 'Clavos para concreto 2 pulgadas', 'Caja', 0, 30);
INSERT INTO Producto(idCategoria, idProveedor, codigo, descripcion, unidadMedida, saldo, precioVenta)
VALUES(1, 1, 'HB7986', 'Clavos para concreto 3 pulgadas', 'Caja', 0, 33);
INSERT INTO Producto(idCategoria, idProveedor, codigo, descripcion, unidadMedida, saldo, precioVenta)
VALUES (2, 2, 'ELC001', 'Cable Eléctrico 2.5mm', 'Metro', 100, 2.50);
INSERT INTO Producto(idCategoria, idProveedor, codigo, descripcion, unidadMedida, saldo, precioVenta)
VALUES (3, 3, 'FON002', 'Tubo PVC 1/2 pulgada', 'Metro', 50, 15.00); 
INSERT INTO Producto(idCategoria, idProveedor, codigo, descripcion, unidadMedida, saldo, precioVenta)
VALUES (4, 4, 'PIN003', 'Pintura Blanca Esmalte 1 Galón', 'Galón', 20, 85.00); 
INSERT INTO Producto(idCategoria, idProveedor, codigo, descripcion, unidadMedida, saldo, precioVenta)
VALUES (2, 3, 'ELC004', 'Interruptor Doble', 'Unidad', 75, 12.50);
INSERT INTO Producto(idCategoria, idProveedor, codigo, descripcion, unidadMedida, saldo, precioVenta)
VALUES (3, 2, 'FON005', 'Grifo de Jardín', 'Unidad', 30, 45.00); 

INSERT INTO Cliente (nombre, nit)
VALUES ('Jose Perez', '123456789');

INSERT INTO Cliente (nombre, nit)
VALUES ('María Gomez', '987654321');

INSERT INTO Cliente (nombre, nit)
VALUES ('Carlos Rivera', '1118989');

-- Actualizar producto
UPDATE Producto SET precioVenta = 34 WHERE codigo = 'HB7986';

-- Eliminar lógicamente un producto
UPDATE Producto SET estado = -1 WHERE codigo = 'HB7986';

-- Visualizar productos
SELECT * FROM Producto;
SELECT * FROM Categoria;
SELECT * FROM Proveedor;
SELECT * FROM Empleado;
SELECT * FROM Usuario;
SELECT * FROM Cliente;
SELECT * FROM Venta;
SELECT * FROM VentaDetalle;
