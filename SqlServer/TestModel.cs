namespace SqlServer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestModel : DbContext
    {
        public TestModel()
            : base("C:\\Users\\STRAT\\Documents\\GitHub\\SmallBussiness.Suite\\SmallBussiness.Suite\\SmallBussiness.Suite.mdf;Integrated Security=True;Connect Timeout=30")
        {
        }

        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
