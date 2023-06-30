// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using OnlineStoreApi.Application.Colors.Commands.AddEdit;

namespace OnlineStoreApi.Application.Colors.Commands.Delete;

public class DeleteColorCommandValidator : AbstractValidator<DeleteColorCommand>
{
        public DeleteColorCommandValidator()
        {
        }
}
    

