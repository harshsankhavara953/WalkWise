using Microsoft.AspNetCore.Mvc;
using ShoeStoreMVC.Models;
using ShoeStoreMVC.Services;

namespace ShoeStoreMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicatinoDbConext context;
        private readonly IWebHostEnvironment environment;

        public ProductsController(ApplicatinoDbConext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var products = context.products.OrderByDescending(p => p.Id).ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductDto productdto)
        {
            if (productdto?.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image file is required");
            }

            if (!ModelState.IsValid)
            {
                return View(productdto);
            }
            //save image file
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //newFileName += Path.GetExtension(productdto.ImageFile.FileName);
            newFileName += Path.GetExtension(productdto!.ImageFile!.FileName ?? "");



            string imageFullPath = environment.WebRootPath + "/SHOE/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productdto.ImageFile.CopyTo(stream);
            }
            //save the new product in database

            Product product = new Product()
            {
                name = productdto.Name,
                Brand = productdto.Brand,
                category = productdto.Category,
                Price = productdto.Price,
                Description = productdto.Description,
                ImageFileName = newFileName,
                CreateAt = DateTime.Now,

            };
            context.products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
        public IActionResult Edit(int id)
        {

            var product = context.products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            var productDto = new ProductDto()
            {
                Name = product.name,
                Brand = product.Brand,
                Category = product.category,
                Price = product.Price,
                Description = product.Description,

            };

            ViewData["ProductId"] = product.Id;
            ViewData["ImageFileName"] = product.ImageFileName;
            ViewData["CreatedAt"] = product.CreateAt.ToString("MM/dd/yyyy");

            return View(productDto);
        }



        [HttpPost]
        public IActionResult Edit(int id, ProductDto productdto)
        {
            var product = context.products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id;
                ViewData["ImageFileName"] = product.ImageFileName;
                ViewData["CreatedAt"] = product.CreateAt.ToString("MM/dd/yyyy");
                return View(productdto);
            }
            string newFileName = product.ImageFileName;

            if (productdto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHssfff");
                newFileName += Path.GetExtension(productdto.ImageFile.FileName ?? "");


                string imageFullPath = environment.WebRootPath + "/SHOE/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productdto.ImageFile.CopyTo(stream);

                }
                string oldImageFullPath = environment.WebRootPath + "/SHOE/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }
            product.name = productdto.Name;
            product.Brand = productdto.Brand;
            product.category = productdto.Category;
            product.Description = productdto.Description;
            product.ImageFileName = newFileName;


            context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }

        public IActionResult Delete(int id)
        {
            var product = context.products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            string ImageFullPath = environment.WebRootPath + "/SHOE/" + product.ImageFileName;
            System.IO.File.Delete(ImageFullPath);

            context.products.Remove(product);
            context.SaveChanges(true);
            return RedirectToAction("Index", "Products");


        }

    }

}