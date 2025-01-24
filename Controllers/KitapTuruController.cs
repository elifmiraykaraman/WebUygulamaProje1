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
            List<KitapTuru> objKitapTuruList = _uygulamaDbContext.KitapTurleri.ToList();
            return View(objKitapTuruList);
        }

        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {

            if (ModelState.IsValid)
            {
                _uygulamaDbContext.KitapTurleri.Add(kitapTuru);
                _uygulamaDbContext.SaveChanges();  //SaveChanges yapmazsanız bilgiler veri tabanına eklenmez!
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();

        }

    public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0) 
            { 
            return NotFound();
            }
            KitapTuru? kitapTuruVt = _uygulamaDbContext.KitapTurleri.Find(id);
            if(kitapTuruVt == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru)
        {

            if (ModelState.IsValid)
            {
                _uygulamaDbContext.KitapTurleri.Update(kitapTuru);
                _uygulamaDbContext.SaveChanges();  //SaveChanges yapmazsanız bilgiler veri tabanına eklenmez!
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();

        }
    }
}
