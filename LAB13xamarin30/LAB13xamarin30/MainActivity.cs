using Android.App;
using Android.Widget;
using Android.OS;

namespace LAB13xamarin30
{
    [Activity(Label = "LAB13xamarin30", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

		Android.App.ProgressDialog progress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += async delegate 
            {
				var Client = new SALLab13.ServiceClient();
				string EMail = "martindjavb@gmail.com"; /* Aquí pon tu correo */
				string Password = "fsoe250asot600"; /* Aquí pon tu contraseña */

				progress = new Android.App.ProgressDialog(this);
				progress.Indeterminate = true;
				progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
				progress.SetMessage("Por favor espera...");
				progress.SetCancelable(false);
				progress.Show();

				var Result = await Client.ValidateAsync(this, EMail, Password);
				Android.App.AlertDialog.Builder Builder =
					new AlertDialog.Builder(this);
                progress.Cancel();

				AlertDialog Alert = Builder.Create();
				Alert.SetTitle("Resultado de la verificación");
				Alert.SetIcon(Resource.Drawable.dotnet);
				Alert.SetMessage(
					$"{Result.Status}\n{Result.FullName}\n{Result.Token}");
				Alert.SetButton("Ok", (s, ev) => { });
				Alert.Show();
            
            };
        }
    }
}

