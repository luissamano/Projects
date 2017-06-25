using System;
using Android.App;
using System.Runtime.Remoting.Proxies;
using Android.OS;
namespace Lab11
{
    public class Complex : Fragment
    {
        public int Real { get; set;}
        public int Ima { get; set; }
        public string res { get; set; }

        public string ToRes()
        {
            return $"{res}";
        }


        public override string ToString()
        {
            return $"{Real} + {Ima}i";
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }
    }
}