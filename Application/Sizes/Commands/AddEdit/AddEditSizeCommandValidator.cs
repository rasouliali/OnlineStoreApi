﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;

namespace OnlineStoreApi.Application.Sizes.Commands.AddEdit;

public class AddEditSizeCommandValidator : AbstractValidator<AddEditSizeCommand>
{
    public AddEditSizeCommandValidator()
    {
        RuleFor(v => v.Name)
             .MaximumLength(200)
             .NotEmpty();
    }
}

