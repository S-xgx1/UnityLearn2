#region

using System;
using Cysharp.Threading.Tasks;
using PunctualSolutionsTool.Tool;
using UnityEngine;

#endregion

public class CallAndroidAPI : MonoBehaviour
{
    const string ClassName                   = "android/app/Instrumentation";
    const string MethodName                  = "sendPointerSync";
    const string MethodSignature             = "(Landroid/view/MotionEvent;)V";
    const string SystemClassName             = "java/lang/System";
    const string CurrentTimeMillisMethodName = "currentTimeMillis";

    static AndroidJavaObject instrumentation;

    void Start()
    {
        // 获取Instrumentation对象
        using var classJavaObject = new AndroidJavaClass(ClassName);
        instrumentation = classJavaObject.CallStatic<AndroidJavaObject>("newInstrumentation");
        In().Forget();
        return;

        async UniTaskVoid In()
        {
            await 5.Delay();
            SimulateTouch(Screen.width / 2f, Screen.height / 2f);
        }
    }

    static void SimulateTouch(float x, float y)
    {
        using var motionEventClass = new AndroidJavaClass("android/view/MotionEvent");
        using var javaSystem       = new AndroidJavaClass(SystemClassName);
        var       uptimeMillis     = javaSystem.CallStatic<long>(CurrentTimeMillisMethodName);

        // 创建ACTION_DOWN事件
        var downEvent = motionEventClass.CallStatic<AndroidJavaObject>(
                "obtain",
                uptimeMillis,
                uptimeMillis,
                0,
                x,
                y,
                0
        );

        // 创建ACTION_UP事件
        var upEvent = motionEventClass.CallStatic<AndroidJavaObject>(
                "obtain",
                uptimeMillis,
                uptimeMillis,
                1,
                x,
                y,
                0
        );
        var javaValue = new jvalue
        {
                l = AndroidJNI.NewObject(downEvent.GetRawClass(), downEvent.GetRawObject(), Span<jvalue>.Empty),
        };
        // 调用sendPointerSync发送ACTION_DOWN事件
        AndroidJNI.CallStaticVoidMethod(
                instrumentation.GetRawClass(),
                AndroidJNI.GetStaticMethodID(instrumentation.GetRawClass(), MethodName, MethodSignature),
                new[] { javaValue }
        );
        var javaValue2 = new jvalue
        {
                l = AndroidJNI.NewObject(upEvent.GetRawClass(), upEvent.GetRawObject(), Span<jvalue>.Empty),
        };
        // 调用sendPointerSync发送ACTION_UP事件
        AndroidJNI.CallStaticVoidMethod(
                instrumentation.GetRawClass(),
                AndroidJNI.GetStaticMethodID(instrumentation.GetRawClass(), MethodName, MethodSignature),
                new[] { javaValue2 }
        );
    }
}