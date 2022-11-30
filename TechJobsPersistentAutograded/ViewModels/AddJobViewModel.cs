using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechJobsPersistentAutograded.Models;

namespace TechJobsPersistentAutograded.ViewModels
{
    public class AddJobViewModel
    {
        [Required(ErrorMessage = "Job name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Employer is required")]
        public int EmployerId { get; set; }
        public int Id { get; set; }
        public List<SelectListItem> Employer { get; set; }
        public List<SelectListItem> JobSkills { get; set; }
        public int SkillId { get; set; }

        public AddJobViewModel(List<Employer> employers, List<Skill> skills)
        {

            Employer = new List<SelectListItem>();

            foreach (Employer employerItem in employers)
            {
                Employer.Add(new SelectListItem
                {
                    Value = employerItem.Id.ToString(),
                    Text = employerItem.Name

                });
            }

            JobSkills = new List<SelectListItem>();

            foreach (Skill skillItem in skills)
            {
                JobSkills.Add(new SelectListItem
                {
                    Value = skillItem.Id.ToString(),
                    Text = skillItem.Name

                });
            }



        }
        public AddJobViewModel()
        {

        }
        //public AddJobViewModel(string name, int employerId, int id, List<SelectListItem> employer, List<SelectListItem> jobSkills, int skillId)
        //{
        //    Name = name;
        //    EmployerId = employerId;
        //    Id = id;
        //    Employer = employer;
        //    JobSkills = jobSkills;
        //    SkillId = skillId;
        //}
    }
}
