using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Acme.BookStore.EntityFrameworkCore
{
    public static class BookStoreDbContextModelCreatingExtensions
    {
        public static void ConfigureBookStore(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            builder.Entity<Book>(b =>
            {
                b.ToTable("Books");
                b.Property("BookName").HasMaxLength(20);
                b.Property("BookType").HasMaxLength(20);
                b.Property("Description").HasMaxLength(50);
                b.ConfigureByConvention();
                //auto configure for the base class props
                //...
            });

        }
    }
}