// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using OnlineStoreApi.Application.Sizes.Commands.AddEdit;

namespace OnlineStoreApi.Application.Sizes.Commands.Delete;

public class DeleteSizeCommandValidator : AbstractValidator<DeleteSizeCommand>
{
        public DeleteSizeCommandValidator()
        {
        }
}
    

