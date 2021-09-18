# Dispatcher
[출처](https://github.com/cocodding0723/UnityToolbag/tree/main/Dispatcher)

`Dispatcher` 다른 스레드에서 주 스레드의 코드를 호출 할 수있게 해주는 매우 간단한 `Component/System`입니다. 사용법은 간단합니다.

1. `Dispatcher Component`를 `Scene`에 존재하는 `GameObject`에 추가합니다.
2. 정적 메서드 `InvokeAsync(Action)` 를 호출 하거나 `Invoke(Action)` 메인 스레드에서 작업을 실행합니다.
3. `InvokeAsync`는 작업을 대기열에 넣고 즉시 반환하므로 `스레드가 계속 진행`될 수 있습니다. 반면에 `Invoke` 는 작업이 실행될 때까지 호출 `스레드를 차단`합니다. 두 경우 모두 `Dispatcher` 호출 스레드가 `메인 스레드` 인 경우 즉시 작업을 호출합니다. (이것은 또한 `Invoke` `메인 스레드`에서 호출 하는 경우 `교착 상태를 방지` 합니다).

4. `InvokeAsync` 단순히 작업을 대기열에 추가하기 때문에 쓰레기를 생성하지 않습니다. `Invoke` 현재 주어진 인수를 호출하고 메서드에서 반환 할 시기를 알기 위해 `Bool`값을 설정하는 새 람다를 할당합니다. 
- `경고` : 나중에 실행중 수정될 수 있지만 대부분 `InvokeAsync` 사용을 추천합니다.