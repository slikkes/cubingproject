using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class cube
{
    public GameObject vcube;
    //public int orientation;
    public string type;
    public cube(GameObject vc, string t)
    {
        this.vcube = vc;
        this.type = t;
    }
}


public class main : MonoBehaviour {
    public cube[] Cubes=new cube[26];
    public Transform pivot;
    public int rotating = 25;
    public string algorithm = "RUR'U2FD'BL2";
    public char code;
    public int step=-1;
    public bool n, d=false;

	void Start () {
        pivot = GameObject.FindWithTag("pivot").transform;
        CubesInit();


    }
	
	void Update () {
        isRotating();        
    }

    void isRotating()
    {
        
        if (Input.GetKeyDown("right"))
        {
            rotating = 0;
            n = d = false;
            speller();
        }
        if (rotating < 24)
        {
            if (d) { rotate();rotate(); }
            else { rotate(); }
            
        }
        if (rotating == 24)
        {

            foreach (cube element in Cubes)
            {
                element.vcube.transform.parent = null;
            }
            pivot.rotation = Quaternion.identity;
            
            rotating++;
        } else if(rotating == 25)
        {
            
            Debug.Log(algorithm[step + 1]);
        }
        else
        {
            rotating++;
        }

    }

    void speller()
    {
        if (algorithm[step+1].Equals('2'))
        {
            code = algorithm[step];
            d = true;
            step++;
            step++;
        } else if (algorithm[step + 1].Equals('\''))
        {
            code = algorithm[step];
            n = true;
            step++;
            step++;
        }
        else
        {
            code = algorithm[step];
            step++;
        }

        
    }

    void CubesInit()
    {
        for (int i = 0; i < 8; i++)
        {
            Cubes[i] = new cube(GameObject.FindWithTag("corner" + i),"corner");
            
        }
        for (int i = 0; i < 12; i++)
        {
            Cubes[i+8] = new cube(GameObject.FindWithTag("edge" + i), "edge");
            
        }
        for (int i = 0; i < 6; i++)
        {
            Cubes[i+20] = new cube(GameObject.FindWithTag("center" + i), "center");
            
        }
    }

    void rotate()
    {

        foreach (cube element in Cubes)
        {
            
            if (positionSet(element.vcube.transform))
            {
                element.vcube.transform.SetParent(pivot);

            }
        }

        whichAxis();
    }

    public bool positionSet(Transform cube)
    {
        switch (code)
        {
            case 'R':
                if ((int)Math.Round(cube.position.x / 2.05) == 1) { return true; }
                break;
            case 'M':
                if ((int)Math.Round(cube.position.x / 2.05) == 0) { return true; }
                break;
            case 'L':
                if ((int)Math.Round(cube.position.x / 2.05) == -1) { return true; }
                break;
            case 'U':
                if ((int)Math.Round(cube.position.y / 2.05) == 1) { return true; }
                break;
            case 'E':
                if ((int)Math.Round(cube.position.y / 2.05) == 0) { return true; }
                break;
            case 'D':
                if ((int)Math.Round(cube.position.y / 2.05) == -1) { return true; }
                break;
            case 'F':
                if ((int)Math.Round(cube.position.z / 2.05) == -1) { return true; }
                break;
            case 'S':
                if ((int)Math.Round(cube.position.z / 2.05) == 0) { return true; }
                break;
            case 'B':
                if ((int)Math.Round(cube.position.z / 2.05) == 1) { return true; }
                break;
            default: return false;

        }
        return false;
    }

    void whichAxis()
    {
        
        float speed = 3.75f;
        if (n) { speed *= -1; }
        if (code == 'R' || code == 'M') { pivot.transform.Rotate(speed, 0, 0); }
        else if (code == 'L') { pivot.transform.Rotate(-speed, 0, 0); }
        else if (code == 'U' || code == 'E' ) { pivot.transform.Rotate(0, speed, 0); }
        else if (code == 'D') { pivot.transform.Rotate(0, -speed, 0); }
        else if (code == 'F') { pivot.transform.Rotate(0, 0, -speed); }
        else{ pivot.transform.Rotate(0, 0, speed); }
    }
}