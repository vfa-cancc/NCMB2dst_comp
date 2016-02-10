# NCMB2dst_comp

##　※自分でニフティクラウド mobile backendを利用する方へ

ニフティクラウド mobile backendを使われる際は自分のプロジェクトにSDKを導入する必要があります。
下記のクイックスタートからそちらを行ってください

http://mb.cloud.nifty.com/doc/current/introduction/quickstart_unity.html

##スコア保存

### スコア保存のコード説明

スコア保存のコードはAsset>Scriptsの「SaveScore.cs」にて行っています。
下記のコードが、スコア保存に関するコードになります、ご参照ください。

```
//mobile backendのSDKを読み込む
using NCMB; 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveScore : MonoBehaviour {
	// mobile backendに接続---------------------
	---
	public void save( string name, int score ) {
	  //データストアにスコアクラスを定義
	    //あらかじめ定義していない場合は自動で作成される
		NCMBObject obj = new NCMBObject ("Score");
		
		//obj["name"]でKeyを　=nameでvalueを設定
		obj ["name"] = name;
		
		//obj["score"]でKeyを　=scoreでvalueを設定
		obj ["score"] = score;
		
		//obj["Log"]でKeyを　= Player.posListでvalueを設定
		  //走行ログの取得方法は後述
		obj ["Log"] = Player.posList;
		
		//この処理でサーバーに書き込む
		obj.SaveAsync ();
	}
}
```

### 走行ログについて

走行ログはゲームプレイ中にPlayerの座標を取得・配列化しておき、スコア保存時にサーバーに一緒に保存しています。
コードとしてはAsset>Scriptsの「Player.cs」にて行っています。
下記のコードをご参照ください。

```

public class Player : MonoBehaviour
{
	// 配列を定義しておく
	public static List<float[]> posList = new List<float[]>();
  ・
  ・
  ・省略
  // 機体の移動
	  void Move (Vector2 direction)
	  {
  	・
  	・
    ・省略
  		//---ゴーストをつくるため、ポジションをリスト化する-----
	  	float[] postion = new float[2];
	  	postion [0] = transform.position.x;
	  	postion [1] = transform.position.y;
	  	posList.Add(postion);
	  	//----------------------------------------------
	  }
	 ・
	 ・
	 ・省略
  }
  
```

## オンラインランキングの表示

### 構成要素説明

ランキングはAsset>Scenesの「 LeaderBoard 」 シーンで生成されています。
下記にその構成要素と各役割を記します。

LeaderBoardシーンには以下のスクリプトが含まれています。

- LeaderBoard.cs
  - mobile backendと接続してスコアと名前を引き出す
- LeaderBoardManager.cs
  - ランキングの表示部、LeadeBoard.csを呼び
  - スコアと名前を引き出しランキングを描画
- Rankers.cs
  - 引き出したスコアと名前を一時的に保存するもの
  - スクリプト以外のものは以下になります。
- 各種見出しGUITextオブジェクト
  - 順位とプレイヤー名、スコアを表示するGUITextオブジェクト

### ロジック説明

ニフティクラウド mobile backendから子スコアのデータを取得するロジックは
Asset>Scripts 「LeaderBoard.cs」に記載されています。

```
// データストアの「Score」クラスから検索
NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("Score");

//「Score」クラスの「score」カラムを降順に並び替え
query.OrderByDescending ("score");

//上位5個のみ取得
query.Limit = 5;

//実際に取得
query.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {
		非同期の処理
	if (e != null) {
		//検索失敗時の処理
	} else {
		//検索成功時の処理
		List<NCMB.Rankers> list = new List<NCMB.Rankers>();
		
		// 取得したレコードをscoreクラスとして保存
		foreach (NCMBObject obj in objList) {
		
		  //引き出したオブジェクトからscoreだけを取り出す
			int  s = System.Convert.ToInt32(obj["score"]);
		
			//引き出したオブジェクトからnameだけを取り出す
			string n = System.Convert.ToString(obj["name"]);
		
			list.Add( new Rankers( s, n ) );
		}
			topRankers = list;
		}
	});
```
