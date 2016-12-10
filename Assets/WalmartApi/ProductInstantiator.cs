using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ProductInstantiator : MonoBehaviour
{
    [SerializeField]
    private Transform parentTransform;

    [SerializeField]
    private GameObject defaultObject;

    [SerializeField]
    private GameObject productInfoCanvas;   

    [SerializeField]
    private float offset = 8.0f;

    [SerializeField]
    private TableApiTest _tableApiTest;

    [Serializable]
    public class ProductArrayItem
    {
        public string keyword;
        public GameObject prefab;
    }

    public ProductArrayItem[] productObjects;

    [SerializeField]
    private Dictionary<string, GameObject> productObjs;

    // Use this for initialization
    void Start()
    {
        productObjs = new Dictionary<string, GameObject>();

        foreach (var pitem in productObjects)
        {
            productObjs.Add(pitem.keyword.ToLower(), pitem.prefab);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateStuff(ProductApiRequest pi)
    {
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }

        var currentItem = 1;

        foreach (var p in pi.items)
        {
            var productFound = false;
            GameObject instantiatedObject = null;

            foreach (var k in productObjs.Keys)
            {
                if (p.name.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0 || p.categoryPath.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    instantiatedObject = (GameObject)Instantiate(productObjs[k], parentTransform);
                    productFound = true;
                    break;
                }
            }

            if (!productFound)
            {
                instantiatedObject = (GameObject)Instantiate(defaultObject, parentTransform);
            }

            // todo: position correctly
            var position = parentTransform.position;
            position.x = currentItem * offset * (currentItem % 2 == 0 ? 1 : -1);
            instantiatedObject.transform.position = position;

            var infoGo = (GameObject)Instantiate(productInfoCanvas, instantiatedObject.transform);
            var tmpPos = instantiatedObject.transform.position;
            tmpPos.y += offset;
            infoGo.transform.position = tmpPos;

            infoGo.GetComponent<ProductInfoTextController>().UpdateText(p.name, "$" + p.salePrice.ToString());


            _tableApiTest.PostTableApi(p.name, p.salePrice.ToString());


            ++currentItem;
            
        }
    }

}
