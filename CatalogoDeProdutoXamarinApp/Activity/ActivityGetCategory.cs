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
    [Activity(Label = "ActivityGetCategory")]
    public class ActivityGetCategory : Android.App.Activity
    {
        private TextView textCategory;
        private ListView listCategory;
        private Button btnAddCategory;

        List<Categoria> categorySelected;
        
        private readonly HttpClient _client = new HttpClient();
      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GetCategory);

            textCategory = FindViewById<TextView>(Resource.Id.textCategory);
            listCategory = FindViewById<ListView>(Resource.Id.listCategory);
            btnAddCategory = FindViewById<Button>(Resource.Id.btnAddCategory);

            listCategory.ItemLongClick += ListCategory_ItemLongClick;
            btnAddCategory.Click += BtnAddCategory_Click;

              LoadListCategory();
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            //adicionar nova categoria
            StartActivity(typeof(ActivityAddCategory));
        }

        private void ListCategory_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
          //seleciona a categoria da lista para deletar ou editar
                Categoria _categorySelected = categorySelected[e.Position];

                //abre a caixa de mensagem com as opição de edição e eliminação
                AlertDialog.Builder BoxAddUpdatetDelete = new AlertDialog.Builder(this);

                BoxAddUpdatetDelete.SetTitle("Update/Delete");
                BoxAddUpdatetDelete.SetMessage(_categorySelected.Titulo);

                //editar
                BoxAddUpdatetDelete.SetPositiveButton("Update", delegate { UpdateCategory(_categorySelected.Id); });

                //deletar
                BoxAddUpdatetDelete.SetNegativeButton("Delete", delegate { DeleteCategory(_categorySelected.Id); });
            
        }

        private void UpdateCategory(int id)
        {
            //abrir a atividade para editar os dados da categoria
            Intent i = new Intent(this, typeof(ActivityUpdateCategory));
            i.PutExtra("Id", id.ToString());
            StartActivityForResult(i, 0);
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

        private async void LoadListCategory()
        {


            //Buscar
            var uri = $"{MainActivity.UrlApi}" + "/v1/categorias";
            var result = await _client.GetStringAsync(uri);
            var categories = JsonConvert.DeserializeObject<List<Categoria>>(result);


            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1,
                categories);

            listCategory.Adapter = adapter;

            textCategory.Text = "Categories: " + categories.Count.ToString();
        }

    }
}
