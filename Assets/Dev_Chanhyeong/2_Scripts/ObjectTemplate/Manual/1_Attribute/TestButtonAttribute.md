# ProgressBarAttribute
[출처](hhttps://gist.github.com/LotteMakesStuff/dd785ff49b2a5048bb60333a6a125187)

이 [PropertyDrawer](https://docs.unity3d.com/ScriptReference/PropertyDrawer.html) 는 인스펙터 창에 `Test Button`을 띄우는 도우미입니다.   

Example
```cs
using UnityEngine;
using System.Collections;

public class InspectorButtonsTest : MonoBehaviour
{
    [TestButton("Generate world", "DoProcGen", isActiveInEditor = false)]
    [TestButton("Clear world", "ClearWorld", 2, isActiveInEditor = false)]
    [ProgressBar(hideWhenZero = true, label = "procGenFeedback")]
    public float procgenProgress = -1;

    [HideInInspector]
    public string procGenFeedback;

    // Silly little enumerator to test progress bars~
    IEnumerator DoProcGen()
    {
        // lets pretend we have some code here that procedurally generates a map
        procGenFeedback = "Initilizing";
        procgenProgress = 0.01f;
        yield return new WaitForSeconds(0.25f);
        procGenFeedback = "Seeding terrain";
        procgenProgress = 0.2f;
        yield return new WaitForSeconds(0.25f);
        procGenFeedback = "Plotting cities";
        procgenProgress = 0.4f;
        yield return new WaitForSeconds(0.25f);
        procGenFeedback = "Drawing roads";
        procgenProgress = 0.6f;
        yield return new WaitForSeconds(0.25f);
        procGenFeedback = "Reticulating splines";
        procgenProgress = 0.8f;
        yield return new WaitForSeconds(0.25f);
        procGenFeedback = "Finalizing";
        procgenProgress = 0.9f;
        yield return new WaitForSeconds(0.25f);
        procGenFeedback = "DONE in 6 seconds";
        procgenProgress = 1f;
    }

    // resets values
    void ClearWorld()
    {
        procgenProgress = 0;
    }
}
```  

인스펙터창에 `Test Button` 을 표시하고 `Attribute`로 지정한 `함수`, `코루틴` 을 인스펙터 창에 존재하는 버튼을 누를 경우 실행합니다.  

단, 선택한 오브젝트만 실행하며 매개변수는 들어갈 수 없습니다.
 
이 기능은 오브젝트를 생성, 테스트 할 경우 유용합니다.

![./example.gif](https://gist.github.com/LotteMakesStuff/dd785ff49b2a5048bb60333a6a125187/raw/b3f1633db509027782ac0d626c7db07e76177c08/demo.gif)