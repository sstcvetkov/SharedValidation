using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Server.Requests;

namespace Server.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IStringLocalizer<AccountController> _localizer;

		public AccountController(IStringLocalizer<AccountController> localizer)
		{
			_localizer = localizer;
		}

		[HttpPost(nameof(Registration))]
		public ActionResult Registration([FromBody]RegistrationRequest request)
		{
			return Ok(string.Format(_localizer["RegisteredMessage"], request.Name));
		}
	}
}
