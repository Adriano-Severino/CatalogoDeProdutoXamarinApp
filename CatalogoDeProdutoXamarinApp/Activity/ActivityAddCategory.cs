using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Net.Http.Headers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CatalogoDeProdutoXamarinApp.Models;
using Newtonsoft.Json;

namespace CatalogoDeProdutoXamarinApp.Activity
{
    [Activity(Label = "ActivityAddCategory")]
    public class ActivityAddCategory : Android.App.Activity
    {
        private Button btnAddCategory;
        private EditText editTextAddCategory;

        readonly HttpClient _client = new HttpClient();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.AddCategory);

            //===============================================================================
            editTextAddCategory = FindViewById<EditText>(Resource.Id.editTextAddCategory);
            btnAddCategory = FindViewById<Button>(Resource.Id.btnAddCategory);

            //evento
            btnAddCategory.Click += BtnAddCategory_Click;
        }

        private async void BtnAddCategory_Click(object sender, EventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.Titulo = editTextAddCategory.Text;
            var url = $"{MainActivity.UrlApi}" + "/v1/categorias/";// + id.ToString(); ;
            var uri = new Uri(url);
            var json = JsonConvert.SerializeObject(categoria);
             _client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
             HttpResponseMessage response;
             var content = new StringContent(json, Encoding.UTF8, "application/json");
             response = await _client.PostAsync(uri, content);

             editTextAddCategory.Text = "";

             if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
             {
                 Toast.MakeText(this,"Categoria adicionado", ToastLength.Long).Show();
             }

             else
             {
                 Toast.MakeText(this, "Categoria não foi adicionado", ToastLength.Long).Show();
             }

        }

    }

  
}