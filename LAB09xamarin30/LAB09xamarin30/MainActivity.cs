using Android.App;
using Android.Widget;
using Android.OS;

namespace LAB09xamarin30
{
    [Activity(Label = "LAB09xamarin30", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private SALLab09.ResultInfo Result;
        private string StudentEmail = "martindjavb@gmail.com", Password = "fsoe250asot600";

        protected async override void OnCreate(Bundle savedInstanceState)
        {



			base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			await Validate();



        }

			

		public async System.Threading.Tasks.Task Validate()
		{


			var txt1 = FindViewById<TextView>(Resource.Id.txt1);
			var txt2 = FindViewById<TextView>(Resource.Id.txt2);
			var txt3 = FindViewById<TextView>(Resource.Id.txt3);

			SALLab09.ServiceClient client = new SALLab09.ServiceClient();
			string myD = Android.Provider.Settings.Secure.GetString(
				ContentResolver, Android.Provider.Settings.Secure.AndroidId);


			Result =
				await client.ValidateAsync(
					StudentEmail, Password, myD);

			txt1.Text = $"{Result.Fullname}";
			txt2.Text = $"{Result.Status}";
			txt3.Text = $"{Result.Token}";
			
		}

	}
}
