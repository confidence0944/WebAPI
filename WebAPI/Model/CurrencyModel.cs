using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model
{
    public class CurrencyModel
    {
        public string Currency { get; set; }

        public string CurrencyName { get; set; }

        public class CurrencyModelValidator : AbstractValidator<CurrencyModel>
        {
            public CurrencyModelValidator()
            {
                RuleFor(person => person.Currency).NotEmpty().WithMessage("請輸入幣別");
                RuleFor(person => person.Currency).NotEmpty().WithMessage("請輸入幣別名稱");
            }
        }
    }
}
