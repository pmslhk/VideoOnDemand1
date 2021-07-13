using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Common.DTOModels;
using VOD.Database.Services;


namespace VOD.Admin.Pages.Modules
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        #region Properties
        private readonly IAdminService _db;
        [BindProperty]
        public ModuleDTO Input { get; set; } =
        new ModuleDTO();
        [TempData] public string Alert { get; set; }
        #endregion
        #region Constructor
        public CreateModel(IAdminService db)
        {
            _db = db;
        }
        #endregion
        #region Actions
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                ViewData["Courses"] = (await _db.GetAsync<Module,
                ModuleDTO>()).ToSelectList("Id", "Title");
                return Page();
            }
            catch
            {
                return RedirectToPage("/Index", new
                {
                    alert = "You do not have access to this page."
                });
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var succeeded = (await _db.CreateAsync<ModuleDTO,
                Module>(Input)) > 0;
                if (succeeded)
                {
                    // Message sent back to the Index Razor Page.
                    Alert = $"Created a new Module: {Input.Title}.";
                    return RedirectToPage("Index");
                }
            }
            // Something failed, redisplay the form.
            ViewData["Courses"] = (await _db.GetAsync<Course,
            CourseDTO>()).ToSelectList("Id", "Title");
            return Page();
        }
        #endregion
    }
}