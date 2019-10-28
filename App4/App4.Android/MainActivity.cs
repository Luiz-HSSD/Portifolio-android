using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Media;
using Plugin.CurrentActivity;
using Plugin.Fingerprint;
using FFImageLoading.Forms.Platform;
using Org.Opencv.Imgproc;
using Org.Opencv.Android;
using Org.Opencv.Core;
using Android.Graphics;
using System.IO;
using Org.Opencv.Objdetect;
using Plugin.LocalNotifications;

namespace App4.Droid
{
    [Activity(Label = "App4", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static readonly Scalar FACE_RECT_COLOR = new Scalar(0, 255, 0, 255);
        public static readonly int JAVA_DETECTOR = 0;
        public static readonly int NATIVE_DETECTOR = 1;

        private IMenuItem mItemFace50;
        private IMenuItem mItemFace40;
        private IMenuItem mItemFace30;
        private IMenuItem mItemFace20;
        private IMenuItem mItemType;

        private Mat mRgba;
        private Mat mGray;
        //public File mCascadeFile { get; set; }
        public CascadeClassifier mJavaDetector { get; set; }
        public DetectionBasedTracker mNativeDetector { get; set; }

        private int mDetectorType = JAVA_DETECTOR;
        private string[] mDetectorName;

        private float mRelativeFaceSize = 0.2f;
        private int mAbsoluteFaceSize = 0;

        private CameraBridgeViewBase mOpenCvCameraView;

        //private Callback mLoaderCallback;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            await CrossMedia.Current.Initialize();
            CrossFingerprint.SetCurrentActivityResolver(() => CrossCurrentActivity.Current.Activity);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.icon;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            LoadApplication(new App(new SoapService()));
            /*
            if (!OpenCVLoader.InitDebug())
            {
                Console.WriteLine("Init OpenCV failed!!");
            }
            else
            {
                Console.WriteLine("Init OpenCV succefuly!!");
            }
            
            //mNativeDetector = new DetectionBasedTracker(_activity.mCascadeFile.AbsolutePath, 0);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                //button.Text = string.Format ("{0} clicks!", count++);

                SetImage();
            };
            */

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        int count = 1;
        Mat grayM;

/*
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (!OpenCVLoader.InitDebug())
            {
                Console.WriteLine("Init OpenCV failed!!");
            }
            else
            {
                Console.WriteLine("Init OpenCV succefuly!!");
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                //button.Text = string.Format ("{0} clicks!", count++);

                SetImage();
            };
        }
        */
        void SetImage()
        {
            ImageView iView = FindViewById<ImageView>(Resource.Id.imageView1);

            using (Bitmap img = BitmapFactory.DecodeResource(Resources, Resource.Drawable.lena))
            {
                if (img != null)
                {

                    Mat mm = new Mat();
                    mRgba = new Mat();
                    Utils.BitmapToMat(img, mRgba);

                    //Imgproc.CvtColor(mm, mRgba, Imgproc.ColorGray2rgba);

                    MatOfRect faces = new  MatOfRect();
                    
                    //else if (mDetectorType == NATIVE_DETECTOR)
                    {
                        if (mNativeDetector != null)
                            mNativeDetector.detect(mGray, faces);
                    }
                    Mat m = new Mat();
                    grayM = new Mat();

                    Utils.BitmapToMat(img, m);

                    Imgproc.CvtColor(m, grayM, Imgproc.ColorBgr2gray);

                    Imgproc.CvtColor(grayM, m, Imgproc.ColorGray2rgba);
                    //if (mDetectorType == JAVA_DETECTOR)
                    {
                        if (mJavaDetector != null)
                            mJavaDetector.DetectMultiScale(grayM, faces, 1.1, 2, 2, // TODO: objdetect.CV_HAAR_SCALE_IMAGE
                                    new Size(mAbsoluteFaceSize, mAbsoluteFaceSize), new Size());
                    }
                    Org.Opencv.Core.Rect[] facesArray = faces.ToArray();
                    for (int i = 0; i < facesArray.Length; i++)
                        Imgproc.Rectangle(mRgba, facesArray[i].Tl(), facesArray[i].Br(), FACE_RECT_COLOR, 3);
                    using (Bitmap bm = Bitmap.CreateBitmap(mRgba.Cols(), mRgba.Rows(), Bitmap.Config.Argb8888))
                    {
                        Utils.MatToBitmap(mRgba, bm);

                        //iView.SetImageBitmap(bm);
                        iView.SetImageBitmap(bm);
                    }

                    m.Release();
                    grayM.Release();
                }
            }
        }
    }
}