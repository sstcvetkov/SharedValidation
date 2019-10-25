using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocaleController : ControllerBase
    {
		private readonly IStringLocalizerFactory _factory;
		private readonly string _assumbly;
		private readonly string _location;

		public LocaleController(IStringLocalizerFactory factory)
		{
			_factory = factory;
			_assumbly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			_location = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
		}

		[HttpGet("Config")]
		public IActionResult GetConfig(string culture)
		{
			if (!string.IsNullOrEmpty(culture))
			{
				CultureInfo.CurrentCulture = new CultureInfo(culture);
				CultureInfo.CurrentUICulture = new CultureInfo(culture);
			}

			var resources = Directory.GetFiles(_location, "*.resx", SearchOption.AllDirectories)
				.Select(x => x.Replace(_location + Path.DirectorySeparatorChar, string.Empty))
				.Select(x => x.Substring(0, x.IndexOf('.')))
				.Distinct();

			var config = new Dictionary<string, Dictionary<string, string>>();
			foreach (var resource in resources.Select(x => x.Replace('\\', '.')))
			{
				var section = _factory.Create(resource, _assumbly)
					.GetAllStrings()
					.OrderBy(x => x.Name)
					.ToDictionary(x => x.Name, x => x.Value);
				config.Add(resource.Replace('.', '-'), section);
			}

			var result = JsonConvert.SerializeObject(config, Formatting.Indented);

			return Ok(result);
		}
    }
}
