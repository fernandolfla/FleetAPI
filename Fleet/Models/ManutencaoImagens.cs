﻿namespace Fleet.Models
{
    public class ManutencaoImagens
    {
        public string Url { get; set; } = string.Empty;
        public int ManutencaoId { get; set; }
        public virtual Manutencao Manutencao { get; set; }
    }
}
