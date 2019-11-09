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
    [Activity(Label = "Update Category")]
    public class ActivityUpdateCategory : Android.App.Activity
    {
        int category_id = 0;
        bool edit = false;

        //=================================================================
        private Button BtnSave;
        private EditText EditTitle;
        private TextView textCategoryProduct;
        private ListView listProduct;
        //==============================================================

        private readonly HttpClient _client = new HttpClient();
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            

            RequestWindowFeature(WindowFeatures.NoTitle);

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.UpdateCategory);

            //=====================================================
            BtnSave = FindViewById<Button>(Resource.Id.BtnSave);
            EditTitle = FindViewById<EditText>(Resource.Id.EditTitle);
            textCategoryProduct = FindViewById<TextView>(Resource.Id.textCategoryProduct);
            listProduct = FindViewById<ListView>(Resource.Id.listProduct);

            //verificar se existe variavel category_id no intent
            if (this.Intent.GetStringExtra("Id") != null)
            {
                category_id = int.Parse(this.Intent.GetStringExtra("Id"));
                LoadListCategory();
                edit = true;
            }

            //eventos
            BtnSave.Click += BtnSave_Click;

        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
           //salvar os dados
           Categoria categoria = new Categoria();

           categoria.Id = category_id;
           categoria.Titulo = EditTitle.Text;
           string url = $"{MainActivity.UrlApi}" + "/v1/categorias/" + categoria.Id.ToString();
           var uri = new Uri(url);
           var json = JsonConvert.SerializeObject(categoria);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           HttpResponseMessage response;
           var content = new StringContent(json, Encoding.UTF8, "application/json");
           response = await _client.PutAsync(uri, content);

           if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
           {
               Toast.MakeText(this, "Categoria Atualizada", ToastLength.Long).Show();
           }

           else
           {
               Toast.MakeText(this, "Categoria não foi Atualizada", ToastLength.Long).Show();
           }

           EditTitle.Text = "";

        }

        private async void LoadListCategory()
        {
            Categoria categoria = null;
            string url = $"{MainActivity.UrlApi}" + "/v1/categorias/" + category_id;
            var result = await _client.GetAsync(url);
            var json = await result.Content.ReadAsStringAsync();

            try
            {
                categoria = Newtonsoft.Json.JsonConvert.DeserializeObject<Categoria>(json);
            }
            catch (Exception e)
            {

            }

            if (categoria == null)
            {
                Toast.MakeText(this, json, ToastLength.Short).Show();
            }
            else
            {
                EditTitle.Text = categoria.Titulo;
            }
            
            LoadListProduct();
        }

        private async void LoadListProduct()
        {

            using (var client = new HttpClient())
            {
                //Get products category
                var uri = $"{MainActivity.UrlApi}" + "/v1/categorias/produtos";
                var result = await _client.GetStringAsync(uri);
                var products = JsonConvert.DeserializeObject<List<Produto>>(result);
                
                ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1,
                    products);

                //preenxe a lista com os produtos da categorias
                listProduct.Adapter = adapter;

                textCategoryProduct.Text = "textCategoryProduct: " + products.Count.ToString();
            }

        }


    }
}