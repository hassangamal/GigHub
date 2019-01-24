using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;
using System.Data.Entity;
namespace GigHub.Persistence.EntityConfigurations
{
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {
            Property(g => g.ArtistId)
                    .IsRequired();

            Property(g => g.GenreId)
                    .IsRequired();

            Property(g => g.Venue)
                    .IsRequired()
                    .HasMaxLength(255);

           HasMany(g => g.Attendances)
                .WithRequired(a=>a.Gig)
                .WillCascadeOnDelete(false);

        }
    }
}