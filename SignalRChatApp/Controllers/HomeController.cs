using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Repositories;

namespace SignalRChatApp.Controllers
{
    public class HomeController: Controller
    {
        private readonly IChatRepository _chatRepository;
        public HomeController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public IActionResult Index()
        {
            var messages = _chatRepository.getAllMessages();
            return View(messages);
        }
    }
}
