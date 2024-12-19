using System.ComponentModel.DataAnnotations;

namespace Common.Domain;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDelete { get; set; }
}