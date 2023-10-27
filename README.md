# Intro
* Unity 2021.3.30f1 URP 버전으로 만들어 졌습니다.
* 유니티에 내장된 [AI Navigation](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UnityEngine.AIModule.html)이 아닌 [Navmesh Components](https://github.com/Unity-Technologies/NavMeshComponents.git#package)가 적용되어 있습니다.
    * Packages/manifest.json 파일 내에 다음과 같은 코드를 작성하여 추가합니다.
  ```json
  "com.unity.ai.navigation.components": "https://github.com/Unity-Technologies/NavMeshComponents.git#package"
  ```
* [unity-chan SSU]()캐릭터 사용하기 위해 Unity-chan Toon Shader : [UTS](com.unity.toonshader) 패키지를 설치합니다.
  * 셰이더 적용을 위해 `URP-HighFidelity-Renderer`설정의 `Depth Priming Mode`를 `Disabled`로 변경
* 위와 마찬가지로 [Unity-chan Spring Bone](https://github.com/unity3d-jp/UnityChanSpringBone.git) 패키지를 설치합니다.
<br/>
<br/>

# NavMesh Surface
* Window - AI - Navigation - Bake 탭을 사용하지 않습니다.
* NavMesh Surface Component를 사용하여 Bake 합니다.
* 필요에 따라 NavMeshModifier Component를 사용하여, Override Area탭의 추가 설정을 변경합니다.
<br/>
<br/>

# NavMesh Link
* NavMeshLink Component를 사용하여, 연결되지 않은 NavMesh 오브젝트 사이를 연결합니다.
* `NavMesh Link`로 연결된 NavMesh 사이의 움직임 보간을 위해 [AgentLinkMover](https://github.com/Unity-Technologies/NavMeshComponents/blob/master/Assets/Examples/Scripts/AgentLinkMover.cs)를 사용합니다.

# Used Assets
* [Unity-Chan! Sunny Side Up(URP)](https://github.com/unity3d-jp/UnityChanSSU/releases/download/1.0.5/UnityChanSSU_URP-release-1.0.5.zip)
* [Unity-Chan 3D Model Data](https://unity-chan.com/download/download.php?id=UnityChan&v=1.4.0)








