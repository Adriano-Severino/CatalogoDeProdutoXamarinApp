using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using Uri = Android.Net.Uri;

namespace CatalogoDeProdutoXamarinApp.Activity
{
    [Activity(Label = "ActivityAbout")]
    public class ActivityAbout : Android.App.Activity
    {
        //private ImageView Logo;
        TextView lbl_github;
        TextView lbl_linkedin;
        TextView lbl_versao;
        TextView lbl_voltar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.About);

            lbl_github = FindViewById<TextView>(Resource.Id.lbl_github);
            lbl_linkedin = FindViewById<TextView>(Resource.Id.lbl_linkedin);
            lbl_versao = FindViewById<TextView>(Resource.Id.lbl_versao);
            lbl_voltar = FindViewById<TextView>(Resource.Id.lbl_voltar);

            //logo
            //Logo = FindViewById<ImageView>(Resource.Id.Logo);
            //Logo.SetImageResource(Resource.Drawable.logo);

            //mostra informação da versao

            lbl_versao.Text = "Version 1.0.0";

            lbl_github.Click += lbl_github_Click;
            lbl_linkedin.Click += lbl_linkedin_Click;
            lbl_voltar.Click += lbl_voltar_Click;
        }

        private void lbl_voltar_Click(object sender, EventArgs e)
        {
            //fechar esta janela sobre
            this.Finish();
        }



        private void lbl_linkedin_Click(object sender, EventArgs e)
        {
            //abre pagina do linkedin
            Uri linkedin = Uri.Parse("https://www.linkedin.com/in/adriano-severino-10024816a/?jobid=1234");
            Intent i = new Intent(Intent.ActionView, linkedin);
            StartActivity(i);
        }

        private void lbl_github_Click(object sender, EventArgs e)
        {
            //abre pagina do GitHUb
            //https://github.com/Adriano-Severino
            Uri github = Uri.Parse("https://github.com/Adriano-Severino");
            Intent i = new Intent(Intent.ActionView, github);
            StartActivity(i);
        }
    }
    
}