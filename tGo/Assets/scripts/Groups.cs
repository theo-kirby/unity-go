using UnityEngine;

public class Groups : MonoBehaviour
{

    public int test;
    public int groupCount = 0;
    public Stone[][] stoneGroups;

    public void addGroup(Stone[] stones)
    {
        this.stoneGroups[groupCount] = stones;
        this.groupCount++;
    }
}
