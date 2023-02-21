using AnadoluPrmPracticum.Data.Context;
using AnadoluPrmPracticum.Data.Model;
using AnadoluPrmPracticum.UnitTests.Application.Queries;
using AnadoluPrmPracticum.UnitTests.TestSetup;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace AnadoluPrmPracticum.UnitTests.Application.GameOperations.QueryOperations
{
    public class GetGameQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GetGameQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetGameDetailQuery query = new GetGameDetailQuery(_context, _mapper);
            query.gameId = -1;
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author bulunamadı");
        }

        [Fact]
        public void WhenExistGameIdIsGiven_GameDetailViewModel_ShouldBeReturn()
        {
            var game = new Game()
            {
                Name = "WhenExistGameIdIsGiven_GameDetailViewModel_ShouldBeReturn",
            };
            if (_context.Games.FirstOrDefault(x => x.Name == game.Name ) == null)
            {
                _context.Games.Add(game);
                _context.SaveChanges();
            }

            var gameId = _context.Games.SingleOrDefault(x => x.Name == game.Name ).ID;

            GetGameDetailQuery query = new GetGameDetailQuery(_context, _mapper);
            query.gameId = gameId;

            FluentActions.Invoking(() => query.Handle()).Invoke().Should().NotBeNull();

        }

    }
}
