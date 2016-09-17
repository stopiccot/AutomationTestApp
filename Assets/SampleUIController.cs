using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SampleUIController : MonoBehaviour {

	public Text huitaEbalaText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonClickHandler() {
		huitaEbalaText.gameObject.SetActive(true);
	}
}
