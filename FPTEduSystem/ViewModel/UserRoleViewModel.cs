using FPTEduSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTEduSystem.ViewModel
{
	public class UserRoleViewModel
	{
		public ApplicationUser UserName { get; set; }
		public ApplicationUser Email { get; set; }
		public ApplicationUser Name { get; set; }
		public RegisterViewModel RoleName { get; set; }
	}
}