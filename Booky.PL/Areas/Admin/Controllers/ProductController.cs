using AutoMapper;
using Booky.ADL.Models;
using Booky.BL.Interface;
using Booky.PL.Helper;
using Booky.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booky.PL.Controllers; 

[Area("Admin")]
[Authorize]
public class ProductController : Controller
{ 
  private readonly IunitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductController( IunitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _unitOfWork.ProductRepository.GetAll();
        // this line under it will convert IEnumerable<Product> to IEnumerable<ProductViewModel> with mapper to can deal with 
        // ProductViewModel instead of Product and then pass it to view 
        var mapperProducts = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(products);
        if (mapperProducts is null)
        {
            return BadRequest();
        }

        return View(mapperProducts);

    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // instead of pass category id and you dont save id of category we will select category name by make 
        IEnumerable<SelectListItem> catSelectListItems =
            (await _unitOfWork.CategoryRepository.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        ViewBag.Category = catSelectListItems;return View();
    }
  
    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel ProductVM)
    {
        if (ModelState.IsValid)
        {
          ProductVM.ImageUrl = await FileManagement.Upload(ProductVM.Image,"images");
          var mapperProduct = _mapper.Map<ProductViewModel,Product>(ProductVM);
            await _unitOfWork.ProductRepository.Add(mapperProduct);
             await _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        return View(ProductVM);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }

        var product = await _unitOfWork.ProductRepository.GetById(id);
        var mapperProduct = _mapper.Map<Product,ProductViewModel>(product);
        if (mapperProduct is null)
        {
            return NotFound();

        }

        return View(mapperProduct);
    }


    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        Product product = await _unitOfWork.ProductRepository.GetById(id);
        if (product is null)
        {
            return NotFound();
        }

        _unitOfWork.ProductRepository.Delete(product);
        await _unitOfWork.Save();
        return RedirectToAction("Index");


    }

    [HttpGet]

    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }
        IEnumerable<SelectListItem> catSelectListItems =
            (await _unitOfWork.CategoryRepository.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

        ViewBag.Category = catSelectListItems;
        Product product = await _unitOfWork.ProductRepository.GetById(id);
        var mapperProduct = _mapper.Map<Product,ProductViewModel>(product);
        if (mapperProduct is null)
        {
            return NotFound();
        }
        return View(mapperProduct);
        
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            if (productVM.Image != null)
            {
                if (!string.IsNullOrEmpty(productVM.ImageUrl))
                {
                    FileManagement.Delete(productVM.ImageUrl, "images");
                }

                productVM.ImageUrl = await FileManagement.Upload(productVM.Image, "images");
            }
            var mapperProduct = _mapper.Map<ProductViewModel,Product>(productVM);
            _unitOfWork.ProductRepository.Update(mapperProduct);
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        return View(productVM);
        
    }

    public async Task<IActionResult> getAll()
    {
        var products = await _unitOfWork.ProductRepository.GetAll();
        return Json(new { data = products });
    }

}