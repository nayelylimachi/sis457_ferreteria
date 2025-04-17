# sis457_ferreteria
A. Descripción del proyecto

Este proyecto tiene como objetivo diseñar una aplicacion de escritorio para la *Ferretería Bicentenario*, un negocio dedicado a la venta de materiales de construcción, herramientas, pinturas y artículos ferreteros en general.

B. Entidades y Campos Tentativos

1. producto
- id
- codigo
- descripcion
- precio_venta
- cantidad
- unidad_medida
- id_categoria
- id_proveedor
- usuario_registro
- fecha_creacion
- fecha_modificacion
- fecha_eliminacion

---

2. categoria
- id
- nombre
- descripcion
- usuario_registro
- fecha_creacion
- fecha_modificacion
- fecha_eliminacion

---

3. proveedor
- id
- nit
- razon_social
- telefono
- direccion
- representante
- usuario_registro
- fecha_creacion
- fecha_modificacion
- fecha_eliminacion

---

4. cliente
- id
- nombre
- ci_nit
- usuario_registro
- fecha_creacion
- fecha_modificacion
- fecha_eliminacion

---

5. venta
- id
- fecha
- id_cliente
- id_usuario
- transaccion
- usuario_registro
- fecha_creacion
- fecha_modificacion
- fecha_eliminacion

---

6. detalle_venta
- id
- id_venta
- id_producto
- cantidad
- precio_unitario
- total
- usuario_registro
- fecha_creacion
- fecha_modificacion
- fecha_eliminacion

---

7. usuario
- id
- usuario
- contraseña
- id_empleado
- usuario_registro
- fecha_creacion
- fecha_modificacion
- fecha_eliminacion

---

8. empleado
- id
- ci
- nombre
- paterno
- materno
- direccion
- celular
- cargo
- usuario_registro
- fecha_creacion
- fecha_modificacion
- fecha_eliminacion