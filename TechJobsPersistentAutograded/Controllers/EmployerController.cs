using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
            AddEmployerViewModel viewModel = new AddEmployerViewModel();
            return View(viewModel);
        }
        //4
        [HttpPost]
        //[Route("")]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            if(ModelState.IsValid)
                {
                Employer employer = new Employer
                {
                    Location = addEmployerViewModel.Location,
                    Name = addEmployerViewModel.Name
                };
                //{
                //    Name = addEmployerViewModel.Name,
                //    Location = addEmployerViewModel.Location,
                //};
                _repo.AddNewEmployer(employer);
                _repo.SaveChanges();
                return Redirect("/Employer");
                }
            return View("Add", addEmployerViewModel);
        }
        // 5
        public IActionResult About(int id)
        {
            AddEmployerViewModel viewModel = new AddEmployerViewModel();
            Employer employer = _repo.GetAllEmployers().Where(x=> x.Id == id).First();
            viewModel.Name = employer.Name;
            viewModel.Location = employer.Location;
            return View(viewModel);
            //Employer employerAbout = _repo.FindEmployerById(id);
            //return View(employerAbout);
        }
    }
}