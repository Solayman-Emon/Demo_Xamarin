using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo_Xamarin
{
    class User
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public String Prkinsn { get; set; }


        public User(String name, String email, String prkinsn)
        {
            Name = name;
            Email = email;
            Prkinsn = prkinsn;
        }

        public User()
        {

        }

        public override string ToString()
        {
            return Name + " " + Email + " " + Prkinsn; 
        }
    }
}