﻿using System;
using System.Collections.Generic;

namespace TechJobsPersistentAutograded.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Employer Employer { get; set; }

        public int EmployerId { get; set; }

        public List<JobSkill> JobSkills { get; set; }

        public Job()
        {
        }

        public Job(string name) : this()
        {
            Name = name;
        }
    }
}
