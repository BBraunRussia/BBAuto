using System;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.Policy
{
  public class PolicyModel
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public int PolicyTypeId { get; set; }
    public int OwnerId { get; set; }
    public int CompId { get; set; }
    public string Number { get; set; }
    public DateTime DateBegin { get; set; }
    public DateTime DateEnd { get; set; }
    public decimal? Pay1 { get; set; }
    public string File { get; set; }
    public decimal? LimitCost { get; set; }
    public decimal? Pay2 { get; set; }
    public DateTime? Pay2Date { get; set; }
    public int? AccountId { get; set; }
    public int? AccountId2 { get; set; }
    public bool NotificationSent { get; set; }
    public string Comment { get; set; }

    public PolicyType PolicyType => (PolicyType) PolicyTypeId;
  }
}
