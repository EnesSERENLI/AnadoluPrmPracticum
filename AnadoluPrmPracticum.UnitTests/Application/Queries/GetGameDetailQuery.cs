using AnadoluPrmPracticum.Data.Context;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnadoluPrmPracticum.UnitTests.Application.Queries
{
    public class GetGameDetailQuery
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public int gameId { get; set; }
        public GetGameDetailQuery(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GameDetailViewModel Handle()
        {
            var result = _context.Games.FirstOrDefault(game => game.ID == gameId);
            if (result == null)
                throw new InvalidOperationException("Game bulunamadı");
            return _mapper.Map<GameDetailViewModel>(result);
        }

        public class GameDetailViewModel
        {
            public string Name { get; set; }
        }
    }
}
