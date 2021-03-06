using FPTEduSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTEduSystem.ViewModel
{
	public class CategoryCourseViewModel
	{
		public Course Course { get; set; }
		public int Id { get; set; }
		public IEnumerable<Category> Categories { get; set; }
	}
}