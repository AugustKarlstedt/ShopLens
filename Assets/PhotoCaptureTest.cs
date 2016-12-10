using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.WebCam;
using System.Linq;
using System.Collections.Generic;

public class PhotoCaptureTest : MonoBehaviour
{
    [SerializeField]
    private Material _outputMaterial;

    [SerializeField]
    private VisionApiTest _visionApiTest;

    PhotoCapture photoCaptureObject = null;

    // Use this for initialization
    void Start()
    {
        
    }

    public void CapturePhoto()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        cameraResolution = new Resolution()
        {
            width = 896,
            height = 504,
            refreshRate = 0
        };

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.JPEG;

        captureObject.StartPhotoModeAsync(c, false, OnPhotoModeStarted);
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        /* Saving to file */
        //if (result.success)
        //{
        //    string filename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);
        //    string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

        //    photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
        //}
        //else
        //{
        //    Debug.LogError("Unable to start photo mode!");
        //}


        /* Saving to Texture2D */
        if (result.success)
        {
            photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
        }
        else
        {
            Debug.LogError("Unable to start photo mode!");
        }
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Saved Photo to disk!");
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
        }
        else
        {
            Debug.Log("Failed to save Photo to disk");
        }
    }

    //void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    //{
    //    if (result.success)
    //    {
    //        // Create our Texture2D for use and set the correct resolution
    //        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
    //        Texture2D targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);
    //        // Copy the raw image data into our target texture
    //        photoCaptureFrame.UploadImageDataToTexture(targetTexture);
    //        // Do as we wish with the texture such as apply it to a material, etc.

    //        _outputMaterial.mainTexture = targetTexture;
    //    }
    //    // Clean up
    //    photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    //}

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        if (result.success)
        {
            List<byte> imageBufferList = new List<byte>();
            // Copy the raw IMFMediaBuffer data into our empty byte list.
            photoCaptureFrame.CopyRawImageDataIntoBuffer(imageBufferList);

            //_visionApiTest.QueryVisionApi("http://www.technewscentral.co.uk/wp-content/uploads/2015/07/sony-tablets1-hands2-lg.jpg"); //tablet
            //_visionApiTest.QueryVisionApi("https://pixabay.com/static/uploads/photo/2012/04/01/18/38/television-23936_960_720.png"); //tv
            //_visionApiTest.QueryVisionApi("https://i5.walmartimages.com/dfw/4ff9c6c9-9356/k2-_c59a878c-3d51-4807-aabd-84b0410de921.v1.jpg"); //phone
            //_visionApiTest.QueryVisionApi("http://core0.staticworld.net/images/article/2015/02/hp-spectre-x360_beauty-100570598-orig.jpg"); //laptop
            _visionApiTest.QueryVisionApi(imageBufferList.ToArray());            
        }

        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
