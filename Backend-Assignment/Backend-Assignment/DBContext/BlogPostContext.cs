using Backend_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Backend_Assignment.DBContext
{
    public class BlogPostContext  : DbContext
    {
        public BlogPostContext(DbContextOptions<BlogPostContext> options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
