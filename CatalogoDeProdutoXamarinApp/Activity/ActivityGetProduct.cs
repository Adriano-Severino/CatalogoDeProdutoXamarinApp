using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CatalogoDeProdutoXamarinApp.Models;


namespace CatalogoDeProdutoXamarinApp.Activity
{
    [Activity(Label = "GetProduct")]
    public class ActivityGetProduct : Android.App.Activity
    {
        

        private Button Get_Product;
        private TextView textproduct;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GetProduct);

            Get_Product = FindViewById<Button>(Resource.Id.Get_Product);
            textproduct = FindViewById<TextView>(Resource.Id.textProduct);

          
        }

        private async void Get_Product_Click(object sender, EventArgs e)
        {
          
        }
    }
}