﻿using Buriti_store.Catalog.Application.Interfaces;
using Buriti_store.Catalog.Application.Services;
using Buriti_store.Catalog.Data;
using Buriti_store.Catalog.Data.Repositories;
using Buriti_store.Catalog.Domain;
using Buriti_store.Catalog.Domain.Events;
using Buriti_store.Catalog.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Buriti_Store.Orders.Application.Commands;
using Buriti_Store.Core.Messages.CommonMessages.Notifications;
using Buriti_Store.Core.Communication.Mediator;
using Buriti_Store.Orders.Application.Events;
using Buriti_Store.Orders.Application.Queries;
using Buriti_Store.Orders.Application.Queries.Interfaces;

namespace Buriti_Store.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catalogo
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>(); 

            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<ProductOutOfStockEvent>, ProductEventHandler>();

            //Handler
            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();

            //Commands
            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyVoucherOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, OrderCommandHandler>();
            //services.AddScoped<IRequestHandler<StartOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, OrderCommandHandler>();

            //Events
            services.AddScoped<INotificationHandler<OrderDraftStartedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderUpdateEvent>, OrderEventHandler>();

        }
    }
}