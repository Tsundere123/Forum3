using Forum3.Models;
using Microsoft.AspNetCore.Identity;

namespace Forum3.Data;

public class InitDb
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ApplicationDbContext accountDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        ForumDbContext forumDbContext = serviceScope.ServiceProvider.GetRequiredService<ForumDbContext>();
        accountDbContext.Database.EnsureCreated();
        forumDbContext.Database.EnsureCreated();

        //User accounts
        if (!accountDbContext.Users.Any())
        {
            var admin = new ApplicationUser()
            {
                Id = "9c46cebe-38d8-471c-ab0a-a1df156965d8",
                UserName = "Admin",
                Email = "admin@test.com",
                NormalizedEmail = "ADMIN@TEST.COM",
                NormalizedUserName = "ADMIN@TEST.COM",
                Avatar = "badd0fff-4888-4ceb-9b97-e55591c32766.png",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var mod = new ApplicationUser
            {
                Id = "ed8baa38-e5ee-4b93-8c50-0ecfe2726c2f",
                UserName = "Mod",
                Email = "mod@test.com",
                NormalizedEmail = "MOD@TEST.COM",
                NormalizedUserName = "MOD@TEST.COM",
                Avatar = "5c34b3a0-5ab1-4083-beea-257cd7f2b0dd.png",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var regular = new ApplicationUser
            {
                Id = "1f03d161-afc3-4507-9d6e-cdb55417b4ca",
                UserName = "User",
                Email = "user@test.com",
                NormalizedEmail = "USER@TEST.COM",
                NormalizedUserName = "USER@TEST.COM",
                Avatar = "default.png",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            
            var ceno = new ApplicationUser
            {
                Id = "dacf1d5f-3ae8-4b0a-bf7e-d00a633821a9",
                UserName = "Ceno",
                Email = "ceno@test.com",
                NormalizedEmail = "CENO@TEST.COM",
                NormalizedUserName = "CENO@TEST.COM",
                Avatar = "b0dde580-eed1-44b8-9852-de7842d8a07d.jpg",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var yes = new ApplicationUser
            {
                Id = "264cc5d0-3fb6-471c-a45e-296d46c71efa",
                UserName = "Yes",
                Email = "yes@no.com",
                NormalizedEmail = "YES@NO.COM",
                NormalizedUserName = "YES@NO.COM",
                Avatar = "2a198a7d-6ad8-42b9-a116-a5443135d7c0.jpg",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            
            var pass = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = pass.HashPassword(admin, "Yes1234-");
            mod.PasswordHash = pass.HashPassword(mod, "Yes1234-");
            regular.PasswordHash = pass.HashPassword(regular, "Yes1234-");
            ceno.PasswordHash = pass.HashPassword(ceno, "Yes1234-");
            yes.PasswordHash = pass.HashPassword(yes, "Yes1234-");

            accountDbContext.Users.Add(admin);
            accountDbContext.Users.Add(mod);
            accountDbContext.Users.Add(regular);
            accountDbContext.Users.Add(ceno);
            accountDbContext.Users.Add(yes);
            accountDbContext.SaveChanges();
        }

        //Forum categories
        if (!forumDbContext.ForumCategory.Any())
        {
            var missiles = new ForumCategory()
            {
                Id = 1, 
                Name = "Missiles", 
                Description = "The missile knows where it is at all times"
            };

            var sheeps = new ForumCategory()
            {
                Id = 2, 
                Name = "Sheeps", 
                Description = "Poi"
            };
            
            var empty = new ForumCategory()
            {
                Id = 3, 
                Name = "Empty", 
                Description = "Nothing here"
            };
            
            forumDbContext.ForumCategory.Add(missiles);
            forumDbContext.ForumCategory.Add(sheeps);
            forumDbContext.ForumCategory.Add(empty);
            
            forumDbContext.SaveChanges();
        }

        //Forum Threads
        if (!forumDbContext.ForumThread.Any())
        {
            var first = new ForumThread()
            {
                Id = 1,
                CreatorId = "9c46cebe-38d8-471c-ab0a-a1df156965d8",
                CreatedAt = DateTime.Now,
                CategoryId = 1, 
                Title = "First thread"
            };
            
            var missileKnows = new ForumThread()
            {

                Id = 2,
                CreatorId = "264cc5d0-3fb6-471c-a45e-296d46c71efa",
                CreatedAt = DateTime.Now,
                CategoryId = 1, 
                Title = "The missile knows where it is at all times"
            };

            var yamato = new ForumThread()
            {
                Id = 3,
                CreatorId = "ed8baa38-e5ee-4b93-8c50-0ecfe2726c2f",
                CreatedAt = DateTime.Now,
                CategoryId = 2,
                Title = "Yamato, the biggest battleship ever built"
            };
            
            var poi = new ForumThread()
            {
                Id = 4,
                CreatorId = "ed8baa38-e5ee-4b93-8c50-0ecfe2726c2f",
                CreatedAt = DateTime.Now,
                CategoryId = 2,
                Title = "poipoipoi",
                IsSoftDeleted = true
            };
            
            var poidachi = new ForumThread()
            {
                Id = 5,
                CreatorId = "ed8baa38-e5ee-4b93-8c50-0ecfe2726c2f",
                CreatedAt = DateTime.Now,
                CategoryId = 2,
                Title = "poipoipoi"
            };
            
            var rules = new ForumThread()
            {
                Id = 6,
                CreatorId = "9c46cebe-38d8-471c-ab0a-a1df156965d8",
                CreatedAt = DateTime.Now,
                CategoryId = 2,
                Title = "Rules",
                IsPinned = true,
                IsLocked = true
            };

            forumDbContext.ForumThread.Add(first);
            forumDbContext.ForumThread.Add(missileKnows);
            forumDbContext.ForumThread.Add(yamato);
            forumDbContext.ForumThread.Add(poi);
            forumDbContext.ForumThread.Add(poidachi);
            forumDbContext.ForumThread.Add(rules);

            forumDbContext.SaveChanges();
        }

        //Forum Posts
        if (!forumDbContext.ForumPost.Any())
        {
            var post1 = new ForumPost()
            {
                ThreadId = 1,
                Id = 1,
                Content = "This is in fact the first post",
                CreatorId = "9c46cebe-38d8-471c-ab0a-a1df156965d8",
                CreatedAt = DateTime.Now,
            };
            var post2 = new ForumPost()
            {
                ThreadId = 1,
                Id = 2,
                Content = "This is the second post",
                CreatorId = "ed8baa38-e5ee-4b93-8c50-0ecfe2726c2f",
                CreatedAt = DateTime.Now,
            };
            var post3 = new ForumPost()
            {
                ThreadId = 1,
                Id = 3,
                Content = "This is the third post",
                CreatorId = "1f03d161-afc3-4507-9d6e-cdb55417b4ca",
                CreatedAt = DateTime.Now,
                IsSoftDeleted = true
            };
            
            //Thread 2
            var post4 = new ForumPost()
            {
                ThreadId = 2,
                Id = 4,
                Content = "ThemissileknowswhereitisatalltimesItknowsthisbecauseitknowswhereitisntBysubtractingwhereitisfromwhereitisntorwhereitisntfromwhereitiswhicheverisgreateritobtainsadifferenceordeviationTheguidancesubsystemusesdeviationstogeneratecorrectivecommandstodrivethemissilefromapositionwhereitistoapositionwhereitisntandarrivingatapositionwhereitwasntitnowisConsequentlythepositionwhereitisisnowthepositionthatitwasntanditfollowsthatthepositionthatitwasisnowthepositionthatitisntIntheeventthatthepositionthatitisinisnotthepositionthatitwasntthesystemhasacquiredavariationthevariationbeingthedifferencebetweenwherethemissileisandwhereitwasntIfvariationisconsideredtobeasignificantfactorittoomaybecorrectedbytheGEAHoweverthemissilemustalsoknowwhereitwasThemissileguidancecomputerscenarioworksasfollowsBecauseavariationhasmodifiedsomeoftheinformationthemissilehasobtaineditisnotsurejustwhereitisHoweveritissurewhereitisntwithinreasonanditknowswhereitwasItnowsubtractswhereitshouldbefromwhereitwasntorviceversaandbydifferentiatingthisfromthealgebraicsumofwhereitshouldntbeandwhereitwasitisabletoobtainthedeviationanditsvariationwhichiscallederror",
                CreatorId = "264cc5d0-3fb6-471c-a45e-296d46c71efa",
                CreatedAt = DateTime.Now,
            };
            var post5 = new ForumPost()
            {
                ThreadId = 2,
                Id = 5,
                Content = "Really? A great wall of China??",
                CreatorId = "9c46cebe-38d8-471c-ab0a-a1df156965d8",
                CreatedAt = DateTime.Now,
            };
            
            //Thread 3
            var post6 = new ForumPost()
            {
                ThreadId = 3,
                Id = 6,
                Content = "![Yamato](https://upload.wikimedia.org/wikipedia/commons/e/e0/Yamato_sea_trials_2.jpg)",
                CreatorId = "ed8baa38-e5ee-4b93-8c50-0ecfe2726c2f",
                CreatedAt = DateTime.Now,
            };
            
            var post7 = new ForumPost()
            {
                ThreadId = 3,
                Id = 7,
                Content = "46cm guns, they're big",
                CreatorId = "1f03d161-afc3-4507-9d6e-cdb55417b4ca",
                CreatedAt = DateTime.Now,
            };
            
            //Thread 4
            var post8 = new ForumPost()
            {
                ThreadId = 4,
                Id = 8,
                Content = "Poi~",
                CreatorId = "ed8baa38-e5ee-4b93-8c50-0ecfe2726c2f",
                CreatedAt = DateTime.Now,
                IsSoftDeleted = true
            };
            
            //Thread 5
            var post9 = new ForumPost()
            {
                ThreadId = 5,
                Id = 9,
                Content = "Poi~",
                CreatorId = "ed8baa38-e5ee-4b93-8c50-0ecfe2726c2f",
                CreatedAt = DateTime.Now,
            };
            
            //Thread Rules
            var rules = new ForumPost()
            {
                ThreadId = 6,
                Id = 10,
                Content = "Rules: Donacdum",
                CreatorId = "9c46cebe-38d8-471c-ab0a-a1df156965d8",
                CreatedAt = DateTime.Now,
            };
            
            forumDbContext.ForumPost.Add(post1);
            forumDbContext.ForumPost.Add(post2);
            forumDbContext.ForumPost.Add(post3);
            forumDbContext.ForumPost.Add(post4);
            forumDbContext.ForumPost.Add(post5);
            forumDbContext.ForumPost.Add(post6);
            forumDbContext.ForumPost.Add(post7);
            forumDbContext.ForumPost.Add(post8);
            forumDbContext.ForumPost.Add(post9);
            forumDbContext.ForumPost.Add(rules);

            forumDbContext.SaveChanges();
        }
    }
}