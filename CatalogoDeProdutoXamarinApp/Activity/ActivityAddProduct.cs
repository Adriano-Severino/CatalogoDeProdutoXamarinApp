using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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
    [Activity(Label = "ActivityAddProduct")]
    public class ActivityAddProduct : Android.App.Activity
    {
        private Button btnAddProduct;
        private EditText editTextAddProduct;

        readonly HttpClient _client = new HttpClient();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AddProduct);

            //===============================================================================
            editTextAddProduct = FindViewById<EditText>(Resource.Id.editTextAddProduct);
            btnAddProduct = FindViewById<Button>(Resource.Id.btnAddProduct);

            //evento
            btnAddProduct.Click += BtnAddProduct_Click;
        }

        private async void BtnAddProduct_Click(object sender, EventArgs e)
        {
            Produto produtos = new Produto();
            produtos.Titulo = editTextAddProduct.Text;
            var url = $"{MainActivity.UrlApi}" + "/v1/produtos/";
            var uri = new Uri(url);
            var json = JsonConvert.SerializeObject(produtos);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await _client.PostAsync(uri, content);

            editTextAddProduct.Text = "";

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Toast.MakeText(this, "Produto adicionado", ToastLength.Long).Show();
            }

            else
            {
                Toast.MakeText(this, "produto não foi adicionado", ToastLength.Long).Show();
            }

        }
    }
}