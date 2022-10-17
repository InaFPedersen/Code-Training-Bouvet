using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Data;
using System;
using System.Linq;

namespace TodoApp.Models
{
    
        public static class SeedData
        {
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var context = new TodoAppContext(
                    serviceProvider.GetRequiredService<
                        DbContextOptions<TodoAppContext>>()))
                {
                    // Look for any movies.
                    if (context.Todo.Any())
                    {
                        return;   // DB has been seeded
                    }

                    context.Todo.AddRange(
                        new Todo
                        {
                            Title = "Take out the trash",
                            CreatedDate = DateTime.Parse("2022-10-12"),
                            Importance = "medium"
                            
                        },

                        new Todo
                        {
                            Title = "Clean the bathroom ",
                            CreatedDate = DateTime.Parse("2022-10-12"),
                            Importance = "High"
                        },

                        new Todo
                        {
                            Title = "Buy new clothes",
                            CreatedDate = DateTime.Parse("2022-10-13"),
                            Importance = "low"
                        },

                        new Todo
                        {
                            Title = "Visit Family",
                            CreatedDate = DateTime.Parse("2022-10-14"),
                            Importance = "medium"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
 }

