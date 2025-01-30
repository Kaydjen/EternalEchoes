using UnityEngine;

namespace ProceduralGeneration.SeriazableObjects
{
    [CreateAssetMenu(fileName = "NewDungeon", menuName = "ProcedualGeneration/Dungeon")]
    public class SeriazableDungeon : SeriazableLocation
    {
        [Space(4)]
        [TextArea(1, 50)]

        public string layout =
            "■■□■■" + "\n" +
            "□■■■□" + "\n" +
            "□□■□□" + "\n" +
            "□■■■□" + "\n" +
            "■■□■■";
    }
}
