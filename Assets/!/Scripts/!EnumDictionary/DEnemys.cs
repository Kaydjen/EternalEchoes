using System.Collections.Generic;
using UnityEngine;

public static class DEnemys
{
    public static readonly Dictionary<EEnemys, Pool<Transform>> List = new()
    {
        { EEnemys.Tir1, EnemyPoolTir1.Instance },
        { EEnemys.Tir2, EnemyPoolTir2.Instance },
        { EEnemys.Tir3, EnemyPoolTir3.Instance },
        { EEnemys.Tir4, EnemyPoolTir4.Instance }
    };
}
