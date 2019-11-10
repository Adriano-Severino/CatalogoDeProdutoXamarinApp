using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using CatalogoDeProdutoXamarinApp.Models;
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
    [Activity(Label = "GetProduct")]
    public class ActivityGetProduct : Android.App.Activity
    {
        private TextView textProduct;
        private ListView listProduct;
        private Button btnAddProduct;

        List<Produto> productSelected;

        private const string Url = "http://localhost:3000/v1/";
        private readonly HttpClient _client = new HttpClient();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GetProduct);

            textProduct = FindViewById<TextView>(Resource.Id.textProduct);
            listProduct = FindViewById<ListView>(Resource.Id.listProduct);
            btnAddProduct = FindViewById<Button>(Resource.Id.btnAddProduct);

            listProduct.ItemLongClick += ListProduct_ItemLongClick;
            btnAddProduct.Click += BtnAddProduct_Click1;

            LoadListProduct();
        }

        private void BtnAddProduct_Click1(object sender, EventArgs e)
        {
            //adicionar novo produto
            StartActivity(typeof(ActivityAddProduct));
        }
        
        private void ListProduct_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            //seleciona a categoria da lista para deletar ou editar
            Produto _productSelected = productSelected[e.Position];

            //abre a caixa de mensagem com as opição de edição e eliminação
            AlertDialog.Builder BoxAddUpdatetDelete = new AlertDialog.Builder(this);

            BoxAddUpdatetDelete.SetTitle("Update/Delete");
            BoxAddUpdatetDelete.SetMessage(_productSelected.Titulo);

            //editar
            BoxAddUpdatetDelete.SetPositiveButton("Update", delegate { UpdateProduct(_productSelected.Id); });

            //deletar
            BoxAddUpdatetDelete.SetNegativeButton("Delete", delegate { DeleteProduct(_productSelected.Id); });

        }

        private void UpdateProduct(int id)
        {
            //abrir a atividade para editar os dados do producto
            Intent i = new Intent(this, typeof(ActivityUpdateProduct));
            i.PutExtra("Id", id.ToString());
            StartActivityForResult(i, 0);
        }

        private async void DeleteProduct(int id)
        {
            //elimina um produto

            Produto produtos = new Produto();

            string url = $"{MainActivity.UrlApi}" + "/v1/produtos/" + id.ToString();
            var uri = new Uri(url);
            var json = JsonConvert.SerializeObject(produtos);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await _client.DeleteAsync(uri);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Toast.MakeText(this, "Produto deletada", ToastLength.Long).Show();
            }

            else
            {
                Toast.MakeText(this, "Produto não foi deletada", ToastLength.Long).Show();
            }



            LoadListProduct();
        }

        private async void LoadListProduct()
        {


            //Buscar produtos
            var uri = $"{MainActivity.UrlApi}" + "/v1/Produtos";
            var result = await _client.GetStringAsync(uri);
            var products = JsonConvert.DeserializeObject<List<Produto>>(result);


            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1,
                products);

            listProduct.Adapter = adapter;

            textProduct.Text = "textCategoryProduct: " + products.Count.ToString();
        }
    }
}