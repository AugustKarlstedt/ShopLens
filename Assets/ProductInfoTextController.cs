using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProductInfoTextController : MonoBehaviour {

    [SerializeField]
    private Text ProductName;

    [SerializeField]
    private Text ProductPrice;

	public void UpdateText(string name, string price)
    {
        ProductName.text = name;
        ProductPrice.text = price;
    }
}
