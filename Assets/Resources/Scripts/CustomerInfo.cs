using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class CustomerInfo
{
    public string name;
    public int mentalPoint;
    public int physicalPoint;
    public string want;
    public string need;
    public string sick;
    public string body;
    public string face;
    public string emote;
    public string hair;
    public string script;
    public string advice;
    public int[,] point = new int[3,2];

    public CustomerInfo(JSONNode node)
    {
        int typeLength = node["status"].Count;
        int type = Random.Range(0, typeLength);

        name = node["name"].Value;
        mentalPoint = node["mental_point"].AsInt;
        physicalPoint = node["physical_point"].AsInt;
        want = node["status"][type]["want"].Value;
        need = node["status"][type]["need"].Value;
        sick = node["status"][type]["sick"].Value;
        body = node["status"][type]["body"].Value;
        face = node["status"][type]["face"].Value;
        emote = node["status"][type]["emote"].Value;
        hair = node["status"][type]["hair"].Value;
        script = node["status"][type]["script"].Value;
        advice = node["status"][type]["advice"].Value;

        for (int i = 0; i < 3; i++)
        {
            point[i, 0] = node["status"][type]["point"]["mental_point"].AsInt;
            point[i, 1] = node["status"][type]["point"]["physical_point"].AsInt;
        }
    }
}