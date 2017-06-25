using Android.App;
using Android.Widget;
using Android.OS;

namespace LAB10xamarin30
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int counter = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var ContentHeader = FindViewById<TextView>(Resource.Id.ContentHeader);
            ContentHeader.Text = GetText(Resource.String.ContentHeader);

            var ClickMe = FindViewById<Button>(Resource.Id.ClickMe);
            var btnvalidar = FindViewById<Button>(Resource.Id.Validar);
            var ClickCounter = FindViewById<TextView>(Resource.Id.ClickCounter);

            ClickMe.Click += (sender, e) => 
            {
                counter++;
                ClickCounter.Text = Resources.GetQuantityString(
                    Resource.Plurals.numberOfClicks, counter, counter);

                var player = Android.Media.MediaPlayer.Create(
                    this, Resource.Raw.sound);

                player.Start();

            };

            btnvalidar.Click += (sender, e) => 
            {
                StartActivity(typeof(Validar));
            };


			Android.Content.Res.AssetManager man = this.Assets;

			using (var Reader =
				  new System.IO.StreamReader(man.Open("Contenido.txt")))
			{
				ContentHeader.Text += $"\n\n{Reader.ReadToEnd()}";
			}
        }
    }
}