using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace TiposDeNubes.Models
{
    public class NubeCRUD
    {

        MySqlConnection conexion = new MySqlConnection("server=localhost;database=tiposNubes;user=root;password=root");

        MySqlCommand sql;
        MySqlDataReader lector;
        public ObservableCollection<NubeModel> Nubes { get; set; } = new ObservableCollection<NubeModel>();

        public NubeCRUD()
        {
            // Preguntar si la conexion esta cerrada
            Conectar();
            // abrirla 

            // Cargar los datos y colocarlos en la lista
            //sql = new MySqlCommand("select * from frutas order by Nombre", 
            //  conexion);
            sql = new MySqlCommand();
            sql.CommandText = "select * from Nube order by genero";
            sql.Connection = conexion;
            lector = sql.ExecuteReader();

            while (lector.Read())
            {
                NubeModel cloud = new NubeModel()
                {
                    Id = (int)lector["Id"],
                    Genero = (string)lector["Genero"],
                    Especie = (string)lector["Especie"],
                    Nivel = (string)lector["Nivel"],
                    Caracteristicas = Convert.IsDBNull(lector["Caracteristicas"]) ? "" : (string)lector["Caracteristicas"]
                };
                Nubes.Add(cloud);
            }
            lector.Close();



        }
        ~NubeCRUD()
        {
            Conectar();
        }
        public void Agregar(NubeModel minube)
        {
            if (minube != null)
            {
                sql.Connection = conexion;

                sql.CommandText = $"insert into Nube (Genero, Especie, Nivel) values ('{minube.Genero}','{minube.Especie}','{minube.Nivel}');";
           
                
                if (sql.ExecuteNonQuery() > 0)

                    Nubes.Add(minube);
            }
        }
        
        public void Eliminar(NubeModel minube)
        {
            if (minube != null)
            {
                // Riquejo
                sql.CommandText = $"delete from Nube where id ={minube.Id} ";
                sql.Connection = conexion;
                if (sql.ExecuteNonQuery() > 0)
                {
                    Nubes.Remove(minube);
                }
            }

        }
        // Paramtros de salida out 
        public bool Validar(NubeModel minube, out List<string>errores)
        {
            errores = new List<string>();
            if (minube != null)
            {
                if (string.IsNullOrWhiteSpace(minube.Genero)) 
                    errores.Add("Escriba el genero de la nube");
                if (string.IsNullOrWhiteSpace(minube.Especie))
                    errores.Add("Escriba la especie a la que pertenece la nube");
                if (string.IsNullOrWhiteSpace(minube.Nivel))
                    errores.Add("Escriba el nivel en que se encuentra la nube");
                if (string.IsNullOrWhiteSpace(minube.Caracteristicas))
                    errores.Add("Escriba las caracteristicas de la nube");
                if (Nubes.Any(x => x.Genero == minube.Genero)) ;
                errores.Add("La Nube ya ha sido ingresada");
            }
            return true;
        }
        public void Editar(NubeModel minube)
        {
            if (minube != null)
            {

                sql.CommandText = $"update nube set Genero = '{minube.Genero}', Especie = '{minube.Especie}', Nivel='{minube.Nivel}' where Id = '{minube.Id}'";
                sql.Connection = conexion;
                if (sql.ExecuteNonQuery() > 0)
                {
                    var temp = Nubes.FirstOrDefault(x => x.Id == minube.Id);
                    
                    if (temp != null)
                    {
                        temp.Genero = minube.Genero;
                        temp.Especie = minube.Especie;
                        temp.Nivel = minube.Nivel;
                    }
                    // Cambiar en la coleccion 
                    // Buscarlo en la lista y encontralo y remplazar 
                }
            }


        }
        public void Conectar()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
            else
                conexion.Close();
        }
    }
}

