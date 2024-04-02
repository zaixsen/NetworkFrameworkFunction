using ExamDay10;
using examDay10Server;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day10Server
{
    internal class RoomMgr : Singleton<RoomMgr>
    {
        //房间 id 
        Dictionary<int, RoomData> dic_RoomData = new Dictionary<int, RoomData>();
        Dictionary<int, List<Client>> dic_roomClients = new Dictionary<int, List<Client>>();

        public void Init()
        {
            MessageCenter.Ins.AddLisenter(MsgId.CS_UPDATE_ROOM, OnUpdateRoom);
            MessageCenter.Ins.AddLisenter(MsgId.CS_CREAT_ROOM, OnCreatRoom);
            MessageCenter.Ins.AddLisenter(MsgId.CS_JOIN_ROOM, OnJoinRoom);
            MessageCenter.Ins.AddLisenter(MsgId.CS_QUIT_ROOM, OnQuitRoom);
        }

        private void OnQuitRoom(MsgData data)
        {
            int roomId = BitConverter.ToInt32(data.data, 0);
            if (dic_RoomData.ContainsKey(roomId))
            {
                dic_RoomData[roomId].RoomPlayer.Remove(data.client.myPlayerData);
            }
            if (dic_roomClients.ContainsKey(roomId))
            {
                dic_roomClients[roomId].Remove(data.client);
            }
            NetMgr.Ins.SendClient(data.client, MsgId.SC_QUIT_ROOM, new byte[0]);
            NetMgr.Ins.SendRoomList(dic_roomClients[roomId], MsgId.SC_JOIN_ROOM, dic_RoomData[roomId].ToByteArray());
            UpdateRoom();
        }

        private void OnJoinRoom(MsgData data)
        {
            int roomId = BitConverter.ToInt32(data.data, 0);
            //添加到房间
            if (dic_RoomData.ContainsKey(roomId))
            {
                if (dic_RoomData[roomId].RoomPlayer.Count == dic_RoomData[roomId].MaxPerson)
                {
                    return;
                }
                dic_RoomData[roomId].RoomPlayer.Add(data.client.myPlayerData);
            }
            else
            {
                Console.WriteLine("无此房间Id");
                return;
            }
            //房间内的客户端
            if (dic_roomClients.ContainsKey(roomId))
            {
                dic_roomClients[roomId].Add(data.client);
                UpdateRoom();

                //房间内要生成此数据
                NetMgr.Ins.SendRoomList(dic_roomClients[roomId], MsgId.SC_JOIN_ROOM, dic_RoomData[roomId].ToByteArray());
            }
        }

        private void OnCreatRoom(MsgData data)
        {
            RoomData roomData = RoomData.Parser.ParseFrom(data.data);

            if (dic_RoomData.ContainsKey(roomData.Id))
            {
                Console.WriteLine("已经有此房间");
                //NetMgr.Ins.SendClient(data.client, MsgId.SC_CREAT_ROOM, UTF8Encoding.UTF8.GetBytes("已经有此房间"));
                return;
            }
            else
            {
                dic_RoomData.Add(roomData.Id, roomData);
                roomData.RoomPlayer.Add(data.client.myPlayerData);   //添加数据
                dic_roomClients.Add(roomData.Id, new List<Client>() { data.client });   //new 一个把自己放进去

                NetMgr.Ins.SendClient(data.client, MsgId.SC_CREAT_ROOM, roomData.ToByteArray());
                UpdateRoom();
            }
        }

        void UpdateRoom()
        {
            AllRoomData allRoomData = new AllRoomData();
            foreach (var item in dic_RoomData.Values)
            {
                allRoomData.AllRoom.Add(item);
            }
            NetMgr.Ins.SendAllClient(MsgId.SC_REFRESH_ROOM, allRoomData.ToByteArray());
        }

        private void OnUpdateRoom(MsgData data)
        {
            AllRoomData allRoomData = new AllRoomData();
            foreach (var item in dic_RoomData.Values)
            {
                allRoomData.AllRoom.Add(item);
            }
            NetMgr.Ins.SendClient(data.client, MsgId.SC_UPDATE_ROOM, allRoomData.ToByteArray());
        }
    }
}
