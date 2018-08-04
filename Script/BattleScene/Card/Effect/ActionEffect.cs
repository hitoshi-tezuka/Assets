using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace BattleScene { 
	public class ActionEffect : Effect {

        private const string ADVENTURER = "Adventurer";
        private const string BUREAUCRAT = "Bureaucrat";
        private const string CELLAR     = "Cellar";
        private const string CHANCELLOR = "Chancellor";
        private const string CHAPEL     = "Chapel";
        private const string COUNCILROOM = "CouncilRoom";
        private const string FEAST      = "Feast";
        private const string FESTIVAL   = "Festival";
        private const string LABORATORY = "Laboratory";
        private const string LIBRARY    = "Library";
        private const string MARKET     = "Market";
        private const string MILITIA    = "Militia";
        private const string MINE       = "Mine";
        private const string MOAT       = "Moat";
        private const string MONEYLENDER = "Moneylender";
        private const string REMODEL    = "Remodel";
        private const string SMITHY     = "Smithy";
        private const string SPY        = "Spy";
        private const string THIEF      = "Thief";
        private const string THRONEROOM = "ThroneRoom";
        private const string VILLAGE    = "Village";
        private const string WITCH      = "Witch";
        private const string WOODCUTTER = "Woodcutter";

        public ActionEffect(string cardId)  : base(cardId)
        {

        }

		public override void ActivateEffect(Player player)
		{
            switch(m_CardId)
            {
                case ADVENTURER:
                    /* 
                        あなたの山札から財宝カード２枚が公開されるまで、カードを公開する。
                        公開した財宝カード２枚を手札に加え、他の公開したカードは捨て札に置く。
                    */
                    
                    var task = AdventureEffect(player);
                    break;

                case BUREAUCRAT:
                    /*
                        銀貨を獲得し、デッキの上に置く。
                        他のプレイヤーは全員、各自の手札から勝利点カードを１枚公開し、各自のデッキの上に置く。（手札に勝利点カードがない場合、手札を公開する。）
                    */

                    break;
                case CELLAR:
                    /*
                        ＋１アクション
                        手札から好きな枚数のカードを捨て札にし、その枚数分だけデッキからカードを引く。
                    */

                    break;
                case CHANCELLOR:
                    /*
                        ＋２コイン
                        あなたの山札のカードすべてを、即座に捨て札に置くことができる。
                    */
                    break;
                case CHAPEL:
                    /*
                        あなたの手札から４枚までのカードを廃棄する。
                     */
                    break;
                case COUNCILROOM:
                    /*
                        ＋４カードを引く
                        ＋１カードを購入
                        他のプレイヤーは全員、カードを１枚引く。
                     */
                    break;
                case FEAST:
                    /*
                        このカードを廃棄する。
                        コスト５コイン以下のカード１枚を獲得する。
                     */
                    break;
                case FESTIVAL:
                    /*
                        ＋２アクション
                        ＋１カードを購入
                        ＋２コイン
                     */
                    break;
                case LABORATORY:
                    /*
                        ＋２カードを引く
                        ＋１アクション
                     */
                    break;
                case LIBRARY:
                    /*
                        あなたの手札が７枚になるまでカードを引く。この途中で引いたアクションカードは手札に加えず、脇に置いてもよい。脇に置いたカードは、カードを引き終えた後、捨て札にする。
                     */
                    break;
                case MARKET:
                    /*
                        ＋１カードを引く
                        ＋１アクション
                        ＋１カードを購入
                        ＋１コイン
                     */
                    break;
                case MILITIA:
                    /*
                        ＋２コイン
                        他のプレイヤーは全員、各自の手札が３枚になるまで捨て札にする。
                    */
                    break;
                case MINE:
                    /*
                        手札の財宝カードを１枚廃棄してもよい。
                        廃棄したカードのコストに３コインを加えたコストまでのコストを持つ財宝カードを１枚獲得し、手札に加える。
                    */
                    break;
                case MOAT:
                    /*
                        ＋２カードを引く
                        他のプレイヤーがアタックカードを使用した時、先にこのカードを手札から公開することによってそのアタックカードの影響を受けなくすることができる。
                    */
                    break;
                case MONEYLENDER:
                    /*
                        手札から銅貨を１枚を廃棄するのと引き換えに＋３コインを得てもよい。
                    */
                    break;
                case REMODEL:
                    /*
                        手札を１枚廃棄する。廃棄したカードのコストに２コインを加えたコストまでのコストを持つカードを１枚獲得する。
                    */
                    break;
                case SMITHY:
                    /*
                        ＋３カードを引く。
                    */
                    break;
                case SPY:
                    /*
                        ＋１カードを引く
                        ＋１アクション
                        各プレイヤー（あなたも含む）は、自分の山札の一番上のカードを公開し、そのカードを捨て札にするかそのまま戻すかをあなたが選ぶ。"
                    */
                    break;
                case THIEF:
                    /*
                        他のプレイヤーは全員、自分の山札の上から２枚のカードを公開する。
                        財宝カードを公開した場合、その中の１枚をあなたが選んで廃棄する。
                        あなたはここで廃棄したカードのうち好きな枚数を獲得できる。
                        他の公開したカードはすべて捨て札にする。"
                    */
                    break;
                case THRONEROOM:
                    /*
                        手札のアクションカード１枚を２度使用してもよい。
                    */
                    break;
                case VILLAGE:
                    /*
                        ＋１カードを引く
                        ＋２アクション
                    */
                    break;
                case WITCH:
                    /*
                        ＋２カードを引く
                        他のプレイヤーは全員、呪いを１枚獲得する。
                    */
                    break;
                case WOODCUTTER:
                    /*
                        ＋１カードを購入
                        ＋２コイン
                    */
                    break;
            }
		}

        private async Task AdventureEffect(   Player player)
        {
            Debug.Log("冒険者の効果を発動");
            //var getCardList = new List<Card>();
            //var disCard = new List<Card>();
            int i = 0;
            while (i < 2)
            {

                // プレイヤーカードのドローを行う
                var card = player.DrawCard(1)[0];
                card.UpdateState(Card.CardState.PUBLICDRAWCARD);

                await Task.Delay(1000);
               
                Debug.Log(card.CardId + " i +" + i);    
                // 財宝カードだった場合、カウント+1
                if (card.Type == Card.CardType.TREASURE)
                {

                    card.UpdateState(Card.CardState.HAND);
                    //getCardList.Add(card);
                    i++;
                }
                else
                {
                    // 他に公開したカードは捨て札に
                    card.UpdateState(Card.CardState.DISCARD);
                    //disCard.Add(card);
                }
            }
            Debug.Log("冒険者効果終了");
            //getCardList.ForEach(x => x.UpdateState(Card.CardState.HAND));
            //disCard.ForEach(x => x.UpdateState(Card.CardState.DISCARD));
        }

    }
}
