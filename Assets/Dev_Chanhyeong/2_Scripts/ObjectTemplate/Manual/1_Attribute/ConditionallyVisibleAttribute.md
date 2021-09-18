# ConditionallyVisibleAttribute
[출처](https://github.com/cocodding0723/UnityToolbag/tree/main/ConditionallyVisiblePropertyDrawer)

이 [PropertyDrawer](https://docs.unity3d.com/ScriptReference/PropertyDrawer.html) 는 프로퍼티에 `true`값이 없으면 해당 프로퍼티를 숨기는 도우미입니다.   

Example
```cs
class MyBehavior : MonoBehavior
{
    public bool showSomeValue;

    [ConditionallyVisible(nameof(showSomeValue))]
    public float someValue;
}
```  

인스펙터창에 `showSomeValue` 체크박스를 표시하고 사용자가 체크박스를 선택한 경우에만 프로퍼티 값을 표시합니다. 
 
이 기능은 리스트를 사용중이거나 값을 전환할 때만 적용되는 필드가 있는 경우 유용합니다.

![./example.gif](https://github.com/cocodding0723/UnityToolbag/raw/main/ConditionallyVisiblePropertyDrawer/example.gif)