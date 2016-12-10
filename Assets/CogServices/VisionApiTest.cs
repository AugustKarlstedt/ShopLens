using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System;

public class VisionApiTest : MonoBehaviour
{
    [SerializeField]
    private ProductApiTest _productApiTest;

    [SerializeField]
    private ProductInstantiator _productInstantiator;

    void Start()
    {
        
    }

    public void QueryVisionApi(byte[] picData)
    {
        var url = "https://api.projectoxford.ai/vision/v1.0/analyze?visualFeatures=Description";

        var headers = new Dictionary<string, string>();
        headers["Ocp-Apim-Subscription-Key"] = "9c1f62fd8d47496d8e5a19c4d61adb27";
        headers["Content-Type"] = "application/octet-stream";
        WWW www = new WWW(url, picData, headers);

        StartCoroutine(WaitForRequest(www));
    }

    public void QueryVisionApi(string imageUrl)
    {
        var url = "https://api.projectoxford.ai/vision/v1.0/analyze?visualFeatures=Description";

        var headers = new Dictionary<string, string>();
        headers["Ocp-Apim-Subscription-Key"] = "9c1f62fd8d47496d8e5a19c4d61adb27";
        headers["Content-Type"] = "application/json";
        WWW www = new WWW(url, Encoding.UTF8.GetBytes("{\"url\":\"" + imageUrl + "\"}"), headers);

        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);

            var deserialized = JsonUtility.FromJson<DescribeImageRequest>(www.text);

            foreach (var t in deserialized.description.tags)
            {   
                foreach (var k in _productInstantiator.productObjects)
                {
                    if (t.IndexOf(k.keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        _productApiTest.QueryProductApi(t);

                        yield break;
                    }
                }
            }

            
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}