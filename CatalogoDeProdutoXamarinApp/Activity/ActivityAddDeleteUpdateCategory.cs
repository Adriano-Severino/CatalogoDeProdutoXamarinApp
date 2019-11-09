using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    [Activity(Label = "ActivityAddDeleteUpdateCategory")]
    public class ActivityAddDeleteUpdateCategory : Android.App.Activity
    {
        private ListView listCategory;
        private Button btnAddCategory;

        List<Categoria> categorySelected;

        //private const string Url = "http://192.168.0.101:3000/v1/";
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Categoria> _categories;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AddUpdateDeleteCategory);

            listCategory = FindViewById<ListView>(Resource.Id.listCategory);
            btnAddCategory = FindViewById<Button>(Resource.Id.btnAddCategory);

            listCategory.ItemLongClick+= listCategory_ItemLongClick;
            btnAddCategory.Click += btnAddCategory_Click;

            LoadListCategory();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            //adicionar nova categoria
            StartActivity(typeof(ActivityAddCategory));
        }

        private void listCategory_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            //seleciona a categoria da lista para deletar ou editar
            Categoria _categorySelected = categorySelected[e.Position];

            //abre a caixa de mensagem com as opição de edição e eliminação
            AlertDialog.Builder BoxAddUpdatetDelete = new AlertDialog.Builder(this);

            BoxAddUpdatetDelete.SetTitle("Add/Update/Delete");
            BoxAddUpdatetDelete.SetMessage(_categorySelected.Titulo);

            //editar
            BoxAddUpdatetDelete.SetPositiveButton("Update", delegate { UpdateCategory(_categorySelected.Id); });

            //deletar
            BoxAddUpdatetDelete.SetNegativeButton("Delete", delegate { DeleteCategory(_categorySelected.Id); });

            //Add
            BoxAddUpdatetDelete.SetNegativeButton("Add", delegate { AddCategory(); });
        }

       

        private void UpdateCategory(int id)
        {
            //abrir a atividade para editar os dados da categoria
            Intent i = new  Intent(this,typeof(ActivityUpdateCategory));
            i.PutExtra("Id", id.ToString());
            StartActivityForResult(i,0);
        }

        private async void DeleteCategory(int id)
        {
            //elimina a categoria

           Categoria categoria = new Categoria();

           string url = $"{MainActivity.UrlApi}" + "/v1/categorias/" + id.ToString();
           var uri = new Uri(url);
           var json = JsonConvert.SerializeObject(categoria);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await _client.DeleteAsync(uri);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Toast.MakeText(this, "Categoria deletada", ToastLength.Long).Show();
            }

            else
            {
                Toast.MakeText(this, "Categoria não foi deletada", ToastLength.Long).Show();
            }

           

            LoadListCategory();
        }

        private void AddCategory()
        {
            //adicionar nova categoria
           StartActivity(typeof(ActivityAddCategory));
          
        }


        private async void LoadListCategory()
        {
           
            
                // envia a requisição GET
                var uri = $"{MainActivity.UrlApi}" + "/v1/categorias";
                var result = await _client.GetStringAsync(uri);
                var categories = JsonConvert.DeserializeObject<List<Categoria>>(result);
              

                ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1,
                    categories);

                listCategory.Adapter = adapter;
            
        }
    }
}