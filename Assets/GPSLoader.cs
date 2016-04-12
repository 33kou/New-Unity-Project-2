using UnityEngine;
using System.Collections;

public class GPSLoader : MonoBehaviour {
	double Latitude = 0.0d;
	double Longitude = 0.0d;
	private GUIStyle m_guiStyle;
	LocationService a = new LocationService ();

	IEnumerator Start() {

		m_guiStyle = new GUIStyle();
		m_guiStyle.fontSize = 60;   // フォントサイズの変更.
		if (!Input.location.isEnabledByUser) { //GPS許可の確認


			yield break;

		}



		a.Start(5.0f,0.001f);//第一引数でサービスの精度(m)　第二引数で更新されるための最小距離として何m横方向に移動する必要があるかを設定
		
		int maxWait =  120;
			while (a.status == LocationServiceStatus.Initializing && maxWait > 0) {//取得待ちのループ
			
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		if (maxWait < 1) {
			print("Timed out");

			yield break;
		}
			if (a.status == LocationServiceStatus.Failed) {
			print("Unable to determine device location");
			Debug.Log("GPS Failed");

			yield break;
		} else {
			/*print("Location: " + 
				Input.location.lastData.latitude + " " + 
				Input.location.lastData.longitude + " " + 
				Input.location.lastData.altitude + " " + 
				Input.location.lastData.horizontalAccuracy + " " + 
				Input.location.lastData.timestamp);
				*/
			Debug.Log("GPS got");
				Latitude = a.lastData.latitude;//緯度の取得
				Longitude = a.lastData.longitude;//経度の取得
		}


		//a.Stop();//位置情報取得の終了
	}
	void Update()
	{
		
		int maxWait =  120;
		while (a.status == LocationServiceStatus.Initializing && maxWait > 0) {
			
			new WaitForSeconds(1);
			maxWait--;
		}
		if (maxWait < 1) {
			print ("Timed out");


		} else {
			Latitude = a.lastData.latitude;//緯度の取得
			Longitude = a.lastData.longitude;//経度の取得

		}

	}
	void OnGUI () 
	{
		GUI.Label(new Rect(10, 400, 120, 120), "緯度　　"+Latitude+"\n"+"経度　　"+Longitude , m_guiStyle);
	}
}