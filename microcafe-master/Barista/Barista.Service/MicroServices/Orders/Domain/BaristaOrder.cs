﻿using System;

using MicroServices.Common;
using Barista.Common.Events;

namespace Barista.Service.MicroServices.Orders.Domain
{
    public class BaristaOrder : Aggregate
    {
        internal BaristaOrder() { }

        //We use this constructor to create an order in the service, based on an external ProductPlaced
        //event from the cashier. We don't have any user commands to create an order in the barista domain,
        //only external events from the cashier service.
        internal BaristaOrder(Guid id, Guid productId, int quantity)
        {
            if (quantity <= 0) throw new ArgumentOutOfRangeException("quantity", "quantity must be a number from 1 and up");
            if (Guid.Empty.Equals(productId)) throw new ArgumentNullException("productId", "A valid Product Guid must be provided");

            ApplyEvent(new OrderPlaced(id, productId, quantity));
        }

        public Guid ProductId { get; private set; }
        public bool IsCompleted { get; private set; }
        public int Quantity { get; private set; }        

        private void Apply(OrderPlaced o)
        {
            Id = o.Id;
            ProductId = o.ProductId;
            Quantity = o.Quantity;
        }

        private void Apply(OrderPrepared c)
        {
            IsCompleted = true;
        }

        public void CompletePreparation(int originalVersion)
        {
            //can only update the current version of an aggregate
            ValidateVersion(originalVersion);

            ApplyEvent(new OrderPrepared(Id));
        }

        void ValidateVersion(int version)
        {
            if (Version != version)
            {
                throw new ArgumentOutOfRangeException("version", "Invalid version specified: the version is out of sync.");
            }
        }
    }
}
