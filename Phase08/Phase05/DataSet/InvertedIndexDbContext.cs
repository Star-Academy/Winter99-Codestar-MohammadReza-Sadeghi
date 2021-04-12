using Microsoft.EntityFrameworkCore;

namespace Phase05.DataSet
{
    public class InvertedIndexDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordDoc> WordDocs { get; set; }

        public InvertedIndexDbContext(DbContextOptions<InvertedIndexDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordDoc>().HasKey(wd => new { wd.WordId, wd.DocId });

            modelBuilder.Entity<WordDoc>()
                .HasOne<Word>(wd => wd.Word)
                .WithMany(w => w.WordDocs)
                .HasForeignKey(wd => wd.WordId);

            modelBuilder.Entity<WordDoc>()
                .HasOne<Document>(wd => wd.Doc)
                .WithMany(d => d.WordDocs)
                .HasForeignKey(wd => wd.DocId);
        }
    }
}
