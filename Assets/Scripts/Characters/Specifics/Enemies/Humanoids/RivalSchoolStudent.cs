using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalSchoolStudent : CharacterGeneric {

    /*
     * This code is for the Zero Form of the Rival School Student.
     * This script is a child of CharacterGeneric.
     */

    CharacterStats RivalSchoolStudentStats;

    //Update is called once per frame
    void Update()
    {

    }

    /*
     * Specific to each character; Setting up the stats
     * for each character.
     */

    protected override void CharacterSetup()
    {
        base.CharacterSetup();
        RivalSchoolStudentStats = new CharacterStats("RivalSchoolStudent", gameObject.tag, 70, 30, 8, 10, 7, 5, 0, 60, 0, 0);

    }

    //return all character stats for the Rival School Student
    public CharacterStats ReturnStats
    {
        get { return RivalSchoolStudentStats; }
    }
}
