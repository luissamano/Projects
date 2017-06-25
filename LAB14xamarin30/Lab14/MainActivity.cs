using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace Lab14
{
    [Activity(Label = "Lab14", MainLauncher = true)]
    public class MainActivity : Activity
    {

        SALLab14.ResultInfo Result;
		Android.App.ProgressDialog progress;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			var textEmailAddress = FindViewById<EditText>(Resource.Id.editTextEmail);
			var etPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
			var btn = FindViewById<Button>(Resource.Id.buttonValidate);

            btn.Click += async (sender, e) => 
            {

				progress = new Android.App.ProgressDialog(this);
                progress.SetCancelable(false);
                progress.SetTitle("Trabajando");
                progress.SetMessage("Por favor, espera...");
                progress.SetProgressStyle(Android.App.ProgressDialogStyle.Horizontal);
                progress.Indeterminate = true;
                progress.Show();

                await Validate();

				Android.App.AlertDialog.Builder Builder =
					 new AlertDialog.Builder(this);
				AlertDialog Alert = Builder.Create();
				Alert.SetTitle("Resultado de la verificación");
				Alert.SetIcon(Resource.Drawable.dotnet);
				Alert.SetMessage(
					$"{Result.Status}\n{Result.FullName}\n{Result.Token}");
				Alert.SetButton("Ok", (s, ev) => { });
                progress.Cancel();
				Alert.Show();


            };



        }


        public async Task Validate()
        {
            
            SALLab14.ServiceClient client = new SALLab14.ServiceClient();

            string myD = 
                Android.Provider.Settings.Secure.GetString(
                    ContentResolver, Android.Provider.Settings.Secure.AndroidId);

			Result = await client.ValidateAsync(this);

        }
    }
}