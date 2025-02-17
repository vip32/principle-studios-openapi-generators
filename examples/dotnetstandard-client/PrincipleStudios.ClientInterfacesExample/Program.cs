﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//using PrincipleStudios.ClientInterfacesExample.Clients.Petstore;
using PrincipleStudios.ClientInterfacesExample.ApiClient;
using PrincipleStudios.OpenApiCodegen.Json.Extensions;

namespace PrincipleStudios.ClientInterfacesExample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //EnumConversion();

            await Petstore();
        }

        //private static void EnumConversion()
        //{
        //    var sold = System.Text.Json.JsonSerializer.Serialize(Clients.Petstore3.FindPetsByStatusStatusItem.Sold);
        //    System.Diagnostics.Debug.Assert(sold == "\"sold\"");
        //    var enumValue = System.Text.Json.JsonSerializer.Deserialize<Clients.Petstore3.FindPetsByStatusStatusItem>(sold);
        //    System.Diagnostics.Debug.Assert(Clients.Petstore3.FindPetsByStatusStatusItem.Sold == enumValue);
        //}

        private static async Task Petstore()
        {
            using var httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:5001") };
            using var getResponse = await httpClient.GetByIdentity(Guid.NewGuid().ToString());

            if (!getResponse.Response.IsSuccessStatusCode)
            {
                if (getResponse is Operations.GetByIdentityReturnType.InternalServerError error)
                {
                    Console.WriteLine(error.Body.Title);
                }
            }

            var cart = getResponse is Operations.GetByIdentityReturnType.Ok result ? result.Body : throw new InvalidOperationException("no result");
        }

        //    var pet = createdPetResponse is Operations.AddPetReturnType.Ok r1 ? r1.Body : throw new InvalidOperationException();

        //    using var foundPetResponse = await httpClient.FindPetById(pet.Id);
        //    var foundPet = foundPetResponse is Operations.FindPetByIdReturnType.Ok r2 ? r2.Body : throw new InvalidOperationException();
        //    System.Diagnostics.Debug.Assert(pet.Name == foundPet.Name);
        //    using var foundPetsResponse = await httpClient.FindPets(Optional.Create<IEnumerable<string>>(new[] { "Dog", "Cᴀt" }), Optional.Create(1));
        //    var pets = foundPetsResponse is Operations.FindPetsReturnType.Ok r3 ? r3.Body.ToList() : throw new InvalidOperationException();
        //    System.Diagnostics.Debug.Assert(foundPetsResponse.Response.RequestMessage?.RequestUri?.OriginalString.StartsWith("https://localhost:5001/api/") ?? false);
        //    var query = System.Web.HttpUtility.ParseQueryString(foundPetsResponse.Response.RequestMessage?.RequestUri?.Query!);
        //    System.Diagnostics.Debug.Assert(query.GetValues("tags")!.Contains("Dog"));
        //    System.Diagnostics.Debug.Assert(query.GetValues("tags")!.Contains("Cᴀt")); // verifies that unicode is handled properly
        //    System.Diagnostics.Debug.Assert(query["limit"] == "1");
        //    System.Diagnostics.Debug.Assert(pets.Count == 1);
        //    System.Diagnostics.Debug.Assert(pet.Name == pets[0].Name);
        //    using var deletePetResponse = await httpClient.DeletePet(pet.Id);
        //    _ = deletePetResponse is Operations.DeletePetReturnType.NoContent r4 ? r4 : throw new InvalidOperationException();
        //    using var finalFindResponse = await httpClient.FindPets(Optional.Create<IEnumerable<string>>(new[] { "NoDog" }), Optional.Create(1));
        //    var finalPets = finalFindResponse is Operations.FindPetsReturnType.Ok r5 ? r5.Body.ToList() : throw new InvalidOperationException();
        //    System.Diagnostics.Debug.Assert(finalPets.Count == 0);
        //}

    }
}
