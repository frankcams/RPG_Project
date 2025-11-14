using UnityEngine;
using System.Collections.Generic;

public class PartyManager : MonoBehaviour
{
    public List<CharacterStats> partyMembers = new List<CharacterStats>();
    public int activeMemberIndex = 0;

    public CharacterStats ActiveMember => partyMembers[activeMemberIndex];

    void Start()
    {
        Debug.Log($"Active member: {ActiveMember.characterName}");
    }

    public void AddMember(CharacterStats newMember)
    {
        if (!partyMembers.Contains(newMember))
        {
            partyMembers.Add(newMember);
            Debug.Log($"Added {newMember.characterName} to the party.");
        }
    }

    public void RemoveMember(CharacterStats member)
    {
        if (partyMembers.Contains(member))
        {
            partyMembers.Remove(member);
            Debug.Log($"Removed {member.characterName} from the party.");
        }
    }

    public void SwitchToNextMember()
    {
        activeMemberIndex = (activeMemberIndex + 1) % partyMembers.Count;
        Debug.Log($"Switched to: {ActiveMember.characterName}");
    }

    public void SwitchToMember(int index)
    {
        if (index >= 0 && index < partyMembers.Count)
        {
            activeMemberIndex = index;
            Debug.Log($"Switched to: {ActiveMember.characterName}");
        }
    }

    public void UseSkillOnTarget(Skill skill, CharacterStats target)
    {
        if (ActiveMember.IsDead())
        {
            Debug.Log($"{ActiveMember.characterName} is dead and cannot act.");
            return;
        }

        ActiveMember.UseSkill(skill);
        // You can add targeting logic here (e.g., apply damage to target)
    }
}