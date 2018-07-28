using Database;
using UnityEngine;
using System.Text;
using ExitGames.Client.Photon;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BattleScene
{ 
    public static class DataSerializer<T>
    {
        public static void Register(char byteCode)
        {
            PhotonPeer.RegisterType(typeof(T),(byte)byteCode, SerializeCardMasterData, DeSerializeCardMasterData);
        }

        private static byte[] SerializeCardMasterData(object customObject)
        {
            var cardMasterData = (T)customObject;

            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, cardMasterData);

            return ms.ToArray();
        }

        private static object DeSerializeCardMasterData(byte[] customObject)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            ms.Write(customObject, 0, customObject.Length);
            ms.Seek(0, SeekOrigin.Begin);
            T obj = (T)bf.Deserialize(ms);
            return obj;
        }
    }
}