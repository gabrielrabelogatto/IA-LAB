using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject sprite_do_player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sprite_do_player.transform.position += new Vector3(5.0f * Time.deltaTime, 0, 0);
    }
}
