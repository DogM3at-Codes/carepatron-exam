using api.Data;
using api.Models;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Net.Sockets;

namespace api
{
    public static class ClientEndpointsV1
    {
        public static IEndpointRouteBuilder MapClientApiEndpoints(this IEndpointRouteBuilder routes)
        {
            var baseUrl = "/api/v1";

            routes.MapGet(baseUrl + "/clients", GetClients).WithName("GetClients"); 

            routes.MapGet(baseUrl + "/clients/{name}", SearchClients).WithName("SearchClients");

            routes.MapPost(baseUrl + "/clients", CreateClient).WithName("CreateClient");

            routes.MapPut(baseUrl + "/clients/{id}", UpdateClient).WithName("UpdateClient");

            return routes;
        } 

        public static async Task<IResult> GetClients(IClientService clientService)
        {
            var clients =  await clientService.Get();
            return Results.Ok(clients);
        }

        public static async Task<IResult> SearchClients(string name, IClientService clientService)
        {
            return await clientService.SearchClient(name);
           
        }

        public static async Task<IResult> CreateClient(Client client, IClientService clientService)
        {
            return await clientService.Create(client);
        }

        public static async Task<IResult> UpdateClient(string id, Client client, IClientService clientService)
        {
            return await clientService.Update(id, client);
        }
    }
}
