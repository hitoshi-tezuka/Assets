using Database;
using System.Text;
using ExitGames.Client.Photon;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BattleScene
    {
    public class CardSerializer {

        public static void Register()
        {
            PhotonPeer.RegisterType(typeof(Card), (byte)'Q', SerializeCardMasterData, DeSerializeCardMasterData);
        }

        private static byte[] SerializeCardMasterData(object customObject)
        {
            var cardMasterData = (Card)customObject;

            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, cardMasterData);

            /*
             var stringBytes =   Encoding.UTF8.GetBytes(cardMasterData.Card).Length + 
                                Encoding.UTF8.GetBytes(cardMasterData.CardName).Length + 
                                Encoding.UTF8.GetBytes(cardMasterData.ImageName).Length;
            var cardBytes = Protocol.Serialize(cardMasterData.Card);
            var CardName = Encoding.UTF8.GetBytes(cardMasterData.CardName);
           var bytes = new byte[13 * sizeof(int) + stringBytes]; 
            int index = 0;
           */
            return ms.ToArray();
        }

        private static object DeSerializeCardMasterData(byte[] customObject)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            ms.Write(customObject, 0, customObject.Length);
            ms.Seek(0, SeekOrigin.Begin);
            Card obj = (Card)bf.Deserialize(ms);
            return obj;
        }
    }
}