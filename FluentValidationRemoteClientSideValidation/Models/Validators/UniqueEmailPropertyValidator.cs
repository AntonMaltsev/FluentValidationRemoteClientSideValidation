using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace FluentValidationRemoteClientSideValidation.Models.Validators
{
    public class UniqueEmailPropertyValidator : FluentValidationPropertyValidator
    {
        public UniqueEmailPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext, PropertyRule rule, IPropertyValidator validator)
            : base(metadata, controllerContext, rule, validator)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (!this.ShouldGenerateClientSideRules())
                yield break;

            var formatter = new MessageFormatter().AppendPropertyName(Rule.PropertyName);
            string message = formatter.BuildMessage(Validator.ErrorMessageSource.GetString());

            var rule = new ModelClientValidationRule
            {
                ValidationType = "remote",
                ErrorMessage = message
            };
            rule.ValidationParameters.Add("url", "/api/validation/uniqueemail");

            yield return rule;
        }

    }
}