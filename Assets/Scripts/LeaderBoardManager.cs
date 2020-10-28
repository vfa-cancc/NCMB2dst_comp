using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_2019_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

public class LeaderBoardManager : MonoBehaviour {
	
	private LeaderBoard lBoard;
	public GameObject[] top = new GameObject[5];

	
	void Start ()
	{
		lBoard = new LeaderBoard();
		
		// テキストを表示するゲームオブジェクトを取得
		for( int i = 0; i < 5; ++i ) {
			top[i] = GameObject.Find ("Top" + i);
		}
		lBoard.fetchTopRankers(()=>{
			if( lBoard.topRankers != null ){ 
						
				// 取得したトップ5ランキングを表示
				for( int i = 0; i < lBoard.topRankers.Count; ++i) {
					top[i].GetComponent<UnityEngine.UI.Text>().text = i+1 + ". " + lBoard.topRankers[i].print();
				}

			}
		});

	}
	
	

	public void OnBack(){
		#if UNITY_2019_3_OR_NEWER
					SceneManager.LoadScene("Stage");
		#else
					Application.LoadLevel("Stage");
		#endif
	}
	 
}
