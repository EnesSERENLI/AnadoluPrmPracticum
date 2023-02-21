using AnadoluPrmPracticum.Data.Context;
using AnadoluPrmPracticum.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnadoluPrmPracticum.UnitTests.TestSetup
{
    public static class Games
    {
        public static void AddGames(this AppDbContext context)
        {
            context.Games.AddRange(
                    new Game { Name = "God Of WAr", Price = 899, CreatedBy = "Enes SErenli", ReleaseDate = DateTime.Now },
                    new Game { Name = "Dark Souls", Price = 799, CreatedBy = "Enes SErenli", ReleaseDate = DateTime.Now },
                    new Game { Name = "Sekiro:Shadows Die Twice", Price = 599, CreatedBy = "Enes SErenli", ReleaseDate = DateTime.Now }
                );

        }
    }
}
