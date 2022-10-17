using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipToeLogic : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    private readonly int width = 10;
    private readonly int depth = 13;
    private readonly float gap = 3;

    private bool[,] paths = new bool[13,10];

    // Start is called before the first frame update
    void Start()
    {
        platformPrefab.transform.localScale = new Vector3(2.9f, 0.2f, 2.9f);
        for (int i = 0; i < depth; i++)
        {
            Debug.Log(i);   
            if (i <= 5)
            {
                paths[i, 3] = true;
            } 
            if(i >= 5)
            {
                paths[i, 4] = true;
            }
            
        }

        for (int i = 0; i < depth; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Debug.Log("wtf");
                GameObject gameObject = Instantiate(platformPrefab, new Vector3(-13.52f + (gap * j), 0, 10 + (gap * i)), Quaternion.identity);
                // https://docs.unity3d.com/ScriptReference/GameObject.AddComponent.html
                gameObject.AddComponent(typeof(BoxCollider));
                gameObject.AddComponent(typeof(TipToePlatform));
                if (paths[i, j])
                {
                    gameObject.GetComponent<TipToePlatform>().isPath = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
