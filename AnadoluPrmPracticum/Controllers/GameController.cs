using AnadoluPrmPracticum.Common;
using AnadoluPrmPracticum.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnadoluPrmPracticum.Controllers
{
    [Route("api/AnadoluPrcPracticum/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        public GameController()
        {

        }

        private static List<Game> gameList = new List<Game>()
        {
            new Game{ID = 1,Name = "God Of War", Price = 999,ReleaseDate = Convert.ToDateTime("2018-08-30")},
            new Game{ID = 2,Name = "Elden Ring", Price = 899,ReleaseDate = Convert.ToDateTime("2022-03-27")},
            new Game{ID = 3,Name = "Horizon Zero Down", Price = 799,ReleaseDate = Convert.ToDateTime("2016-01-12")},
        };

        [NonAction]
        private CommonResponse<List<Game>> GetGameList(List<Game> games)
        {
            return new CommonResponse<List<Game>>(games.OrderByDescending(x=>x.ReleaseDate).ToList()); //Oyunların çıkış tarihlerine göre sıralayıp listeyi geri dön.
        }

        [HttpGet]
        public CommonResponse<List<Game>> GetAll()
        {
            return GetGameList(gameList);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) //Parametreyi Route'tan alan get metodu
        {
            var result = GetGameList(gameList).Data.Where(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                return Ok(result); //HTTP status codeları ile beraber return işlemleri(200 OK)
            }
            return NotFound("Oyun bulunamadı!");//(404 NotFound)  
        }

        [HttpGet]
        [Route("GetByFilter")]
        public IActionResult Get([FromQuery] string gameName) //Parametreyi query ile alan get metod
        {
            var result = GetGameList(gameList).Data.Where(x => x.Name.ToUpper().Contains(gameName.ToUpper()) || x.Name.ToLower().Contains(gameName.ToLower())).ToList();
            if (result != null && result.Count > 0)
                return Ok(result);
            return NotFound("Bu isme ait bir oyun bulunamadı. Lütfen oyun ismini doğru girdiğinizden emin olunuz!");
        }

        [HttpPost]
        public CommonResponse<List<Game>> Post([FromBody] Game game) //Ekleme işlemleri
        {
            var list = GetGameList(gameList).Data;
            list.Add(game);
            return new CommonResponse<List<Game>>(list);
        }

        [HttpPut("{id}")]
        public CommonResponse<List<Game>> Put([FromRoute] int id, [FromBody] Game updated) //Güncelleme
        {
            var games = GetGameList(gameList).Data;
            Game game = gameList.Where(x => x.ID == id).FirstOrDefault();
            if (game != null)
            {
                games.Remove(game);

                updated.ID = id;
                games.Add(updated);
                return new CommonResponse<List<Game>>(gameList); 
            }
            else
                return new CommonResponse<List<Game>>("Güncellemek istediğiniz oyuna erişilemedi. Lütfen sayfayı yineleyip tekrar deneyiniz!");
        }

        [HttpDelete("{id}")]
        public CommonResponse<List<Game>> Delete([FromRoute] int id) //Silme
        {
            var list = GetGameList(gameList).Data;
            Game game = list.Where(x => x.ID == id).FirstOrDefault();
            if (game != null)
            {
                list.Remove(game);
                return new CommonResponse<List<Game>>(list); 
            }
            else
                return new CommonResponse<List<Game>>("Silmek istediğiniz oyuna erişilemedi. Lütfen sayfayı yineleyip tekrar deneyiniz!");
        }
    }
}
