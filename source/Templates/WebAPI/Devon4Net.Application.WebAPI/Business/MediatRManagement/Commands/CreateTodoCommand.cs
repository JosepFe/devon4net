﻿using Devon4Net.Application.WebAPI.Business.MediatRManagement.Dto;
using Devon4Net.Infrastructure.MediatR.Command;

namespace Devon4Net.Application.WebAPI.Business.MediatRManagement.Commands
{
    /// <summary>
    /// THe command to create a TO-DO
    /// </summary>
    public record CreateTodoCommand : CommandBase<TodoResultDto>
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructor of the query 
        /// </summary>
        /// <param name="description"></param>
        public CreateTodoCommand(string description)
        {
            Description = description;
        }
    }
}
