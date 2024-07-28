namespace ApiNetCoreJwt6._0.Models
{
    public class Usuario
    {
        public int Id { get; set; } 
        public string usuario{ get; set; }
        public string contraseña{ get; set; }
        public string rol { get; set;}

        public static List<Usuario> DataBase()
        {
            var dataBaselista = new List<Usuario>(){
                new Usuario {
                    Id = 1,
                    usuario="pepito",
                    contraseña="123.",
                    rol="empleado"
                },
                new Usuario {
                    Id = 2,
                    usuario="juan",
                    contraseña="123.",
                    rol="empleado"
                },
                new Usuario {
                    Id = 3,
                    usuario="marcos",
                    contraseña="123.",
                    rol="empleado"
                },
                new Usuario {
                    Id = 4,
                    usuario="julian",
                    contraseña="123.",
                    rol="administrador"
                }
            };
            return dataBaselista;
        }

    }
}
