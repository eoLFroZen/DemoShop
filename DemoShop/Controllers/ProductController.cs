using DemoShop.Models.DTOs;
using DemoShop.Models.Entities;
using DemoShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoShop.Controllers
{
    public class ProductController : Controller
    {
        private ProductService productService;
        private UserManager<User> userManager;

        public ProductController(ProductService productService, UserManager<User> userManager)
        {
            this.productService = productService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            List<ProductDTO> products = productService.GetAll();

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            productService.Create(product, user);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Product product = productService.GetById(id);

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            productService.Edit(product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Product product = productService.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            productService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
