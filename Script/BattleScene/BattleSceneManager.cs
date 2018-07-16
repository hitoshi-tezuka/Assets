using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;

namespace BattleScene { 
	public class BattleSceneManager : MonoBehaviour{

		[SerializeField]
		private GameObject m_PlayerPrefab;
		[SerializeField]
		private DataBaseController m_DatabaseController;
        [SerializeField]
        private Supply m_Supply;
        [SerializeField]
        private Transform m_CardPhotonInsantiate;

		#region  データ定義
		// 処理ターン
		public enum TurnType
		{
            SetupPlayer = 0,
			OrderPlayer,	    // プレイヤー順序の決定
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

        
        private int m_PlayerNum = 0;
        public List<Player> m_PlayerList = new List<Player>();
        #endregion

        #region get/set

        public static BattleSceneManager SceneManager
		{
            get;private set;
		}

        public Transform PlayersTransform
        { get { return this.transform.Find("StageCanvas/Players"); } }

        public Transform CardPhotonInstantiate
        {
            get { return this.transform.Find("StageCanvas/CardPhotonInsantiate"); }
        }

        #endregion

        private void Awake()
        {
            SceneManager = this;
        }

        // Use this for initialization
        void Start () {
            // シリアライザ登録
            CardMasterDataSerializer.Register();
            m_ProcessingTurn = TurnType.SetupPlayer;
        }

        // Update is called once per frame
        void Update () {
			switch(m_ProcessingTurn)
			{
                case TurnType.SetupPlayer:
                    break;
				case TurnType.OrderPlayer:
					// プレイヤーを一定の条件でシャッフルする
					m_ProcessingTurn = TurnType.SelectCard;
					break;
				case TurnType.SelectCard:
					// どのカードでゲーム開始を行うか
					foreach( var cardData in m_DatabaseController.SelectCardMaster())
					{
                        var cardBuilder = new CardBuilder();
                        var card = cardBuilder.CreateCardObject(cardData,false);
                        m_Supply.AddSupply(card);
					}
					m_ProcessingTurn = TurnType.PlayerTurn;
					break;
				case TurnType.PlayerTurn:
					// プレイヤーがターン終了するまで他のプレイヤーは待機
					Player nowPlayer = m_PlayerList[0];

					break;
			}
		}

		public void Initialize()
        {
            m_ProcessingTurn = TurnType.SetupPlayer;
            // デッキ初期化
            List<CardMasterData> cardlist = InitializeDeck();

			// プレイヤー作成
			List<Player> list = new List<Player>();
		
            m_PlayerNum++;

            var playerPrefab = PhotonNetwork.Instantiate(m_PlayerPrefab.name,Vector3.zero,Quaternion.identity, 0);
            var view = playerPrefab.GetComponent<PhotonView>();

            Player player = playerPrefab.GetComponent<Player>();
            player.Initialize(cardlist);
		    m_PlayerList.Add(player);
		}

		private List<CardMasterData> InitializeDeck()
		{
			var cardlist =new List<CardMasterData>();
			var cardEstate = m_DatabaseController.SelectCardMaster("Estate");
			var cardCopper = m_DatabaseController.SelectCardMaster("Copper");
			for (int i = 0; i < 3; i++)
			{
				cardlist.Add(cardEstate);
			}
			for (int i = 0; i < 7; i++)
			{
				cardlist.Add(cardCopper);
			}
			return cardlist;

		}
	}
}