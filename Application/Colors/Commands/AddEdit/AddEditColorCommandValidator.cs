// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;

namespace OnlineStoreApi.Application.Colors.Commands.AddEdit;

public class AddEditColorCommandValidator : AbstractValidator<AddEditColorCommand>
{
    public AddEditColorCommandValidator()
    {
        RuleFor(v => v.Name)
             .MaximumLength(200)
             .NotEmpty();
    }
}

