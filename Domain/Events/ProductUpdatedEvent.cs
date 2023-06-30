// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using OnlineStoreApi.Domain.Entities;
using OnlineStoreApi.Domain.Common;
using OnlineStoreApi.Domain.Entities;

namespace OnlineStoreApi.Domain.Events;


    public class ProductUpdatedEvent : BaseEvent
{
        public ProductUpdatedEvent(Product item)
        {
            Item = item;
        }

        public Product Item { get; }
    }

