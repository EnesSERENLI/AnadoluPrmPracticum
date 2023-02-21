using AnadoluPrmPracticum.Data.Context;
using AnadoluPrmPracticum.Service.AutoMapper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnadoluPrmPracticum.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public AppDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("server=DESKTOP-JOE5KI8\\SQLEXPRESS02;Database=AnadoluParamDB;Trusted_Connection=True; MultipleActiveResultSets=True;").Options;
            Context = new AppDbContext(options);
            Context.Database.EnsureCreated();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<Mapping>(); }).CreateMapper();

        }
    }
}
