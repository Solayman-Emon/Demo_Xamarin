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
    [Activity(Label = "Activity1")]
    public class SecondActivity : Activity
    {
        Button bckButton, nxtButton1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_second);

            // Create your application here
            bckButton = FindViewById<Button>(Resource.Id.bckButton);
            nxtButton1 = FindViewById<Button>(Resource.Id.nxtButton1);

            nxtButton1.Click += (sender, e) =>
            {
                Intent nextActivivty = new Intent(this, typeof(ThirdActivity));
                this.StartActivity(nextActivivty);
                this.Finish();
            };

            bckButton.Click += delegate
            {
                this.Finish();
            };

        }
    }
}