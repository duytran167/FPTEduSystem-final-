using FPTEduSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FPTEduSystem.Controllers
{
	[Authorize(Roles = "Trainee")]
	public class TraineeController : Controller
	{
		private ApplicationUser _user;
		private ApplicationDbContext _context;
		private UserManager<ApplicationUser> _usermanager;
		public TraineeController()
		{
			_user = new ApplicationUser();
			_context = new ApplicationDbContext();
			_usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
		}

		// GET: Trainee
		public ActionResult Index()
		{
			var traineeId = User.Identity.GetUserId();
			var trainee = _context.Users.OfType<Trainee>().SingleOrDefault(t => t.Id == traineeId);
			return View(trainee);
		}
		public ActionResult AllCourse(string searchString)
		{
			var allCourse = _context.Courses.Include(t => t.Category).ToList();
			if (!String.IsNullOrWhiteSpace(searchString))
			{
				allCourse = _context.Courses
				.Where(t => t.CourseName.Contains(searchString))
				.Include(t => t.Category)
				.ToList();
			}
			return View(allCourse);
		}
		public ActionResult CourseAssign()
		{
			var traineeId = User.Identity.GetUserId();
			var courseAssign = _context.TraineeCourses
					.Where(t => t.TraineeID == traineeId)
					.Select(t => t.Course)
					.Include(t => t.Category)
					.ToList();
			return View(courseAssign);
		}
	}
}