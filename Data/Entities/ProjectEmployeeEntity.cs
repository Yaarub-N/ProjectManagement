using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

     //chat gpt4o
namespace Data.Entities
{
    public class ProjectEmployeeEntity
    {
        [Key]
        [Column(Order = 1)]
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; } = null!;

        [Key]
        [Column(Order = 2)]
        public int EmployeeId { get; set; }
        public EmployeeEntity Employee { get; set; } = null!;
    }
}
