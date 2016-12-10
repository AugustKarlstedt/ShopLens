using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;

public class TableApiTest : MonoBehaviour
{
    void Start()
    {

    }

    public void PostTableApi(string name, string price)
    {
        WWWForm form = new WWWForm();
        var headers = form.headers;

        headers["ZUMO-API-VERSION"] = "2.0.0";
        headers["Content-Type"] = "application/json";
        form.AddField("newTitle", "New Value");
        form.AddField("newDate", "Today");

        //Object to store in JSON
        string newObj = "{ \"name\":\"" + name + "\", \"price\":" + price + "}";

        string url = "http://myventure-table.azurewebsites.net/tables/myInfo";

        WWW www = new WWW(url, Encoding.ASCII.GetBytes(newObj), headers);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}