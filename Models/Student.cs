using System.ComponentModel.DataAnnotations;

namespace StudentAdoNet.Models;

public class Student
{

    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [StringLength(100, ErrorMessage = "Email address cannot be longer than 100 characters.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a course.")]
    [RegularExpression(
        "^(Computer Science|Information Technology|Electronics|Mechanical Engineering|Civil Engineering)$",
        ErrorMessage = "Please select a valid course.")]
    [Display(Name = "Course")]
    public string Course { get; set; } = string.Empty;


    [Display(Name = "Full Name")]
    public string FullName => $"{FirstName} {LastName}".Trim();


    public static readonly string[] AllowedCourses =
    {
        "Computer Science",
        "Information Technology",
        "Electronics",
        "Mechanical Engineering",
        "Civil Engineering"
    };
}
