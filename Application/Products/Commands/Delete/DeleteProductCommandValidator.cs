// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using OnlineStoreApi.Application.Products.Commands.AddEdit;

namespace OnlineStoreApi.Application.Products.Commands.Delete;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
        public DeleteProductCommandValidator()
        {
        }
}
    

