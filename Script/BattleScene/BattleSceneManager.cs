using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;

namespace BattleScene { 
	public class BattleSceneManager : MonoBehaviour {

        [SerializeField]
        private GameObject m_PlayerPrefab;
        [SerializeField]
        private DataBaseController m_DatabaseController;
        
        #region  データ定義
		// 処理ターン
		public enum TurnType
		{
			OrderPlayer = 0,	// プレイヤー順序の決定
			SelectCard,			// カード選択
			PlayerTurn,			// プレイヤーターン処理
		} 

		// プレイヤーターン
		public enum PlayerTurn
		{
			SelectCard = 0,		// カード選択
			Action,				// アクションフェーズ
			Purchase,			// 購入フェーズ
			CleanUp,			// クリーンアップフェーズ
		}

		#endregion

		#region 変数定義
		public TurnType m_ProcessingTurn;               // 現在のターン


		private static BattleSceneManager m_Manager;
		private List<Player> m_PlayerList = new List<Player>();
		#endregion

		#region get/set
		public static BattleSceneManager SceneManager
		{
			get { return m_Manager; }
		}
		#endregion

		// Use this for initialization
		void Start () {
			Initialize();
		}
	
		// Update is called once per frame
		void Update () {
			switch(m_ProcessingTurn)
			{
				case TurnType.OrderPlayer:
                    // プレイヤーを一定の条件でシャッフルする
                    m_ProcessingTurn = TurnType.SelectCard;
                    break;
				case TurnType.SelectCard:
                    // どのカードでゲーム開始を行うか
                    foreach( var card in m_DatabaseController.SelectCardMaster())
                    {
                        Debug.Log( card.Card);
                    }
                    m_ProcessingTurn = TurnType.PlayerTurn;
					break;
				case TurnType.PlayerTurn:
					// ゲーム開始を行う

					// プレイヤーがターン終了するまで他のプレイヤーは待機
					Player nowPlayer = m_PlayerList[0];


					break;
			}
		}

		// 初期化
		public void Initialize()
		{
			m_ProcessingTurn = TurnType.OrderPlayer;

			// プレイヤー作成
			List<Player> list = new List<Player>();
			for(int i = 0; i<1;i++)
			{
                var playerPrefab = Instantiate(m_PlayerPrefab,this.transform.Find("StageCanvas"));
                Player player = playerPrefab.GetComponent<Player>();
                player.Initialize();
                player.transform.localPosition = Vector3.zero;
				m_PlayerList.Add(player);
			}
		}
	}
}