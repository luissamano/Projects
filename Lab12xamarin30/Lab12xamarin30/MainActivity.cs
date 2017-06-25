using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab12xamarin30
{
    [Activity(Label = "Lab12xamarin30", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {


        string res;
		Complex Data;
		string Email = "martindjavb@gmail.com";
		string Password = "fsoe250asot600";
		Android.App.ProgressDialog progress;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var ListItemColor = FindViewById<ListView>(Resource.Id.ListItemColor);
            var txview = FindViewById<TextView>(Resource.Id.txview);


            ListItemColor.Adapter = new CustomAdapters.ColorAdapter( this,

                Resource.Layout.ListItems,
                Resource.Id.textView2,
                Resource.Id.textView3,
                Resource.Id.imageView1
            );


            Data = new Complex();
            if (Data.res == null)
            {
				progress = new Android.App.ProgressDialog(this);
				progress.Indeterminate = true;
				progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
				progress.SetMessage("Loading is Progress...");
				progress.SetCancelable(false);
				progress.Show();

				res = await Validate();
                Data.res = res;
                progress.Cancel();
            } 
            else
            {
                res = Data.GetRes();
            }

            txview.Text = res;

        }


		public async System.Threading.Tasks.Task<string> Validate()
		{

			SALLab12.ServiceClient client = new SALLab12.ServiceClient();
			
            string myD = Android.Provider.Settings.Secure.GetString(
				ContentResolver, Android.Provider.Settings.Secure.AndroidId);


			SALLab12.ResultInfo Result =
				await client.ValidateAsync(
					Email, Password, myD);


			return ($"{Result.Status}\n{Result.FullName}\n{Result.Token}");

		}

    }
}

