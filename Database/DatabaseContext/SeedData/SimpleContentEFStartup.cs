using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatabaseContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DatabaseContext.SeedData {
	public static class SimpleContentEFStartup {
		public static async Task InitializeDatabaseAsync(IServiceProvider services) {
			var context = services.GetRequiredService<CrowDoDbContext>();
			if (!await context.Set<User>().AnyAsync()) {
				try {
					var creatorsInfoText = File.ReadAllText(@"SeedData\Creators_info.json");
					var creatorsInfo = JsonConvert.DeserializeObject<List<User>>(creatorsInfoText);
					await context.Set<User>().AddRangeAsync(creatorsInfo);
					await context.SaveChangesAsync();
				} catch (FileNotFoundException) { }
			}

			if (!await context.Set<Project>().AnyAsync()) {
				try {
					var projectInfoText = File.ReadAllText(@"SeedData\Initial_data_for_your_project.json");
					var projectSeedViewModels =
						JsonConvert.DeserializeObject<List<ProjectSeedViewModel>>(projectInfoText);
					var projects = projectSeedViewModels.Select(async p => {
						var user = await context.Users.SingleOrDefaultAsync(u => u.Email == p.creator);
						if (user != null) {
							//TODO: Check Default Property Values 
							return new Project() {
								UserId = user.UserId,
								ProjectTitle = p.nameOfProject,
								PledgeOfFunding = p.demandedfunds,
								CreationDate = p.dateOfCreation,
								ProjectInfo = new List<ProjectInfo>() {
									new ProjectInfo() {Description = p.description, Title = p.nameOfProject}
								}
							};
						}

						return null;
					}).Select(t => t.Result).Where(i => i != null);
					await context.Set<Project>().AddRangeAsync(projects);
					await context.SaveChangesAsync();
				} catch (FileNotFoundException) { }
			}
		}
	}
}