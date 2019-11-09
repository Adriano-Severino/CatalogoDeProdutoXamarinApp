using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Xamarin.Essentials;


namespace CatalogoDeProdutoXamarinApp.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static string UrlApi = "https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY";/* DeviceInfo.Platform == DevicePlatform.Android*/
        //    ? "http://10.2.2:5000"
        //    : "http://localhost:5000";
        ////  "https://localhost:5001";


        private Button AddUpdateDeleteProduct;
        private Button AddUpdateDeleteCategory;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            AddUpdateDeleteProduct = FindViewById<Button>(Resource.Id.AddEditDeleteProduct);
            AddUpdateDeleteCategory = FindViewById<Button>(Resource.Id.AddEditDeleteCategory);

            AddUpdateDeleteProduct.Click += AddUpdateDeleteProduct_Click;
            AddUpdateDeleteCategory.Click += AddUpdateDeleteCategory_Click;

        }

        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void AddUpdateDeleteProduct_Click(object sender, System.EventArgs e)
        {
            //abrir a atividade para add/editar/deletar e consular produtos
           StartActivity( typeof(ActivityAddDeleteUpdateProduct));
           

        }

        private void AddUpdateDeleteCategory_Click(object sender, System.EventArgs e)
        {
            //abrir a atividade para add/editar/deletar e consultar categorias
           StartActivity(typeof(ActivityAddDeleteUpdateCategory));
          
        }



    }
}