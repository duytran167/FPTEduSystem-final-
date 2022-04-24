using FPTEduSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FPTEduSystem.Controllers
{
	[Authorize(Roles = "Staff")]
	public class StaffController : Controller

	{
		private ApplicationUser _user;
		private ApplicationDbContext _context;
		private UserManager<ApplicationUser> _usermanager;
		public StaffController()
		{
			_user = new ApplicationUser();
			_context = new ApplicationDbContext();
			_usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
		}
		// GET: Staff
		// hien thi thon tin ca nhan cua minh khi la role staff
		public ActionResult Index()
		{
			var staff = User.Identity.GetUserId();
			var viewstaff = _context.Users.OfType<Staff>().SingleOrDefault(t => t.Id == staff);
			return View(viewstaff);
		}


		//trainee manage
		public ActionResult TraineeManagement(string searchString)
		{

			var trainee = _context.Users.Where(t => t.Roles.Any(r => r.RoleId == "3")).ToList();
			if (!String.IsNullOrEmpty(searchString))
			{
				trainee = _context.Users
						.Where(t => t.Roles.Any(r => r.RoleId == "3") && t.UserName.Contains(searchString) == true)
						.ToList();
			}
			return View(trainee);
		}
		//xoa trainee
		public ActionResult DeleteTrainee(string id)
		{
			var removeTrainee = _context.Users.SingleOrDefault(t => t.Id == id);
			_context.Users.Remove(removeTrainee);
			_context.SaveChanges();
			return RedirectToAction("TraineeManagement");
		}
		//updaet trainee
		[HttpGet]
		public ActionResult UpdateTrainee(string id)
		{
			var trainee = _context.Users
							 .OfType<Trainee>()
							 .SingleOrDefault(t => t.Id == id);
			var updateTrainee = new Trainee()
			{
				Id = trainee.Id,
				Email = trainee.Email,
				UserName = trainee.UserName,
				Name = trainee.Name,
				Age = trainee.Age,
				DateofBirth = trainee.DateofBirth,
				Education = trainee.Education,
				MainProgrammingLang = trainee.MainProgrammingLang,
				ToeicScore = trainee.ToeicScore,
				ExpDetail = trainee.ExpDetail,
				Department = trainee.Department,
				Location = trainee.Location

			};
			return View(updateTrainee);
		}
		[HttpPost]
		public ActionResult UpdateTrainee(Trainee detailsTrainee)
		{
			var traineesearch = _context.Users.OfType<Trainee>().FirstOrDefault(t => t.Id == detailsTrainee.Id);
			traineesearch.UserName = detailsTrainee.UserName;
			traineesearch.Age = detailsTrainee.Age;
			traineesearch.Name = detailsTrainee.Name;
			traineesearch.DateofBirth = detailsTrainee.DateofBirth;
			traineesearch.Education = detailsTrainee.Education;
			traineesearch.MainProgrammingLang = detailsTrainee.MainProgrammingLang;
			traineesearch.ToeicScore = detailsTrainee.ToeicScore;
			traineesearch.ExpDetail = detailsTrainee.ExpDetail;
			traineesearch.Department = detailsTrainee.Department;
			traineesearch.Location = detailsTrainee.Location;
			_context.SaveChanges();
			return RedirectToAction("TraineeManagement");
		}
		// chi tiet trainee
		public ActionResult DetailsTrainee(string id)
		{
			var trainee = _context.Users.SingleOrDefault(t => t.Id == id);
			return View(trainee);
		}
		//trainer manage
		public ActionResult TrainerManagement(string searchString)
		{

			var trainer = _context.Users.Where(t => t.Roles.Any(r => r.RoleId == "4")).ToList();
			if (!String.IsNullOrEmpty(searchString))
			{
				trainer = _context.Users
						.Where(t => t.Roles.Any(r => r.RoleId == "4") && t.UserName.Contains(searchString) == true)
						.ToList();
			}
			return View(trainer);
		}
		// update trainer
		[HttpGet]
		public ActionResult UpdateTrainer(string id)
		{
			var trainer = _context.Users
							 .OfType<Trainer>()
							 .SingleOrDefault(t => t.Id == id);
			var updateTrainer = new Trainer()
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
			return View(updateTrainer);
		}
		[HttpPost]
		public ActionResult UpdateTrainer(Trainer detailsTrainer)
		{
			var traineesearch = _context.Users.OfType<Trainer>().FirstOrDefault(t => t.Id == detailsTrainer.Id);
			traineesearch.UserName = detailsTrainer.UserName;
			traineesearch.Name = detailsTrainer.Name;
			traineesearch.Education = detailsTrainer.Education;
			traineesearch.WorkPlace = detailsTrainer.WorkPlace;
			traineesearch.Telephone = detailsTrainer.Telephone;
			traineesearch.Type = detailsTrainer.Type;
			_context.SaveChanges();
			return RedirectToAction("TraineeManagement");
		}
		// chi tiet trainer
		public ActionResult DetailsTrainer(string id)
		{
			var trainer = _context.Users.SingleOrDefault(t => t.Id == id);
			return View(trainer);
		}
		//xoa trainer
		public ActionResult DeleteTrainer(string id)
		{
			var removeTrainer = _context.Users.SingleOrDefault(t => t.Id == id);
			_context.Users.Remove(removeTrainer);
			_context.SaveChanges();
			return RedirectToAction("TrainerManagement");
		}
		//course manage
		public ActionResult CourseManagement(string searchString)
		{
			var courses = _context.Courses.Include(t => t.Category).ToList();
			if (!String.IsNullOrWhiteSpace(searchString))
			{
				courses = _context.Courses
				.Where(t => t.CourseName.Contains(searchString))
				.Include(t => t.Category)
				.ToList();
			}
			return View(courses);
		}
		//tao course
		[HttpGet]
		public ActionResult CreateCourse()
		{
			var courseCategory = new ViewModel.CategoryCourseViewModel()
			{
				Categories = _context.Categories.ToList(),
			};
			return View(courseCategory);
		}
		[HttpPost]
		public ActionResult CreateCourse(ViewModel.CategoryCourseViewModel categoryCourseModel)
		{
			var new_course = new Course()
			{
				CourseName = categoryCourseModel.Course.CourseName,
				Detail = categoryCourseModel.Course.Detail,
				CategoryID = categoryCourseModel.Id
			};
			_context.Courses.Add(new_course);
			_context.SaveChanges();
			return RedirectToAction("CourseManagement");
		}

		//xóa course
		public ActionResult DeleteCourse(int id)
		{
			var removeCourse = _context.Courses.SingleOrDefault(t => t.Id == id);
			_context.Courses.Remove(removeCourse);
			_context.SaveChanges();
			return RedirectToAction("CourseManagement");
		}

		//edit course
		public ActionResult EditCourse(int id)
		{
			var course = _context.Courses.SingleOrDefault(t => t.Id == id);
			var courses = new ViewModel.CategoryCourseViewModel()
			{
				Id = id,
				Course = course,
				Categories = _context.Categories.ToList()
			};
			return View(courses);
		}
		[HttpPost]
		public ActionResult EditCourse(ViewModel.CategoryCourseViewModel viewModel)
		{
			var course = _context.Courses.SingleOrDefault(t => t.Id == viewModel.Id);
			course.CourseName = viewModel.Course.CourseName;
			course.CategoryID = viewModel.Course.CategoryID;
			course.Detail = viewModel.Course.Detail;
			_context.SaveChanges();
			return RedirectToAction("CourseManagement", "Staff");
		}
		//detail course
		public ActionResult DetailCourse(int id)
		{
			var course = new ViewModel.CategoryCourseViewModel();
			course.Id = id;
			course.Course = _context.Courses.Include(t => t.Category).SingleOrDefault(t => t.Id == id);
			return View(course);
		}
		//manage category
		public ActionResult CategoryView(string searchString)
		{
			var categories = _context.Categories.ToList();
			if (!String.IsNullOrWhiteSpace(searchString))
			{
				categories = _context.Categories
			 .Where(t => t.CategoryName.Contains(searchString))
			 .ToList();
			}
			return View(categories);
		}
		//tao category
		[HttpGet]
		public ActionResult CreateCategory()
		{
			return View();
		}
		[HttpPost]
		public ActionResult CreateCategory(Category category)
		{
			var create_category = new Category() { CategoryName = category.CategoryName };
			_context.Categories.Add(create_category);
			_context.SaveChanges();
			return RedirectToAction("CategoryView");
		}
		//xoa category
		public ActionResult DeleteCategory(int id)
		{
			var removeCategory = _context.Categories.SingleOrDefault(t => t.Id == id);
			_context.Categories.Remove(removeCategory);
			_context.SaveChanges();
			return RedirectToAction("CategoryView");
		}
		//chinh sau category
		[HttpGet]
		public ActionResult EditCategory(int id)
		{
			var category = _context.Categories.SingleOrDefault(t => t.Id == id);
			var categories = new Category()
			{
				Id = id,
				CategoryName = category.CategoryName
			};
			return View(categories);
		}
		[HttpPost]
		public ActionResult EditCategory(Category viewModel)
		{
			var category = _context.Categories.SingleOrDefault(t => t.Id == viewModel.Id);
			category.CategoryName = viewModel.CategoryName;

			_context.SaveChanges();
			return RedirectToAction("CategoryView", "Staff");
		}
		// assign trainee and trainer
		public ActionResult Assign(int id)
		{
			var assign = new ViewModel.AssignViewModel()
			{
				TraineeCourses = _context.TraineeCourses.Where(t => t.CourseID == id).Include(t => t.Trainee).ToList(),
				TrainerCourses = _context.TrainerCourses.Where(t => t.CourseId == id).Include(t => t.Trainer).ToList(),
				Course = _context.Courses.FirstOrDefault(t => t.Id == id)
			};

			return View(assign);
		}
		[HttpGet]
		public ActionResult AssignTrainee(int id)
		{
			var assignModel = new ViewModel.AssignViewModel()
			{
				Course = _context.Courses.SingleOrDefault(t => t.Id == id),
				Trainees = _context.Users.OfType<Trainee>().ToList(),
			};

			return View(assignModel);
		}
		[HttpPost]
		public ActionResult AssignTrainee(ViewModel.AssignViewModel model)
		{
			var traineeCourse = new TraineeCourse()
			{
				TraineeID = model.TraineeId,
				CourseID = model.Course.Id,
			};
			if (_context.TraineeCourses.Any(t => t.CourseID == model.Course.Id && t.TraineeID == model.TraineeId))
			{
				ModelState.AddModelError("Validation", "Existed before");
				return View(model);
			}
			_context.TraineeCourses.Add(traineeCourse);
			_context.SaveChanges();
			return RedirectToAction("Assign", "Staff", new { @id = model.Course.Id });
		}
		//xoa trainee ra khoi course
		public ActionResult RemoveTrainee(int id)
		{
			var traineeCourse = _context.TraineeCourses.SingleOrDefault(t => t.Id == id);
			_context.TraineeCourses.Remove(traineeCourse);
			_context.SaveChanges();
			return RedirectToAction("Assign", "Staff", new { @id = traineeCourse.CourseID });
		}
		//assign trainer vao course
		[HttpGet]
		public ActionResult AssignTrainer(int id)
		{
			var assignModel = new ViewModel.AssignViewModel()
			{
				Course = _context.Courses.SingleOrDefault(t => t.Id == id),
				Trainers = _context.Users.OfType<Trainer>().ToList(),
			};

			return View(assignModel);
		}

		[HttpPost]
		public ActionResult AssignTrainer(ViewModel.AssignViewModel model)
		{
			var trainerCourse = new TrainerCourse()
			{
				TrainerId = model.TrainerId,
				CourseId = model.Course.Id,
			};
			if (_context.TrainerCourses.Any(t => t.CourseId == model.Course.Id && t.TrainerId == model.TrainerId))
			{
				ModelState.AddModelError("Validation", "Existed before");
				return View(model);
			}
			_context.TrainerCourses.Add(trainerCourse);
			_context.SaveChanges();
			return RedirectToAction("Assign", "Staff", new { @id = model.Course.Id });
		}
		//xoa trainer khỏi course
		public ActionResult RemoveTrainer(int id)
		{
			var trainerCourse = _context.TrainerCourses.SingleOrDefault(t => t.Id == id);
			_context.TrainerCourses.Remove(trainerCourse);
			_context.SaveChanges();
			return RedirectToAction("Assign", "Staff", new { @id = trainerCourse.CourseId });
		}
		//department for trainer


	}
}