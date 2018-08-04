using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene {
	public abstract class Effect  {
        protected string m_CardId;
        public Effect(string cardId) { m_CardId = cardId; }
		public abstract void ActivateEffect(Player player);
	}
}
