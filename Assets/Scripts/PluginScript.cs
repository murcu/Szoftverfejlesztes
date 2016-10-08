using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PluginScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AndroidJavaClass pluginClass = new AndroidJavaClass ("com.cornhub.arfantasy.HelloPlugin");
		GetComponent<Text>().text = pluginClass.CallStatic<string>("getMessage");
	}

}
