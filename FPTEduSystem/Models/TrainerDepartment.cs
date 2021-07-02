using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTEduSystem.Models
{
	public class TrainerDepartment
	{
		
			public int Id { get; set; }
			public string TrainerId { get; set; }
			public Trainer Trainer { get; set; }
			public int DepartmentId { get; set; }
			public Department Department { get; set; }
		
	}
}