using Microsoft.AspNetCore.Mvc;
using WebUygulamaProje1.Utility;
using WebUygulamaProje1.Models;

namespace WebUygulamaProje1.Controllers
{
    public class KitapTuruController : Controller
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public KitapTuruController(UygulamaDbContext context)
        {
            _uygulamaDbContext = context;
        }
        public IActionResult Index()
        {
            List<KitapTuru> objKitapTuruList  = _uygulamaDbContext.KitapTurleri.ToList();
            return View(objKitapTuruList);
        }
    }
}
