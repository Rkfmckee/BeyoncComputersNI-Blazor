using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Dal.Entities;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
}
