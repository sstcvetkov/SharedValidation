using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Requests;

namespace Server.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		[HttpPost(nameof(Registration))]
		public ActionResult Registration([FromBody]RegistrationRequest request)
		{
			return Ok($"{request.Name}, вы зарегистрированы!");
		}
	}
}
