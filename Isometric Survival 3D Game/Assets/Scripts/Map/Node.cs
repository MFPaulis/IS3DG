using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool walkable;
    public int x;
    public int z;

    public int G;
    public int H;
    public int F;

    public Node cameFromNode;

    private CharacterMovement character;

    private void Start()
    {
        character = FindObjectOfType<CharacterMovement>();
    }

    public override bool Equals(object other)
    {
        if((other == null) || !this.GetType().Equals(other.GetType()))
        {
            return false;
        } else
        {
            return x == ((Node)other).x && z == ((Node)other).z;
        }
    }

    public override string ToString()
    {
        return x + "," + z;
    }

    public Vector2 toVector()
    {
        Vector2 vector = new Vector2(x, z);
        return vector;
    }

    public override int GetHashCode()
    {
        return 0;
    }

    private void OnMouseUpAsButton()
    {
        character.MoveToPoint(x, z);
    }

}
