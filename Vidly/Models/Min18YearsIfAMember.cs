using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    // La seguente Validazione controllerà se il customer ha meno di 18 anni. In questo caso, il MembershipType dovrà essere necessariamente "Pay as You Go"
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer= (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.BirthDate == null)
                return new ValidationResult("Birthdate is required.");

            // Calcoliamo l'età del customer (per esercizio lo facciamo basandoci solo sull'anno di nascita)

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be at least 18 years old to go on a membership.");

        }
    }
}