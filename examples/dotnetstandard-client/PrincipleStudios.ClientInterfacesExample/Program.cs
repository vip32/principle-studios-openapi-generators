using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PrincipleStudios.ClientInterfacesExample.Clients.Petstore;

namespace PrincipleStudios.ClientInterfacesExample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Petstore();
        }

        private static async Task Petstore()
        {
            using var httpClient = new HttpClient();
            using var createdPetResponse = await httpClient.AddPet(new Clients.Petstore.NewPet(Name: "Fido", Tag: "Dog"));
            var pet = createdPetResponse is Operations.AddPetReturnType.Ok r1 ? r1.Body : throw new InvalidOperationException();
            using var foundPetResponse = await httpClient.FindPetById(pet.Id);
            var foundPet = foundPetResponse is Operations.FindPetByIdReturnType.Ok r2 ? r2.Body : throw new InvalidOperationException();
            System.Diagnostics.Debug.Assert(pet.Name == foundPet.Name);
            using var foundPetsResponse = await httpClient.FindPets(new[] { "Dog" }, 1);
            var pets = foundPetsResponse is Operations.FindPetsReturnType.Ok r3 ? r3.Body.ToList() : throw new InvalidOperationException();
            System.Diagnostics.Debug.Assert(pets.Count == 1);
            System.Diagnostics.Debug.Assert(pet.Name == pets[0].Name);
            using var deletePetResponse = await httpClient.DeletePet(pet.Id);
            _ = deletePetResponse is Operations.DeletePetReturnType.NoContent r4 ? r4 : throw new InvalidOperationException();
            using var finalFindResponse = await httpClient.FindPets(new[] { "Dog " }, 1);
            var finalPets = finalFindResponse is Operations.FindPetsReturnType.Ok r5 ? r5.Body.ToList() : throw new InvalidOperationException();
            System.Diagnostics.Debug.Assert(finalPets.Count == 0);
        }

    }
}
