using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Common.DTOModels.Admin;
using VOD.Database.Services;


namespace VOD.Admin.Pages.instructors
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        #region Properties
        public IEnumerable<InstructorDTO> Items = new List<InstructorDTO>();


        [TempData] 
        public string Alert { get; set; }
        #endregion
        #region Constructor

        private readonly IAdminService _db;
        public IndexModel(IAdminService db)
        {
            _db = db;
        }

        #endregion

        public async Task<IActionResult> OnGetAsync()

        {
            try
            {
                Items = await _db.GetAsync<Instructor, InstructorDTO>(true);
                return Page();
            }
            catch

            {
                Alert = "You do not have access to this page.";
                return RedirectToPage("/Index");
            }
        }


    }

 

}