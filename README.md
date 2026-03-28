# CannonPackingAPI
Es una RESTful API diseñada para gestionar y optimizar el embalaje de artículos para su transporte.  
Proporciona endpoints para crear items y cajas, ver su lista, eliminar de forma suave (soft deleting) y empacar productos.

## 🛠️ Tecnologías utilizadas
CannonPackingAPI está construida con tecnologia de .NET 9.0, utilizando C# como lenguaje de programación.  
La API sigue el patrón de diseño RESTful y se implementa utilizando ASP.NET Core Web API y se ejecuta usando la tecnologia Swagger.

## 🚀 Migraciones
Esta API cuenta con migraciones para actualizar o crear la base de datos en tu máquina local.  
Para aplicar las migraciones a la base de datos, puedes usar el siguiente comando en la terminal
```bash
dotnet ef database update
```