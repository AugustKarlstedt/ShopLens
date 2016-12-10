using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class DeleteObjectCommand : MonoBehaviour
{
    [SerializeField]
    private GazeManager _gazeManager;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeleteObject()
    {
        if (_gazeManager.FocusedObject != null)
        {
            Destroy(_gazeManager.FocusedObject);
        }
    }
}
