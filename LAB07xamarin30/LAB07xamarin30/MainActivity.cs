using Android.App;
using Android.Widget;
using Android.OS;

namespace LAB07xamarin30
{
    [Activity(Label = "LAB07xamarin30", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private string StudentEmail = "martindjavb@gmail.com";
        private string Password = "fsoe250asot600";
        string not;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btn);
            TextView txview = FindViewById<TextView>(Resource.Id.txview);

            button.Click += async (sender, e) =>
            {
                
                not = await Validate();


                if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {

                    var Builder = new Notification.Builder(this)
                              .SetContentTitle("Validacion de actividad")
                              .SetSmallIcon(Resource.Drawable.bugermenu)
                              .SetContentText("" + not);

                    Builder.SetCategory(Notification.CategoryMessage);
                    var ObjectNotification = Builder.Build();
                    var Manager = GetSystemService(
                        Android.Content.Context.NotificationService) as NotificationManager;

                    Manager.Notify(0, ObjectNotification);
                }
                else
                {
                    txview.Text = not;
                }

			};

        }


        public async System.Threading.Tasks.Task<string> Validate()
        {

			SALLab07.ServiceClient client = new SALLab07.ServiceClient();
			string myD = Android.Provider.Settings.Secure.GetString(
				ContentResolver, Android.Provider.Settings.Secure.AndroidId);


			SALLab07.ResultInfo Result =
				await client.ValidateAsync(
					StudentEmail, Password, myD);


			return ($"{Result.Status}\n{Result.Fullname}\n{Result.Token}");
        }



    }
}

