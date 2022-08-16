using UnityEngine;

public class CheatPlayer : MonoBehaviour
{
    private void Start()
    {
        CheatManager.Instance.AddCheat(KeyCode.F2, Test);
        CheatManager.Instance.AddCheat(KeyCode.Alpha1, Test1);
        CheatManager.Instance.AddCheat(KeyCode.Slash, Test2);
    }

    private void Test() => print("Cheat Test: F2");
    private void Test1() => print("Cheat Test: 1");
    private void Test2() => print("Cheat Test: /");
}
