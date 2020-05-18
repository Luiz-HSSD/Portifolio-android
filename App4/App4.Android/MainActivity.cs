
#define portifolio
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App4.Droid.Utilities;
using Org.Opencv.Core;
using Org.Opencv.Objdetect;
using Org.Opencv.Android;
using Java.IO;
using App4.Droid.ColorBlobDetection;
using Android.Util;
using Size = Org.Opencv.Core.Size;
using Org.Opencv.Imgproc;
using Java.Lang;
using Plugin.Media;
using Plugin.CurrentActivity;
using Plugin.Fingerprint;
using FFImageLoading.Forms.Platform;
using Plugin.LocalNotifications;
using Android.Content.PM;
using System.Net;
using ZXing.Mobile;
using Badge.Plugin;
using Poz1.NFCForms.Abstract;
using Poz1.NFCForms.Droid;
using Android.Nfc;
using Android.OS;
using Android.Content;
using Android.Content.PM;
using Android.Telephony;
using Plugin.NFC;

namespace App4.Droid
{
    [Activity(Label = "App4", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity 
        //,CameraBridgeViewBase.ICvCameraViewListener2
    {
        private static readonly Scalar FACE_RECT_COLOR = new Scalar(0, 255, 0, 255);
        public static readonly int JAVA_DETECTOR = 0;
        public static readonly int NATIVE_DETECTOR = 1;

        private IMenuItem mItemFace50;
        private IMenuItem mItemFace40;
        private IMenuItem mItemFace30;
        private IMenuItem mItemFace20;
        private IMenuItem mItemType;

        private Mat mRgbaT;
        private Mat mGrayT;
        public  File mCascadeFile { get; set; }
        public CascadeClassifier mJavaDetector { get; set; }
        public DetectionBasedTracker mNativeDetector { get; set; }
        public object UIApplication { get; private set; }

        private int mDetectorType = JAVA_DETECTOR;
        private string[] mDetectorName;

        private float mRelativeFaceSize = 0.2f;
        private int mAbsoluteFaceSize = 0;

        private CameraBridgeViewBase mOpenCvCameraView;

        private Callback mLoaderCallback;
        public NfcAdapter NFCdevice;
        public NfcForms x;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            MobileBarcodeScanner.Initialize(this.Application);
            SmsAndroid.dev = this;
            await CrossMedia.Current.Initialize();
            CrossFingerprint.SetCurrentActivityResolver(() => CrossCurrentActivity.Current.Activity);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.icon;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            NfcManager NfcManager = (NfcManager)Android.App.Application.Context.GetSystemService(Context.NfcService);
            NFCdevice = NfcManager.DefaultAdapter;
            CrossNFC.Init(this);
            Xamarin.Forms.DependencyService.Register<INfcForms, NfcForms>();
            x = Xamarin.Forms.DependencyService.Get<INfcForms>() as NfcForms;

#if portifolio
            CrossBadge.Current.SetBadge(9);
            LoadApplication(new App(new SoapService(),new OpenCVActivity(),new SmsAndroid()));
#else
            if (!OpenCVLoader.InitDebug())
            {
                System.Console.WriteLine("Init OpenCV failed!!");
            }
            else
            {
                System.Console.WriteLine("Init OpenCV succefuly!!");
            }
            
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            mOpenCvCameraView = FindViewById<CameraBridgeViewBase>(Resource.Id.fd_activity_surface_view);
            mOpenCvCameraView.Visibility = ViewStates.Visible;
            mOpenCvCameraView.SetCvCameraViewListener2(this);
            mLoaderCallback = new Callback(this, this, mOpenCvCameraView);

#endif
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        int count = 1;
        Mat grayM;
#if portifolio
        protected override void OnResume()
        {
            base.OnResume();
            if (NFCdevice != null)
            {
                var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
                NFCdevice.EnableForegroundDispatch
                (
                    this,
                    PendingIntent.GetActivity(this, 0, intent, 0),
                    new[] { new IntentFilter(NfcAdapter.ActionTechDiscovered) },
                    new string[][] {new string[] {
                            NFCTechs.Ndef,
                        },
                        new string[] {
                            NFCTechs.MifareClassic,
                        },
                    }
                );
            }
        }
        protected override void OnPause()
        {
            base.OnPause();
            NFCdevice.DisableForegroundDispatch(this);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            x.OnNewIntent(this, intent);
            CrossNFC.OnNewIntent(intent);
        }
#endif

#if !portifolio
        protected override void OnPause()
        {
            base.OnPause();
            if (mOpenCvCameraView != null)
                mOpenCvCameraView.DisableView();
        }

        protected override void OnResume()
        {

            base.OnResume();
            if (!OpenCVLoader.InitDebug())
            {
                Log.Debug(ActivityTags.FaceDetect, "Internal OpenCV library not found. Using OpenCV Manager for initialization");
                OpenCVLoader.InitAsync(OpenCVLoader.OpencvVersion300, this, mLoaderCallback);
            }
            else
            {
                Log.Debug(ActivityTags.FaceDetect, "OpenCV library found inside package. Using it!");
                mLoaderCallback.OnManagerConnected(LoaderCallbackInterface.Success);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mOpenCvCameraView.DisableView();
        }

        public void OnCameraViewStarted(int width, int height)
        {
            mGrayT = new Mat();
            mRgbaT = new Mat();
        }

        public void OnCameraViewStopped()
        {
            mGrayT.Release();
            mRgbaT.Release();
        }

        public Mat OnCameraFrame(CameraBridgeViewBase.ICvCameraViewFrame inputFrame)
        {
             mRgbaT = inputFrame.Rgba();
             //mRgbaT = mRgba.T();
            //Core.Flip(mRgba.T(), mRgbaT, 1);
            //Imgproc.Resize(mRgbaT, mRgbaT, mRgba.Size());

             mGrayT = inputFrame.Gray();
             //mGrayT = mGray.T();
            //Core.Flip(mGray.T(), mGrayT, 1);
            //Imgproc.Resize(mGrayT, mGrayT, mGray.Size());
            //mRgba = mRgbaT;
            //mGray = mGrayT;

            if (mAbsoluteFaceSize == 0)
            {
                int height = mGrayT.Rows();
                if (Java.Lang.Math.Round(height * mRelativeFaceSize) > 0)
                {
                    mAbsoluteFaceSize = Java.Lang.Math.Round(height * mRelativeFaceSize);
                }
                mNativeDetector.setMinFaceSize(mAbsoluteFaceSize);
            }

            MatOfRect faces = new MatOfRect();

            if (mDetectorType == JAVA_DETECTOR)
            {
                if (mJavaDetector != null)
                    mJavaDetector.DetectMultiScale(mGrayT, faces, 1.1, 2, 2, // TODO: objdetect.CV_HAAR_SCALE_IMAGE
                            new Size(mAbsoluteFaceSize, mAbsoluteFaceSize), new Size());
            }
            else if (mDetectorType == NATIVE_DETECTOR)
            {
                if (mNativeDetector != null)
                    mNativeDetector.detect(mGrayT, faces);
            }
            else
            {
                Log.Error(ActivityTags.FaceDetect, "Detection method is not selected!");
            }

            Rect[] facesArray = faces.ToArray();
            for (int i = 0; i < facesArray.Length; i++)
                Imgproc.Rectangle(mRgbaT, facesArray[i].Tl(), facesArray[i].Br(), FACE_RECT_COLOR, 3);

            return mRgbaT;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            Log.Info(ActivityTags.FaceDetect, "called onCreateOptionsMenu");
            mItemFace50 = menu.Add("Face size 50%");
            mItemFace40 = menu.Add("Face size 40%");
            mItemFace30 = menu.Add("Face size 30%");
            mItemFace20 = menu.Add("Face size 20%");
            mItemType = menu.Add(mDetectorName[mDetectorType]);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Log.Info(ActivityTags.FaceDetect, "called onOptionsItemSelected; selected item: " + item);
            if (item == mItemFace50)
                setMinFaceSize(0.5f);
            else if (item == mItemFace40)
                setMinFaceSize(0.4f);
            else if (item == mItemFace30)
                setMinFaceSize(0.3f);
            else if (item == mItemFace20)
                setMinFaceSize(0.2f);
            else if (item == mItemType)
            {
                int tmpDetectorType = (mDetectorType + 1) % mDetectorName.Length;
                item.SetTitle(mDetectorName[tmpDetectorType]);
                setDetectorType(tmpDetectorType);
            }
            return true;
        }

        private void setMinFaceSize(float faceSize)
        {
            mRelativeFaceSize = faceSize;
            mAbsoluteFaceSize = 0;
        }

        private void setDetectorType(int type)
        {
            if (mDetectorType != type)
            {
                mDetectorType = type;

                if (type == NATIVE_DETECTOR)
                {
                    Log.Info(ActivityTags.FaceDetect, "Detection Based Tracker enabled");
                    mNativeDetector.start();
                }
                else
                {
                    Log.Info(ActivityTags.FaceDetect, "Cascade detector enabled");
                    mNativeDetector.stop();
                }
            }
        }
#endif
    }

}