// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;

namespace OnlineStoreApi.Application.Products.Commands.AddEdit;

public class AddEditProductCommandValidator : AbstractValidator<AddEditProductCommand>
{
    public AddEditProductCommandValidator()
    {
        RuleFor(v => v.Name)
             .MaximumLength(200)
             .NotEmpty();
    }
}

