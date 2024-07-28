using System.Security.Claims;

namespace ApiNetCoreJwt6._0.Models
{
    public class Jwt
    {
        public string Key{ get; set; }
        public string Issuer{ get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }


        //metodo general para validar 
        public static dynamic validatoken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        succes = false,
                        message = "verificar si estas enviando un token",
                        result = ""
                    };
                }
                var id = identity.Claims.FirstOrDefault(x => x.Type == "Id").Value;

                Usuario usuario = Usuario.DataBase().FirstOrDefault(x=> x.Id.ToString() == id);
                return new
                {
                    succes = true,
                    message = "Exitoo", 
                    result = usuario
                };


            }
            catch (Exception ex)
            {

                return new
                {
                    succes = false,
                    message = "catch err" + ex.Message,
                    result = ""
                };
            }
        }
    }
}
