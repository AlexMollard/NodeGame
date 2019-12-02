using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> ConnectedNodes;
    public LineRenderer Line;
    public bool CanGive = false;
    public bool isPressed = false;
	public Color NodeColor = Color.white;
	public Renderer renderer;
    // Start is called before the first frame update
    void Awake()
    {
        ConnectedNodes = new List<Node>();
    }
	private void Start()
	{
		renderer.material.color = NodeColor;
	}
	List<Node> GetConnectedNodes()
    {
        return null;
    }
    public void AddConnection(Node newNode)
    {
        ConnectedNodes.Add(newNode);
    }
    public void createConnection()
    {
        List<Vector3> poses = new List<Vector3>();
        poses.Add(transform.position);
        for (int i = 0; i < ConnectedNodes.Count; i++)
        {
            poses.Add(ConnectedNodes[i].transform.position);
			poses[i] = new Vector3(poses[i].x, poses[i].y, 0.1f);
        }
        Vector3[] newPostitions = poses.ToArray();
		Line.startColor = NodeColor;
		Line.endColor = ConnectedNodes[0].NodeColor;
        Line.SetPositions(newPostitions);
    }
    public void UpdateNode()
    {
        if (isPressed)
        {
            Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(MousePosition);
            transform.position = objPosition;
            createConnection();
            for (int i = 0; i < ConnectedNodes.Count; i++)
            {
                ConnectedNodes[i].createConnection();
            }
        }
    }
    private void OnMouseDown()
    {
        isPressed = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
    }
}
