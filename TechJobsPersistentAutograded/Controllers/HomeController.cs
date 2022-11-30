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
            List<Employer> employers = _repo.GetAllEmployers().ToList();
            List<Skill> skills = _repo.GetAllSkills().ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(employers, skills);
            return View(addJobViewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Employer newEmployer = _repo.FindEmployerById(addJobViewModel.EmployerId);
                Job newJob = new Job
                {
                    Name = addJobViewModel.Name,
                    //EmployerId = addJobViewModel.EmployerId
                    EmployerId = newEmployer.Id,
                };
                for (int i = 0; i < selectedSkills.Length; i++)
                {
                    JobSkill jobSkill = new JobSkill
                    {
                        JobId = newJob.Id,
                        Job = newJob,
                        SkillId = int.Parse(selectedSkills[i]),
                    };
                    _repo.AddNewJobSkill(jobSkill);

                }
                _repo.AddNewJob(newJob);
                _repo.SaveChanges();
               
                return Redirect("Index");
            }

            return View("AddJob", addJobViewModel);
        }


        public IActionResult Detail(int id)
        {
            Job theJob = _repo.FindJobById(id);

            List<JobSkill> jobSkills = _repo.FindSkillsForJob(id).ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }

    }

}



