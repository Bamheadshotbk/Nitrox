﻿using System.Collections.Generic;
using NitroxClient.Map;
using NitroxModel;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.Logger;
using NitroxModel.Packets;
using NitroxModel.DataStructures.Util;

namespace NitroxClient.Communication
{
    // TODO: Spinlocks don't seem to be necessary here, but I don't know for certain.
    public class PacketReceiver
    {
        private readonly Queue<Packet> receivedPackets;

        public PacketReceiver()
        {
            receivedPackets = new Queue<Packet>();
        }

        public void PacketReceived(Packet packet)
        {
            lock (receivedPackets)
            {
                receivedPackets.Enqueue(packet);
            }
        }

        public Queue<Packet> GetReceivedPackets()
        {
            Queue<Packet> packets = new Queue<Packet>();

            lock (receivedPackets)
            {
                while (receivedPackets.Count > 0)
                {
                    packets.Enqueue(receivedPackets.Dequeue());
                }
            }

            return packets;
        }        
    }
}
