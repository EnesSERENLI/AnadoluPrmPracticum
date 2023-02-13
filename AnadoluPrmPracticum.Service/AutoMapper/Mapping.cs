using AnadoluPrmPracticum.Data.Model;
using AnadoluPrmPracticum.Dto.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnadoluPrmPracticum.Service.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            #region Game Mapper
            CreateMap<Game, CreateGameDTO>().ReverseMap();
            CreateMap<Game, UpdateGameDTO>().ReverseMap();
            #endregion

            #region User Mapper
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap(); 
            #endregion
        }
    }
}
