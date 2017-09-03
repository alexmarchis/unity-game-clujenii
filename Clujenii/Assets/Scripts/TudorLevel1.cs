using UnityEngine;
using System.Collections;

public class TudorLevel1 : Level
{
    private Dummy dummy;
    void Awake()
    {
        var dummyObject = GameObject.Find("Dummy");
        dummy = dummyObject.GetComponent<Dummy>();
        base.Awake();
    }
    public override void EndGame()
    {
        if (dummy.IsCured())
        {
            base.EndGame();
        }
    }
}
