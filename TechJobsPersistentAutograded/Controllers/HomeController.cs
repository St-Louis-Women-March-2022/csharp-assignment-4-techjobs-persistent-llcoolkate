using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistentAutograded.Models;
using TechJobsPersistentAutograded.ViewModels;
using TechJobsPersistentAutograded.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace TechJobsPersistentAutograded.Controllers
{

    public class HomeController : Controller

    {
        private JobRepository _repo;

        public HomeController(JobRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()

        {
            IEnumerable<Job> jobs = _repo.GetAllJobs();

            return View(jobs);
        }

        //in AddJob() pass an instance of AddJobViewModel to the view
        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> allEmployers = _repo.GetAllEmployers().ToList();
            List<Skill> allSkills = _repo.GetAllSkills().ToList();
            AddJobViewModel viewModel = new AddJobViewModel(allEmployers, allSkills);
            return View(viewModel);
        }

        //[HttpPost()]
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                //Employer newEmployer = _repo.FindEmployerById(addJobViewModel.EmployerId);
                Job job = new Job
                {
                    Name = addJobViewModel.Name,
                    //Employer = _repo.FindEmployerById(addJobViewModel.EmployerId),
                    EmployerId = addJobViewModel.EmployerId
                };
                _repo.AddNewJob(job);
                foreach(string skill in selectedSkills)
                {
                    JobSkill jobSkill = new JobSkill
                    {
                        Job = job,
                        SkillId = int.Parse(skill)
                    };
                    _repo.AddNewJobSkill(jobSkill);
                }
                _repo.SaveChanges();
                return Redirect("Index");
                //{
                //    JobSkill jobSkill = new JobSkill
                //    {
                //        Job = job,
                //        SkillId = int.Parse(skill)
                //    };
                //    _repo.AddNewJobSkill(jobSkill);
                //}
                _repo.AddNewJob(job);
                _repo.SaveChanges();
                return Redirect("Index");
            }

            return View("AddJob", new AddJobViewModel(_repo.GetAllEmployers().ToList(), _repo.GetAllSkills().ToList()));
        }


        public IActionResult Detail(int id)
        {
            Job theJob = _repo.FindJobById(id);

            List<JobSkill> jobSkills = _repo.FindSkillsForJob(id).ToList();

            JobDetailViewModel jobDetailViewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(jobDetailViewModel);
        }

    }

}



