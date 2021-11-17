using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    //* The Chesspiece that was tapped to create this MovePlate .
    GameObject reference = null;
    //* Locations on the board .
    int matrixX;
    int matrixY;
    //* Meaning: true - move and attack, false - move. 
    public bool attack = false;
    public void Start()
    {
        if (attack)
        {
            //* To attack set the color of the MovePlate to red (which is (255, 0, 0, 255)  or  (1.0f, 0.0f, 0.0f, 1.0f) in float .
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        //* Destroy enemy chesspiece .
        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");

            Destroy(cp);
        }
        //* Set the chesspiece's original location to be empty .
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());
        //* Move reference chesspiece to this position .
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();
        //* Update the matrix .
        controller.GetComponent<Game>().SetPosition(reference);
        //* Switch Current Player .
        controller.GetComponent<Game>().NextTurn();
        //* Destroy the move plates including self.
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }
    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }
    public GameObject GetReference()
    {
        return reference;
    }
}




