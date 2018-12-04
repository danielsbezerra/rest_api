using System.ComponentModel.DataAnnotations;


namespace APIUsuario.Models
{
    public class Usuario
    {
        [Key]
        public int id_usuario {get; set;}
        public int id_local_acesso {get; set;}
        public string ds_nome {get; set;}
        public string ds_login {get; set;}
        public string ds_email {get; set;}
        public string st_ativo {get; set;}
        public string st_troca_senha {get; set;}
        public string st_excluido {get; set;}
        

    }
}