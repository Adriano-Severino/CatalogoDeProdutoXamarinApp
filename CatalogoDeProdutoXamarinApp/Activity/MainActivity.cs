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
        public static string UrlApi = "https://localhost:5001";/* DeviceInfo.Platform == DevicePlatform.Android*/
        //    ? "http://10.2.2:5000"
        //    : "http://localhost:5000";
        ////  "https://localhost:5001";


        private Button GetProduct;
        private Button GetCategory;
        private Button btnAbout;
        //private ImageView Logo;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            GetProduct = FindViewById<Button>(Resource.Id.GetProduct);
            GetCategory = FindViewById<Button>(Resource.Id.GetCategory);
            btnAbout = FindViewById<Button>(Resource.Id.btnAbout);

            //logo
            //Logo = FindViewById<ImageView>(Resource.Id.Logo);
           // Logo.SetImageResource(Resource.Drawable.logo);

            //==========================================================================================
            GetProduct.Click += GetProduct_Click;
            GetCategory.Click += GetCategory_Click;
            btnAbout.Click += BtnAbout_Click;


        }

        private void BtnAbout_Click(object sender, System.EventArgs e)
        {
            //sobre
            StartActivity(typeof(ActivityAbout));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void GetProduct_Click(object sender, System.EventArgs e)
        {
            //abrir a atividade para add/editar/deletar e consular produtos
           StartActivity( typeof(ActivityGetProduct));
           

        }

        private void GetCategory_Click(object sender, System.EventArgs e)
        {
            //abrir a atividade para add/editar/deletar e consultar categorias
           StartActivity(typeof(ActivityGetProduct));
          
        }



    }
}