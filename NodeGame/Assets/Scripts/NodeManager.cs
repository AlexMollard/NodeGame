using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    public GameObject NodePrefab;
    public List<Node> Nodes;

    void Start()
    {
        Nodes = new List<Node>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z += 10.0f;
            screenPoint = Camera.main.ScreenToWorldPoint(screenPoint);

            int total = 0;
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Vector3.Distance(Nodes[i].transform.position, screenPoint) < 0.5f)
                {
                    break;
                }
                total++;
            }

            if (total == Nodes.Count)
            {
                CreateNode(screenPoint);
            }
        }
    }



    void CreateNode(Vector3 screenPoint)
    {
        GameObject newNodeObject = Instantiate(NodePrefab, screenPoint, Quaternion.identity);
        newNodeObject.transform.parent = this.transform;
        newNodeObject.name = "Node: " + screenPoint;
        Node NewNode = newNodeObject.GetComponent<Node>();

        if (Nodes.Count > 0)
        {
            float ClosestDistance = float.MaxValue;
            Node ClosestNode = null;
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Vector2.Distance(Nodes[i].transform.position, NewNode.transform.position) < ClosestDistance)
                {
                    ClosestDistance = Vector2.Distance(Nodes[i].transform.position, NewNode.transform.position);
                    ClosestNode = Nodes[i];
                }
            }

            NewNode.AddConnection(ClosestNode);
            ClosestNode.AddConnection(NewNode);

            NewNode.createConnection();
        }
        Nodes.Add(NewNode);
    }
}
