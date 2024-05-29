
using System.Text.Json.Serialization;
using CqrsMediatr.Domain.Validations;

namespace CqrsMediatr.Domain.Entities
{
    public sealed class Member : Entity
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Gender { get; private set; }
        public string? Email { get; private set; }
        public bool? IsActive { get; private set; }

        [JsonConstructor]
        public Member(int id, string firstName, string lastName, string gender, string email, bool? isActive)
        {
            DomainValidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidateDomain(firstName, lastName, gender, email, isActive);
        }

        public Member(string firstName, string lastName, string gender, string email, bool? isActive)
        {
            ValidateDomain(firstName, lastName, gender, email, isActive);
        }

        public Member() { }

        public void Update(string firstName, string lastName, string gender, string email, bool? IsActive)
        {
            ValidateDomain(firstName, lastName, gender, email, IsActive);
        }

        

        private void ValidateDomain(string firstName, string lastName, string gender, string email, bool? isActive)
        {
            DomainValidation.When(string.IsNullOrEmpty(firstName), "Invalid name. FirstName is required");

            DomainValidation.When(firstName.Length < 3,
                "Invalid name, too short, minimum 3 characters");

            DomainValidation.When(string.IsNullOrEmpty(lastName), "Invalid lastname. Lastname is required");

            DomainValidation.When(lastName.Length < 3, "Invalid lastname, too short, minimum 3 characters");

            DomainValidation.When(email?.Length > 250, "Invalid email, too long, maximum 250 characters");

            DomainValidation.When(email?.Length < 6, "Invalid email, too short, minimum 6 characters");

            DomainValidation.When(string.IsNullOrEmpty(gender), "Invalid gender, Gender is required");

            DomainValidation.When(!isActive.HasValue, "Must define activity");

            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            IsActive = isActive;

        }

    }
}