﻿using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using Barista.Service.DataTransferObjects.Commands;
using Barista.Service.MicroServices.Orders.Commands;
using Barista.Service.MicroServices.Orders.Handlers;
using MicroServices.Common.Exceptions;

namespace Barista.Service.Controllers
{
    public class OrderController : ApiController
    {
        private readonly BaristaOrderCommandHandlers handler;

        public OrderController()
            : this(ServiceLocator.OrderCommands)
        {}

        public OrderController(BaristaOrderCommandHandlers handler)
        {
            this.handler = handler;
        }


        [HttpPost]
        [Route("api/orders/{id:guid}/prepared")]
        public IHttpActionResult Post(Guid id, CompletePreparationCommand cmd)
        {            
            try
            {
                var command = new CompleteOrder(id, cmd.Version);
                handler.Handle(command);

                return Ok();
            }
            catch (AggregateNotFoundException)
            {
                return NotFound();
            }
            catch (AggregateDeletedException)
            {
                return Conflict();
            }
        }

        //[HttpPost]
        //[Route("api/products/{id:guid}/cancel")]
        //public IHttpActionResult Post(Guid id, CancelOrderCommand cmd)
        //{
        //    try
        //    {
        //        var command = new CancelOrder(id, cmd.Version);
        //        handler.Handle(command);

        //        return Ok();
        //    }
        //    catch (AggregateNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //    catch (AggregateDeletedException)
        //    {
        //        return Conflict();
        //    }
        //}
    }
}
