using UnityEngine;
using System.Collections;

public class ProductApiTest : MonoBehaviour
{

    [SerializeField]
    private ProductInstantiator _productInstantiator;

    void Start()
    {
        
    }

    public void QueryProductApi(string query)
    {
        WWW www = new WWW("http://api.walmartlabs.com/v1/search?query=" + query + "&format=json&apiKey=deetbmcsnubx46u26yxpmk53");
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);

            var deserialized = JsonUtility.FromJson<ProductApiRequest>(www.text);
            _productInstantiator.InstantiateStuff(deserialized);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}