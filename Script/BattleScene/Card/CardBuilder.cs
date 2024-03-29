﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;

namespace BattleScene
{
    public class CardBuilder : MonoBehaviour {

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

        public Card CreateCard(CardMasterData data)
        {
            GameObject card;
            switch(data.CardTypes)
            {
                case CardMasterData.CardType.TREASURE:
                    card = Instantiate(m_CoinCard);
                    break;
                case CardMasterData.CardType.VICTORYPOINT:
                case CardMasterData.CardType.CURSE:
                    card = Instantiate(m_PointCard);
                    break;
                case CardMasterData.CardType.ACTION:
                case CardMasterData.CardType.ATTACKACTION:
                    card = Instantiate(m_ActionCard);
                    break;
                default:
                    card = Instantiate(m_CoinCard);
                    break;
            }
            var component = card.GetComponent<Card>();
            component.Setup(data);
            return component;
        }
    }
}