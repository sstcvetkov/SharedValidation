using Server.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Server.Requests
{
	public class RegistrationRequest
	{
		[Resx(sectionName: "Controllers.AccountController")]
		public string Name { get; set; }

		[Resx(sectionName: "Controllers.AccountController")]
		public string Email { get; set; }

		[Resx(sectionName: "Controllers.AccountController")]
		public string Password { get; set; }

		[Resx(sectionName: "Controllers.AccountController")]
		public string Age { get; set; }

		[Resx(sectionName: "Controllers.AccountController")]
        public string Culture { get; set; }
	}
}
