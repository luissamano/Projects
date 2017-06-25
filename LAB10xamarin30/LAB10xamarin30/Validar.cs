
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

namespace LAB10xamarin30
{
    [Activity(Label = "@string/ApplicationName")]
    public class Validar : Activity
    {

        string Email = "";
        string Password = "";
        Android.App.ProgressDialog progress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Validar);

            var textEmailAddress = FindViewById<EditText>(Resource.Id.etCorreo);
            var etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            var btn = FindViewById<Button>(Resource.Id.btn);
            var txview = FindViewById<TextView>(Resource.Id.txview);

            btn.Click += async (sender, e) =>
            {

				progress = new Android.App.ProgressDialog(this);
				progress.Indeterminate = true;
				progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
				progress.SetMessage("Loading is Progress...");
				progress.SetCancelable(false);
				progress.Show();

				this.Email = textEmailAddress.Text.ToString();
                this.Password = etPassword.Text.ToString();

                string res = await Validate();
                txview.Text= res.ToString();
                progress.Cancel();
            };



        }

        public async System.Threading.Tasks.Task<string> Validate()
        {

            SALLab10.ServiceClient client = new SALLab10.ServiceClient();
            string myD = Android.Provider.Settings.Secure.GetString(
                ContentResolver, Android.Provider.Settings.Secure.AndroidId);


            SALLab10.ResultInfo Result =
                await client.ValidateAsync(
                    Email, Password, myD);


            return ($"{Result.Status}\n{Result.Fullname}\n{Result.Token}");

		}
    }
}
