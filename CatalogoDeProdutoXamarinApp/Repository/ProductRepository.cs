using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatalogoDeProdutoXamarinApp.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.Essentials;

namespace CatalogoDeProdutoXamarinApp.Repository
{
    class ProductRepository : IRepository<Product>
    {
        HttpClient client;
        private IEnumerable<Product> _items;

        public ProductRepository()
        {
            client.BaseAddress = new Uri($"{MainActivity.BackendUrl}");

            _items = new List<Product>();
        }

        private bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> AddItemAsync(Product item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"v1/item", new StringContent
                (serializedItem,Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"v1/item/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<Product> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"v1/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Product>
                    (json));
            }

            return null;
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"v1/item");
                _items = await Task.Run(() => 
                    JsonConvert.DeserializeObject<IEnumerable<Product>>(json));

            }

            return _items;
        }

        public async Task<bool> UpdateItemAsync(Product item)
        {
            if (item == null ||  !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var  byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"v1/item/{item}"),
                byteContent);

            return response.IsSuccessStatusCode;
        }
    }
}