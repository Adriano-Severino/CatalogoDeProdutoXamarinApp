using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using  CatalogoDeProdutoXamarinApp.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CatalogoDeProdutoXamarinApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CatalogoDeProdutoXamarinApp.Activity
{
    [Activity(Label = "ActivityAddDeleteEditCategory")]
    public class ActivityAddDeleteEditCategory : Android.App.Activity
    {
        private ListView ListCategory;
        private Button GetCategory;

        private const string Url = "localhost:5000/v1/categorias";
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Category> _categories;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AddEditDeleteCategory);

            ListCategory = FindViewById<ListView>(Resource.Id.listCategory);
            GetCategory = FindViewById<Button>(Resource.Id.GetCategory);

            GetCategory.Click += GetCategory_Click;



          


        }

        private async void GetCategory_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                // envia a requisição GET
                var uri =  $"{MainActivity.UrlApi}" + "/v1/categorias";
                var result = await client.GetStringAsync(uri);
                var categories = JsonConvert.DeserializeObject<List<Category>>(result);
                // gera a saida

                ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1,
                    categories);

                ListCategory.Adapter = adapter;
            }
        }


    }
}