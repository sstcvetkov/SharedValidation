using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Server.Requests
{
	public class RegistrationRequest
	{
		[StringLength(maximumLength: 50, MinimumLength = 2,
			ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
		[Required(ErrorMessage = "Требуется имя")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Требуется адрес эл. почты")]
		[EmailAddress(ErrorMessage = "Некорректный адрес эл. почты")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Требуется пароль")]
		[MaxLength(100, ErrorMessage = "{0} не может превышать {1} символов")]
		[MinLength(6, ErrorMessage ="{0} должен быть минимум {1} символов")]
		[DisplayName("Пароль")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Требуется возраст")]
		[Range(18,150, ErrorMessage = "Возраст должен быть в пределах от 18 до 150")]
		public string Age { get; set; }

		[DisplayName("Культура")]
        public string Culture { get; set; }
	}
}
