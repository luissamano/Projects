using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab11
{
    [Activity(Label = "ApplicationName", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        string text;
        Complex Data;
        int Counter = 0;
		string Email = "martindjavb@gmail.com";
		string Password = "fsoe250asot600";
        SALLab11.ResultInfo Result;
		Android.App.ProgressDialog progress;

        protected override async void OnCreate(Bundle savedInstanceState)
        {

			Android.Util.Log.Debug("Lab11Log", "Activity - On Create");

			base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var txview = FindViewById<TextView>(Resource.Id.txview);

            Data = (Complex)this.FragmentManager.FindFragmentByTag("Data");

            if(Data == null)
            {
                Data = new Complex();
                var FragmentTrans = this.FragmentManager.BeginTransaction();
                FragmentTrans.Add(Data, "Data");
                FragmentTrans.Commit();
            }

            if(Data.res == null)
            {
				progress = new Android.App.ProgressDialog(this);
				progress.Indeterminate = true;
				progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
				progress.SetMessage("Loading is Progress...");
				progress.SetCancelable(false);
				progress.Show();

                text = await Validate();
				progress.Cancel();
                Data.res = text;
            } else {
                text = Data.ToRes();
            }

            txview.Text = text;

            if(savedInstanceState != null)
            {
                Counter = savedInstanceState.GetInt("CounterValue", 0);
                Android.Util.Log.Debug("Lab11Log", "Activity A - Recovery Instance State");
            }


			FindViewById<Button>(Resource.Id.StartActivity).Click += (sender, e) => 
            {
                var ActivityIntent =
                    new Android.Content.Intent(this, typeof(SecondActivity));
                StartActivity(ActivityIntent);
            };

            var ClickCounter =
                FindViewById<Button>(Resource.Id.ClicksCounter);
            ClickCounter.Text =
                Resources.GetString((Resource.Id.ClicksCounter));

            ClickCounter.Text += $"\n{Data.ToString()}";

            ClickCounter.Click += (sender, e) => 
            {
                Counter++;
                ClickCounter.Text =
                   Resources.GetString(Resource.String.ClicksCounter_Text);

                Data.Real++;
                Data.Ima++;


                ClickCounter.Text += $"\n{Data.ToString()}";
            };

	    }


		public async System.Threading.Tasks.Task<string> Validate()
		{

			SALLab11.ServiceClient client = new SALLab11.ServiceClient();
			string myD = Android.Provider.Settings.Secure.GetString(
				ContentResolver, Android.Provider.Settings.Secure.AndroidId);


			Result =
				await client.ValidateAsync(
					Email, Password, myD);


			return ($"{Result.Status}\n{Result.Fullname}\n{Result.Token}");

		}

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("CounterValue", Counter);
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnSave");

            base.OnSaveInstanceState(outState);
        }

        protected override void OnStart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activicty A - OnStart");
            base.OnStart();
        }

        protected override void OnResume()
        {
			Android.Util.Log.Debug("Lab11Log", "Activicty A - OnResume");
			base.OnResume();
        }

        protected override void OnPause()
        {

			Android.Util.Log.Debug("Lab11Log", "Activicty A - OnPause");
			base.OnPause();
        }

        protected override void OnStop()
        {
			Android.Util.Log.Debug("Lab11Log", "Activicty A - OnStop");
            base.OnStop();
		}

        protected override void OnDestroy()
        {
			Android.Util.Log.Debug("Lab11Log", "Activicty A - OnDestroy");
            base.OnDestroy();
		}

		protected override void OnRestart()
		{
			Android.Util.Log.Debug("Lab11Log", "Activicty A - OnRestart");
			base.OnRestart();
		}
    }
}

