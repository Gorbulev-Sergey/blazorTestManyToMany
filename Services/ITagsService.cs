using blazorTestManyToMany.Data;
using blazorTestManyToMany.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazorTestManyToMany.Services
{
    interface ITagsService
    {
        List<tag> tags();
        Task create(tag tag);
        Task update(tag tag);
        Task delete(tag tag);
    }

    public class TagsService : ITagsService
    {
        DbContextOptions<ApplicationDbContext> options;

        public TagsService(DbContextOptions<ApplicationDbContext> options)
        {
            this.options = options;
        }

        public List<tag> tags()
        {
            using(var context=new ApplicationDbContext(options)){
                return context.tags.Include(p=>p.posts).ToList();
            }
        }

        public async Task create(tag tag)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.tags.Add(tag);
                await context.SaveChangesAsync();
            }
        }

        public async Task delete(tag tag)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.tags.Update(tag);
                await context.SaveChangesAsync();
            }
        } 

        public async Task update(tag tag)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.tags.Remove(tag);
                await context.SaveChangesAsync();
            }
        }
    }
}
