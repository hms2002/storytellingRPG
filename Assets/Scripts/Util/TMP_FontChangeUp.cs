using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Modules.Util
{
    public class TMP_FontChangeUp : MonoBehaviour
    {
        public const string PATH_FONT_TEXTMESHPRO = "Assets/TextMesh Pro/Resources/Fonts & Materials/JunnamYuna OTF Regular SDF.asset";
        [MenuItem("CustomMenu/Text/ChangeAllTMPFont(텍스트 폰트를 변경함)")]
        public static void ChangeAllTMPFont()
        {
            GameObject[] rootObj = SceneManager.GetActiveScene().GetRootGameObjects();

            for(int i = 0; i < rootObj.Length; i++)
            {
                GameObject gbj = (GameObject)rootObj[i] as GameObject;
                Component[] com = gbj.transform.GetComponentsInChildren(typeof(TextMeshProUGUI), true);
                foreach(TextMeshProUGUI tmp in com)
                {
                    tmp.font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(PATH_FONT_TEXTMESHPRO);
                }
            }
        }
    }
}
