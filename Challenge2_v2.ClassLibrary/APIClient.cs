using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2_v2.ClassLibrary
{
    public class APIClient
    {
        public async Task DeleteOwnerPetsTreatments(List<Pet> pPets)
        {
            HttpClient client = new HttpClient();
            List<Treatment> treatmentsToDelete = new List<Treatment>();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            foreach (Pet p in pPets)
            {
                treatmentsToDelete = await GetTreatmentsByPetID(p.PetID);
            }
            foreach (Treatment t in treatmentsToDelete)
            {
                string treatmentToDeleteJSON = JsonConvert.SerializeObject(t, Formatting.None);
                var content = new StringContent(treatmentToDeleteJSON, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync("treatments/" + t.TreatmentID);
            }
        }

        public async Task DeleteOwnerPets(int pOwnerID)
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            List<Pet> petsToDelete = await GetPetsByOwnerID(pOwnerID);

            foreach (Pet p in petsToDelete)
            {
                string petToDeleteJSON = JsonConvert.SerializeObject(p, Formatting.None);
                var content = new StringContent(petToDeleteJSON, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync("bookings/" + p.PetID);
            }
        }

        public async Task<List<Pet>> GetPetsByOwnerID(int pID)
        {
            List<Pet> allPets = await GetPets();

            List<Pet> ownerPets = new List<Pet>();

            return ownerPets = allPets.Where(p => p.PetID == pID).ToList();
        }

        public async Task<int> GetPetIDByOwnerIDAndName(int pPetID, string pPetName)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            List<Pet> allPets = await GetPets();

            int petID = (from p in allPets
                        where p.PetID == pPetID
                        where p.Name == pPetName
                        select p.PetID).FirstOrDefault();
            return petID;
        }

        public async Task<List<Treatment>> GetTreatmentsByPetID(int pID)
        {
            List<Treatment> allTreatments = await GetTreatments();

            List<Treatment> petTreatments = new List<Treatment>();

            return petTreatments = allTreatments.Where(p => p.PetID == pID).ToList();
        }

        public async Task UpdateOwner(Owner pOwnerToUpdate)
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            string ownerToUpdateJSON = JsonConvert.SerializeObject(pOwnerToUpdate, Formatting.None);

            var content = new StringContent(ownerToUpdateJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("owners/" + pOwnerToUpdate.OwnerID, content);

        }

        public async Task CreateOwner(Owner pNewOwner)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            string newOwnerJSON = JsonConvert.SerializeObject(pNewOwner, Formatting.None);

            var content = new StringContent(newOwnerJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("owners", content);

        }

        public async Task<int> GetNextOwnerID()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            HttpResponseMessage response = await client.GetAsync("ownersonlies");

            var content = await response.Content.ReadAsStringAsync();

            List<Owner> owners = JsonConvert.DeserializeObject<List<Owner>>(content);

            return owners.Max(c => c.OwnerID) + 1;
        }


        public async Task<List<Owner>> GetOwners()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            HttpResponseMessage response = await client.GetAsync("ownersonlies");

            var content = await response.Content.ReadAsStringAsync();

            List<Owner> owners = JsonConvert.DeserializeObject<List<Owner>>(content);

            return owners;
        }

        public async Task<List<Procedure>> GetProcedures()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            HttpResponseMessage response = await client.GetAsync("proceduresonlies");

            var content = await response.Content.ReadAsStringAsync();

            List<Procedure> procedures = JsonConvert.DeserializeObject<List<Procedure>>(content);

            return procedures;
        }

        public async Task<List<Pet>> GetPets()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            HttpResponseMessage response = await client.GetAsync("petsonlies");

            var content = await response.Content.ReadAsStringAsync();

            List<Pet> pets = JsonConvert.DeserializeObject<List<Pet>>(content);

            return pets;
        }

        public async Task<List<Treatment>> GetTreatments()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            HttpResponseMessage response = await client.GetAsync("treatmentsonlies");

            var content = await response.Content.ReadAsStringAsync();

            List<Treatment> treatments = JsonConvert.DeserializeObject<List<Treatment>>(content);

            return treatments;
        }

        public async Task DeleteOwner(Owner pOwnerToDelete)
        { 
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://challenge2v2.azurewebsites.net/api/");

            string ownerToDeleteJSON = JsonConvert.SerializeObject(pOwnerToDelete, Formatting.None);

            var content = new StringContent(ownerToDeleteJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.DeleteAsync("owners/" + pOwnerToDelete.OwnerID);

        }
    }
}
