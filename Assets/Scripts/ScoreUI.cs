using UnityEngine;
using System.Collections;
#if UNITY_2019_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif
using UnityEngine.UI;
using System;
public class ScoreUI : MonoBehaviour {
	
	private GameObject guiTextSaveScore;   // SaveScore Text
	
	
	// テキストボックスで入力される文字列を格納
	public InputField name;
	public InputField score_string;
	
	void Start () {
		// ゲームオブジェクトを検索し取得する
		guiTextSaveScore  = GameObject.Find ("GUITextSaveScore");
		int score = PlayerPrefs.GetInt ("Score", 1);
		score_string.text = score.ToString();
	}
	public void OnSubmit(){

		int score = 0;
		if(int.TryParse(score_string.text,out score)){
			FindObjectOfType<SaveScore> ().save (name.text, score);
					#if UNITY_2019_3_OR_NEWER
								SceneManager.LoadScene("Stage");
					#else
								Application.LoadLevel("Stage");
					#endif
		}else{
			Debug.Log("sdnjkhsakjfhksa");
		}
		
	}

}
