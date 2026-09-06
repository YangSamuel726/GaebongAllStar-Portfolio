<h1 align="center">GaebongAllStar-Portfolio</h1>

<p align="center"><img src="GBS Images/ApplicationFrameHost_niz8BniqSh.png" width="300"> <img src="GBS Images/explorer_hl48loQzOD.png" width="300"> </p>

<p align="center">
  <a href="https://store.steampowered.com/app/3406920/GaebongAllStar/">
    <img src="https://img.shields.io/badge/Steam-Store-000000?logo=steam&style=for-the-badge&logoColor=white" alt="Steam">
  </a>
  <a href="https://youtu.be/GJ9Nbg19cho">
    <img src="https://img.shields.io/badge/YouTube-Trailer-FF0000?logo=youtube&style=for-the-badge&logoColor=white" alt="Trailer">
  </a>
</p>
<br>
  
---
## 1. 게임 소개
GaebongAllStar는 2~10인이 함께 즐기는 실시간 온라인 PvP 액션 게임입니다.
각자의 캐릭터와 아이템, 맵을 활용해서 적을 모두 쓰러뜨리는 것을 목표로 하는 게임입니다.


|||
|:---:|---|
|개발 기간|2024.06 ~ 2026.07|
|플랫폼|PC · Steam(출시 완료)|
|장르|멀티플레이 · 2D 액션|

---

## 2. 팀 · 역할
- 개발 팀 : GoldTongs (인디게임 개발 팀)
- 팀 구성 : 3명(**개발자1**, 아티스트2)
- 담당 역할 : Unity 클라이언트 개발 전담<br>
&emsp; - Photon Fusion 기반 로비 · 인게임 네트워크 <br>
&emsp; - 캐릭터 전투와 상태 시스템<br>
&emsp; - Steamworks · PlayFab 연동<br>
&emsp; - UI · Animation

---

## 3. Tech Stack
![Static Badge](https://img.shields.io/badge/C%23-512BD4?style=for-the-badge&logo=C%23&logoColor=white&labelColor=512BD4&color=512BD4)
![Static Badge](https://img.shields.io/badge/Unity-black?style=for-the-badge&logo=Unity&logoColor=black&labelColor=white&color=white)
![Static Badge](https://img.shields.io/badge/Photon_Fusion-blue?style=for-the-badge&logo=Photon)
![Static Badge](https://img.shields.io/badge/GitHub-gray?style=for-the-badge&logo=GitHub)
![Static Badge](https://img.shields.io/badge/Steamworks-white?style=for-the-badge&logo=Steam&logoColor=white&color=black)
![Static Badge](https://img.shields.io/badge/Playfab-orange?style=for-the-badge)

---
## 4. 핵심 기술 경험
<a href="https://youtu.be/d4LPapnxcG4">
  <img
    src="https://img.youtube.com/vi/d4LPapnxcG4/hqdefault.jpg"
    alt="투사체 비교 영상"
    width="400"
  />
</a>

#### 01. 계층형 네트워크 태그 시스템
> 상태 추가에 따른 조건문 수정과 중첩 상태의 조기 해제 문제를 계층형 태그와 활성화 횟수 동기화로 해결하여, 상태 확장 시 수정 범위를 줄이고 중첩 해제를 안전하게 처리했습니다.

**[상세 내용 보기 →](<01. 계층형 네트워크 태그 시스템>)**
<br></br>

#### 02. 클라이언트 예측을 활용한 투사체 시각 반응 지연 개선
> 서버 응답 대기로 발생하는 투사체 표시 지연에 클라이언트 예측을 적용하여, 지연 환경에서 평균 표시 준비 시간을 221.55ms에서 21.68ms로 약 90.2% 단축했습니다.

**[상세 내용 보기 →](<02. 클라이언트 예측을 활용한 투사체 시각 반응 지연 개선>)**
<br></br>

#### 03. NetworkBehaviourId를 이용한 SFX·VFX 추적 대상 동기화
> 시작 좌표만으로 이동 대상을 따라갈 수 없던 이펙트에 대상 ID 조회와 부모 연결을 적용하여, 각 클라이언트에서 동일한 캐릭터나 무기를 추적하도록 구현했습니다.

**[상세 내용 보기 →](<03. NetworkBehaviourId를 이용한 SFX·VFX 추적 대상 동기화>)**
<br></br>

#### 04. VFX 교체 지점 표준화를 통한 아티스트 테스트 과정 개선
> 분산된 VFX 연결 위치로 인해 개발자 안내가 필요했던 교체 과정을 역할별 VFXSO로 통일하여, 아티스트가 직접 리소스를 교체·테스트하도록 개선했습니다.

**[상세 내용 보기 →](<./04. VFX 교체 지점 표준화를 통한 아티스트 테스트 과정 개선>)**
<br></br>
