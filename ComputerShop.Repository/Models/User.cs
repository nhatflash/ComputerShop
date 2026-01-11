using ComputerShop.Repository.Enums;

namespace ComputerShop.Repository.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? PhoneNumber { get;set; }

        public Gender? Gender { get; set; }

        public string? Address { get; set; }

        public string? Ward { get; set; }

        public string? District { get; set; }

        public string? City { get; set; }

        public string? Province { get; set; }

        public string? PostalCode { get; set; }

        public string? ProfileImage { get; set; }

        public UserStatus Status { get; set; }

        public DateTimeOffset? VerifiedAt { get; set; }

        public DateTimeOffset CreatedAt { get; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
    }
}
