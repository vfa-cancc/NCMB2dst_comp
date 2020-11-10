using UnityEngine;
#if UNITY_2019_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

public class Manager : MonoBehaviour
{
	// Playerプレハブ
	public GameObject player;
	public GameObject ghost;
	// タイトル
	private GameObject title;

	 void Start ()
	{
		// Titleゲームオブジェクトを検索し取得する
		title = GameObject.Find ("Title");
	}	 

	public void GameStart (bool withGhost)
	{
		// ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
		title.SetActive (false);

		//------------------------------------------------------------------------------
		if (withGhost == true) {
			// ゴーストボタンを押下したらゴーストを表示する
			Instantiate (ghost, ghost.transform.position, ghost.transform.rotation);
			Instantiate (player, player.transform.position, player.transform.rotation);
		} else {
			// 画面を押下したらゴーストを表示しないでゲームを開始する
			Instantiate (player, player.transform.position, player.transform.rotation);
		}
		//------------------------------------------------------------------------------
	}
	
	public void GameOver ()
	{
		FindObjectOfType<Score> ().Save ();
#if UNITY_2019_3_OR_NEWER
		SceneManager.LoadScene("SaveScore");
#else
		Application.LoadLevel("SaveScore");
#endif
		// ゲームオーバー時に、タイトルを表示する
		//title.SetActive (true);
	}
	
	public bool IsPlaying ()
	{
		// ゲーム中かどうかはタイトルの表示/非表示で判断する
		return title.activeSelf == false;
	}

	public void OnClickLeaderBoardButton(){
		#if UNITY_2019_3_OR_NEWER
			SceneManager.LoadScene("LeaderBoard");
		#else
			Application.LoadLevel("LeaderBoard");
		#endif
	}

	public void OnClickGhost() {
		//---Bg_ghost.csでゴーストデータを取得できたら、ゴーストボタンを表示する-------------
		if (Bg_ghost.readyGhost == true) {
			GameStart (true);
		}
	}
}