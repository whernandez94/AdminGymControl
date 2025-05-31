# AdminGymControl

# proyecto creado bajo el enfoque codefirst

## pasos para ejecutar las migraciones en local
## comando a ejecutar:
### dotnet ef migrations add models --output-dir "Data/Migrations"
### dotnet ef database update

## modificar la cadena de conexion a la base de datos en el fichero appsettings.json
## reemplazar las lineas
### "ConnectionStrings": {
  "DefaultConnection": "Data Source=WHERNANDEZ\\SQLEXPRESS;Initial Catalog=GymControlDB;Integrated Security=True;Encrypt=False"
}
