using System;
using Mirror;
using UnityEngine;

[Serializable]
public struct CreatePlayerMessage : NetworkMessage
{
    public Color color;
}
