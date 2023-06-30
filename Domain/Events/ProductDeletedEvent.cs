// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Domain.Common;

namespace OnlineStoreApi.Domain.Events;

    public class ProductDeletedEvent : BaseEvent
{
        public ProductDeletedEvent(Product item)
        {
            Item = item;
        }

        public Product Item { get; }
    }

