using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo_Xamarin
{
    [Activity(Label = "Activity1")]
    public class ThirdActivity : Activity
    {
        Button bckButton1, strtRecordButton, stpRecordButton;
        TextView countText;
        int count;
        string pathSave = " ";
        MediaRecorder mediaRecorder;
        MediaPlayer mediaPlayer;
        private const int REQUEST_PERMISSION_CODE = 1000;
        private bool isGrantedPermission = false;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case REQUEST_PERMISSION_CODE:
                    {
                        if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                        {
                            Toast.MakeText(this, "Granted", ToastLength.Short).Show();
                            isGrantedPermission = true;
                        }
                        else
                        {
                            Toast.MakeText(this, "Not Granted", ToastLength.Short).Show();
                            isGrantedPermission = false; 
                        }
                    }
                    break;
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_third);
            // Create your application here

            bckButton1 = FindViewById<Button>(Resource.Id.bckButton1);
            strtRecordButton = FindViewById<Button>(Resource.Id.strtRecordButton);
            stpRecordButton = FindViewById<Button>(Resource.Id.stpRecordButton);

            // Run-time Permission
            if(CheckSelfPermission(Manifest.Permission.WriteExternalStorage) != Android.Content.PM.Permission.Granted 
                && CheckSelfPermission(Manifest.Permission.RecordAudio) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[]
                {
                    Manifest.Permission.WriteExternalStorage,
                    Manifest.Permission.RecordAudio

                }, REQUEST_PERMISSION_CODE);
                    
            }
            else
            {
                isGrantedPermission = false;
            }


            strtRecordButton.Enabled = true;
            stpRecordButton.Enabled = false;

            // Event 
            strtRecordButton.Click += delegate
            {
                RecordAudio();
            };

            stpRecordButton.Click += delegate
            {
                StopRecord();
            };

            void RecordAudio()
            {
                if (isGrantedPermission)
                {
                    pathSave = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath.ToString() + "/" + new
                    Guid().ToString() + "_audio.3gp";

                    SetupMediaRecorder();

                    try
                    {
                        mediaRecorder.Prepare();
                        mediaRecorder.Start();

                        strtRecordButton.Enabled = false;
                        stpRecordButton.Enabled = true;
                        Toast.MakeText(this, "Recording...", ToastLength.Short).Show();
                    }
                    catch(Exception Ex)
                    {
                        Log.Debug("DEBUG", Ex.Message);
                    }
                }

                stpRecordButton.Enabled = true;
            }

            void StopRecord()
            {
                mediaRecorder.Stop();
                strtRecordButton.Enabled = true;
                stpRecordButton.Enabled = false;
                Toast.MakeText(this, "Stop Recording...", ToastLength.Short).Show();
            }

            void SetupMediaRecorder()
            {
                mediaRecorder = new MediaRecorder();
                mediaRecorder.SetAudioSource(AudioSource.Mic);
                mediaRecorder.SetOutputFormat(OutputFormat.ThreeGpp);
                mediaRecorder.SetAudioEncoder(AudioEncoder.AmrNb);
                mediaRecorder.SetOutputFile(pathSave);
            }
            //countButton = FindViewById<Button>(Resource.Id.countButton);
            //countText = FindViewById<TextView>(Resource.Id.countText);

            //countButton.Click += (sender, e) => { countText.Text = (++count).ToString(); };

            bckButton1.Click += (sender, e) =>
            {
                Intent nextActivivty = new Intent(this, typeof(SecondActivity));
                this.StartActivity(nextActivivty);
                this.Finish();
            };

        }
    }
}