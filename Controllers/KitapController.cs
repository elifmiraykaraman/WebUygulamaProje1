using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUygulamaProje1.Utility;
using WebUygulamaProje1.Models;

namespace WebUygulamaProje1.Controllers
{
    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnviroment;

        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnviroment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnviroment = webHostEnviroment;
        }
        public IActionResult Index()
        {
            List<Kitap> objKitapList = _kitapRepository.GetAll().ToList();
            return View(objKitapList);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Ad,
                Value = k.Id.ToString()
            });

            ViewBag.KitapTuruList = KitapTuruList;
            if(id== null || id == 0)
            {
                //ekle
                return View(new Kitap());
            }
     
            else
            {
                //guncelleme
                Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);  //Expression<Func<T, bool>> filtre
                if (kitapVt == null)
                {
                    return NotFound();
                }
                return View(kitapVt);
            }
        }
        
        [HttpPost]
        public IActionResult EkleGuncelle(Kitap kitap, IFormFile? file)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                //var errors = ModelState.Values.SelectMany(x => x.Errors);

                string wwwRootPath = _webHostEnviroment.WebRootPath ;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

                using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                kitap.ResimUrl = @"\img\" + file.FileName;

                if (kitap.Id == 0)
                {
                    _kitapRepository.Ekle(kitap);
                    TempData["basarili"] = "Yeni Kitap Başarıyla Oluşturuldu!";

                }
                else
                {
                    _kitapRepository.Guncelle(kitap);
                    TempData["basarili"] = "Kitap Guncelleme Başarılı !";

                }

                _kitapRepository.Kaydet();  //SaveChanges yapmazsanız bilgiler veri tabanına eklenmez!
                return RedirectToAction("Index", "Kitap");
            }
            return View();

        }
/*
    public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0) 
            { 
            return NotFound();
            }
            Kitap? kitapVt = _kitapRepository.Get(u=>u.Id==id);  //Expression<Func<T, bool>> filtre
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View();
        }
*/
/*
        [HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {

            if (ModelState.IsValid)
            {
                _kitapRepository.Guncelle(kitap);
                _kitapRepository.Kaydet();  //SaveChanges yapmazsanız bilgiler veri tabanına eklenmez!
                TempData["basarili"] = "Kitap Başarıyla Güncellendi!";
                return RedirectToAction("Index", "Kitap");
            }
            return View();

        }
*/
        //GET ACTİON
        public IActionResult Sil (int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }

        //POST ACTION
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {

           Kitap? kitap = _kitapRepository.Get(u => u.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }
            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "Kayıt Silme İşlemi Başarılı!";
            return RedirectToAction("Index", "Kitap");

        }
    }
}
