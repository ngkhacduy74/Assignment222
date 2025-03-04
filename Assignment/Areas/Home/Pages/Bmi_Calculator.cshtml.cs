using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging; // Import thư viện Logger

namespace Assignment.Areas.Home.Pages
{
    public class Bmi_CalculatorModel : PageModel
    {
        [BindProperty] public float Height { get; set; }
        [BindProperty] public float Weight { get; set; }
        [BindProperty] public int Age { get; set; }
        [BindProperty] public int Gender { get; set; }

        public float BmiResult { get; set; }
        public string Status { get; set; }

        public Bmi_CalculatorModel()
        {
 
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (Height <= 0 || Weight <= 0 || Age <= 0)
            {
                BmiResult = 0;
                Status = "Invalid Input!";
                return;
            }
            float heightInMeters = Height / 100;
            BmiResult = (float)Math.Round((Weight / (heightInMeters * heightInMeters)) + (0.5 * ((Age - 20) / 10) - (2 * Gender)), 2);

            if (BmiResult < 18.5) Status = "Underweight";
            else if (BmiResult < 24.9) Status = "Healthy";
            else if (BmiResult < 29.9) Status = "Overweight";
            else Status = "Obese";
        }
    }
}
