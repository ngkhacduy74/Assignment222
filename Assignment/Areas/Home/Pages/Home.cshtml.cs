using Assignment.Models;

using Assignment.Models;

using Assignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assignment.Areas.Home.Pages
{
    public class HomeModel : PageModel
    {
        private readonly ISession _session;
        private readonly trainingPakageService _trainingPakageService;

        public HomeModel(trainingPakageService trainingPakageService, IHttpContextAccessor httpContextAccessor)
        {
            _trainingPakageService = trainingPakageService;
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void OnGet()
        {
            List<TrainingPackage> listTrainingPackage = _trainingPakageService.get3TrainingPackage();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            string list = JsonSerializer.Serialize(listTrainingPackage, options);

            _session.SetString("list", list);
        }
    }
}