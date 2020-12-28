using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GalvProducts.Api.Tests
{
    /// <summary>
    /// This is a model validation helper for model validation. Specifically used for unit testing.
    /// We cannot directly validate models in the request flow as in MVC/Web API. This helper does the same job of model validation
    /// </summary>
    public class ValidationHelper
    {
        public static IList<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, results, true);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);
            return results;
        }
    }
}
