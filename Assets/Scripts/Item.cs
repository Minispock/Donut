using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    [SerializeField]
    private ItemType type;
    public ItemType Type { get { return type; } }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.Is<Donut>())
        {
            Donut.Instance.PickUp(this);
        }

    }
}

public enum ItemType
{
    Bad,
    Good
}
