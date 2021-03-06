using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Java.IO;
using Java.Nio;
using Java.Nio.Channels;
using System;
using SQLite;
using System.IO;

namespace Demo_Xamarin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText user_name, email, prkinsnSyn;
        Button nextButton, submitButton;
        TextView projectName;
        int count;
        // string that holds the path information about database
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbUserInfo.db3");
     
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            user_name = FindViewById<EditText>(Resource.Id.userName);
            email = FindViewById<EditText>(Resource.Id.email);
            prkinsnSyn = FindViewById<EditText>(Resource.Id.prkinsnSyn);

            nextButton = FindViewById<Button>(Resource.Id.nextButton);
            submitButton = FindViewById<Button>(Resource.Id.submitButton);
            projectName = FindViewById<TextView>(Resource.Id.ProjectName);

            // Set up a db connection
            var db = new SQLiteConnection(dbpath);

            // Set up a table
            db.CreateTable<User>();

            // Create Some User Object
            User user = new User(user_name.ToString(), email.ToString(), prkinsnSyn.ToString());


            submitButton.Click += (sender, e) =>
            {
                Context context = Application.Context;
                string text = "Data Inserted on Dataset";
                ToastLength duration = ToastLength.Short;

                var toast = Toast.MakeText(context, text, duration);
                toast.Show();

                // Inserting the Created Object
                db.Insert(user);
            };

            nextButton.Click += (sender, e) => { 
                Intent nextActivivty = new Intent(this, typeof(SecondActivity));
                StartActivity(nextActivivty);
            };

            MappedByteBuffer GetModelAsMappedByteBuffer()
            {
                var assetDescriptor = Application.Context.Assets.OpenFd("model.tflite");
                var inputStream = new FileInputStream(assetDescriptor.FileDescriptor);

                var mappedByteBuffer = inputStream.Channel.Map(FileChannel.MapMode.ReadOnly, assetDescriptor.StartOffset, assetDescriptor.DeclaredLength);

                return mappedByteBuffer;
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}