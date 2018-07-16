using UnityEngine;
using Database;

namespace BattleScene
{
    public class CardBuilder  {

        private GameObject m_CoinCard;
        private GameObject m_PointCard;
        private GameObject m_ActionCard;

        private const string COINCARDPATH = "card/CoinCard";
        private const string POINTCARDPATH = "card/PointCard";
        private const string ACTIONCARDPATH = "card/ActionCard";


        public CardBuilder()
        {
            m_CoinCard = Resources.Load(COINCARDPATH) as GameObject;
            m_PointCard = Resources.Load(POINTCARDPATH) as GameObject;
            m_ActionCard = Resources.Load(ACTIONCARDPATH) as GameObject;
        }

        public Card CreateCardObject(CardMasterData data, bool isPhotonNetworkCreate)
        {
            GameObject card;
            switch(data.CardTypes)
            {
                case CardMasterData.CardType.TREASURE:
                    card = isPhotonNetworkCreate ? PhotonNetwork.Instantiate(COINCARDPATH, Vector3.zero,Quaternion.identity,0) : MonoBehaviour.Instantiate(m_CoinCard);
                    break;
                case CardMasterData.CardType.VICTORYPOINT:
                case CardMasterData.CardType.CURSE:
                    card = isPhotonNetworkCreate ? PhotonNetwork.Instantiate(POINTCARDPATH, Vector3.zero, Quaternion.identity, 0):MonoBehaviour.Instantiate(m_CoinCard);
                    break;
                case CardMasterData.CardType.ACTION:
                case CardMasterData.CardType.ATTACKACTION:
                    card = isPhotonNetworkCreate ? PhotonNetwork.Instantiate(ACTIONCARDPATH, Vector3.zero, Quaternion.identity, 0) : MonoBehaviour.Instantiate(m_CoinCard);
                    break;
                default:
                    card = MonoBehaviour.Instantiate(m_CoinCard);
                    break;
            }
            var component = card.GetComponent<Card>();
            component.Setup(data);
            return component;
        }

        public Card CreateCardData(CardMasterData data)
        {
            Card card;
            switch (data.CardTypes)
            {
                case CardMasterData.CardType.TREASURE:
                    card = new CoinCard();
                    break;
                case CardMasterData.CardType.VICTORYPOINT:
                case CardMasterData.CardType.CURSE:
                    card = new PointCard();
                    break;
                case CardMasterData.CardType.ACTION:
                case CardMasterData.CardType.ATTACKACTION:
                    card = new ActionCard();
                    break;
                default:
                    card = new CoinCard();
                    break;
            }
            card.Setup(data);
            return card;
        }
    }
}