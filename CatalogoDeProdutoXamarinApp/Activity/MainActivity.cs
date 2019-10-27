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
        public static string UrlApi = DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.2.2:5000"
            : "http://localhost:5000";
        //  "https://localhost:5001";


        private Button AddEditDeleteProduct;
        private Button AddEditDeleteCategory;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            AddEditDeleteProduct = FindViewById<Button>(Resource.Id.AddEditDeleteProduct);
            AddEditDeleteCategory = FindViewById<Button>(Resource.Id.AddEditDeleteCategory);

            AddEditDeleteProduct.Click += AddEditDeleteProduct_Click;
            AddEditDeleteCategory.Click += AddEditDeleteCategory_Click;

        }

        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void AddEditDeleteProduct_Click(object sender, System.EventArgs e)
        {
            //abrir a atividade para add/editar/deletar e consular produtos
            var intent = new Intent(this, typeof(ActivityAddDeleteEditProduct));
            StartActivity(intent);

        }

        private void AddEditDeleteCategory_Click(object sender, System.EventArgs e)
        {
            //abrir a atividade para add/editar/deletar e consultar categorias
            var intent = new Intent(this,typeof(ActivityAddDeleteEditCategory));
            StartActivity(intent);
        }



    }
}