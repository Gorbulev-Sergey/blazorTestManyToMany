using blazorTestManyToMany.Data;
using blazorTestManyToMany.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazorTestManyToMany.Services
{
    interface IPostsService
    {
        List<post> posts();
        void create(post post);
        void update(post post);
        void delete(post post);
    }

    public class PostsService : IPostsService
    {
        DbContextOptions<ApplicationDbContext> options;
        public PostsService(DbContextOptions<ApplicationDbContext> options)
        {
            this.options = options;
        }
        public List<post> posts()
        {
            using (var context  = new ApplicationDbContext(options))
            {
                return context.posts.Include(t=>t.tags).ToList();
            }
        }

        public void create(post post)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.posts.Add(post);
                context.SaveChanges();
            }
        }

        public void delete(post post)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.posts.Remove(post);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Функция обновления тегов в посте:
        /// Сравниваем обновлённые теги и старые теги
        /// Выбираем новые теги на добавление
        /// Выбираем старые теги, которые нужно удалить
        /// </summary>
        /// <param name="post"></param>
        public void update(post post)
        {
            using (var context = new ApplicationDbContext(options))
            {
                post postDB = context.posts.Include(t => t.tags).FirstOrDefault(p => p.Id == post.Id);
                // Обновляем данные публикации
                post.tags.Add(new tag {text="как дела" });
                postDB.name = post.name = "Обновлённое название 11";

                // Работаем с тегами
                List<tag> новыеТеги = post.tags;
                List<tag> старыеТеги = postDB.tags;
                List<tag> наДобавление = новыеТеги.Where(t => t.posts.Count == 0).ToList();
                List<tag> наУдаление = старыеТеги.Where(x => новыеТеги.All(a => a.Id != x.Id)).ToList();
                foreach (var t in наДобавление)
                {
                    postDB.tags.Add(t);
                }
                foreach (var t in наУдаление)
                {
                    postDB.tags.Remove(t);
                }                

                context.SaveChanges();
            }
        }
    }
}
