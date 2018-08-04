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

        public Card CreateCardObject(Entity_CardMaster.CardMasterData data, bool isPhotonNetworkCreate)
        {
            GameObject card;
            switch(data.CardType)
            {
                case Card.CardType.TREASURE:
                    card = isPhotonNetworkCreate ? PhotonNetwork.Instantiate(COINCARDPATH, Vector3.zero,Quaternion.identity,0) : MonoBehaviour.Instantiate(m_CoinCard);
                    break;
                case Card.CardType.VICTORYPOINT:
                case Card.CardType.CURSE:
                    card = isPhotonNetworkCreate ? PhotonNetwork.Instantiate(POINTCARDPATH, Vector3.zero, Quaternion.identity, 0):MonoBehaviour.Instantiate(m_CoinCard);
                    break;
                case Card.CardType.ACTION:
                case Card.CardType.ATTACKACTION:
                case Card.CardType.REACTION:
                    card = isPhotonNetworkCreate ? PhotonNetwork.Instantiate(ACTIONCARDPATH, Vector3.zero, Quaternion.identity, 0) : MonoBehaviour.Instantiate(m_CoinCard);
                    break;
                default:
                    Debug.LogError("カードタイプが存在していません。");
                    card = null;
                    break;
            }
            var component = card.GetComponent<Card>();
            component.Setup(data);
            return component;
        }
    }
}