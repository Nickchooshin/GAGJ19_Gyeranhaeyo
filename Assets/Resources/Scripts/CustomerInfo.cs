﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class CustomerInfo
{
    public string name;
    public int mentalPoint;
    public int physicalPoint;
    private string[] want;
    private string[] need;
    private string[] sick;
    private string[] body;
    private string[] face;
    private string[] emote;
    private string[] hair;
    private string[] hair2;
    private string[] script;
    private string[] advice;
    private int[,,] point;
    private int m_type = 0;
    private int m_maximumType = 0;
    public bool isVisit = true;
    private bool m_isWarning = false;
    private bool m_isWarningOnce = true;

    public string Want { get { return want[m_type]; } }
    public string Need { get { return need[m_type]; } }
    public string Sick { get { return sick[m_type]; } }
    public string Body { get { return body[m_type]; } }
    public string Face { get { return face[m_type]; } }
    public string Emote { get { return emote[m_type]; } }
    public string Hair { get { return hair[m_type]; } }
    public string Hair2 { get { return hair2[m_type]; } }
    public string Script { get { return script[m_type]; } }
    public string Advice { get { return advice[m_type]; } }

    public CustomerInfo(JSONNode node)
    {
        int typeCount = node["status"].Count;

        name = node["name"].Value;
        mentalPoint = node["mental_point"].AsInt;
        physicalPoint = node["physical_point"].AsInt;
        m_maximumType = node["status"].Count;

        want = new string[typeCount];
        need = new string[typeCount];
        sick = new string[typeCount];
        body = new string[typeCount];
        face = new string[typeCount];
        emote = new string[typeCount];
        hair = new string[typeCount];
        hair2 = new string[typeCount];
        script = new string[typeCount];
        advice = new string[typeCount];
        point = new int[typeCount, 3, 2];

        for (int i = 0; i < typeCount; i++)
        {
            JSONNode status = node["status"][i];
            want[i] = status["want"].Value;
            need[i] = status["need"].Value;
            sick[i] = status["sick"].Value;
            body[i] = status["body"].Value;
            face[i] = status["face"].Value;
            emote[i] = status["emote"].Value;
            hair[i] = status["hair"].Value;
            hair2[i] = status["hair2"].Value;
            script[i] = status["script"].Value;
            advice[i] = status["advice"].Value;

            for (int j = 0; j < 3; j++)
            {
                point[i, j, 0] = status["mental_point"][j].AsInt;
                point[i, j, 1] = status["physical_point"][j].AsInt;
            }
        }
    }

    public void SetType(int type)
    {
        m_type = type;
    }

    public void SetRandomType()
    {
        m_type = Random.Range(0, m_maximumType);
    }

    public void WantReview()
    {
        mentalPoint += point[m_type, 0, 0];
        physicalPoint += point[m_type, 0, 1];
        CheckPoint();
    }

    public void NeedReview()
    {
        mentalPoint += point[m_type, 1, 0];
        physicalPoint += point[m_type, 1, 1];
        CheckPoint();
    }

    public void OtherReview()
    {
        mentalPoint += point[m_type, 2, 0];
        physicalPoint += point[m_type, 2, 1];
        CheckPoint();
    }

    private void CheckPoint()
    {
        if (mentalPoint <= 0 || physicalPoint <= 0)
            isVisit = false;

        if (!m_isWarning)
        {
            if (mentalPoint <= 3 || physicalPoint <= 3)
                m_isWarning = true;
        }
        else
        {
            if (mentalPoint > 3 && physicalPoint > 3)
            {
                m_isWarning = false;
                m_isWarningOnce = true;
            }
        }
    }

    public bool IsWarning()
    {
        if (m_isWarning && m_isWarningOnce)
        {
            m_isWarningOnce = false;
            return true;
        }

        return false;
    }
}