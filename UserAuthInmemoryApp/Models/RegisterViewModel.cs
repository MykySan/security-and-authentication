using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password {get; set;} = string.Empty;
}