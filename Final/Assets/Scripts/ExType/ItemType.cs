using System;
using UnityEngine;

public enum ItemType
{
    // 공격 업그레이드, 획득 즉시 무기가 단계별로 업그레이드 된다. (1단계에서 5단계까지 강화)
    WeaponLevelUp,
    // 무적 상태, 획득 즉시 적의 공격으로 받는 피해량이 0이 된다. (3초간 지속)
    NullityBuff,
    // 체력 회복, 획득 즉시 체력을 회복한다. (20 만큼 회복)
    PlayerHeal,
    // 고통 감소, 획득 즉시 고통을 감소시킨다. (20 만큼 감소)
    StageHeal,
    // 추가 점수, 획득 즉시 추가 점수를 획득한다. (500 만큼 추가)
    GetScore,
    // 특수 공격, 획득 즉시 특수 공격을 가한다. (방사형 총알 발사)
    Boom,
    
}

[Serializable]
public struct ItemInfo
{
    public Sprite sprite;
    public string name;
    public string description;
}