using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Dungeon : MonoBehaviour {

	private DungeonData data;
	private float lat;
	private float lon;

	private float easting;
	private float northing;

	private float easting_offset;
	private float northing_offset;

	void Start(){		
	}

	void Update(){
	}

	public void setData(DungeonData d){
		data = d;
		convertLatLonToUTM (data.lat, data.lon);
		init ();
	}

	private void init(){
		for (int i = 0; i < data.difficulty; i++) {
			GameObject dif = GameObject.CreatePrimitive (PrimitiveType.Plane);
			dif.transform.parent = transform;
			dif.transform.localScale = new Vector3 (0.05f, 0.05f, 0.05f);
			dif.transform.position = transform.position + new Vector3 (6.0f*i, 1.0f, 10f);
		}
	}

	public void setOffset(float e, float n){
		easting_offset = e;
		northing_offset = n;

		Vector3 pos = new Vector3 (-(easting_offset-easting), transform.position.y, -(northing_offset - northing));
		transform.position = pos;
	}

	void convertLatLonToUTM(float lat, float lon){
		int zone = (int) Mathf.Floor(lon/6+31);

		easting = 0.5f*Mathf.Log((1f+Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin(lon*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f))/(1f-Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin(lon*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f)))*0.9996f*6399593.62f/Mathf.Pow((1f+Mathf.Pow(0.0820944379f, 2f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f), 2f)), 0.5f)*(1f+ Mathf.Pow(0.0820944379f,2f)/2f*Mathf.Pow((0.5f*Mathf.Log((1f+Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin(lon*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f))/(1f-Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin(lon*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f)))),2f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f)/3f)+500000f;
		easting = (float)Mathf.Round(easting*100f)*0.01f;
		northing = (Mathf.Atan(Mathf.Tan(lat*Mathf.PI/180f)/Mathf.Cos((lon*Mathf.PI/180f-(6f*zone -183f)*Mathf.PI/180f)))-lat*Mathf.PI/180f)*0.9996f*6399593.625f/Mathf.Sqrt(1f+0.006739496742f*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f))*(1f+0.006739496742f/2f*Mathf.Pow(0.5f*Mathf.Log((1f+Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin((lon*Mathf.PI/180f-(6f*zone -183f)*Mathf.PI/180f)))/(1f-Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin((lon*Mathf.PI/180f-(6f*zone -183f)*Mathf.PI/180f)))),2f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f))+0.9996f*6399593.625f*(lat*Mathf.PI/180f-0.005054622556f*(lat*Mathf.PI/180f+Mathf.Sin(2f*lat*Mathf.PI/180f)/2f)+4.258201531e-05f*(3f*(lat*Mathf.PI/180f+Mathf.Sin(2f*lat*Mathf.PI/180f)/2f)+Mathf.Sin(2f*lat*Mathf.PI/180f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f))/4f-1.674057895e-07f*(5f*(3f*(lat*Mathf.PI/180f+Mathf.Sin(2f*lat*Mathf.PI/180f)/2f)+Mathf.Sin(2f*lat*Mathf.PI/180f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f))/4f+Mathf.Sin(2f*lat*Mathf.PI/180f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f))/3f);
		northing = (float)Mathf.Round(northing*100f)*0.01f;
	}

	void openMessage(){
		Debug.Log (data.dungeonName + " touched");
		buildDungeon ();
	}

	void buildDungeon(){
		
	}

}