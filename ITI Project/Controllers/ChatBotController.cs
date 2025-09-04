using ITI_Project.DTO;
using ITI_Project.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Project.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly IChatBotRepository chatBotRepository;
        public ChatBotController(IChatBotRepository chatBotRepository)
        {
            this.chatBotRepository = chatBotRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ask(ChatBotDTO dto)
        {
            var reply = await chatBotRepository.AskGeminiAsync(dto.UserMessage);
            dto.BotReply = reply;
            return Json(dto);
        }
    }
}
