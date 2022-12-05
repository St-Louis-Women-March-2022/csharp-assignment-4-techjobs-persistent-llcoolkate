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
        //public int Id { get; set; }
        public List<SelectListItem> Employers { get; set; }
       
        public int SkillId { get; set; }
        public List<Skill> Skills { get; set; }
        public AddJobViewModel()
        {
            Employers = new List<SelectListItem>();
        }

        public AddJobViewModel(List<Employer> employers, List<Skill> skills) : this()
        {

            foreach (var item in employers)
            {
                Employers.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                });
            }
            Skills = skills;
          


        }
      
    }
}
