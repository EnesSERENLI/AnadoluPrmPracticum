using AnadoluPrmPracticum.Common;
using AnadoluPrmPracticum.Data.Model;
using AnadoluPrmPracticum.Data.UnitOfWork.Abstract;
using AnadoluPrmPracticum.Service.UnitOfWork.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace AnadoluPrmPracticum.Controllers
{
    [Route("api/AnadoluPrcPracticum/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GameController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //private static List<Game> gameList = new List<Game>() //Artık veritabanında verile gelecek.
        //{
        //    new Game{ID = 1,Name = "God Of War", Price = 999,ReleaseDate = Convert.ToDateTime("2018-08-30")},
        //    new Game{ID = 2,Name = "Elden Ring", Price = 899,ReleaseDate = Convert.ToDateTime("2022-03-27")},
        //    new Game{ID = 3,Name = "Horizon Zero Down", Price = 799,ReleaseDate = Convert.ToDateTime("2016-01-12")},
        //};

        //[NonAction]
        //private async Task<CommonResponse<List<Game>>> GetGameList(List<Game> games)
        //{            
        //    return new CommonResponse<List<Game>>(games.OrderByDescending(x=>x.ReleaseDate).ToList()); //Oyunların çıkış tarihlerine göre sıralayıp listeyi geri dön.
        //}
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gamesList = await _unitOfWork.GameRepository.GetAllAsync();
            return Ok(gamesList);
            //return GetGameList(gameList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) //Parametreyi Route'tan alan get metodu
        {
            var game = await _unitOfWork.GameRepository.GetByIdAsync(id);
            if (game == null)
                return NotFound("Oyun bulunamadı!");
            return Ok(game); 
        }

        [HttpGet]
        [Route("GetByFilter")]
        public async Task<IActionResult>Get([FromQuery] string gameName) //Parametreyi query ile alan get metod
        {
            var gameList = await _unitOfWork.GameRepository.GetAllAsync(); //Şuanda arama yaparken tüm listeyi çekip içerisinden arıyorum. Ancak GenericRepository kullandığım için bu şekilde. Özel bir Game repository yazdığımda bu tarz servisler orada olacak. 
            var result = gameList.Where(x => x.Name.ToUpper().Contains(gameName.ToUpper()) || x.Name.ToLower().Contains(gameName.ToLower())).ToList();
            if (result != null && result.Count > 0)
                return Ok(result);
            return NotFound("Bu isme ait bir oyun bulunamadı. Lütfen oyun ismini doğru girdiğinizden emin olunuz!");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Game game) //ekleme işlemleri
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            game.CreatedAt = DateTime.Now;
            game.CreatedBy = "SystemUser";
            await _unitOfWork.GameRepository.InsertAsync(game);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetById", new { game.ID }, game);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Game updated) //Güncelleme
        {
            if (!ModelState.IsValid) //Dto'lar yazılınca burası işe yaramış olacak.
                return BadRequest();

            var item = await _unitOfWork.GameRepository.GetByIdAsync(id);
            if (item is null)
            {
                return NotFound("Güncellemek istediğiniz oyuna erişilemedi. Lütfen sayfayı yineleyip tekrar deneyiniz!");
            }

            item.Name =updated.Name; //Bu işlemler daha sonra Automapper ile yapılacak. 
            item.Price = updated.Price;
            item.ReleaseDate = updated.ReleaseDate;

            _unitOfWork.GameRepository.Update(item);
            await _unitOfWork.CompleteAsync();

            return Ok("Oyun başarı ile güncellendi.!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) //Silme
        {
            var item = await _unitOfWork.GameRepository.GetByIdAsync(id);
            if (item is null)
            {
                return NotFound("Silmek istediğiniz oyuna erişilemedi. Lütfen sayfayı yineleyip tekrar deneyiniz!");
            }

            _unitOfWork.GameRepository.RemoveAsync(item);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
