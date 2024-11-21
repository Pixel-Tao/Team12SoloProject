# Team12SoloProject
Team12SoloProject

# 주특기 심화 개인 과제
 - 도전 관제에 대한 구현은 되어 있지 않습니다.

# 프로젝트 아키텍쳐 설계
 - 프로젝트를 진행하는데 필요한 아키텍쳐를 설계했습니다.

### resources_base 핵심 포인트 
  - Resources/Execls : 엑셀 파일을 작성하면 unity-excel-importer가 자동으로 ScriptableObject로 변환해줍니다.
    (엑셀 파일 단위는 하나의 Entity로 보고 Sheet 단위로 관계를 맺어 데이터를 관리합니다.)
  - Resources/SO/Sheets : 엑셀에서 변환한 ScriptableObject가 저장되는 폴더 입니다. 폴더는 우클릭하여 Create > Convert to DataList 하게 되면 DBManager에서 캐싱할 수 있는 구조로 다시 변환합니다.
  - Resources/SO/DataList : DBManager에서 캐싱 할때 필요한 데이터 파일들을 저장합니다.
  - ScriptableObjects/Sheets : 엑셀파일을 SO로 변환하는데 필요한 class 입니다.
  - ScriptableObjects/DataList : DBManager에서 캐싱 할때 필요한 데이터 class 입니다.
  - UI : UI의 Prefab을 제외한 데이터 연동 및 이벤트 연동부분을 모드 동적으로 처리되도록 설계 했습니다.
  - Sound : AudioMixer를 사용하여 사운드를 관리하고 SFX의 경우 Pooling 하여 리소스를 사용합니다.
  - ObjectPool : PoolManager에서 거의 모든 오브젝트들을 Pool로 관리 할 수 있도록 했습니다.
  - 기존 Manager들을 Managers 라는 하나의 Singleton으로 관리하도록 했습니다.

### addressabes_base 핵심 포인트 (개발중)
  - 모든 리소스는 addressabes에 관리하에 작동 됩니다.
  - Runtime에 Live로 LoadAsync 하여 리소스를 가져오지 않고 어플리케이션 시작 시 리소스를 다운로드 하고 캐싱하는 작업이 선행 됩니다.
  - @Resources/Execls : 엑셀 파일을 작성하면 unity-excel-importer가 자동으로 ScriptableObject로 변환해줍니다.
    (엑셀 파일 단위는 하나의 Entity로 보고 Sheet 단위로 관계를 맺어 데이터를 관리합니다.)
  - @Resources/SO/Sheets : 엑셀에서 변환한 ScriptableObject가 저장되는 폴더 입니다. 폴더는 우클릭하여 Create > Convert to DataList 하게 되면 DBManager에서 캐싱할 수 있는 구조로 다시 변환합니다.
  - @Resources/SO/DataList : DBManager에서 캐싱 할때 필요한 데이터 파일들을 저장합니다.
  - ScriptableObjects/Sheets : 엑셀파일을 SO로 변환하는데 필요한 class 입니다.
  - ScriptableObjects/DataList : DBManager에서 캐싱 할때 필요한 데이터 class 입니다.
  - UI : UI의 Prefab을 제외한 데이터 연동 및 이벤트 연동부분을 모드 동적으로 처리되도록 설계 했습니다.
  - Sound : AudioMixer를 사용하여 사운드를 관리하고 SFX의 경우 Pooling 하여 리소스를 사용합니다.
  - ObjectPool : PoolManager에서 거의 모든 오브젝트들을 Pool로 관리 할 수 있도록 했습니다.
  - 기존 Manager들을 Managers 라는 하나의 Singleton으로 관리하도록 했습니다.
