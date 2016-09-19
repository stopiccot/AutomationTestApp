using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SampleUIController : MonoBehaviour {

	public Text huitaEbalaText;
	public Button button1;
	public Button button2;
	public Button button3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Button1ClickHandler() {
		if (!button2.isActiveAndEnabled) {
			button2.gameObject.SetActive(true);
		} else {
			button3.gameObject.SetActive(true);
		}
	}

	public void Button2ClickHandler() {
		button2.GetComponentInChildren<Text>().text = "NEW TEXT";
	}

	public void Button3ClickHandler() {
		huitaEbalaText.gameObject.SetActive(true);
	}
}
