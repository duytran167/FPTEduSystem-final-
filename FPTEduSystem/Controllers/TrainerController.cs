using FPTEduSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FPTEduSystem.Controllers
{
	[Authorize(Roles = "Trainer")]
	public class TrainerController : Controller
	{

		// GET: Trainer
		private ApplicationUser _user;
		private ApplicationDbContext _context;
		private UserManager<ApplicationUser> _usermanager;
		public TrainerController()
		{
			_user = new ApplicationUser();
			_context = new ApplicationDbContext();
			_usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
		}
		public ActionResult Index()
		{
			var ftrainerId = User.Identity.GetUserId();
			var trainer = _context.Users.OfType<Trainer>().SingleOrDefault(t => t.Id == ftrainerId);
			return View(trainer);
		}
		//edit trainer
		public ActionResult EditTrainer()
		{
			var trainerId = User.Identity.GetUserId();
			var trainer = _context.Users.OfType<Trainer>().SingleOrDefault(t => t.Id == trainerId);
			var updateTrainerView = new Trainer()
			{
				Id = trainer.Id,
				Email = trainer.Email,
				Name = trainer.Name,
				UserName = trainer.UserName,
				Education = trainer.Education,
				WorkPlace = trainer.WorkPlace,
				Telephone = trainer.Telephone,
				Type = trainer.Type
			};
			return View(updateTrainerView);
		}
		[HttpPost]
		public ActionResult EditTrainer(Trainer detailsTrainer)
		{
			var trainerId = User.Identity.GetUserId();
			var trainer = _context.Users.OfType<Trainer>().SingleOrDefault(t => t.Id == trainerId);
			trainer.UserName = detailsTrainer.UserName;
			trainer.Name = detailsTrainer.Name;
			trainer.Education = detailsTrainer.Education;
			trainer.WorkPlace = detailsTrainer.WorkPlace;
			trainer.Telephone = detailsTrainer.Telephone;
			trainer.Type = detailsTrainer.Type;
			_context.SaveChanges();
			return RedirectToAction("Index", "Trainer");
		}
		// trang course ma da duoc assign vao
		public ActionResult CourseAssign()
		{
			var trainerId = User.Identity.GetUserId();
			var courseAssign = _context.TrainerCourses
					.Where(t => t.TrainerId == trainerId)
					.Select(t => t.Course)
					.Include(t => t.Category)
					.ToList();
			return View(courseAssign);
		}

	}
}