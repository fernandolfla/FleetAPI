namespace Fleet.Models
{
    public class Listas_Itens : DbEntity
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descrição { get; set; } = string.Empty;
        public int ListasId { get; set; }
        public virtual Listas Listas { get; set; }  
    }
}
