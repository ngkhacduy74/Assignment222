using Assignment.Model;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment.Areas.Home.Pages
{
    public class Class_DetailModel : PageModel
    {
        private readonly ptService _ptService;
        public List<PersonalTrainer> listPT { get; set; }

        public Class_DetailModel(ptService ptService)
        {
            _ptService = ptService;
        }

        public void OnGet()
        {
            listPT = _ptService.GetAllPersonalTrainer();
        }
    }
}