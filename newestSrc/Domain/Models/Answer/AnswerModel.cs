using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Answer
{
    public class AnswerModel
    {
        [Key] 
        public int Id { get; set; }
        [MaxLength(64)] 
        public string Answer { get; set; }
        public override string ToString() { return Answer; }
    }
}
