using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene { 
    public class Status : MonoBehaviour {

	    [SerializeField]
	    Text Money;
	    [SerializeField]
	    Text Score;
	    [SerializeField]
	    Text Action;
	    [SerializeField]
	    Text Purchase;

	    /// <summary>
	    /// ステータスを初期化
	    /// </summary>
	    /// <param name="money">手札の所持金</param>
	    /// <param name="score"></param>
	    /// <param name="action"></param>
	    /// <param name="purchase"></param>
        public void UpdateStatus(Player.PlayerStatus status)
        {
            Money.text = status.Money.ToString();
            Action.text = status.Action.ToString();
            Purchase.text = status.Purchase.ToString();
        }
    }
}
