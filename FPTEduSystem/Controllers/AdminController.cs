using FPTEduSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace FPTEduSystem.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private ApplicationUser _user;
		private ApplicationDbContext _context;
		private UserManager<ApplicationUser> _usermanager;
		public AdminController()
		{
			_user = new ApplicationUser();
			_context = new ApplicationDbContext();
			_usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
		}
		// GET: Admin
		public ActionResult Index()
		{
			var displayuser = _usermanager.Users.Where(t => t.Roles.Any(m => m.RoleId == "2" || m.RoleId == "3") == true).ToList();
			return View(displayuser);
		}
		// list staff
		public ActionResult StaffView()
		{
			var staff = _context.Users.Where(t => t.Roles.Any(m => m.RoleId == "2")).ToList();
			return View(staff);
		}
		//list trainer
		public ActionResult TrainerView()
		{
			var trainer = _context.Users.Where(t => t.Roles.Any(m => m.RoleId == "4")).ToList();
			return View(trainer);
		}
		//delete User
		public ActionResult Delete(string id)
		{
			var removeUser = _context.Users.SingleOrDefault(t => t.Id == id);
			_context.Users.Remove(removeUser);
			_context.SaveChanges();
			return RedirectToAction("");
		}
		//update staff
		[HttpGet]
		public ActionResult UpdateStaff(string id)
		{
			var staff = _context.Users
					.OfType<Staff>()
					.SingleOrDefault(t => t.Id == id);
			var updateStaff = new Staff()
			{
				Id = staff.Id,
				Email = staff.Email,
				UserName = staff.UserName,
				Name = staff.Name,

			};
			return View(updateStaff);
		}
		[HttpPost]
		public ActionResult UpdateStaff(Staff detailStaff)
		{
			var staffID = _context.Users.OfType<Staff>().FirstOrDefault(t => t.Id == detailStaff.Id);
			staffID.UserName = detailStaff.UserName;
			staffID.Name = detailStaff.Name;
			_context.SaveChanges();
			return RedirectToAction("Index", "Admin");
		}
		//update trainer
		[HttpGet]
		public ActionResult UpdateTrainer(string id)
		{
			var trainer = _context.Users.OfType<Trainer>().SingleOrDefault(t => t.Id == id);
			var updateTrainerView = new Trainer()
			{
				Id = trainer.Id,
				Email = trainer.Email,
				UserName = trainer.UserName,
				Name = trainer.Name,
				Education = trainer.Education,
				WorkPlace = trainer.WorkPlace,
				Telephone = trainer.Telephone,
				Type = trainer.Type
			};
			return View(updateTrainerView);
		}
		[HttpPost]
		public ActionResult UpdateTrainer(Trainer detailsTrainer)
		{
			var trainer = _context.Users.OfType<Trainer>().SingleOrDefault(t => t.Id == detailsTrainer.Id);
			trainer.UserName = detailsTrainer.UserName;
			trainer.Name = detailsTrainer.Name;
			trainer.Education = detailsTrainer.Education;
			trainer.WorkPlace = detailsTrainer.WorkPlace;
			trainer.Telephone = detailsTrainer.Telephone;
			trainer.Type = detailsTrainer.Type;
			_context.SaveChanges();
			return RedirectToAction("Index", "Admin");
		}
		// change password by admin
		[HttpGet]
		public ActionResult ChangePassword(string id)
		{
			var user = _context.Users.FirstOrDefault(model => model.Id == id);
			var changePasswordViewModel = new AdminChangePasswordViewModel()
			{
				UserId = user.Id
			};

			return View(changePasswordViewModel);
		}
		[HttpPost]
		public ActionResult ChangePassword(AdminChangePasswordViewModel model)
		{
			var user = _context.Users.SingleOrDefault(t => t.Id == model.UserId);
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Validation", "Some thing is wrong");
				return View(model);
			}
			if (user.PasswordHash != null)
			{
				_usermanager.RemovePassword(user.Id);
			}
			_usermanager.AddPassword(user.Id, model.NewPassword);
			return _usermanager.GetRoles(user.Id).First() == "trainer" ?
					RedirectToAction("TrainerView", "Admin") :
					RedirectToAction("StaffView", "Admin");
		}

	}
}
