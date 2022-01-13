using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class IdAlunoMapping : IEntityTypeConfiguration<IdAlunoModel>
    {
        public void Configure(EntityTypeBuilder<IdAlunoModel> builder)
        {
            builder.HasNoKey();
        }
    }
}
