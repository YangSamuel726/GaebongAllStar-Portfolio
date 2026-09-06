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
[![Video Label](https://youtu.be/d4LPapnxcG4)

#### 01. 계층형 네트워크 태그 시스템
> Ability / Effect / Tag 기반으로 캐릭터와 스킬 확장을 고려한 전투 구조를 설계했습니다.

**[상세 내용 보기 →](<01. 계층형 네트워크 태그 시스템>)**
<br></br>

#### 02. 클라이언트 예측을 활용한 투사체 시각 반응 지연 개선
> 네트워크 객체 생성 방식이 아닌 데이터 기반 로컬 생성 방식으로 투사체 반응성을 개선했습니다.
>
**[상세 내용 보기 →](<02. 클라이언트 예측을 활용한 투사체 시각 반응 지연 개선>)**
<br></br>

#### 03. NetworkBehaviourId를 이용한 SFX·VFX 추적 대상 동기화
> 데이터의 고유성을 보장하고 쉽게 확장할 수 있도록 하는 툴을 개발하여 생산 시간을 단축했습니다.

**[상세 내용 보기 →](<03. NetworkBehaviourId를 이용한 SFX·VFX 추적 대상 동기화>)**
<br></br>

#### 04. VFX 교체 지점 표준화를 통한 아티스트 테스트 과정 개선
> 리소스와 ID를 매핑하여 실시간 재생시에 네트워크 전송 데이터를 최소화하도록 구성했습니다.

**[상세 내용 보기 →](<./04. VFX 교체 지점 표준화를 통한 아티스트 테스트 과정 개선>)**
<br></br>
