﻿using Fleet.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Repository.Configuration
{
    public class VeiculosConfiguration : BaseConfiguration, IEntityTypeConfiguration<Veiculos>
    {
        public void Configure(EntityTypeBuilder<Veiculos> builder)
        {

        }
    }
}
