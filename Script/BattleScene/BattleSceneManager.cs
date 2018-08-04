using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
using UnityEngine.UI;

namespace BattleScene { 
	public class BattleSceneManager : MonoBehaviour, ITurnManagerCallbacks{

		[SerializeField]
		private GameObject m_PlayerPrefab;
		[SerializeField]
		private DataBaseController m_DatabaseController;
        [SerializeField]
        private Supply m_Supply;
        [SerializeField]
        private Transform m_CardPhotonInsantiate;
        [SerializeField]
        private BattleTurnManager m_TurnManager;
        [SerializeField]
        private GameObject m_PlayerTurnView;
        [SerializeField]
        private Text m_PlayerText;

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
        public List<Player> m_PlayerList = new List<Player>();
        public Player NowPlayer { get; private set;}
        private List<Entity_CardMaster.CardMasterData> m_card = new List<Entity_CardMaster.CardMasterData>();
        #endregion

        #region get/set

        public static BattleSceneManager SceneManager
		{
            get;private set;
		}

        public Transform PlayersTransform
        { get { return this.transform.Find("StageCanvas/Players"); } }
        public Transform FieldTransform
        { get { return this.transform.Find("StageCanvas/Stage/Field"); } }
        public Transform SupplyTransform
        { get { return this.transform.Find("StageCanvas/Stage/Supply/Scroll View/Content"); } }
        #endregion

        private void Awake()
        {
            SceneManager = this;
        }
    
        // Use this for initialization
        void Start () {
            Entity_CardMaster card = Resources.Load<Entity_CardMaster>("CardData");
            card.Card.ForEach(x => { m_card.Add(x); Debug.Log(x.Card); });

            // シリアライザ登録
            DataSerializer<Entity_CardMaster.CardMasterData>.Register('C');
            DataSerializer<Player.PlayerStatus>.Register('S');
            m_TurnManager = GetComponent<BattleTurnManager>();
            m_TurnManager.TurnManagerListener = this;
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
                    if(PhotonNetwork.isMasterClient)
                    { 
					    // どのカードでゲーム開始を行うか
					    foreach( var cardData in m_card)
					    {
                            var cardBuilder = new CardBuilder();
                            var card = cardBuilder.CreateCardObject(cardData,true);
                            m_Supply.AddSupply(card);
					    }
                        m_ProcessingTurn = TurnType.PlayerTurn;
                    }
                    break;
				case TurnType.PlayerTurn:
					// プレイヤーがターン終了するまで他のプレイヤーは待機
					break;

			}
		}

        public void Initialize()
        {
            m_ProcessingTurn = TurnType.OrderPlayer;
            // デッキ初期化
            List<Entity_CardMaster.CardMasterData> cardlist = InitializeDeck();

			// プレイヤー作成
			List<Player> list = new List<Player>();
		
            var playerPrefab = PhotonNetwork.Instantiate(m_PlayerPrefab.name,Vector3.zero,Quaternion.identity, 0);
            Player player = playerPrefab.GetComponent<Player>();
            player.Initialize(cardlist);
        }

		private List<Entity_CardMaster.CardMasterData> InitializeDeck()
		{
			var cardlist =new List<Entity_CardMaster.CardMasterData>();
            var cardEstate = m_card.Find(x => x.Card.Equals("Estate"));
			var cardCopper = m_card.Find(x => x.Card.Equals("Copper"));
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

        public void PlayerAdd(Player player)
        {
            m_PlayerList.Add(player);
        }

        public void GameStart()
        {
            if (PhotonNetwork.isMasterClient)
                StartTurn();
        }

        private void StartTurn()
        {
                m_TurnManager.BeginTurn();
        }

        public void OnTurnBegins()
        {
            Debug.Log("OnTurnBegins");
            var player = m_TurnManager.PlayerTurn;
            m_PlayerText.text = player.ID.ToString();
            m_PlayerList.ForEach(x =>
            {
                if (x.OwnerPlayer.Equals(player))
                {
                    x.gameObject.SetActive(true);
                    NowPlayer = x;
                }
                else
                { 
                    x.gameObject.SetActive(false);
                }
            });

        }

        public void OnTurnCompleted()
        {
            NowPlayer.EndTurn();
            StartTurn();
        }

        public void OnTurnTimeEnds()
        {
            OnTurnCompleted();
        }
    }
}