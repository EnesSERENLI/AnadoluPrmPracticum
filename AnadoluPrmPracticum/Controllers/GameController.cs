using AnadoluPrmPracticum.Common;
using AnadoluPrmPracticum.Data.Model;
using AnadoluPrmPracticum.Data.UnitOfWork.Abstract;
using AnadoluPrmPracticum.Dto.Model;
using AnadoluPrmPracticum.Dto.Validations;
using AnadoluPrmPracticum.Service.UnitOfWork.Concrete;
using AutoMapper;
using FluentValidation.Results;
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
        private readonly IMapper _mapper;
        public GameController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
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
        public async Task<IActionResult> Post([FromBody] CreateGameDTO game) //ekleme işlemleri
        {
            CreateGameDTOValidation validator = new CreateGameDTOValidation();
            ValidationResult results = validator.Validate(game);//Validasyon islemi.. 
            if (!results.IsValid)
            {
                return BadRequest();
            }
            var newGame = _mapper.Map<Game>(game);//Automapper

            newGame.CreatedAt = DateTime.Now;
            newGame.CreatedBy = "SystemUser";
            await _unitOfWork.GameRepository.InsertAsync(newGame);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetById", new { newGame.ID }, newGame);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateGameDTO updated) //Güncelleme
        {
            UpdateGameDTOValidator validator = new UpdateGameDTOValidator();
            ValidationResult results = validator.Validate(updated);
            if (!results.IsValid)
                return BadRequest();

            var item = await _unitOfWork.GameRepository.GetByIdAsync(id);
            if (item is null)
            {
                return NotFound("Güncellemek istediğiniz oyuna erişilemedi. Lütfen sayfayı yineleyip tekrar deneyiniz!");
            }

            var game = _mapper.Map<Game>(updated);//Automapper

            _unitOfWork.GameRepository.Update(game);
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
