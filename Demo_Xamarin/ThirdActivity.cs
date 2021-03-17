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
    public class ThirdActivity : Activity
    {
        Button bckButton1, countButton;
        TextView countText;
        int count;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_third);
            // Create your application here

            bckButton1 = FindViewById<Button>(Resource.Id.bckButton1);
            countButton = FindViewById<Button>(Resource.Id.countButton);
            countText = FindViewById<TextView>(Resource.Id.countText);

            countButton.Click += (sender, e) => { countText.Text = (++count).ToString(); };
            bckButton1.Click += (sender, e) =>
            {
                Intent nextActivivty = new Intent(this, typeof(SecondActivity));
                this.StartActivity(nextActivivty);
                this.Finish();
            };

        }
    }
}