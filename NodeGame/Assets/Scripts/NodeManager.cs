using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    public GameObject NodePrefab;
    public List<Node> Nodes;

	bool MouseUp = true;
	bool MovingNode = false;
	bool Second = false;
	Node NodeBeingMoved = null;

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
			MouseUp = true;
			NodeBeingMoved = null;
		}
        else if (Input.GetMouseButton(0) && MouseUp)
		{
			MouseUp = false;
			for (int i = 0; i < Nodes.Count; i++)
			{
				if (Nodes[i].isPressed)
				{
					MovingNode = true;
					NodeBeingMoved = Nodes[i];
				}
			}
		}
		else if (MovingNode && NodeBeingMoved)
		{
			NodeBeingMoved.UpdateNode();
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


			Color newCol;

			if (ClosestNode.NodeColor == new Color(0.2f, 0.7f, 0.2f))
				newCol = new Color(0.7f, 0.2f, 0.2f);
			else
				newCol = new Color(0.2f, 0.7f, 0.2f);

			NewNode.NodeColor = newCol;

			NewNode.createConnection();
		}
		else
			NewNode.NodeColor = new Color(0.7f, 0.2f, 0.2f);

		Nodes.Add(NewNode);
    }
}
