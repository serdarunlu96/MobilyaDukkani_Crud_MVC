using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MobilyaMagazasi_Crud_MVC.Data;

namespace MobilyaMagazasi_Crud_MVC.Controllers
{
    public class MobilyaController : Controller
    {
        private readonly MobilyaDbContext db;
        public MobilyaController(MobilyaDbContext context)
        {
            db = context;
        }

        public IActionResult MainPage()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View(db.Mobilyalar.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(Mobilya mobilya)
        {
            if (!ModelState.IsValid) return View();  // ModelBinding işleminde tür uyumsuzluğu yoksa aşağı devam et. Varsa View e geri dön

            if (db.Mobilyalar.Any(x => x.Name.ToLower() == mobilya.Name.ToLower()))
            {
                // Eğer hali hazırda kanepe varsa ====>>> yeni isim olarak ekleme var olanın stock adedini gelen kadar arttır
                Mobilya varOlanMobilya = db.Mobilyalar.First(x => x.Name.ToLower() == mobilya.Name.ToLower());
                varOlanMobilya.Stock += mobilya.Stock;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                db.Mobilyalar.Add(mobilya);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            Mobilya silinecekMobilya = db.Mobilyalar.Find(id);

            if (silinecekMobilya == null) return NotFound();

            return View(silinecekMobilya);
        }

        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            Mobilya silinecekMobilya = db.Mobilyalar.Find(id);
            db.Mobilyalar.Remove(silinecekMobilya);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            Mobilya duzenlenecekMobilya = db.Mobilyalar.Find(id);

            if (duzenlenecekMobilya == null) return NotFound();

            return View(duzenlenecekMobilya);
        }

        [ActionName("Edit")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditConfirm(Mobilya mobilya)
        {
            if (ModelState.IsValid)
            {
                db.Update(mobilya);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }




    }
}
