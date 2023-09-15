using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkills : MonoBehaviour
{
    Character character;
    [SerializeField]
    public int skillCount1;
    public int skillCount2;
    public int skillCount3;
    public int skillCount4;
    public int skillCount5;
    public int skillCount6;
    // Start is called before the first frame update
    void Awake()
    {
        // Initialize skillCount for each skills
        skillCount1 = 0;
        skillCount2 = 0;
        skillCount3 = 0;
        skillCount4 = 0;
        skillCount5 = 0;
        skillCount6 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
