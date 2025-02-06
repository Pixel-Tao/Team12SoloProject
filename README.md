# Team12SoloProject
Team12SoloProject

# 프로젝트 아키텍쳐 설계
 - 프로젝트를 진행하는데 필요한 아키텍쳐를 설계했습니다.

### addressabes_base 핵심 포인트
 - 모든 리소스는 addressabes에 관리하에 작동 됩니다.
 - Runtime에 Live로 LoadAsync 하여 리소스를 가져오지 않고 어플리케이션 시작 시 리소스를 다운로드 하고 캐싱하는 작업이 선행 됩니다.
 - Execls : 엑셀 파일을 작성하면 [unity-excel-to-json](https://github.com/Pixel-Tao/unity-excel-to-json) 툴을 사용하여 json 형식의 파일로 변환해 줍니다.
 - UI : UI의 Prefab을 제외한 데이터 연동 및 이벤트 연동부분을 모드 동적으로 처리되도록 설계 했습니다.
 - Sound : AudioMixer를 사용하여 사운드를 관리하고 SFX의 경우 Pooling 하여 리소스를 사용합니다.
 - ObjectPool : PoolManager에서 거의 모든 오브젝트들을 Pool로 관리 할 수 있도록 했습니다.
 - 기존 Manager들을 Managers 라는 하나의 Singleton으로 관리하도록 했습니다.
 - SafeArea : 노치 및 펀치 홀 영역에 대응 할 수 있는 스크립트 입니다.
 - BTPathFinder : 길 찾기 알고리즘 스크립트 입니다.
 - ResourceDownloader : 어드레서블을 별도의 서버에 저장 했을 때 사용 할 수 있는 다운로더 스크립트 입니다.
