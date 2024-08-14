namespace Fleet.Models
{
    public class UsuarioResetSenha : DbEntity
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Datacriacao { get; set; }
        public DateTime Dataexpiracao { get; set; } 
        public bool Usado {  get; set; } = false;
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
