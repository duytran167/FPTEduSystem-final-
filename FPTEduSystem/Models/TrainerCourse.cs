using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTEduSystem.Models
{
	public class TrainerCourse
	{
		public int Id { get; set; }
		public string TrainerId { get; set; }
		public Trainer Trainer { get; set; }
		public int CourseId { get; set; }
		public Course Course { get; set; }
	}
}