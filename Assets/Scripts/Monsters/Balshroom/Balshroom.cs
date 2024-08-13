using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balshroom : Monster
{
    [SerializeField]GameObject redMushroom;
    [SerializeField]GameObject blueMushroom;
    List<GameObject> mushrooms = new List<GameObject>();

    private void Awake()
    {
        MAX_HP = 43;
        hp = MAX_HP;
        encounterText = "그 버섯에게 접근할 때는 항상 신중해야 한다. 적어도 저건 식용이 아니다.";
    }

    public GameObject CreateRedMushroom(Transform position)
    {
        GameObject red = Instantiate(redMushroom);
        mushrooms.Add(red);
        red.transform.position = position.position + new Vector3(Random.Range(0.5f, 1f) -1, 0.3f, 0);
        return redMushroom;
    }
    public GameObject CreateBlueMushroom(Transform position)
    {
        GameObject blue = Instantiate(blueMushroom);
        mushrooms.Add(blue);
        blue.transform.position = position.position + new Vector3(Random.Range(0.5f, 1f) - 1, 0.3f, 0);
        return blue;
    }
    private void OnDestroy()
    {
        mushrooms.ForEach((item) => { Destroy(item); });
    }
}
