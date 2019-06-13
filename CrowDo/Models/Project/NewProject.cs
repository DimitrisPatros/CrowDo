using System;

namespace CrowDo.Models.Project {
	public struct NewProject
	{
		public string Email { get; set; }
		public string ProjectTitle { get; set; }
		public double FundingBudjet { get; set; }
	}
	public struct Category
	{
		public string Email { get; set; }
		public int ProjectId { get; set; }
		public string CategoryName { get; set; }
	}
	public struct NewPledgeOptionToProject
	{
		public string Email { get; set; }
		public int ProjectId { get; set; }
		public string TitleOfPledge { get; set; }
		public double PriceOfPledge { get; set; }
		public DateTime EstimateDelivery { get; set; }
		public DateTime DurationOfPldege { get; set; }
		public int NumberOfAvailablePledges { get; set; }
		public string Descritpion { get; set; }
	}
	public struct NewProjectInfo
	{
		public string Email { get; set; }
		public int ProjectId { get; set; }
		public string Title { get; set; }
		public string Filename { get; set; }
		public string Descritpion { get; set; }
	}
	public struct RemovePledge
	{
		public string Email { get; set; }
		public int ProjectId { get; set; }
		public int PledgeOptionsId { get; set; }
	}
	public struct UpdatePledges
	{
		public string Email { get; set; }
		public int ProjectId { get; set; }
		public int PledgeOptionsId { get; set; }
		public string TitleOfPledge { get; set; }
		public double PriceOfPledge { get; set; }
		public DateTime EstimateDelivery { get; set; }
		public DateTime DurationOfPldegey { get; set; }
		public int NumberOfAvailablePledges { get; set; }
		public string Description { get; set; }

	}
}