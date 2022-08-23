using System.ComponentModel.DataAnnotations;

namespace StepMediaAssignment.Models
{
    public class UserInputView : IValidatableObject
    {
        [Required(ErrorMessage = "This field cannot be empty.")] 
        public string CommaSeparatedNumbers { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var splitString = CommaSeparatedNumbers.Split(",");
            if (splitString.Length < 30)
            {
                yield return new ValidationResult("The size of the input items cannot be less than 30"
                , new []{ nameof(CommaSeparatedNumbers) });
            }

            if (splitString.Any(string.IsNullOrWhiteSpace))
            {
                yield return new ValidationResult("One of the input items is empty. Please check your commas!"
                    , new []{ nameof(CommaSeparatedNumbers) });
            }
        }
    }
}
