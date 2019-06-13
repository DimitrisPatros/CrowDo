using System;

namespace DatabaseContext.SeedData {
	public class ProjectSeedViewModel {
		public string nameOfProject { get; set; }
		public string description { get; set; }
		public string creator { get; set; }
		public string keywords { get; set; }
		public int demandedfunds { get; set; }
		public DateTime dateOfCreation { get; set; }
		public string deadline { get; set; }
		public int estimatedDurationInMonths { get; set; }
	}
}