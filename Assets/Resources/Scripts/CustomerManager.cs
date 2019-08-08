using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class CustomerManager : MonoBehaviour
{
    private JSONNode m_node;

    public Customer prefabCustomer;

    public Vector3 createCustomerPosition;
    public Vector3 startZonePosition;
    public Vector3 endZonePosition;

    private void Start()
    {
        Init();
        VisitCustomer();
    }

    private void Init()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/customers");
        string jsonString = textAsset.text;
        m_node = JSON.Parse(jsonString);
    }

    private void VisitCustomer()
    {
        int customerLength = m_node.Count;
        int customerNum = Random.Range(0, 3) + 1;

        for (int i = 0; i < customerNum; i++)
        {
            int id = Random.Range(0, customerLength);
            float x = (Mathf.Abs(startZonePosition.x) + Mathf.Abs(endZonePosition.x)) / (customerNum + 1) * (i + 1);
            Vector3 position = startZonePosition;
            position.x += x;

            Customer customer = Instantiate<Customer>(prefabCustomer);
            customer.Init(new CustomerInfo(m_node[id]));
            customer.transform.position = createCustomerPosition;
            customer.MoveToPosition(position, 1.0f, 1.0f * i);
        }
    }
}
