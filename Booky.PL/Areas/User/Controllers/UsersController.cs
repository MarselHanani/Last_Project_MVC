using AutoMapper;
using Booky.ADL.Models;
using Booky.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Runtime.InteropServices;
using System;

namespace Booky.PL.Areas.User.Controllers;
[Area("User")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
    IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.Select(user => new UsersViews()
        {
            Id = user.Id,
            Fname = user.Fname,
            Lname = user.Lname,
            Email = user.Email,
            Roles = _userManager.GetRolesAsync(user).Result

        }).ToListAsync();
        return View(users);
    }
    public async Task<IActionResult> Getall()
    {
        var users = await _userManager.Users.Select(user => new UsersViews()
        {
            Id = user.Id,
            Fname = user.Fname,
            Lname = user.Lname,
            Email = user.Email,
            Roles = _userManager.GetRolesAsync(user).Result

        }).ToListAsync();
        return Json(new { data = users });
    }
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        var mapper = _mapper.Map<ApplicationUser, UsersViews>(user);
        return View(mapper);

    }

    [HttpPost]
    public async Task<IActionResult> Edit(UsersViews model, [FromRoute] string id)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(id);
            var mapper = _mapper.Map(model, user);
            var result = await _userManager.UpdateAsync(mapper);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        var mapper = _mapper.Map<ApplicationUser, UsersViews>(user);
        return View(mapper);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id, string password)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Check if the entered password is correct
        var passwordCheckResult = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordCheckResult)
        {
            ModelState.AddModelError(string.Empty, "Incorrect password");
            return View("Error");
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Users");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("Error");
        }
    }





    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var allRoles = await _roleManager.Roles.ToListAsync();
        var rolesSelectItems = allRoles.Select(role => new SelectListItem
        {
            Text = role.Name,
            Value = role.Name
        });
        ViewBag.roles = rolesSelectItems;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UsersViews model)
    {
        if (ModelState.IsValid)
        {
            var mapperUser = new ApplicationUser()
            {
                UserName = model.Email.Split('@')[0],
                Fname = model.Fname,
                Lname = model.Lname,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(mapperUser, model.Password);
            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult() ||
                    !_roleManager.RoleExistsAsync("Customer").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("Customer"));
                    //multiple role add
                }
                if (model.Roles != null && model.Roles.Any())
                {
                    IdentityResult addRole = await _userManager.AddToRolesAsync(mapperUser, model.Roles);
                    if (addRole.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to add roles");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed Create User");
                }
            }
        }
        return View(model);
    }

}