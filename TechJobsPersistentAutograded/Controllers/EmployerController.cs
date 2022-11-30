using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TechJobsPersistentAutograded.Data;
using TechJobsPersistentAutograded.Models;
using TechJobsPersistentAutograded.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistentAutograded.Controllers
{
    public class EmployerController : Controller
    {
        //1) private JobRepository variable to perform CRUD on the database
        private JobRepository _repo;

        public EmployerController(JobRepository repo)
        {
            _repo = repo;
        }

        //2) complete Index() so that is passes all of the Employer objects in the database to the view
        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Employer> employers = _repo.GetAllEmployers();
            return View(employers);
        }
        //3
        public IActionResult Add()
        {
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();

            return View(addEmployerViewModel);
        }
        //4
        [HttpPost]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            if(ModelState.IsValid)
                {
                    Employer employer = new Employer
                    {
                        Name = addEmployerViewModel.Name,
                        Location = addEmployerViewModel.Location,
                    };
                    _repo.AddNewEmployer(employer);
                    _repo.SaveChanges();
                    return Redirect("/Employer");
                }
            return View("Add"/*, addEmployerViewModel*/);
        }
        // 5
        public IActionResult About(int id)
        {
            Employer employerAbout = _repo.FindEmployerById(id);
            return View(employerAbout);
        }
    }
}