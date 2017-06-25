using System;
using Android.App;
namespace Lab12xamarin30
{
    public class Complex : Fragment
    {

        public string res { get; set; }


        public string GetRes ()
        {
            return $"{res}";
        }
        
    }
}
