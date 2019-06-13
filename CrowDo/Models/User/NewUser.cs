using System;

namespace CrowDo.Models.User {
	public struct NewUser
	{
		public string Email { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Address { get; set; }
		public string Country { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}