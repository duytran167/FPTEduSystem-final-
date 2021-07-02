using FPTEduSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTEduSystem.ViewModel
{
	public class AssignDepartmentViewModel
	{
		public string TrainerId { get; set; }
		public Department Department { get; set; }
		public List<TrainerDepartment> TrainerDepartment { get; set; }
		public IEnumerable<Trainer> Trainers { get; set; }
	}
}