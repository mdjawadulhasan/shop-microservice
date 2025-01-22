﻿global using Ordering.Domain.Enums;
global using BuildingBlocks.CQRS;
global using Ordering.Application.Dtos;
global using Ordering.Application.Data;
global using Ordering.Domain.Models;
global using Ordering.Domain.ValueObjects;
global using Microsoft.EntityFrameworkCore;
global using FluentValidation;
global using Ordering.Application.Exceptions;
global using BuildingBlocks.Behaviours;
global using Microsoft.Extensions.DependencyInjection;
global using System.Reflection;
global using MediatR;
global using Microsoft.Extensions.Logging;
global using Ordering.Domain.Events;
global using Ordering.Application.Extensions;
global using BuildingBlocks.Messaging.Events;
global using MassTransit;
global using Ordering.Application.Orders.Commands.CreateOrder;
global using BuildingBlocks.Messaging.MassTransit;
global using Microsoft.Extensions.Configuration;
